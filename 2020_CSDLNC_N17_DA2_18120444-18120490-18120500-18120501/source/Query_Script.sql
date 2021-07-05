-- DELIVERY: Quản lý những đơn hàng đang giao 
create proc sp_DeliveringOrders @MaNM int
as
begin
	select PGH.MAPHIEUGH, dh.MA_DH, sp.TENSP, CTDH.SO_LUONG, PGH.TINHTRANG, PGH.NGAYGIAO, tt.HINH_THUC
	from ((((PHIEU_GIAO_HANG PGH join CHI_TIET_DON_HANG CTDH
	on PGH.ID = CTDH.ID) join SAN_PHAM sp on CTDH.MASP = sp.MASP ) join DON_HANG dh 
	on CTDH.MA_DH = dh.MA_DH) join THANH_TOAN tt on dh.MATT = tt.MATT)
	where dh.MANM = @MaNM  and PGH.TINHTRANG = 'Delivering'  
end
go

-- Cập nhật địa chỉ, số điện thoại của người mua
CREATE PROC updateBuyer @MANM INT, @DIACHI CHAR(50), @SDT CHAR(10)
AS
BEGIN
	IF(NOT EXISTS(SELECT MANM FROM dbo.NGUOI_MUA))
		BEGIN
			PRINT('ERROR')
			RETURN
		END
	ELSE
		BEGIN
			UPDATE dbo.NGUOI_MUA SET DIACHINM = @DIACHI, SDT = @SDT WHERE MANM = @MANM
		END
END
GO

-- Cập nhật địa chỉ, số điện thoại của người bán
CREATE PROC updateSeller @MANB INT, @DIACHI CHAR(50), @SDT CHAR(10)
AS
BEGIN
	IF(NOT EXISTS(SELECT MANB FROM dbo.NGUOI_BAN))
		BEGIN
			PRINT('ERROR')
			RETURN
		END
	ELSE
		BEGIN
			UPDATE dbo.NGUOI_BAN SET DIACHINB = @DIACHI, SDT = @SDT WHERE MANB = @MANB
		END
END
GO

-- Xem DS sản phẩm của @MANB
CREATE PROC PRINTPRODUCTS 
	@MANB INT
AS
BEGIN
	SELECT SP.*
    FROM NGUOI_BAN NB, BAN B, SAN_PHAM SP, LOAI_SAN_PHAM L
    WHERE NB.MANB = @MANB AND NB.MANB = B.MANB AND B.MASP = SP.MASP AND SP.MALOAI = L.MALOAI
END
GO


-- ADMIN: Cập nhật tình trạng đơn hàng
CREATE PROC CAPNHAT_TINH_TRANG_DON_HANG @MAPGH INT, @TINHTRANG VARCHAR(10)
AS
BEGIN
     IF(NOT EXISTS(SELECT* FROM dbo.PHIEU_GIAO_HANG WHERE MAPHIEUGH=@MAPGH))
	 BEGIN
	     PRINT('OOPS, MADH IS NOT EXISTS')
	     RETURN
	 END
     UPDATE dbo.PHIEU_GIAO_HANG SET TINHTRANG=@TINHTRANG WHERE MAPHIEUGH=@MAPGH
END
GO

-- Tìm kiếm sản phẩm theo tên
CREATE PROC searchByProductName @TENSP CHAR(20)
AS
BEGIN
	SELECT * 
	FROM dbo.SAN_PHAM WHERE TENSP = @TENSP
END
GO

EXEC searchByProductName 'Floor'

-- ADMIN: Thống kê số đơn hàng được đặt trong một ngày của một tháng
CREATE PROC TheNumberOfOrders @month int, @day int
AS
BEGIN 
	SELECT COUNT(*) AS 'The number of Orders'
	FROM DON_HANG DH
	WHERE MONTH(DH.NGAYDATHANG) = @month AND DAY(DH.NGAYDATHANG) = @day
END
GO

EXEC TheNumberOfOrders 4,20

