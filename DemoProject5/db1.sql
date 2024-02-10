CREATE DATABASE DemoDb3
GO

USE DemoDb3
GO

CREATE TABLE Items(
    ItemId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    ItemName NVARCHAR(500) NOT NULL,
	UnitPrice Decimal(18,4) NOT NULL Default 0
)
GO
-- Insert data into the Items table
INSERT INTO Items (ItemName,UnitPrice) VALUES ('NSP Exercise 80 Pages',90);
INSERT INTO Items (ItemName,UnitPrice) VALUES ('NSP Exercise 120 Pages',100);
INSERT INTO Items (ItemName,UnitPrice) VALUES ('NSP Exercise 160 Pages',110);
INSERT INTO Items (ItemName,UnitPrice) VALUES ('NSP Exercise 200 Pages',120);
GO

CREATE TABLE BillMasters(
    BillMasterId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    BillDate DATETIME NOT NULL,
	BillTime DATETIME NOT NULL,
	SubTotal Decimal(18,4) NOT NULL Default 0, 
    Discount Decimal(18,4) NOT NULL Default 0,
    VAT Decimal(18,4) NOT NULL Default 0,
	GrandTotal Decimal(18,4) NOT NULL Default 0
);
GO

CREATE TABLE BillDetails(
    BillDetailId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    BillMasterId INT NOT NULL, 
	ItemId INT NOT NULL,
	Quantity INT NOT NULL Default 0,
	Amount Decimal(18,4) NOT NULL Default 0,
    FOREIGN KEY (BillMasterId) REFERENCES BillMasters(BillMasterId),
	FOREIGN KEY (ItemId) REFERENCES Items(ItemId)
);
GO

CREATE PROCEDURE GetItems
AS
BEGIN
    SELECT ItemId, ItemName, UnitPrice
    FROM Items;
END
GO

CREATE PROCEDURE GetItemById
    @Id INT
AS
BEGIN
    SELECT ItemId, ItemName, UnitPrice
    FROM Items
    WHERE ItemId = @Id;
END
GO

CREATE PROCEDURE CreateItem
    @ItemName NVARCHAR(500),
    @UnitPrice DECIMAL(18,4)
AS
BEGIN
    INSERT INTO Items (ItemName, UnitPrice)
    VALUES (@ItemName, @UnitPrice);
END
GO

CREATE PROCEDURE UpdateItem
    @Id INT,
    @ItemName NVARCHAR(500),
    @UnitPrice DECIMAL(18,4)
AS
BEGIN
    UPDATE Items
    SET ItemName = @ItemName,
        UnitPrice = @UnitPrice
    WHERE ItemId = @Id;
END
Go

CREATE PROCEDURE DeleteItem
    @Id INT
AS
BEGIN
    DELETE FROM Items
    WHERE ItemId = @Id;
END
GO

CREATE PROCEDURE GetBillDetails
AS
BEGIN
    SELECT BillDetailId, BillMasterId, ItemId, Quantity, Amount
    FROM BillDetails
END
GO

CREATE PROCEDURE GetBillDetailById
    @Id INT
AS
BEGIN
    SELECT BillDetailId, BillMasterId, ItemId, Quantity, Amount
    FROM BillDetails
    WHERE BillDetailId = @Id;
END
GO

CREATE PROCEDURE CreateBillDetail
    @BillMasterId INT,
    @ItemId INT,
    @Quantity INT,
    @Amount DECIMAL
AS
BEGIN
    INSERT INTO BillDetails (BillMasterId, ItemId, Quantity, Amount)
    VALUES (@BillMasterId, @ItemId, @Quantity, @Amount);

    DECLARE @InsertedId INT;
    SET @InsertedId = SCOPE_IDENTITY();

    SELECT BillDetailId, BillMasterId, ItemId, Quantity, Amount
    FROM BillDetails
    WHERE BillDetailId = @InsertedId;
END
GO

CREATE PROCEDURE UpdateBillDetail
    @Id INT,
    @ItemId INT,
    @Quantity INT,
    @Amount DECIMAL
AS
BEGIN
    UPDATE BillDetails
    SET ItemId = @ItemId,
        Quantity = @Quantity,
        Amount = @Amount
    WHERE BillDetailId = @Id;
END
GO

CREATE PROCEDURE SaveBillDetails
    @BillDate DATETIME,
    @BillTime DATETIME,
    @VAT DECIMAL,
    @Discount DECIMAL,
    @SubTotal DECIMAL,
    @GrandTotal DECIMAL
AS
BEGIN
    INSERT INTO BillMasters (BillDate, BillTime, VAT, Discount, SubTotal, GrandTotal)
    VALUES (@BillDate, @BillTime, @VAT, @Discount, @SubTotal, @GrandTotal);
END
GO

CREATE PROCEDURE GetBillMasters
AS
BEGIN
    SELECT BillMasterId, BillDate, BillTime, Subtotal, Discount, VAT, GrandTotal
    FROM BillMasters;
END
GO

CREATE PROCEDURE GetBillMasterById
    @Id INT
AS
BEGIN
    SELECT BillMasterId, BillDate, BillTime, Subtotal, Discount, VAT, GrandTotal
    FROM BillMasters
    WHERE BillMasterId = @Id;
END
GO

CREATE PROCEDURE CreateBillMaster
    @BillDate DATETIME,
    @BillTime DATETIME,
    @VAT DECIMAL,
    @Discount DECIMAL,
    @SubTotal DECIMAL,
    @GrandTotal DECIMAL
AS
BEGIN
    INSERT INTO BillMasters (BillDate, BillTime, VAT, Discount, SubTotal, GrandTotal)
    VALUES (@BillDate, @BillTime, @VAT, @Discount, @SubTotal, @GrandTotal);

    DECLARE @InsertedId INT;
    SET @InsertedId = SCOPE_IDENTITY();

    SELECT BillMasterId, BillDate, BillTime, Subtotal, Discount, VAT, GrandTotal
    FROM BillMasters
    WHERE BillMasterId = @InsertedId;
END
GO

CREATE PROCEDURE UpdateBillMaster
    @Id INT,
    @BillDate DATETIME,
    @BillTime DATETIME,
    @VAT DECIMAL,
    @Discount DECIMAL,
    @SubTotal DECIMAL,
    @GrandTotal DECIMAL
AS
BEGIN
    UPDATE BillMasters
    SET BillDate = @BillDate,
        BillTime = @BillTime,
        VAT = @VAT,
        Discount = @Discount,
        SubTotal = @SubTotal,
        GrandTotal = @GrandTotal
    WHERE BillMasterId = @Id;
END
GO
