# Kiến trúc Clean Architecture cho Backend

## Cấu trúc và Phân lớp

Dự án English Vocabulary App được thiết kế dựa trên nguyên tắc Clean Architecture, tổ chức thành các lớp tách biệt như sau:

### 1. EnglishVocab.Constants
- **Vai trò**: Chứa các hằng số, enums và cấu hình được sử dụng trong toàn bộ ứng dụng
- **Phụ thuộc**: Không phụ thuộc vào bất kỳ module nào khác

### 2. EnglishVocab.Domain
- **Vai trò**: Lớp cốt lõi chứa các business entities và business rules
- **Phụ thuộc**: Chỉ phụ thuộc vào Constants
- **Thành phần chính**:
  - Entities (User, Word, WordSet, StudySession, UserProgress)
  - Value Objects
  - Domain Events
  - Domain Exceptions
  - Interfaces cốt lõi

### 3. EnglishVocab.Application
- **Vai trò**: Chứa business logic và use cases của ứng dụng
- **Phụ thuộc**: Domain, Constants
- **Thành phần chính**:
  - Commands/Queries (CQRS pattern)
  - Command/Query Handlers
  - Interfaces cho repositories và services
  - DTOs và View Models
  - Validation
  - Application exceptions

### 4. EnglishVocab.Infrastructure
- **Vai trò**: Triển khai các interfaces từ Application layer
- **Phụ thuộc**: Application, Domain, Constants
- **Thành phần chính**:
  - DbContext và Entity Configurations
  - Repository Implementations
  - External Service Implementations
  - Data access và infrastructure services
  - Migrations

### 5. EnglishVocab.Identity
- **Vai trò**: Xử lý authentication, authorization và quản lý người dùng
- **Phụ thuộc**: Application, Domain, Constants
- **Thành phần chính**:
  - Identity DbContext và Configurations
  - JWT Token Service
  - User Management Service
  - Authentication Services
  - Role và Permission Services

### 6. EnglishVocab.API
- **Vai trò**: Entry point của ứng dụng, xử lý HTTP requests
- **Phụ thuộc**: Application, Infrastructure, Identity
- **Thành phần chính**:
  - Controllers
  - Middleware
  - Filters
  - API Models và Documentation

## Luồng dữ liệu

1. **Client** → HTTP Request → **API**
2. **API** → Command/Query → **Application**
3. **Application** → Tương tác với Domain Models → **Domain**
4. **Application** → Gọi Infrastructure thông qua interfaces
5. **Infrastructure** → Xử lý dữ liệu với database và external services
6. **Identity** → Xác thực người dùng và cung cấp tokens

## Quy tắc Phụ thuộc

Để duy trì tính module và khả năng thay thế các thành phần:

1. Layer bên trong **không bao giờ** phụ thuộc vào layer bên ngoài
2. Domain là layer trung tâm, không phụ thuộc vào bất kỳ layer nào khác
3. Application phụ thuộc vào Domain
4. Infrastructure và Identity phụ thuộc vào Application
5. API phụ thuộc vào Infrastructure, Identity và Application

## Lợi ích của kiến trúc này

- **Dễ bảo trì**: Mỗi layer có trách nhiệm rõ ràng
- **Dễ kiểm thử**: Các thành phần có thể được kiểm thử độc lập
- **Dễ mở rộng**: Dễ dàng thêm tính năng mới
- **Linh hoạt**: Có thể thay đổi infrastructure mà không ảnh hưởng tới business logic 