-- BUY
--Chọn mua hàng, nhập số lượng, tên người nhận, địa chỉ nhận hàng,
--chọn đơn vị giao hàng, nhập mã km và mã freeship nếu có, chọn 
--phương thức thanh toán, thanh toán nếu chọn thanh toán online.
ALTER PROC addToCart 
	@TENSP CHAR(20), 
	@MASP INT, 
	@MANM INT, 
	@SOLUONG INT, 
	@HINHTHUCTHANHTOAN CHAR(10), 
	@MADVVC INT
AS 
BEGIN
	DECLARE @MADH INT, @MAGIOHANG INT, @ID INT, @MATT INT, @MAPHIEUGH INT, @TONGTIEN FLOAT
	SET @ID = (SELECT MAX(dbo.CHI_TIET_DON_HANG.ID) FROM dbo.CHI_TIET_DON_HANG)
	SET @ID=@ID+1
	SET @MADH = (SELECT MAX( dbo.DON_HANG.MA_DH ) FROM dbo.DON_HANG)
	SET @MADH = @MADH + 1
	SET @MAGIOHANG = (SELECT MAX( dbo.GIO_HANG.MAGIOHANG) FROM dbo.GIO_HANG)
	SET @MAGIOHANG = @MAGIOHANG+1
	SET @MATT = (SELECT MAX(dbo.THANH_TOAN.MATT) FROM dbo.THANH_TOAN)
	SET @MATT = @MATT +1 
	SET @MAPHIEUGH = (SELECT MAX (dbo.PHIEU_GIAO_HANG.MAPHIEUGH) FROM dbo.PHIEU_GIAO_HANG)
	SET @MAPHIEUGH = @MAPHIEUGH + 1
	SET @TONGTIEN = (SELECT GIAHIENTHOI FROM dbo.SAN_PHAM WHERE MASP = @MASP)
	SET @TONGTIEN = @TONGTIEN* @SOLUONG
	IF((SELECT SOLUONGTON FROM dbo.SAN_PHAM WHERE MASP = @MASP) = 0)
		BEGIN
			PRINT('Oops, out of stock')
			RETURN
		END
			INSERT INTO dbo.THANH_TOAN
			(
			    MATT,
			    HINH_THUC,
			    SO_TIEN
			)
			VALUES
			(   @MATT,  -- MATT - int
			    @HINHTHUCTHANHTOAN, -- HINH_THUC - char(10)
			    @TONGTIEN -- SO_TIEN - float
			)

			INSERT INTO dbo.DON_HANG
			(
			    MA_DH,
			    MATT,
			    MANM,
			    MAUD,
			    NGAYDATHANG
			)
			VALUES
			(   @MADH,        -- MA_DH - int
			    @MATT,        -- MATT - int
			    @MANM,        -- MANM - int
			    12,        -- MAUD - int
			    GETDATE() -- NGAYDATHANG - datetime
			)
			INSERT INTO dbo.GIO_HANG
			(
				MAGIOHANG,
				TENSP,	
				MASP,
				MANM,
				MADH,
				SOLUONG
			)
			VALUES
			(
				@MAGIOHANG,
				@TENSP,
				@MASP,
				@MANM,
				@MADH,
				@SOLUONG
			)

			INSERT INTO dbo.CHI_TIET_DON_HANG
			(
			    SO_LUONG,
			    COMMENT,
			    DANHGIA,
			    ID,
			    MASP,
			    MA_DH
			)
			VALUES
			(   
				@SOLUONG,  -- SO_LUONG - int
			    NULL, -- COMMENT - char(1000)
			    NULL, -- DANHGIA - char(10)
			    @ID,  -- ID - int
			    @MASP,  -- MASP - int
			    @MADH   -- MA_DH - int
			)
		
			INSERT INTO dbo.PHIEU_GIAO_HANG
			(
			    MAPHIEUGH,
			    ID,
			    MA_DVVC,
			    MAUD,
			    NGAYGIAO,
			    LANGIAO,
			    TINHTRANG,
			    PHI_SHIP
			)
			VALUES
			(   @MAPHIEUGH,         -- MAPHIEUGH - int
			    @ID,         -- ID - int
			    @MADVVC,         -- MA_DVVC - int
			    12,         -- MAUD - int
			    GETDATE() + 3, -- NGAYGIAO - datetime
			    1,         -- LANGIAO - int
			    NULL,        -- TINHTRANG - varchar(10)
			    30000        -- PHI_SHIP - float
			)
			
	UPDATE dbo.SAN_PHAM SET SOLUONGTON = SOLUONGTON - 1 WHERE MASP = @MASP
