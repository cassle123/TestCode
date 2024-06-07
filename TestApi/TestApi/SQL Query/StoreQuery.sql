drop proc if EXISTS CheckUser
go
create proc CheckUser
    @Username nvarchar(50),
	@Password nvarchar(50)
as
begin
	if (@Password is null)
	begin
		SELECT count(*) FROM Users WHERE Username = @Username
	end
	else
	begin
		select count(*) from Users where Username = @Username and Password = @Password
	end
end
go

drop proc if EXISTS CreateUser
go
create proc CreateUser @Username nvarchar(50), @Email nvarchar(50), @Password nvarchar(50)
as 
begin 
	insert into Users (Username, Email, Password) values (@Username, @Email, @Password)
end
go

drop proc if exists GetUserBase
go
create proc GetUserBase
@Username nvarchar(100)
as
	select UserId, Username from Users where Username = @Username -- caanf gi them sau
go

drop proc if exists GetProfileUser
go
create proc GetProfileUser @Username nvarchar(50)
as
begin
	select top 1 Username, Email, CreatedAt from Users where Username = @Username
end

drop proc if exists GetListOrder
go
create proc GetListOrder
	@Orderid int
as
begin
	if (@Orderid is null)
	select t0.OrderId, t3.Username, t0.Description, t0.OrderDate from PurchaseOrder t0
	inner join Users t3 on t3.UserId = t0.UserId
	else
	select t0.OrderId, t1.ProductId, 
	   t1.Quantity, t2.ProductName, 
	   t2.Quantity as Instock, 
	   t2.Price 
	from PurchaseOrder t0
			inner join PurchaseOrderDetail t1 on t1.OrderId = t0.OrderId
			inner join ProductInformation t2 on t2.ProductId = t1.ProductId
			inner join Users t3 on t3.UserId = t0.UserId
	where t0.OrderId = @Orderid
end
go