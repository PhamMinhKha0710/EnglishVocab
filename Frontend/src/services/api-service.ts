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

// Interface cho Category
export interface Category {
  id: number;
  name: string;
  description?: string;
  wordCount?: number;
  Name?: string; // Hỗ trợ cả key Name với chữ hoa đầu tiên (API có thể trả về)
}

// Interface cho DifficultyLevel
export interface DifficultyLevel {
  id: number;
  name: string;
  Name?: string; // Thêm trường Name với chữ cái đầu tiên viết hoa (API có thể trả về)
  description?: string;
  value: number;
}

// Interface cho tham số cập nhật hồ sơ người dùng
export interface UpdateProfileParams {
  userId?: string;
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
    id: string;
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
  id: string;
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

// Service gọi API
export const apiService = {
  // Đăng ký người dùng mới
  async register(params: RegisterParams): Promise<{ succeeded: boolean; message: string; errors?: string[] }> {
    try {
      const response = await fetch(`${API_URL}/Auth/register`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(params),
      });

      const data = await response.json();

      // Đảm bảo trả về đúng định dạng
      const result = {
        succeeded: response.ok && (data.isAuthenticated === true || data.succeeded === true),
        message: data.message || (response.ok ? "Đăng ký thành công" : "Đăng ký thất bại"),
        errors: data.errors
      };

      if (!response.ok) {
        throw new Error(data.message || "Đăng ký thất bại");
      }

      return result;
    } catch (error: any) {
      throw new Error(error.message || "Đã xảy ra lỗi khi đăng ký");
    }
  },