END
GO

EXEC addToCart 'Room', 466, 8802, 3, 'Online', 3

-- ADD A PRODUCT
CREATE PROC addProduct 
	@MASP INT, 
	@MALOAI INT, 
	@TENSP CHAR(20), 
	@MOTA CHAR(1000), 
	@SOLUONGTON INT, 
	@GIAHIENTHOI FLOAT, 
	@MANB int
AS
BEGIN
	INSERT INTO dbo.SAN_PHAM
	(
	    MASP,
	    MALOAI,
	    TENSP,
	    MOTA,
	    SOLUONGTON,
	    GIAHIENTHOI
	)
	VALUES
	(   @MASP,   -- MASP - int
	    @MALOAI,   -- MALOAI - int
	    @TENSP,  -- TENSP - char(20)
	    @MOTA,  -- MOTA - char(1000)
	    @SOLUONGTON,   -- SOLUONGTON - int
	    @GIAHIENTHOI -- GIAHIENTHOI - float
	    )
	INSERT INTO dbo.BAN 
	(
		MANB,
		MASP
	)
	VALUES
	(
		@MANB,
		@MASP
	)
END
GO

EXEC addProduct 10005,1,'Shaking','Wonderful',5,10000000,26

-- ORDERS: Quản lý những đơn hàng của Người Bán
create proc Orders @MaNB int
as
begin 
	SELECT DH.MA_DH, DH.NGAYDATHANG, CT.MASP, CT.SO_LUONG, 
	       CT.COMMENT, CT.DANHGIA, PGH.NGAYGIAO, PGH.LANGIAO,
		   PGH.TINHTRANG, DVVC.MA_DVVC, DVVC.TEN_DVVC
	FROM BAN B, SAN_PHAM SP, CHI_TIET_DON_HANG CT, DON_HANG DH,
	     PHIEU_GIAO_HANG PGH, DON_VI_VAN_CHUYEN DVVC
	WHERE B.MANB = @MaNB AND B.MASP = SP.MASP AND SP.MASP = CT.MASP 
	      AND CT.MA_DH = DH.MA_DH AND PGH.ID = CT.ID AND DVVC.MA_DVVC = PGH.MA_DVVC
end
go

EXEC Orders 26

-- PURCHASE HISTORY: Quản lý những đơn hàng đã giao, bị hủy, bị trễ
create proc sp_ViewPurchaseOrders @MANM int
as
begin
	SELECT DH.MA_DH, PGH.MAPHIEUGH, DH.NGAYDATHANG, CT.MASP, SP.MASP, CT.SO_LUONG, 
		   CT.COMMENT, CT.DANHGIA, PGH.NGAYGIAO, PGH.LANGIAO,
		   PGH.TINHTRANG, PGH.PHI_SHIP
	FROM DON_HANG DH, CHI_TIET_DON_HANG CT, SAN_PHAM SP,
		 PHIEU_GIAO_HANG PGH, DON_VI_VAN_CHUYEN DVVC
	WHERE DH.MANM = @MANM AND DH.MA_DH = CT.MA_DH AND CT.ID = PGH.ID 
		  AND PGH.MA_DVVC = DVVC.MA_DVVC
	      AND PGH.TINHTRANG <> 'Delivering'
end
go

EXEC sp_ViewPurchaseOrders 1673


-- Cài đặt index:
CREATE NONCLUSTERED INDEX idx_tenSP ON SAN_PHAM(TENSP)
CREATE NONCLUSTERED INDEX idx_maSP ON CHI_TIET_DON_HANG(MASP)
CREATE NONCLUSTERED INDEX idx_maDH ON CHI_TIET_DON_HANG(MA_DH)
CREATE NONCLUSTERED INDEX idx_ID ON PHIEU_GIAO_HANG(ID)
CREATE NONCLUSTERED INDEX idx_maNM ON DON_HANG(MANM)
