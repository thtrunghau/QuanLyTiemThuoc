create database QuanLyTiemThuoc

use QuanLyTiemThuoc


---Medicine
---MedicineSlaver
---MedicineCategory
---Account
---Bill
---BillInfor

create table MedicineSlaver
(
	id int identity primary key,
	name nvarchar(100) not null default N'Chưa đặt tên',
	status nvarchar(100) not null, --trống, có người
)

create table Account(
	DisplayName nvarchar(100) not null default N'User',
	UserName nvarchar(100) primary key,
	PassWord nvarchar(1000) not null default 0 ,
	Type int not null default 0 ---1: admin , 0: staff
)

create table MedicineCategory (
	id int identity primary key,
	name nvarchar(100) not null default N'Chưa đặt tên'
)

create table Medicine(
	id int identity primary key,
	name nvarchar(100) not null default N'Chưa đặt tên',
	idCategory int not null,
	price float not null default 0
	foreign key (idCategory) references MedicineCategory(id)
)

create table Bill(
	id int identity primary key,
	DateCheckIn date not null,
	DateCheckOut date not null,
	idSlaver int not null,
	status int not null default 0, -- 1: da thanh toan, 0: chua thanh toan,	
	foreign key (idSlaver) references MedicineSlaver(id)
)

create table BillInfor (
	id int identity primary key,
	idBill int not null,
	idMedicine int not null,
	count int not null default 0
	foreign key (idBill) references Bill(id),
	foreign key (idMedicine) references Medicine(id)
)