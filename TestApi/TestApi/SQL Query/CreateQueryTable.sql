
DROP TABLE IF EXISTS PurchaseOrderDetail;
DROP TABLE IF EXISTS PurchaseOrder;
DROP TABLE IF EXISTS ProductInformation;
DROP TABLE IF EXISTS Users;

-- Create Users table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

-- Insert data into Users table
INSERT INTO Users (Username, Email, Password) VALUES ('user1', 'user1@example.com', 'password1');
INSERT INTO Users (Username, Email, Password) VALUES ('user2', 'user2@example.com', 'password2');
INSERT INTO Users (Username, Email, Password) VALUES ('user3', 'user3@example.com', 'password3');
INSERT INTO Users (Username, Email, Password) VALUES ('user4', 'user4@example.com', 'password4');
INSERT INTO Users (Username, Email, Password) VALUES ('user5', 'user5@example.com', 'password5');
INSERT INTO Users (Username, Email, Password) VALUES ('user6', 'user6@example.com', 'password6');
INSERT INTO Users (Username, Email, Password) VALUES ('user7', 'user7@example.com', 'password7');
INSERT INTO Users (Username, Email, Password) VALUES ('user8', 'user8@example.com', 'password8');
INSERT INTO Users (Username, Email, Password) VALUES ('user9', 'user9@example.com', 'password9');
INSERT INTO Users (Username, Email, Password) VALUES ('user10', 'user10@example.com', 'password10');


-- Create ProductInformation table
CREATE TABLE ProductInformation (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(50) NOT NULL UNIQUE,
    Quantity INT,
    Price DECIMAL(18, 2) NOT NULL
);

-- Insert data into ProductInformation table
INSERT INTO ProductInformation (ProductName, Quantity, Price) VALUES ('Sofa', 10, 500.00);
INSERT INTO ProductInformation (ProductName, Quantity, Price) VALUES ('Dining Table', 5, 700.00);
INSERT INTO ProductInformation (ProductName, Quantity, Price) VALUES ('Bed Frame', 15, 400.00);
INSERT INTO ProductInformation (ProductName, Quantity, Price) VALUES ('Coffee Table', 20, 150.00);
INSERT INTO ProductInformation (ProductName, Quantity, Price) VALUES ('Bookshelf', 7, 200.00);
INSERT INTO ProductInformation (ProductName, Quantity, Price) VALUES ('Armchair', 12, 250.00);
INSERT INTO ProductInformation (ProductName, Quantity, Price) VALUES ('TV Stand', 8, 300.00);
INSERT INTO ProductInformation (ProductName, Quantity, Price) VALUES ('Wardrobe', 6, 800.00);
INSERT INTO ProductInformation (ProductName, Quantity, Price) VALUES ('Desk', 10, 350.00);
INSERT INTO ProductInformation (ProductName, Quantity, Price) VALUES ('Nightstand', 25, 100.00);


-- Create PurchaseOrder table
CREATE TABLE PurchaseOrder(
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Description NVARCHAR (100),
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users (UserId)
);

-- Insert data into PurchaseOrder table
INSERT INTO PurchaseOrder (UserID, Description, OrderDate) VALUES (1, 'Sample order 1', '2024-01-01');
INSERT INTO PurchaseOrder (UserID, Description, OrderDate) VALUES (2, 'Sample order 2', '2024-02-01' );
INSERT INTO PurchaseOrder (UserID, Description, OrderDate) VALUES (3, 'Sample order 3', '2024-03-01' );
INSERT INTO PurchaseOrder (UserID, Description, OrderDate) VALUES (4, 'Sample order 4', '2024-04-01' );
INSERT INTO PurchaseOrder (UserID, Description, OrderDate) VALUES (5, 'Sample order 5', '2024-05-01' );
INSERT INTO PurchaseOrder (UserID, Description, OrderDate) VALUES (6, 'Sample order 6', '2024-06-01' );
INSERT INTO PurchaseOrder (UserID, Description, OrderDate) VALUES (7, 'Sample order 7', '2024-07-01' );
INSERT INTO PurchaseOrder (UserID, Description, OrderDate) VALUES (8, 'Sample order 8', '2024-08-01');
INSERT INTO PurchaseOrder (UserID, Description, OrderDate) VALUES (9, 'Sample order 9', '2024-09-01');
INSERT INTO PurchaseOrder (UserID, Description, OrderDate) VALUES (10, 'Sample order 10', '2024-10-01');



-- Create PurchaseOrderDetail table
CREATE TABLE PurchaseOrderDetail (
    ID INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES PurchaseOrder(OrderId),
    FOREIGN KEY (ProductId) REFERENCES ProductInformation(ProductId)
);

-- Insert data into PurchaseOrderDetail table
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (1, 1, 1);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (1, 2, 10);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (2, 3, 1);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (2, 4, 2);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (3, 5, 1);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (3, 6, 2);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (4, 7, 1);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (4, 8, 1);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (5, 9, 2);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (5, 10, 1);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (6, 1, 10);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (6, 2, 1);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (7, 3, 4);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (7, 4, 2);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (8, 5, 5);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (8, 6, 1);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (9, 7, 2);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (9, 8, 3);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (10, 9, 1);
INSERT INTO PurchaseOrderDetail (OrderId, ProductId, Quantity) VALUES (10, 10, 4);