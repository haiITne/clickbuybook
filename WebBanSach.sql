-- Tạo cơ sở dữ liệu
CREATE DATABASE WebBanSach
GO

USE WebBanSach
GO

-- Bảng Thể Loại Sách
CREATE TABLE TheLoaiSach (
    MaTheLoai INT IDENTITY(1,1) PRIMARY KEY,
    TenTheLoai NVARCHAR(50) NOT NULL
)
GO

-- Bảng Nhà Xuất Bản
CREATE TABLE NhaXuatBan (
    MaNXB INT IDENTITY(1,1) PRIMARY KEY,
    TenNXB NVARCHAR(50) NOT NULL,
    DiaChi NVARCHAR(MAX),
    DienThoai VARCHAR(15)
)
GO

-- Bảng Sách
CREATE TABLE Sach (
    MaSach INT IDENTITY(1,1) PRIMARY KEY,
    TenSach NVARCHAR(100) NOT NULL,
    GiaBan DECIMAL(18,2) NOT NULL,
    MoTa NVARCHAR(MAX),
    AnhBia NVARCHAR(MAX),
    NgayCapNhat DATETIME DEFAULT GETDATE(),
    SoLuongTon INT,
    MaNXB INT NOT NULL,
    MaTheLoai INT NOT NULL,
    FOREIGN KEY (MaNXB) REFERENCES NhaXuatBan(MaNXB),
    FOREIGN KEY (MaTheLoai) REFERENCES TheLoaiSach(MaTheLoai)
)
GO

-- Bảng Tác Giả
CREATE TABLE TacGia (
    MaTacGia INT IDENTITY(1,1) PRIMARY KEY,
    TenTacGia NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(MAX),
    TieuSu NVARCHAR(MAX),
    DienThoai VARCHAR(15)
)
GO

-- Bảng Tham Gia (liên kết giữa Sách và Tác Giả)
CREATE TABLE ThamGia (
    MaSach INT NOT NULL,
    MaTacGia INT NOT NULL,
    VaiTro NVARCHAR(50),
    ViTri NVARCHAR(50),
    PRIMARY KEY (MaSach, MaTacGia),
    FOREIGN KEY (MaSach) REFERENCES Sach(MaSach),
    FOREIGN KEY (MaTacGia) REFERENCES TacGia(MaTacGia)
)
GO

-- Bảng Khách Hàng
CREATE TABLE KhachHang (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    TaiKhoan VARCHAR(50) NOT NULL UNIQUE,
    MatKhau NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(200),
    DienThoai VARCHAR(15),
    GioiTinh NVARCHAR(3),
    NgaySinh DATETIME
)
GO

-- Bảng Đơn Hàng
CREATE TABLE DonHang (
    MaDonHang INT IDENTITY(1,1) PRIMARY KEY,
    DaThanhToan BIT DEFAULT 0,
    TinhTrangGiaoHang NVARCHAR(50),
    NgayDat DATETIME DEFAULT GETDATE(),
    NgayGiao DATETIME,
    MaKH INT NOT NULL,
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
)
GO

-- Bảng Chi Tiết Đơn Hàng
CREATE TABLE ChiTietDonHang (
    MaDonHang INT NOT NULL,
    MaSach INT NOT NULL,
    SoLuong INT,
    DonGia DECIMAL(18,2),
    PRIMARY KEY (MaDonHang, MaSach),
    FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang),
    FOREIGN KEY (MaSach) REFERENCES Sach(MaSach)
)
GO

CREATE TABLE Cart (
    CartID INT IDENTITY(1,1) PRIMARY KEY,
    MaKH INT NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
)
GO

CREATE TABLE CartItem (
    CartItemID INT IDENTITY(1,1) PRIMARY KEY,
    CartID INT NOT NULL,
    MaSach INT NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (CartID) REFERENCES Cart(CartID),
    FOREIGN KEY (MaSach) REFERENCES Sach(MaSach)
)
GO


-- Thêm dữ liệu mẫu
INSERT INTO TheLoaiSach (TenTheLoai) VALUES 
('Công nghệ thông tin'),
('Ngoại ngữ'),
('Phật Giáo'),
('Sách học làm người'),
('Văn học nước ngoài'),
('Kinh Tế'),
('Khoa học Vật lý'),
('Khoa học Xã hội'),
('Luật'),
('Từ điển'),
('Lịch sử, địa lý'),
('Vật nuôi - Cây cảnh'),
('Mỹ thuật'),
('Nghệ thuật'),
('Âm nhạc'),
('Sách giáo khoa'),
('Sách tham khảo'),
('Danh nhân'),
('Tâm lý giáo dục'),
('Thể thao - Võ thuật'),
('Văn hóa - Xã hội'),
('Nữ công gia chánh'),
('Nghệ thuật làm đẹp'),
('Du lịch'),
('Y Học dân tộc vn')
GO

