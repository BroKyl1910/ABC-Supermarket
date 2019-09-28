use master
CREATE DATABASE ABCSupermarket
go

use ABCSupermarket

CREATE TABLE Product(
	ProductID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	ProductName VARCHAR(100) NOT NULL,
	ProductDesc VARCHAR(200),
	ProductImage VARBINARY(MAX),
	ProductPrice DECIMAL(6,2)
);