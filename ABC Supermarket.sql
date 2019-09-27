use master
CREATE DATABASE ABCSupermarket
go

use ABCSupermarket

CREATE TABLE Product(
	ProductID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	ProductName VARCHAR(50) NOT NULL,
	ProductDesc VARCHAR(100),
	ProductImage VARBINARY(MAX),
	ProductPrice DECIMAL(6,2)
);

ALTER TABLE PRODUCT
ALTER COLUMN ProductImage VARBINARY(MAX);

select * from product

delete from product where productid=13