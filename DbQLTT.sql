create database QuanLyTiemThuoc

use QuanLyTiemThuoc


---Medicine
---MedicineSlaver
---MedicineCategory
---Account
---Bill
---BillInfor
------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------
create table MedicineSlaver
(
	id int identity primary key,
	name nvarchar(100) not null default N'Chưa đặt tên',
	status nvarchar(100) not null, --trống, có người
)
ALTER TABLE MedicineSlaver
DROP COLUMN status
alter table MedicineSlaver add  status nvarchar(100) not null default N'Trống'

insert into MedicineSlaver values (N'Khay 10')

declare @i int = 0

while @i < 10
begin 
	insert into MedicineSlaver (name) values (N'Khay ' + cast(@i as nvarchar(100)))
	set @i = @i + 1
end

create proc USP_GetSlaverList 
as select * from MedicineSlaver

exec USP_GetSlaverList

alter proc USP_DeleteSlaver
@idSlaver int
as
begin
	delete from BillInfor where idBill IN (SELECT id FROM Bill WHERE idSlaver = @idSlaver)
	delete from Bill where idSlaver = @idSlaver
	delete from MedicineSlaver where id = @idSlaver
end

select * from BillInfor where idBill IN (SELECT id FROM Bill WHERE idSlaver = 53)
select * from MedicineSlaver
select * from Bill
select * from BillInfor
select * from Medicine
select * from MedicineCategory

create proc USP_SearchSlaver
@name nvarchar(1000)
as
begin
	SELECT * FROM MedicineSlaver WHERE dbo.fuConvertToUnsign(name) LIKE N'%' + dbo.fuConvertToUnsign(@name) + '%'
end

exec USP_SearchSlaver N'Khay 1'
------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------
create table Account(
	DisplayName nvarchar(100) not null default N'User',
	UserName nvarchar(100) primary key,
	PassWord nvarchar(1000) not null default 0 ,
	Type int not null default 0 ---1: admin , 0: staff
)

insert into Account (DisplayName, UserName, PassWord, Type) values 
(N'Trung Hau', N'trunghau', N'123', 1),
(N'Hai Dang', N'haidang', N'123', 0)

select * from Account where UserName = N'trunghau'
create proc USP_Login
@userName nvarchar(100), @passWord nvarchar(100)
as
begin
	select * from Account where UserName = @userName AND PassWord = @passWord
end

create proc USP_UpdateAccount
@userName nvarchar(100), @displayName nvarchar(100), @passWord nvarchar(100), @newPassWord nvarchar(100)
as 
begin
	declare @isRightAccount int
	select @isRightAccount = count(*) from Account where UserName = @userName and PassWord = @passWord
	if(@isRightAccount = 1)	
	begin
		if (@newPassWord = null or @newPassWord = '')
		begin
			update Account set DisplayName = @displayName where UserName = @userName
		end
		else
		begin
			update Account set DisplayName = @displayName, PassWord = @newPassWord where UserName = @userName
		end
	end
		
end

select UserName, DisplayName, Type from Account
create proc USP_SearchAccount
@name nvarchar(1000)
as
begin
	SELECT * FROM Account WHERE dbo.fuConvertToUnsign(UserName) LIKE N'%' + dbo.fuConvertToUnsign(@name) + '%'
end

exec USP_SearchAccount N'trunghau'
------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------
create table MedicineCategory (
	id int identity primary key,
	name nvarchar(100) not null default N'Chưa đặt tên'
)

-- Chèn dữ liệu mẫu vào bảng MedicineCategory
INSERT INTO MedicineCategory (name)
VALUES 
    (N'Sản phẩm sâm Ngọc Linh'),
    (N'Trà thảo mộc'),
    (N'Phương thuốc Bắc'),
    (N'Nấm y học'),
    (N'Bổ sung thảo dược');

select *from MedicineCategory

alter proc USP_DeleteCategory
@idCategory int
as
begin
	delete from BillInfor where idMedicine IN (SELECT id FROM Medicine WHERE idCategory = @idCategory)
	delete from Medicine where idCategory = @idCategory
	delete from MedicineCategory where id = @idCategory
end

exec USP_DeleteCategory 6

create proc USP_SearchCategory
@name nvarchar(1000)
as
begin
	SELECT * FROM MedicineCategory WHERE dbo.fuConvertToUnsign(name) LIKE N'%' + dbo.fuConvertToUnsign(@name) + '%'
end

exec USP_SearchCategory N'moc'
------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------
create table Medicine(
	id int identity primary key,
	name nvarchar(100) not null default N'Chưa đặt tên',
	idCategory int not null,
	price float not null default 0
	foreign key (idCategory) references MedicineCategory(id)
)

-- Chèn dữ liệu mẫu vào bảng Medicine
INSERT INTO Medicine (name, idCategory, price)
VALUES 
    (N'Viên nang sâm Ngọc Linh', 1, 50000),
    (N'Bao tử sâm Ngọc Linh', 1, 35000),
    (N'Trà Gynostemma', 2, 25000),
    (N'Trà Hoa Cúc', 2, 20000),
    (N'Tiểu Hoàn Đan', 3, 60000),
    (N'Bái Hổ Tăng Nhân Sâm Đan', 3, 70000),
    (N'Chiết xuất nấm Linh Chi', 4, 80000),
    (N'Viên nang nấm Mân Côi', 4, 55000),
    (N'Chiết xuất trà xanh', 5, 30000),
    (N'Viên bổ sung Dầu Cá', 5, 45000);

