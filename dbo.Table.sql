CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [client_email] NCHAR(50) NOT NULL, 
	[client_name] NCHAR(100) NOT NULL,
    [order_type] NCHAR(10) NOT NULL, 
    [quantity] INT NOT NULL, 
    [company] NCHAR(100) NOT NULL, 
    [order_time] TIMESTAMP NOT NULL, 
    [order_execution_time] TIMESTAMP NULL, 
    [executed] BIT NOT NULL, 
    [quotation] DECIMAL(10, 2) NULL
    
)
