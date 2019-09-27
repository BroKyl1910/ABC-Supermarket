use master
CREATE DATABASE ABCSupermarket
go

use ABCSupermarket

CREATE TABLE Product(
	ProductID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	ProductName VARCHAR(50) NOT NULL,
	ProductDesc VARCHAR(100),
	ProductImage VARBINARY,
	ProductPrice DECIMAL(6,2)
);