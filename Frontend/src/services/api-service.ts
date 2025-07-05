// Đường dẫn cơ sở của API
const API_URL = "http://localhost:7010/api";

// Token đang được làm mới
let isRefreshing = false;
let failedQueue: {resolve: (token: string) => void; reject: (error: any) => void}[] = [];

// Xử lý hàng đợi sau khi làm mới token
const processQueue = (error: any, token: string | null = null) => {
  failedQueue.forEach(prom => {
    if (error) {
      prom.reject(error);
    } else if (token) {
      prom.resolve(token);
    }
  });
  
  failedQueue = [];
};

// Interface cho tham số đăng ký
interface RegisterParams {
  username: string;
  email: string;
  password: string;
  confirmPassword: string;
  firstName: string;
  lastName: string;
}

// Interface cho tham số đăng nhập
interface LoginParams {
  email: string;
  password: string;
}

// Interface cho tham số làm mới token
interface RefreshTokenParams {
  accessToken: string;
  refreshToken: string;
}

// Interface cho tham số cập nhật hồ sơ người dùng
export interface UpdateProfileParams {
  userId?: number;
  firstName: string;
  lastName: string;
  email: string;
  username: string;
}

// Interface cho kết quả cập nhật hồ sơ
export interface UpdateProfileResponse {
  succeeded: boolean;
  message: string;
  user?: {
    id: number;
    username: string;
    email: string;
    firstName: string;
    lastName: string;
    roles: string[];
    fullName: string;
  };
}

// Interface cho user được trả về từ API
export interface AuthUser {
  id: number;
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  roles: string[];
  isAuthenticated: boolean;
  accessToken: string;
  refreshToken: string;
  message: string;
}

// Hàm che giấu thông tin nhạy cảm trong logs
const maskSensitiveData = (data: any) => {
  if (!data) return data;
  
  const maskedData = { ...data };
  
  // Che giấu mật khẩu
  if (maskedData.password) maskedData.password = "********";
  if (maskedData.Password) maskedData.Password = "********";
  if (maskedData.confirmPassword) maskedData.confirmPassword = "********";
  
  // Che giấu tokens
  if (maskedData.accessToken) maskedData.accessToken = "********";
  if (maskedData.refreshToken) maskedData.refreshToken = "********";
  if (maskedData.AccessToken) maskedData.AccessToken = "********";
  if (maskedData.RefreshToken) maskedData.RefreshToken = "********";
  
  return maskedData;
};