select * from Medicine
select id as [ID], Medicine.name as [Tên Thuốc], idCategory as[ID Category],price as[Giá] from   Medicine

alter proc USP_SearchMedicine
@name nvarchar(1000)
as
begin
	SELECT * FROM Medicine WHERE dbo.fuConvertToUnsign(name) LIKE N'%' + dbo.fuConvertToUnsign(@name) + '%'
end

exec USP_SearchMedicine N'Viên bổ'
------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------
create table Bill(
	id int identity primary key,
	DateCheckIn date not null default getdate(),
	DateCheckOut date not null default getdate(),
	idSlaver int not null,
	status int not null default 0, -- 1: da thanh toan, 0: chua thanh toan,	
	foreign key (idSlaver) references MedicineSlaver(id)
)

--chưa có discount
update Bill set discount = 0

select * from Bill
select * from MedicineSlaver
insert into Bill values
(GetDate(), GetDate(),43),
(GetDate(), GetDate(),44),
(GetDate(), GetDate(),45)

alter table Bill add discount int default 0

select m.name, bi.count, m.price, m.price*bi.count as totalPrice from Bill as b, BillInfor as bi, Medicine as m
where bi.idBill = b.id and bi.idMedicine= m.id and b.status = 0 and b.idSlaver = 32


alter proc UPS_InsertBill
@idSlaver int
as
begin
	INSERT INTO Bill(DateCheckIn, DateCheckOut, idSlaver, status, discount)
	VALUES (GETDATE(), GETDATE(), @idSlaver, 0, 0);
end

exec UPS_InsertBill 44


alter trigger UTG_UpdateBill
on Bill for update
as
begin
	declare @idBill int

	select @idBill = id from inserted

	declare @idSlaver int

	select @idSlaver = idSlaver from Bill where id = @idBill

	declare @count int = 0

	select @count = COUNT(*)  from Bill where idSlaver = @idSlaver and status = 0

	if(@count = 0)
			update MedicineSlaver set status = N'Trống' where id = @idSlaver
end	

alter table Bill add totalPrice float default 0

create proc USP_GetListBillByDate 
@checkIn date, @checkOut date
as
begin
	select ms.name as [Tên Khay], b.DateCheckIn as[Ngày Bán], b.DateCheckOut as[Ngày Thanh Toán], b.status as [Trạng Thái], b.totalPrice as [Tổng Tiền]
	from Bill as b, MedicineSlaver as ms
	where DateCheckIn >= @checkIn and b.DateCheckOut <= @checkOut and b.status = 1 and ms.id = b.idSlaver
end

select *from Bill
exec USP_GetListBillByDate '2024-05-04', '2024-05-09'
------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------
create table BillInfor (
	id int identity primary key,
	idBill int not null,
	idMedicine int not null,
	count int not null default 0
	foreign key (idBill) references Bill(id),
	foreign key (idMedicine) references Medicine(id)
)
select * from BillInfor where idBill = 2
insert into BillInfor values
(1, 1, 2),
(1, 2, 2),
(2, 3, 2),
(2, 4, 1)


create proc USP_InserBillInfo
@idBill int, @idMedicine int, @count int
as
begin
	declare @isExistBillInfo int;
	declare @medicineCount int  = 1;
	select @isExistBillInfo = id,@medicineCount = count from BillInfor where idBill = @idBill and idMedicine = @idMedicine
	
	if(@isExistBillInfo > 0)
	begin 
		declare @newCount int = @medicineCount + @count
		if(@newCount > 0) 
			update BillInfor set count = @medicineCount + @count where idMedicine = @idMedicine
		else
			delete BillInfor where idBill = @idBill and idMedicine = @idMedicine
	end
	else 
	begin
		insert into BillInfor (idBill, idMedicine, count) 
		values (@idBill, @idMedicine, @count)
	end
		
end

alter trigger UTG_UpdateBillInfo
on BillInfor for insert, update
as
begin
	declare @idBill int
	select @idBill = idBill from inserted
	
	declare @idSlaver int

	select @idSlaver = idSlaver from Bill where @idBill = id and status = 0

	declare @countBillInfor int

	select @countBillInfor = count(*) from BillInfor where idBill = @idBill

	if(@countBillInfor > 0)
		update MedicineSlaver set status = N'Có người' where id = @idSlaver
	else
		update MedicineSlaver set status = N'Trống' where id = @idSlaver
end

create trigger UTG_DeleteBillInfor
on BillInfor for delete
as
begin
	declare @idBillInfor int
	declare @idBill int
	select @idBillInfor = id, @idBill = deleted.idBill from deleted

	declare @idSlaver int
	select @idSlaver = idSlaver from Bill where id = @idBill
	
	declare @count int = 0
	select @count = count(*) from BillInfor as bi, Bill as b where b.id = bi.idBill and b.id = @idBill and b.status = 0

	if(@count = 0)
		update MedicineSlaver set status = N'Trống' where id = @idSlaver
end


---function searching
create FUNCTION [dbo].[fuConvertToUnsign] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END
