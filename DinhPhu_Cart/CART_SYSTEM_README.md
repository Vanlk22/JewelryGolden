# Hệ thống Giỏ hàng - JewelryGolden

## Tổng quan
Hệ thống giỏ hàng đã được phát triển hoàn chỉnh với các tính năng theo yêu cầu trong hình ảnh:

### ✅ Các tính năng đã hoàn thành:

1. **Danh sách sản phẩm** - Hiển thị và quản lý sản phẩm trong giỏ hàng
2. **Thêm sửa xóa sản phẩm** - Các chức năng CRUD cho sản phẩm trong giỏ
3. **Đặt hàng - Thanh toán** - Quy trình đặt hàng và thanh toán hoàn chỉnh
4. **Hóa đơn** - Hệ thống tạo và xem hóa đơn đơn hàng

## Cấu trúc Files đã tạo:

### Controllers
- `CartController.cs` - Controller chính xử lý tất cả logic giỏ hàng

### Models
- `CartModels.cs` - Chứa các model: CartItem, CheckoutViewModel, CartViewModel

### Views
- `Views/Cart/Index.cshtml` - Trang giỏ hàng chính
- `Views/Cart/Checkout.cshtml` - Trang thanh toán
- `Views/Cart/OrderSuccess.cshtml` - Trang thành công đặt hàng
- `Views/Cart/Invoice.cshtml` - Trang hóa đơn

### Styles
- `Content/cart.css` - CSS styling cho giao diện giỏ hàng

## Các tính năng chi tiết:

### 1. Danh sách sản phẩm (Cart Index)
- Hiển thị danh sách sản phẩm trong giỏ hàng
- Thông tin sản phẩm: hình ảnh, tên, giá, số lượng
- Tính toán tổng tiền tự động
- Giao diện responsive, đẹp mắt

### 2. Thêm sửa xóa sản phẩm
- **Thêm sản phẩm**: Từ trang sản phẩm hoặc danh sách sản phẩm
- **Cập nhật số lượng**: Nút +/- hoặc nhập trực tiếp
- **Xóa sản phẩm**: Nút xóa với xác nhận
- **AJAX**: Tất cả thao tác đều sử dụng AJAX, không reload trang

### 3. Đặt hàng - Thanh toán
- Form nhập thông tin khách hàng đầy đủ
- Validation dữ liệu đầu vào
- 3 phương thức thanh toán: COD, Chuyển khoản, Thẻ tín dụng
- Hiển thị tổng kết đơn hàng
- Lưu đơn hàng vào database

### 4. Hóa đơn
- Hóa đơn đẹp mắt, chuyên nghiệp
- Thông tin đầy đủ: khách hàng, sản phẩm, tổng tiền
- Chức năng in hóa đơn
- Responsive design

## Cách sử dụng:

### Thêm sản phẩm vào giỏ hàng:
1. Truy cập trang sản phẩm (`/Products/Index` hoặc `/Products/Details/{id}`)
2. Click nút "Thêm vào giỏ hàng"
3. Sản phẩm sẽ được thêm vào giỏ hàng với thông báo thành công

### Xem và quản lý giỏ hàng:
1. Truy cập `/Cart/Index`
2. Xem danh sách sản phẩm
3. Cập nhật số lượng hoặc xóa sản phẩm
4. Click "Thanh toán" để tiếp tục

### Đặt hàng:
1. Điền đầy đủ thông tin khách hàng
2. Chọn phương thức thanh toán
3. Click "Đặt hàng"
4. Chuyển đến trang thành công

### Xem hóa đơn:
1. Từ trang thành công, click "Xem hóa đơn"
2. Hoặc truy cập trực tiếp `/Cart/Invoice/{orderId}`

## Công nghệ sử dụng:
- **Backend**: ASP.NET MVC, Entity Framework
- **Frontend**: Bootstrap 4, jQuery, Font Awesome
- **Database**: SQL Server (qua Entity Framework)
- **Session**: Lưu trữ giỏ hàng tạm thời trong Session

## Lưu ý:
- Giỏ hàng được lưu trong Session, sẽ mất khi đóng trình duyệt
- Sau khi đặt hàng thành công, giỏ hàng sẽ được xóa
- Tất cả giá tiền hiển thị theo định dạng Việt Nam (₫)
- Giao diện responsive, tương thích mobile

## Cải tiến có thể thêm:
- Lưu giỏ hàng vào database cho user đã đăng nhập
- Tích hợp payment gateway thực tế
- Gửi email xác nhận đơn hàng
- Quản lý trạng thái đơn hàng từ admin
- Tính phí vận chuyển theo địa chỉ