// Service gọi API
export const apiService = {
  // Đăng ký người dùng mới
  async register(params: RegisterParams): Promise<{ succeeded: boolean; message: string; errors?: string[] }> {
    try {
      // Che giấu thông tin nhạy cảm trong logs
      console.log("Register attempt with:", maskSensitiveData({
        username: params.username,
        email: params.email
      }));
      
      const response = await fetch(`${API_URL}/Auth/register`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(params),
      });

      console.log("Register response status:", response.status);
      const data = await response.json();

      if (!response.ok) {
        throw new Error(data.message || "Đăng ký thất bại");
      }

      return data;
    } catch (error: any) {
      console.error("Register error:", error.message);
      throw new Error(error.message || "Đã xảy ra lỗi khi đăng ký");
    }
  },

  // Đăng nhập người dùng
  async login(params: LoginParams): Promise<AuthUser> {
    try {
      // Che giấu thông tin nhạy cảm trong logs
      console.log("Login attempt with:", maskSensitiveData({
        email: params.email
      }));
      
      const response = await fetch(`${API_URL}/Auth/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(params),
      });

      console.log("Login response status:", response.status);
      const rawData = await response.json();
      
      // Xử lý phản hồi API - đảm bảo các trường có giá trị phù hợp
      // Lưu ý chữ hoa/chữ thường của các thuộc tính từ API
      const data: AuthUser = {
        id: rawData.Id || 0,
        username: rawData.Username || "",
        email: rawData.Email || "",
        firstName: rawData.FirstName || "",
        lastName: rawData.LastName || "",
        roles: Array.isArray(rawData.Roles) ? rawData.Roles : 
               rawData.Roles ? [rawData.Roles] : [],
        isAuthenticated: rawData.IsAuthenticated === true, // Sửa chữ I viết hoa
        accessToken: rawData.AccessToken || "",
        refreshToken: rawData.RefreshToken || "",
        message: rawData.Message || ""
      };

      if (!response.ok) {
        throw new Error(data.message || "Đăng nhập thất bại");
      }

      return data;
    } catch (error: any) {
      console.error("Login error:", error.message);
      throw new Error(error.message || "Đã xảy ra lỗi khi đăng nhập");
    }
  },

  // Làm mới token
  async refreshToken(params: RefreshTokenParams): Promise<AuthUser> {
    try {
      console.log("Refreshing token");
      
      const response = await fetch(`${API_URL}/Auth/refresh`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(params),
      });

      console.log("Refresh token response status:", response.status);
      const data = await response.json();

      if (!response.ok) {
        throw new Error(data.message || "Làm mới token thất bại");
      }

      return data;
    } catch (error: any) {
      console.error("Refresh token error:", error.message);
      throw new Error(error.message || "Đã xảy ra lỗi khi làm mới token");
    }
  },

  // Đăng xuất (gọi API server)
  async logout(token: string): Promise<boolean> {
    try {
      console.log("Logging out on server");
      
      const response = await fetch(`${API_URL}/Auth/logout`, {
        method: "POST",
        headers: {
          "Authorization": `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      });

      console.log("Logout response status:", response.status);
      
      // Ngay cả khi server lỗi, chúng ta vẫn cần đăng xuất client
      return response.ok;
    } catch (error: any) {
      console.error("Logout error:", error.message);
      // Ngay cả khi có lỗi, chúng ta vẫn trả về true để frontend xóa token local
      return true;
    }
  },

  // Cập nhật hồ sơ người dùng
  async updateProfile(params: UpdateProfileParams, token: string): Promise<UpdateProfileResponse> {
    try {
      console.log("Updating profile for user:", params.username);
      
      const response = await fetch(`${API_URL}/UserProfile/me`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${token}`
        },
        body: JSON.stringify(params),
      });

      console.log("Update profile response status:", response.status);
      const rawData = await response.json();
      
      // Xử lý phản hồi API - đảm bảo các trường có giá trị phù hợp
      const data: UpdateProfileResponse = {
        succeeded: response.ok,
        message: rawData.message || (response.ok ? "Cập nhật hồ sơ thành công" : "Cập nhật hồ sơ thất bại"),
        user: rawData.user ? {
          id: rawData.user.id || 0,
          username: rawData.user.username || params.username,
          email: rawData.user.email || params.email,
          firstName: rawData.user.firstName || params.firstName,
          lastName: rawData.user.lastName || params.lastName,
          roles: Array.isArray(rawData.user.roles) ? rawData.user.roles : 
                 rawData.user.roles ? [rawData.user.roles] : [],
          fullName: rawData.user.fullName || `${params.firstName} ${params.lastName}`
        } : undefined
      };

      if (!response.ok) {
        throw new Error(data.message || "Cập nhật hồ sơ thất bại");
      }

      return data;
    } catch (error: any) {
      console.error("Update profile error:", error.message);
      throw new Error(error.message || "Đã xảy ra lỗi khi cập nhật hồ sơ");
    }
  },

  // Xác thực yêu cầu bằng token
  getAuthHeaders(token: string) {
    return {
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    };
  },

  // Gọi API với token xác thực
  async fetchWithAuth(url: string, options: RequestInit = {}, token: string): Promise<any> {
    try {
      const response = await fetch(url, {
        ...options,
        headers: {
          ...this.getAuthHeaders(token),
          ...options.headers,
        },
      });

      // Kiểm tra nếu token hết hạn (401 Unauthorized)
      if (response.status === 401) {
        return this.handleTokenExpiration(url, options, token);
      }

      if (!response.ok) {
        const errorData = await response.json().catch(() => ({}));
        throw new Error(errorData.message || "API call failed");
      }

      return await response.json();
    } catch (error: any) {
      console.error("API call error:", error.message);
      throw error;
    }
  },
  
  // Xử lý token hết hạn
  async handleTokenExpiration(url: string, options: RequestInit, token: string): Promise<any> {
    // Lấy refresh token từ localStorage hoặc sessionStorage
    const user = JSON.parse(localStorage.getItem("user") || sessionStorage.getItem("user") || "null");
    
    if (!user || !user.refreshToken) {
      // Không có refresh token, chuyển hướng người dùng đến trang đăng nhập
      window.location.href = "/auth/login";
      throw new Error("Refresh token not found");
    }
    
    // Nếu đang có yêu cầu làm mới token, thêm yêu cầu hiện tại vào hàng đợi
    if (isRefreshing) {
      return new Promise((resolve, reject) => {
        failedQueue.push({ resolve, reject });
      })
        .then(newToken => {
          return this.fetchWithAuth(url, options, newToken as string);
        })
        .catch(err => {
          throw err;
        });
    }
    
    isRefreshing = true;
    
    try {
      // Làm mới token
      const refreshResult = await this.refreshToken({
        accessToken: token,
        refreshToken: user.refreshToken
      });
      
      // Lưu token mới
      const newToken = refreshResult.accessToken;
      
      // Cập nhật thông tin người dùng trong localStorage hoặc sessionStorage
      const updatedUser = { ...user, accessToken: newToken, refreshToken: refreshResult.refreshToken };
      if (localStorage.getItem("user")) {
        localStorage.setItem("user", JSON.stringify(updatedUser));
      } else {
        sessionStorage.setItem("user", JSON.stringify(updatedUser));
      }
      
      // Thực hiện lại các yêu cầu trong hàng đợi với token mới
      processQueue(null, newToken);
      isRefreshing = false;
      
      // Thực hiện lại yêu cầu hiện tại với token mới
      return this.fetchWithAuth(url, options, newToken);
    } catch (error) {
      // Nếu làm mới token thất bại, hủy tất cả các yêu cầu trong hàng đợi
      processQueue(error, null);
      isRefreshing = false;
      
      // Xóa thông tin người dùng và chuyển hướng đến trang đăng nhập
      localStorage.removeItem("user");
      sessionStorage.removeItem("user");
      window.location.href = "/auth/login";
      throw error;
    }
  }
}; 