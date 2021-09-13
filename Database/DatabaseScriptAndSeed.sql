use master;


drop database ProductsAdmin;

go

create database ProductsAdmin;


go

use ProductsAdmin;

go

create table Statuses(
	Id				int identity not null,
	Type			varchar(100) not null,
	Name			varchar(100) not null,
	primary key(Id),
	unique(Name,Type)
);

set identity_insert Statuses off;

insert into Statuses values ('ENTRY','ACTIVE');
insert into Statuses values ('ENTRY','CANCELLED');
insert into Statuses values ('ENTRY','DELETED');

set identity_insert Statuses on;

create table Product(
	Id							int  identity not null,
	Name						nvarchar(100) not null,
	Description					nvarchar(100) not null,
	Status						int not null,
	CreatedAt					datetime not null default getdate(),
	primary key(Id),
	foreign key(Status) references Statuses(Id)
);

create index Ix_Product_ProductName on Product(Name);
create index Ix_Product_ProductDescription on Product(Description);

declare @repeat int = 0;
while @repeat<100 begin 

	
	insert into Product values ('Producto '+cast(@repeat as varchar(10)), 'Fake description '+cast(NEWID() as varchar(100)),1,getdate());

	set @repeat+=1;
end


select * from Product

create table Color(
	Id					int	 identity not null,
	Name				nvarchar(100) not null,
	Value				nvarchar(20) not null,
	Format				nvarchar(100) not null default 'HEX',
	Status				int not null,
	CreatedAt			datetime not null default getdate(),
	primary key(Id),
	foreign key(Status) references Statuses(Id),
	unique(Format,Value)
);

create table ProductPrices(
	Id					int  identity not null,
	ProductId			int not null,
	ColorId				int not null,
	Price				money not null,
	Status				int not null,
	IsDefaultPrice		bit not null default 0,
	CreatedAt			datetime not null default getdate()
	primary key(Id),
	foreign key(ProductId) references Product(Id),
	foreign key(Status) references Statuses(Id),
	foreign key(ColorId) references Color(Id)
);

select * from Statuses


set identity_insert Color off;

insert into Color values ('AZUL','0000FF','HEX',1,getdate());
insert into Color values ('ROJO','FF0000','HEX',1,getdate());
insert into Color values ('VERDE','00FF00','HEX',1,getdate());


SELECT * FROM Color


insert into ProductPrices
select  
	a.Id,
	CAST(RAND(CHECKSUM(NEWID())) * 3 as INT) + 1,
	ABS(CHECKSUM(NewId())) % 10000,
	1,
	1,
	getdate()
from Product as a


set @repeat = 1;
while @repeat<100 begin 

	declare @routine int = CAST(RAND(CHECKSUM(NEWID())) * 6 as INT) + 1;
	while @routine>0 begin
		
		insert into ProductPrices values (
			@repeat,
			CAST(RAND(CHECKSUM(NEWID())) * 3 as INT) + 1,
			ABS(CHECKSUM(NewId())) % 10000,
			1,
			0,
			getdate()
		)
		set @routine-=1;
	end
	
	set @repeat+=1;
end