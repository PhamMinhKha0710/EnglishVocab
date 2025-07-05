# FlashLearn - Ứng dụng học từ vựng tiếng Anh

<div align="center">
  <img src="src/Frontend/public/placeholder-logo.svg" alt="FlashLearn Logo" width="120" />
  <br />
  <h3>Học từ vựng hiệu quả với phương pháp Spaced Repetition</h3>
</div>

## 📋 Tổng quan

FlashLearn là ứng dụng học từ vựng tiếng Anh hiện đại, giúp người học ghi nhớ từ vựng hiệu quả thông qua phương pháp lặp lại ngắt quãng (Spaced Repetition). Ứng dụng được thiết kế với giao diện thân thiện, dễ sử dụng và có thể hoạt động trên nhiều thiết bị.

### ✨ Tính năng chính

- **Flashcard học từ vựng**: Học từ vựng với thẻ ghi nhớ hai mặt
- **Spaced Repetition**: Hệ thống lặp lại thông minh giúp ghi nhớ lâu dài
- **Theo dõi tiến độ**: Biểu đồ và thống kê chi tiết về quá trình học
- **Thành tích và động lực**: Hệ thống huy hiệu và thành tích thúc đẩy việc học
- **Đa dạng bài tập**: Trắc nghiệm, điền từ, phát âm và nhiều hình thức khác
- **Hỗ trợ offline**: Sử dụng được khi không có kết nối internet
- **Đăng nhập/Đăng ký**: Hệ thống xác thực để lưu trữ tiến độ học tập

## 🚀 Công nghệ sử dụng

### Frontend
- **React**: Thư viện JavaScript để xây dựng giao diện người dùng
- **Vite**: Công cụ build nhanh và hiệu quả
- **React Router**: Quản lý định tuyến trong ứng dụng React
- **TypeScript**: Hệ thống kiểu dữ liệu tĩnh
- **Tailwind CSS**: Framework CSS tiện lợi
- **Shadcn UI**: Thư viện components đẹp và dễ tùy biến
- **Lucide Icons**: Bộ icon vector chất lượng cao
- **LocalStorage**: Lưu trữ dữ liệu người dùng và xác thực cục bộ

### Backend
- **.NET 9**: Framework mạnh mẽ cho API và dịch vụ
- **Entity Framework Core**: ORM cho tương tác cơ sở dữ liệu
- **SQL Server**: Cơ sở dữ liệu quan hệ
- **JWT Authentication**: Xác thực và phân quyền người dùng

## 🏗️ Kiến trúc dự án

Dự án được tổ chức theo mô hình Domain-Driven Design (DDD) với cấu trúc rõ ràng:

```
EnglishVocabApp/
├── src/
│   ├── Frontend/             # React + Vite frontend
│   │   ├── src/
│   │   │   ├── app/          # App pages (không sử dụng App Router)
│   │   │   ├── components/   # React components
│   │   │   │   ├── audio/    # Components xử lý âm thanh
│   │   │   │   ├── auth/     # Components xác thực
│   │   │   │   ├── flashcards/ # Components thẻ học từ vựng
│   │   │   │   ├── footer/   # Components footer
│   │   │   │   ├── header/   # Components header
│   │   │   │   ├── layout/   # Components layout
│   │   │   │   ├── menu/     # Components menu và navigation
│   │   │   │   ├── quiz/     # Components bài tập và quiz
│   │   │   │   ├── search/   # Components tìm kiếm
│   │   │   │   └── ui/       # UI components cơ bản
│   │   │   ├── contexts/     # React contexts (bao gồm auth-context)
│   │   │   ├── hooks/        # Custom React hooks
│   │   │   ├── lib/          # Utility functions
│   │   │   ├── pages/        # Các trang của ứng dụng
│   │   │   ├── store/        # State management
│   │   │   └── types/        # TypeScript type definitions
│   │   └── public/           # Static assets
│   │
│   └── Backend/              # .NET backend
│       ├── EnglishVocab.API/         # API endpoints
│       ├── EnglishVocab.Application/ # Business logic
│       ├── EnglishVocab.Domain/      # Domain entities
│       └── EnglishVocab.Infrastructure/ # Data access
│
└── docs/                     # Documentation
```

## 🛠️ Cài đặt và chạy dự án

### Yêu cầu hệ thống
- Node.js 18+ và npm
- .NET 9 SDK
- SQL Server (hoặc SQL Server Express)

### Frontend

```bash
# Di chuyển vào thư mục frontend
cd src/Frontend

# Cài đặt dependencies
npm install

# Chạy development server
npm run dev
```

Truy cập frontend tại http://localhost:5173

### Backend

```bash
# Di chuyển vào thư mục API
cd src/Backend/EnglishVocab.API

# Khôi phục packages
dotnet restore

# Chạy migrations (nếu cần)
dotnet ef database update

# Chạy API
dotnet run
```

API sẽ chạy tại https://localhost:7001

## 📱 Các màn hình chính

- **Trang chủ**: Tổng quan và giới thiệu ứng dụng (có thể truy cập khi chưa đăng nhập)
- **Đăng nhập/Đăng ký**: Màn hình xác thực người dùng
- **Học từ vựng**: Flashcards và các bài tập (yêu cầu đăng nhập)
- **Tiến độ**: Biểu đồ và thống kê học tập (yêu cầu đăng nhập)
- **Thành tích**: Huy hiệu và thành tựu (yêu cầu đăng nhập)
- **Cài đặt**: Tùy chỉnh cá nhân (yêu cầu đăng nhập)

## 🔒 Xác thực và bảo mật

- **Đăng ký người dùng**: Tạo tài khoản mới với email và mật khẩu
- **Đăng nhập**: Xác thực người dùng để truy cập các tính năng học tập
- **Route Protection**: Các trang yêu cầu đăng nhập được bảo vệ tự động
- **LocalStorage**: Dữ liệu xác thực được lưu trữ cục bộ để duy trì phiên đăng nhập

## 👥 Đóng góp

Chúng tôi rất hoan nghênh mọi đóng góp cho dự án. Vui lòng tạo issue hoặc pull request nếu bạn muốn cải thiện FlashLearn.

## 📄 Giấy phép

Dự án này được phân phối dưới giấy phép MIT. Xem file `LICENSE` để biết thêm chi tiết.
