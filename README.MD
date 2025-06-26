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

## 🚀 Công nghệ sử dụng

### Frontend
- **Next.js 14**: Framework React hiện đại với SSR và routing
- **TypeScript**: Hệ thống kiểu dữ liệu tĩnh
- **Tailwind CSS**: Framework CSS tiện lợi
- **Shadcn UI**: Thư viện components đẹp và dễ tùy biến
- **Lucide Icons**: Bộ icon vector chất lượng cao

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
│   ├── Frontend/             # Next.js frontend
│   │   ├── app/              # App router và pages
│   │   ├── components/       # React components
│   │   │   ├── audio/        # Components xử lý âm thanh
│   │   │   ├── flashcards/   # Components thẻ học từ vựng
│   │   │   ├── footer/       # Components footer
│   │   │   ├── header/       # Components header
│   │   │   ├── layout/       # Components layout
│   │   │   ├── menu/         # Components menu và navigation
│   │   │   ├── quiz/         # Components bài tập và quiz
│   │   │   ├── search/       # Components tìm kiếm
│   │   │   └── ui/           # UI components cơ bản
│   │   ├── contexts/         # React contexts
│   │   ├── hooks/            # Custom React hooks
│   │   ├── lib/              # Utility functions
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

Truy cập frontend tại http://localhost:3000

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

- **Trang chủ**: Tổng quan và bắt đầu học
- **Học từ vựng**: Flashcards và các bài tập
- **Tiến độ**: Biểu đồ và thống kê học tập
- **Thành tích**: Huy hiệu và thành tựu
- **Cài đặt**: Tùy chỉnh cá nhân

## 👥 Đóng góp

Chúng tôi rất hoan nghênh mọi đóng góp cho dự án. Vui lòng tạo issue hoặc pull request nếu bạn muốn cải thiện FlashLearn.

## 📄 Giấy phép

Dự án này được phân phối dưới giấy phép MIT. Xem file `LICENSE` để biết thêm chi tiết.
