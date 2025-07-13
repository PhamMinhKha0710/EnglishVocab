# Kiểm tra API đã hợp nhất

Tài liệu này chứa các lệnh curl để kiểm tra các API đã hợp nhất trong ứng dụng EnglishVocab.

## Chuẩn bị

1. Đảm bảo ứng dụng đang chạy tại `https://localhost:5001` (hoặc thay đổi URL trong các lệnh curl)
2. Thay thế `{token}` bằng JWT token hợp lệ sau khi đăng nhập
3. Thay thế các giá trị ID bằng giá trị thực tế trong cơ sở dữ liệu của bạn

## Kiểm tra API quản lý người dùng

### Đăng ký và đăng nhập

```bash
# Đăng ký tài khoản mới
curl -X POST https://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Password123!",
    "username": "testuser",
    "fullName": "Test User"
  }'

# Đăng nhập
curl -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Password123!"
  }'
```

### Quản lý hồ sơ người dùng

```bash
# Lấy thông tin người dùng hiện tại
curl -X GET https://localhost:5001/api/userprofile/me \
  -H "Authorization: Bearer {token}"

# Lấy thông tin người dùng cụ thể (chỉ dành cho Admin)
curl -X GET https://localhost:5001/api/userprofile/{userId} \
  -H "Authorization: Bearer {token}"

# Cập nhật hồ sơ người dùng hiện tại
curl -X PUT https://localhost:5001/api/userprofile/me \
  -H "Authorization: Bearer {token}" \
  -H "Content-Type: application/json" \
  -d '{
    "fullName": "Updated Name",
    "bio": "Updated bio",
    "avatarUrl": "https://example.com/avatar.jpg"
  }'

# Cập nhật hồ sơ người dùng khác (chỉ dành cho Admin)
curl -X PUT https://localhost:5001/api/userprofile/{userId} \
  -H "Authorization: Bearer {token}" \
  -H "Content-Type: application/json" \
  -d '{
    "fullName": "Admin Updated Name",
    "bio": "Admin updated bio",
    "avatarUrl": "https://example.com/admin-avatar.jpg"
  }'
```

## Kiểm tra API từ vựng

### Quản lý từ vựng

```bash
# Lấy từ vựng theo danh mục và độ khó
curl -X GET https://localhost:5001/api/words/category/1/difficulty/Basic \
  -H "Authorization: Bearer {token}"

# Lấy từ vựng theo danh mục (không có độ khó)
curl -X GET https://localhost:5001/api/words/category/1 \
  -H "Authorization: Bearer {token}"

# Tìm kiếm từ vựng
curl -X GET "https://localhost:5001/api/words/search?term=example" \
  -H "Authorization: Bearer {token}"
```

### Quản lý bộ từ vựng

```bash
# Thêm một từ vào bộ từ vựng
curl -X POST https://localhost:5001/api/wordsets/1/words/1 \
  -H "Authorization: Bearer {token}"

# Thêm nhiều từ vào bộ từ vựng
curl -X POST https://localhost:5001/api/wordsets/1/words \
  -H "Authorization: Bearer {token}" \
  -H "Content-Type: application/json" \
  -d '[1, 2, 3, 4, 5]'
```

## Kiểm tra API thông báo

```bash
# Lấy tất cả thông báo
curl -X GET https://localhost:5001/api/notifications \
  -H "Authorization: Bearer {token}"

# Lấy chỉ thông báo chưa đọc
curl -X GET "https://localhost:5001/api/notifications?unreadOnly=true" \
  -H "Authorization: Bearer {token}"

# Đánh dấu tất cả thông báo đã đọc
curl -X POST https://localhost:5001/api/notifications/read-all \
  -H "Authorization: Bearer {token}"
```

## Kiểm tra lỗi và xử lý ngoại lệ

```bash
# Kiểm tra xử lý lỗi khi truy cập API không tồn tại
curl -X GET https://localhost:5001/api/nonexistent \
  -H "Authorization: Bearer {token}"

# Kiểm tra xử lý lỗi khi không có quyền truy cập
curl -X GET https://localhost:5001/api/userprofile/admin-only \
  -H "Authorization: Bearer {token-without-admin-role}"
```

## Kiểm tra tự động

Để kiểm tra tự động, bạn có thể sử dụng script bash sau:

```bash
#!/bin/bash

# Đặt URL cơ sở
BASE_URL="https://localhost:5001"

# Đăng nhập và lấy token
echo "Đăng nhập và lấy token..."
LOGIN_RESPONSE=$(curl -s -X POST "$BASE_URL/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Password123!"
  }')

# Trích xuất token từ phản hồi (giả định rằng phản hồi là JSON và có trường accessToken)
TOKEN=$(echo $LOGIN_RESPONSE | grep -o '"accessToken":"[^"]*"' | cut -d'"' -f4)

if [ -z "$TOKEN" ]; then
  echo "Không thể lấy token. Kiểm tra thông tin đăng nhập."
  exit 1
fi

echo "Đã lấy token: $TOKEN"

# Kiểm tra API hồ sơ người dùng
echo "Kiểm tra API hồ sơ người dùng..."
curl -s -X GET "$BASE_URL/api/userprofile/me" \
  -H "Authorization: Bearer $TOKEN"
echo -e "\n"

# Kiểm tra API từ vựng
echo "Kiểm tra API từ vựng theo danh mục..."
curl -s -X GET "$BASE_URL/api/words/category/1" \
  -H "Authorization: Bearer $TOKEN"
echo -e "\n"

# Kiểm tra API thông báo
echo "Kiểm tra API thông báo..."
curl -s -X GET "$BASE_URL/api/notifications?unreadOnly=true" \
  -H "Authorization: Bearer $TOKEN"
echo -e "\n"

echo "Kiểm tra hoàn tất."
```

Lưu script này dưới dạng `test-api.sh`, cấp quyền thực thi (`chmod +x test-api.sh`) và chạy nó để kiểm tra các API đã hợp nhất. 