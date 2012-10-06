create procedure Products_Delete
    @ProductID int
as
    delete from Products
    where ProductID = @ProductID