  // Đăng nhập người dùng
  async login(params: LoginParams): Promise<AuthUser> {
    try {
      const response = await fetch(`${API_URL}/Auth/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(params),
      });
      
      if (!response.ok) {
        if (response.status === 401) {
          throw new Error("Email hoặc mật khẩu không chính xác");
        }
        throw new Error(`Lỗi ${response.status}: Không thể đăng nhập`);
      }

      const rawData = await response.json();
      
      // Đảm bảo chuyển đổi cả chữ hoa và chữ thường
      const data: AuthUser = {
        id: rawData.id || rawData.Id || "",
        username: rawData.username || rawData.Username || "",
        email: rawData.email || rawData.Email || "",
        firstName: rawData.firstName || rawData.FirstName || "",
        lastName: rawData.lastName || rawData.LastName || "",
        roles: Array.isArray(rawData.roles) ? rawData.roles : 
               Array.isArray(rawData.Roles) ? rawData.Roles :
               rawData.roles ? [rawData.roles] : 
               rawData.Roles ? [rawData.Roles] : [],
        isAuthenticated: rawData.isAuthenticated === true || rawData.IsAuthenticated === true,
        accessToken: rawData.accessToken || rawData.AccessToken || "",
        refreshToken: rawData.refreshToken || rawData.RefreshToken || "",
        message: rawData.message || rawData.Message || ""
      };

      if (!data.isAuthenticated) {
        throw new Error(data.message || "Đăng nhập thất bại");
      }

      return data;
    } catch (error: any) {
      console.error("Lỗi đăng nhập:", error.message);
      throw error;
    }
  },

  // Lấy danh sách danh mục từ API
  async getCategories(token: string | undefined): Promise<Category[]> {
    try {
      // Nếu không có token, trả về mảng rỗng và ném lỗi
      if (!token) {
        throw new Error("Bạn cần đăng nhập để lấy danh mục");
      }

      // Gọi API lấy danh mục
      const response = await this.fetchWithAuth(
        `${API_URL}/Category`, 
        { method: "GET" },
        token
      );

      // Kiểm tra dữ liệu trả về
      if (Array.isArray(response)) {
        // API trả về mảng trực tiếp (không có phân trang)
        return response;
      } else if (response && Array.isArray(response.data)) {
        // API trả về đối tượng có thuộc tính data là mảng (có phân trang)
        return response.data;
      } else if (response && Array.isArray(response.items)) {
        // API trả về đối tượng có thuộc tính items là mảng (có phân trang)
        return response.items;
      } else {
        throw new Error("Dữ liệu danh mục không đúng định dạng");
      }
    } catch (error: any) {
      console.error("Lỗi khi lấy danh mục:", error.message);
      throw error;
    }
  },

  // Lấy danh sách cấp độ khó từ API
  async getDifficultyLevels(token: string | undefined): Promise<DifficultyLevel[]> {
    try {
      // Nếu không có token, trả về mảng rỗng và ném lỗi
      if (!token) {
        throw new Error("Bạn cần đăng nhập để lấy cấp độ khó");
      }

      // Gọi API lấy cấp độ khó
      const response = await this.fetchWithAuth(
        `${API_URL}/DifficultyLevels`, 
        { method: "GET" },
        token
      );

      // Kiểm tra dữ liệu trả về
      if (Array.isArray(response)) {
        // API trả về mảng trực tiếp (không có phân trang)
        return response;
      } else if (response && Array.isArray(response.data)) {
        // API trả về đối tượng có thuộc tính data là mảng (có phân trang)
        return response.data;
      } else if (response && Array.isArray(response.items)) {
        // API trả về đối tượng có thuộc tính items là mảng (có phân trang)
        return response.items;
      } else {
        throw new Error("Dữ liệu cấp độ khó không đúng định dạng");
      }
    } catch (error: any) {
      console.error("Lỗi khi lấy cấp độ khó:", error.message);
      throw error;
    }
  },

  // Làm mới token
  async refreshToken(params: RefreshTokenParams): Promise<AuthUser> {
    try {
      const response = await fetch(`${API_URL}/Auth/refresh`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(params),
      });

      const data = await response.json();

      if (!response.ok) {
        throw new Error(data.message || "Làm mới token thất bại");
      }

      return data;
    } catch (error: any) {
      throw new Error(error.message || "Đã xảy ra lỗi khi làm mới token");
    }
  },

  // Đăng xuất (gọi API server)
  async logout(token: string): Promise<boolean> {
    try {
      const response = await fetch(`${API_URL}/Auth/logout`, {
        method: "POST",
        headers: {
          "Authorization": `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      });
      
      // Ngay cả khi server lỗi, chúng ta vẫn cần đăng xuất client
      return response.ok;
    } catch (error: any) {
      // Ngay cả khi có lỗi, chúng ta vẫn trả về true để frontend xóa token local
      return true;
    }
  },

  // Lấy thông tin hồ sơ người dùng hiện tại
  async getUserProfile(token: string): Promise<any> {
    try {
      // Kiểm tra token
      if (!token) {
        throw new Error("Không tìm thấy token xác thực. Vui lòng đăng nhập lại.");
      }

      const response = await fetch(`${API_URL}/UserProfile/me`, {
        method: "GET",
        headers: {
          "Authorization": `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      });

      // Xử lý các trường hợp lỗi cụ thể
      if (response.status === 401) {
        throw new Error("Phiên làm việc đã hết hạn. Vui lòng đăng nhập lại.");
      }

      if (response.status === 404) {
        throw new Error("Không tìm thấy thông tin người dùng.");
      }
      
      if (!response.ok) {
        throw new Error(`Lỗi ${response.status}: Không thể tải thông tin hồ sơ`);
      }
      
      const data = await response.json();
      return data;
    } catch (error: any) {
      console.error("Lỗi lấy thông tin hồ sơ:", error.message);
      throw error;
    }
  },

  // Cập nhật hồ sơ người dùng
  async updateProfile(params: UpdateProfileParams, token: string): Promise<UpdateProfileResponse> {
    try {
      // Kiểm tra token
      if (!token) {
        throw new Error("Không tìm thấy token xác thực. Vui lòng đăng nhập lại.");
      }

      const response = await fetch(`${API_URL}/UserProfile/me`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${token}`
        },
        body: JSON.stringify(params),
      });

      // Xử lý các trường hợp lỗi cụ thể
      if (response.status === 401) {
        throw new Error("Phiên làm việc đã hết hạn. Vui lòng đăng nhập lại.");
      }

      if (response.status === 403) {
        throw new Error("Bạn không có quyền cập nhật hồ sơ này.");
      }
      
      const rawData = await response.json();

      if (!response.ok) {
        throw new Error(rawData.message || `Lỗi ${response.status}: Không thể cập nhật hồ sơ`);
      }
      
      // Xử lý phản hồi API - đảm bảo các trường có giá trị phù hợp
      // Force succeeded = true nếu API trả về 200 OK
      const data: UpdateProfileResponse = {
        succeeded: true, // Luôn coi là thành công nếu đã qua được các kiểm tra lỗi ở trên
        message: "Cập nhật hồ sơ thành công",
        user: rawData.user ? {
          id: rawData.user.id || "",
          username: rawData.user.username || params.username,
          email: rawData.user.email || params.email,
          firstName: rawData.user.firstName || params.firstName,
          lastName: rawData.user.lastName || params.lastName,
          roles: Array.isArray(rawData.user.roles) ? rawData.user.roles : 
                 rawData.user.roles ? [rawData.user.roles] : [],
          fullName: rawData.user.fullName || `${params.firstName} ${params.lastName}`
        } : {
          // Nếu không có thông tin user từ API, tạo từ dữ liệu đầu vào
          id: params.userId || "",
          username: params.username,
          email: params.email,
          firstName: params.firstName,
          lastName: params.lastName,
          roles: [],
          fullName: `${params.firstName} ${params.lastName}`
        }
      };

      return data;
    } catch (error: any) {
      console.error("Lỗi cập nhật hồ sơ:", error.message);
      throw error;
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

      // Trường hợp không có dữ liệu trả về (204 No Content hoặc DELETE thành công)
      if (response.status === 204) {
        return { succeeded: true };
      }

      // Một số API trả về chuỗi rỗng cho các yêu cầu thành công
      const contentType = response.headers.get('content-type');
      if (contentType && contentType.includes('application/json')) {
        // Kiểm tra dữ liệu trả về có phải JSON không
        const text = await response.text();
        const data = text ? JSON.parse(text) : {};
        
        if (!response.ok) {
          throw new Error(data.message || "API call failed");
        }
        
        return data;
      } else {
        // Không phải JSON hoặc body rỗng
        if (!response.ok) {
          throw new Error("API call failed");
        }
        
        return { succeeded: true };
      }
    } catch (error: any) {
      console.error("Lỗi gọi API:", error.message);
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