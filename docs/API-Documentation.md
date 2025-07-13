# API Documentation - EnglishVocab

## Tổng quan

Tài liệu này mô tả các API của ứng dụng EnglishVocab sau khi đã hợp nhất và tối ưu hóa các endpoint trùng lặp.

## Quản lý người dùng

### Xác thực và đăng nhập

#### Đăng ký tài khoản
```
POST /api/auth/register
```
Body:
```json
{
  "email": "user@example.com",
  "password": "password",
  "username": "username",
  "fullName": "Full Name"
}
```

#### Đăng nhập
```
POST /api/auth/login
```
Body:
```json
{
  "email": "user@example.com",
  "password": "password"
}
```

#### Làm mới token
```
POST /api/auth/refresh
```
Body:
```json
{
  "accessToken": "expired-token",
  "refreshToken": "refresh-token"
}
```

#### Đăng xuất
```
POST /api/auth/logout
```
Headers:
```
Authorization: Bearer {token}
```

### Hồ sơ người dùng

#### Lấy thông tin người dùng hiện tại
```
GET /api/userprofile/me
```
Headers:
```
Authorization: Bearer {token}
```

#### Lấy thông tin người dùng cụ thể (chỉ dành cho Admin)
```
GET /api/userprofile/{userId}
```
Headers:
```
Authorization: Bearer {token}
```

#### Cập nhật hồ sơ người dùng hiện tại
```
PUT /api/userprofile/me
```
Headers:
```
Authorization: Bearer {token}
```
Body:
```json
{
  "fullName": "New Full Name",
  "bio": "New bio",
  "avatarUrl": "new-avatar-url"
}
```

#### Cập nhật hồ sơ người dùng (dành cho Admin hoặc chính người dùng)
```
PUT /api/userprofile/{userId?}
```
Headers:
```
Authorization: Bearer {token}
```
Body:
```json
{
  "fullName": "New Full Name",
  "bio": "New bio",
  "avatarUrl": "new-avatar-url"
}
```

## Quản lý từ vựng

### Từ vựng

#### Lấy tất cả từ vựng
```
GET /api/words
```

#### Lấy từ vựng theo danh mục và độ khó (tùy chọn)
```
GET /api/words/category/{categoryId}/difficulty/{difficultyLevel?}
```
- `categoryId`: ID của danh mục
- `difficultyLevel` (tùy chọn): Độ khó của từ vựng (Basic, Intermediate, Advanced)

Ví dụ:
```
GET /api/words/category/1/difficulty/Basic
GET /api/words/category/1
```

#### Tìm kiếm từ vựng
```
GET /api/words/search?term={searchTerm}
```
Headers:
```
Authorization: Bearer {token}
```

#### Lấy từ vựng theo ID
```
GET /api/words/{id}
```

#### Thêm từ vựng mới
```
POST /api/words
```
Headers:
```
Authorization: Bearer {token}
```
Body:
```json
{
  "text": "example",
  "meaning": "ví dụ",
  "categoryId": 1,
  "difficultyLevelId": 1,
  "pronunciation": "/ɪɡˈzɑːmpl/",
  "example": "This is an example."
}
```

#### Cập nhật từ vựng
```
PUT /api/words/{id}
```
Headers:
```
Authorization: Bearer {token}
```
Body:
```json
{
  "id": 1,
  "text": "updated example",
  "meaning": "ví dụ cập nhật",
  "categoryId": 1,
  "difficultyLevelId": 1,
  "pronunciation": "/ɪɡˈzɑːmpl/",
  "example": "This is an updated example."
}
```

#### Xóa từ vựng
```
DELETE /api/words/{id}
```
Headers:
```
Authorization: Bearer {token}
```

### Bộ từ vựng

#### Lấy tất cả bộ từ vựng
```
GET /api/wordsets
```
Headers:
```
Authorization: Bearer {token}
```

#### Lấy bộ từ vựng mặc định
```
GET /api/wordsets/default
```
Headers:
```
Authorization: Bearer {token}
```

#### Lấy bộ từ vựng của người dùng
```
GET /api/wordsets/user/{userId}
```
Headers:
```
Authorization: Bearer {token}
```

#### Lấy chi tiết bộ từ vựng
```
GET /api/wordsets/{id}
```
Headers:
```
Authorization: Bearer {token}
```

#### Tạo bộ từ vựng mới
```
POST /api/wordsets
```
Headers:
```
Authorization: Bearer {token}
```
Body:
```json
{
  "name": "My Word Set",
  "description": "Description of my word set",
  "userId": "user-id",
  "isPublic": true
}
```

#### Cập nhật bộ từ vựng
```
PUT /api/wordsets/{id}
```
Headers:
```
Authorization: Bearer {token}
```
Body:
```json
{
  "id": 1,
  "name": "Updated Word Set",
  "description": "Updated description",
  "isPublic": false
}
```

#### Xóa bộ từ vựng
```
DELETE /api/wordsets/{id}
```
Headers:
```
Authorization: Bearer {token}
```

#### Thêm từ vào bộ từ vựng
```
POST /api/wordsets/{wordSetId}/words/{wordId}
```
Headers:
```
Authorization: Bearer {token}
```

#### Thêm nhiều từ vào bộ từ vựng
```
POST /api/wordsets/{wordSetId}/words
```
Headers:
```
Authorization: Bearer {token}
```
Body:
```json
[1, 2, 3, 4, 5]
```

#### Xóa từ khỏi bộ từ vựng
```
DELETE /api/wordsets/{wordSetId}/words/{wordId}
```
Headers:
```
Authorization: Bearer {token}
```

## Thông báo

#### Lấy thông báo
```
GET /api/notifications?unreadOnly={true|false}
```
Headers:
```
Authorization: Bearer {token}
```
- `unreadOnly` (tùy chọn): Nếu true, chỉ lấy thông báo chưa đọc. Mặc định là false (lấy tất cả)

#### Lấy số lượng thông báo chưa đọc
```
GET /api/notifications/count
```
Headers:
```
Authorization: Bearer {token}
```

#### Đánh dấu thông báo đã đọc
```
POST /api/notifications/{id}/read
```
Headers:
```
Authorization: Bearer {token}
```

#### Đánh dấu tất cả thông báo đã đọc
```
POST /api/notifications/read-all
```
Headers:
```
Authorization: Bearer {token}
```

## Các thay đổi API

### API đã loại bỏ

1. `GET /api/auth/user/{userId}` - Thay thế bằng `GET /api/userprofile/{userId}`
2. `PUT /api/userprofile/update` - Thay thế bằng `PUT /api/userprofile/{userId?}`
3. `GET /api/words/bycategory/{categoryId}` - Thay thế bằng `GET /api/words/category/{categoryId}/difficulty/{difficultyLevel?}`
4. `GET /api/notifications/unread` - Thay thế bằng `GET /api/notifications?unreadOnly=true`

### API đã cải thiện

1. `GET /api/words/category/{categoryId}/difficulty/{difficultyLevel?}` - Thêm tham số tùy chọn cho difficultyLevel
2. `GET /api/notifications` - Thêm tham số query unreadOnly
3. `POST /api/wordsets/{wordSetId}/words` - Hỗ trợ thêm nhiều từ cùng lúc 