INSERT INTO NhaXuatBan (TenNXB, DiaChi, DienThoai) VALUES
('Nhà xuất bản Trẻ', '124 Nguyễn Văn Cừ Q.1 Tp.HCM', '1900156045'),
('NXB Thống kê', 'Biên Hòa-Đồng Nai', '1900151112'),
('Kim đồng', 'Tp.HCM', '1900157090'),
('Đại học quốc gia', 'Tp.HCM', '0908419981'),
('Văn hóa nghệ thuật', 'Đà Nẵng', '0903118833'),
('Văn hóa', 'Bình Dương', '0913336677'),
('NXB Lao động - Xã hội', 'Tp.HCM', '0989888888'),
('Khoa Học & Kỹ Thuật', 'Hà Nội', '0903118824'),
('Thanh Niên', 'Tp.HCM', '0903118811'),
('NXB Tài Chính', 'Huế', '0903118833')
GO

INSERT INTO TacGia (TenTacGia, DiaChi, TieuSu, DienThoai) VALUES
('Nguyễn Nhật Ánh', 'Tp.HCM', 'Tác giả nổi tiếng với nhiều tác phẩm cho tuổi teen', '0912345678'),
('J.K. Rowling', 'Anh Quốc', 'Tác giả của bộ truyện Harry Potter', '0912345679'),
('Dale Carnegie', 'Hoa Kỳ', 'Tác giả của sách Đắc Nhân Tâm', '0912345680')
GO

INSERT INTO KhachHang (HoTen, TaiKhoan, MatKhau, Email, DiaChi, DienThoai, GioiTinh, NgaySinh) VALUES
('Nguyễn Văn A', 'nguyenvana', 'password123', 'nguyenvana@example.com', '123 Đường ABC, Tp.HCM', '0909123456', 'Nam', '1990-01-01'),
('Trần Thị B', 'tranthib', 'password123', 'tranthib@example.com', '456 Đường DEF, Hà Nội', '0909123457', 'Nữ', '1992-02-02')
GO

INSERT INTO Sach (TenSach, GiaBan, MoTa, AnhBia, NgayCapNhat, SoLuongTon, MaNXB, MaTheLoai) VALUES
(N'Harry Potter và Hòn Đá Phù Thủy', 150000, N'Cuốn sách đầu tiên trong bộ truyện Harry Potter', 'harry_potter_1.jpg', '2024-07-25', 100, 1, 5),
(N'Đắc Nhân Tâm', 120000, N'Cuốn sách nổi tiếng về nghệ thuật giao tiếp và thành công trong cuộc sống', 'dac_nhan_tam.jpg', '2024-07-25', 50, 2, 4),
(N'Lập Trình C# Từ Đầu', 200000, N'Cuốn sách hướng dẫn lập trình C# từ cơ bản đến nâng cao', 'lap_trinh_c_sharp.jpg', '2024-07-25', 30, 3, 1),
(N'Hướng Dẫn Sử Dụng Bootstrap', 180000, N'Cuốn sách hướng dẫn sử dụng Bootstrap để tạo các giao diện web hiện đại', 'huong_dan_bootstrap.jpg', '2024-07-25', 25, 4, 1),
(N'Sách Giáo Khoa Toán Lớp 12', 50000, N'Cuốn sách giáo khoa môn Toán dành cho học sinh lớp 12', 'sgk_toan_12.jpg', '2024-07-25', 200, 5, 16),
(N'Những Người Khốn Khổ', 170000, N'Tác phẩm kinh điển của Victor Hugo', 'nhung_nguoi_khon_kho.jpg', '2024-07-25', 60, 1, 5),
(N'Cấu Trúc Dữ Liệu và Giải Thuật', 190000, N'Cuốn sách về cấu trúc dữ liệu và giải thuật trong khoa học máy tính', 'cau_truc_du_lieu.jpg', '2024-07-25', 40, 2, 1),
(N'Lịch Sử Việt Nam', 130000, N'Cuốn sách tổng hợp về lịch sử Việt Nam', 'lich_su_vn.jpg', '2024-07-25', 70, 3, 11),
(N'Nghệ Thuật Nói Trước Công Chúng', 140000, N'Cuốn sách hướng dẫn cách nói trước công chúng một cách tự tin và hiệu quả', 'nghe_thuat_noi_truoc_cong_chung.jpg', '2024-07-25', 45, 4, 19),
(N'Mỹ Thuật Căn Bản', 160000, N'Cuốn sách về các nguyên lý và kỹ thuật cơ bản của mỹ thuật', 'my_thuat_can_ban.jpg', '2024-07-25', 55, 5, 13)
GO
