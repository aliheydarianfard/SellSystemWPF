
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/25/2020 11:13:48
-- Generated from EDMX file: F:\My Appliction\WindowsApp\Systemsell\Sales and warehouse system\ModelLayer\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [sell];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Coustomer_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Coustomers] DROP CONSTRAINT [FK_Coustomer_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_Product_User];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductPrice_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductPrices] DROP CONSTRAINT [FK_ProductPrice_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductPrice_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductPrices] DROP CONSTRAINT [FK_ProductPrice_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Inventory_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Inventories] DROP CONSTRAINT [FK_Inventory_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_Inventory_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Inventories] DROP CONSTRAINT [FK_Inventory_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_UserLog_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserLogs] DROP CONSTRAINT [FK_UserLog_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Invoice_Coustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Invoices] DROP CONSTRAINT [FK_Invoice_Coustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_Invoice_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Invoices] DROP CONSTRAINT [FK_Invoice_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoceItem_Invoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoceItems] DROP CONSTRAINT [FK_InvoceItem_Invoice];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoceItem_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InvoceItems] DROP CONSTRAINT [FK_InvoceItem_Product];
GO
IF OBJECT_ID(N'[dbo].[fk_UserAccess_SystemPartID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserAccesses] DROP CONSTRAINT [fk_UserAccess_SystemPartID];
GO
IF OBJECT_ID(N'[dbo].[fk_UserAccess_UserID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserAccesses] DROP CONSTRAINT [fk_UserAccess_UserID];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Coustomers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Coustomers];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[ProductPrices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductPrices];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[VW_Coustomer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VW_Coustomer];
GO
IF OBJECT_ID(N'[dbo].[VW_Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VW_Product];
GO
IF OBJECT_ID(N'[dbo].[VW_ProductPrice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VW_ProductPrice];
GO
IF OBJECT_ID(N'[dbo].[VW_Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VW_Users];
GO
IF OBJECT_ID(N'[dbo].[Inventories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Inventories];
GO
IF OBJECT_ID(N'[dbo].[VW_Inventory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VW_Inventory];
GO
IF OBJECT_ID(N'[dbo].[UserLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserLogs];
GO
IF OBJECT_ID(N'[dbo].[VW_UserLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VW_UserLog];
GO
IF OBJECT_ID(N'[dbo].[Invoices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Invoices];
GO
IF OBJECT_ID(N'[dbo].[InvoceItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InvoceItems];
GO
IF OBJECT_ID(N'[dbo].[VW_InvoiceItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VW_InvoiceItem];
GO
IF OBJECT_ID(N'[dbo].[VW_InvoiceShow]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VW_InvoiceShow];
GO
IF OBJECT_ID(N'[dbo].[SystemParts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SystemParts];
GO
IF OBJECT_ID(N'[dbo].[VW_SystemPart]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VW_SystemPart];
GO
IF OBJECT_ID(N'[dbo].[UserAccesses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAccesses];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Coustomers'
CREATE TABLE [dbo].[Coustomers] (
    [CoutomerID] int IDENTITY(1,1) NOT NULL,
    [CoustomerName] nvarchar(50)  NOT NULL,
    [CoustomerTell] nvarchar(50)  NULL,
    [CoustoemrAddres] nvarchar(max)  NULL,
    [SetDate] nvarchar(50)  NOT NULL,
    [UserID] int  NOT NULL,
    [UpdateDate] nvarchar(60)  NULL,
    [CoustomerActive] tinyint  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [ProductID] int IDENTITY(1,1) NOT NULL,
    [ProductName] nvarchar(100)  NOT NULL,
    [ProductDescription] nvarchar(max)  NULL,
    [ProdactLastPrice] bigint  NOT NULL,
    [ProductLastSuply] int  NOT NULL,
    [ProductImage] varbinary(max)  NULL,
    [ProductStartTime] nvarchar(50)  NOT NULL,
    [UserID] int  NOT NULL,
    [ProudactActive] tinyint  NULL,
    [ProudactLastPourchFee] bigint  NULL
);
GO

-- Creating table 'ProductPrices'
CREATE TABLE [dbo].[ProductPrices] (
    [ProductPriceID] int IDENTITY(1,1) NOT NULL,
    [ProductPricePurch] bigint  NOT NULL,
    [ProductPriceSell] bigint  NOT NULL,
    [ProductPriceDate] nvarchar(50)  NOT NULL,
    [ProductPricedesc] nvarchar(max)  NULL,
    [ProductID] int  NOT NULL,
    [UserID] int  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [UserFamily] nvarchar(50)  NULL,
    [UserTell] varchar(12)  NULL,
    [UserUserName] varchar(50)  NULL,
    [UserPassword] varchar(128)  NULL,
    [UserAge] tinyint  NULL,
    [UserGender] tinyint  NULL,
    [UserActive] tinyint  NULL,
    [UserStartTime] nvarchar(50)  NULL
);
GO

-- Creating table 'VW_Coustomer'
CREATE TABLE [dbo].[VW_Coustomer] (
    [CoutomerID] int  NOT NULL,
    [CoustomerName] nvarchar(50)  NOT NULL,
    [CoustomerTell] nvarchar(50)  NULL,
    [CoustoemrAddres] nvarchar(max)  NULL,
    [SetDate] nvarchar(50)  NOT NULL,
    [UserID] int  NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [UserFamily] nvarchar(50)  NULL,
    [FullName] nvarchar(101)  NULL,
    [UpdateDate] nvarchar(60)  NULL,
    [CoustomerActive] tinyint  NULL
);
GO

-- Creating table 'VW_Product'
CREATE TABLE [dbo].[VW_Product] (
    [ProductID] int  NOT NULL,
    [ProductName] nvarchar(100)  NOT NULL,
    [ProductDescription] nvarchar(max)  NULL,
    [ProdactLastPrice] bigint  NOT NULL,
    [ProductLastSuply] int  NOT NULL,
    [ProductImage] varbinary(max)  NULL,
    [ProductStartTime] nvarchar(50)  NOT NULL,
    [UserID] int  NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [UserFamily] nvarchar(50)  NULL,
    [FullName] nvarchar(101)  NULL,
    [ProudactActive] tinyint  NULL,
    [ProudactLastPourchFee] bigint  NULL
);
GO

-- Creating table 'VW_ProductPrice'
CREATE TABLE [dbo].[VW_ProductPrice] (
    [ProductPriceID] int  NOT NULL,
    [ProductPricePurch] bigint  NOT NULL,
    [ProductPriceSell] bigint  NOT NULL,
    [UserID] int  NOT NULL,
    [ProductID] int  NOT NULL,
    [ProductPricedesc] nvarchar(max)  NULL,
    [ProductPriceDate] nvarchar(50)  NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [UserFamily] nvarchar(50)  NULL,
    [ProductName] nvarchar(100)  NOT NULL,
    [FullName] nvarchar(101)  NULL
);
GO

-- Creating table 'VW_Users'
CREATE TABLE [dbo].[VW_Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [UserFamily] nvarchar(50)  NULL,
    [UserName] nvarchar(50)  NULL,
    [UserTell] varchar(12)  NULL,
    [UserUserName] varchar(50)  NULL,
    [UserPassword] varchar(128)  NULL,
    [UserAge] tinyint  NULL,
    [UserGender] tinyint  NULL,
    [UserStartTime] nvarchar(50)  NULL,
    [UserActive] tinyint  NULL,
    [UserGenderFarsi] nvarchar(4)  NULL,
    [UserActiveFarsi] nvarchar(8)  NULL,
    [FullName] nvarchar(101)  NULL
);
GO

-- Creating table 'Inventories'
CREATE TABLE [dbo].[Inventories] (
    [InventoryID] int IDENTITY(1,1) NOT NULL,
    [InventoryCount] int  NOT NULL,
    [InventoryDate] nvarchar(50)  NOT NULL,
    [InventoryDesc] nvarchar(max)  NULL,
    [UserID] int  NOT NULL,
    [ProductID] int  NOT NULL
);
GO

-- Creating table 'VW_Inventory'
CREATE TABLE [dbo].[VW_Inventory] (
    [InventoryID] int  NOT NULL,
    [InventoryCount] int  NOT NULL,
    [InventoryDate] nvarchar(50)  NOT NULL,
    [InventoryDesc] nvarchar(max)  NULL,
    [UserID] int  NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [UserFamily] nvarchar(50)  NULL,
    [ProductName] nvarchar(100)  NOT NULL,
    [FullName] nvarchar(101)  NULL,
    [Stutus] nvarchar(13)  NULL,
    [ProductID] int  NOT NULL
);
GO

-- Creating table 'UserLogs'
CREATE TABLE [dbo].[UserLogs] (
    [UserLogID] int IDENTITY(1,1) NOT NULL,
    [ComputerName] nvarchar(300)  NULL,
    [IPAddres] nvarchar(100)  NULL,
    [EnterDateTime] nvarchar(150)  NULL,
    [ExitDateTime] nvarchar(150)  NULL,
    [UserID] int  NULL
);
GO

-- Creating table 'VW_UserLog'
CREATE TABLE [dbo].[VW_UserLog] (
    [UserLogID] int  NOT NULL,
    [ComputerName] nvarchar(300)  NULL,
    [IPAddres] nvarchar(100)  NULL,
    [EnterDateTime] nvarchar(150)  NULL,
    [ExitDateTime] nvarchar(150)  NULL,
    [UserID] int  NULL,
    [UserName] nvarchar(50)  NULL,
    [UserFamily] nvarchar(50)  NULL,
    [FullName] nvarchar(101)  NULL
);
GO

-- Creating table 'Invoices'
CREATE TABLE [dbo].[Invoices] (
    [InvoiceId] int IDENTITY(1,1) NOT NULL,
    [InvoiceDate] nvarchar(60)  NULL,
    [InvoicePrice] bigint  NULL,
    [InvocieDesc] varchar(max)  NULL,
    [CoustoemrId] int  NOT NULL,
    [UserID] int  NOT NULL,
    [InvocePrice_Pourche] bigint  NULL,
    [InvoiceType] tinyint  NULL
);
GO

-- Creating table 'InvoceItems'
CREATE TABLE [dbo].[InvoceItems] (
    [InvoceItemID] int IDENTITY(1,1) NOT NULL,
    [InvoceItemCount] int  NOT NULL,
    [ProudactID] int  NOT NULL,
    [InvoceItemFeeSell] bigint  NOT NULL,
    [InvoceItemFeePurche] bigint  NOT NULL,
    [InvoiceID] int  NOT NULL
);
GO

-- Creating table 'VW_InvoiceItem'
CREATE TABLE [dbo].[VW_InvoiceItem] (
    [InvoceItemID] int  NOT NULL,
    [InvoceItemCount] int  NOT NULL,
    [InvoiceID] int  NOT NULL,
    [InvoceItemFeePurche] bigint  NOT NULL,
    [InvoceItemFeeSell] bigint  NOT NULL,
    [ProudactID] int  NOT NULL,
    [ProductName] nvarchar(100)  NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [UserFamily] nvarchar(50)  NULL,
    [FullName] nvarchar(101)  NULL
);
GO

-- Creating table 'VW_InvoiceShow'
CREATE TABLE [dbo].[VW_InvoiceShow] (
    [InvoiceDate] nvarchar(60)  NULL,
    [InvoicePrice] bigint  NULL,
    [InvocieDesc] varchar(max)  NULL,
    [InvoiceType] tinyint  NULL,
    [UserID] int  NOT NULL,
    [CoustoemrId] int  NOT NULL,
    [InvocePrice_Pourche] bigint  NULL,
    [InvoiceId] int  NOT NULL,
    [CoustomerName] nvarchar(50)  NOT NULL,
    [CoustomerTell] nvarchar(50)  NULL,
    [CoustoemrAddres] nvarchar(max)  NULL,
    [UserName] nvarchar(50)  NULL,
    [UserFamily] nvarchar(50)  NULL,
    [FullName] nvarchar(101)  NULL,
    [InvoceTypeFarsi] nvarchar(6)  NULL
);
GO

-- Creating table 'SystemParts'
CREATE TABLE [dbo].[SystemParts] (
    [SystemPartID] int IDENTITY(1,1) NOT NULL,
    [SystemPartName] nvarchar(200)  NULL,
    [SystemPartDetaile] varchar(max)  NULL,
    [SystemPartLevel] int  NULL
);
GO

-- Creating table 'VW_SystemPart'
CREATE TABLE [dbo].[VW_SystemPart] (
    [SystemPartID] int IDENTITY(1,1) NOT NULL,
    [SystemPartName] nvarchar(200)  NULL,
    [SystemPartDetaile] varchar(max)  NULL,
    [SystemPartLevel] int  NULL,
    [childecount] int  NULL
);
GO

-- Creating table 'UserAccesses'
CREATE TABLE [dbo].[UserAccesses] (
    [UserAccessID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NULL,
    [SystemPartID] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CoutomerID] in table 'Coustomers'
ALTER TABLE [dbo].[Coustomers]
ADD CONSTRAINT [PK_Coustomers]
    PRIMARY KEY CLUSTERED ([CoutomerID] ASC);
GO

-- Creating primary key on [ProductID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([ProductID] ASC);
GO

-- Creating primary key on [ProductPriceID] in table 'ProductPrices'
ALTER TABLE [dbo].[ProductPrices]
ADD CONSTRAINT [PK_ProductPrices]
    PRIMARY KEY CLUSTERED ([ProductPriceID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [CoutomerID], [CoustomerName], [SetDate], [UserID] in table 'VW_Coustomer'
ALTER TABLE [dbo].[VW_Coustomer]
ADD CONSTRAINT [PK_VW_Coustomer]
    PRIMARY KEY CLUSTERED ([CoutomerID], [CoustomerName], [SetDate], [UserID] ASC);
GO

-- Creating primary key on [ProductID], [ProductName], [ProdactLastPrice], [ProductLastSuply], [ProductStartTime], [UserID] in table 'VW_Product'
ALTER TABLE [dbo].[VW_Product]
ADD CONSTRAINT [PK_VW_Product]
    PRIMARY KEY CLUSTERED ([ProductID], [ProductName], [ProdactLastPrice], [ProductLastSuply], [ProductStartTime], [UserID] ASC);
GO

-- Creating primary key on [ProductPriceID], [ProductPricePurch], [ProductPriceSell], [UserID], [ProductID], [ProductPriceDate], [ProductName] in table 'VW_ProductPrice'
ALTER TABLE [dbo].[VW_ProductPrice]
ADD CONSTRAINT [PK_VW_ProductPrice]
    PRIMARY KEY CLUSTERED ([ProductPriceID], [ProductPricePurch], [ProductPriceSell], [UserID], [ProductID], [ProductPriceDate], [ProductName] ASC);
GO

-- Creating primary key on [UserID] in table 'VW_Users'
ALTER TABLE [dbo].[VW_Users]
ADD CONSTRAINT [PK_VW_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [InventoryID] in table 'Inventories'
ALTER TABLE [dbo].[Inventories]
ADD CONSTRAINT [PK_Inventories]
    PRIMARY KEY CLUSTERED ([InventoryID] ASC);
GO

-- Creating primary key on [InventoryID], [InventoryCount], [InventoryDate], [UserID], [ProductName], [ProductID] in table 'VW_Inventory'
ALTER TABLE [dbo].[VW_Inventory]
ADD CONSTRAINT [PK_VW_Inventory]
    PRIMARY KEY CLUSTERED ([InventoryID], [InventoryCount], [InventoryDate], [UserID], [ProductName], [ProductID] ASC);
GO

-- Creating primary key on [UserLogID] in table 'UserLogs'
ALTER TABLE [dbo].[UserLogs]
ADD CONSTRAINT [PK_UserLogs]
    PRIMARY KEY CLUSTERED ([UserLogID] ASC);
GO

-- Creating primary key on [UserLogID] in table 'VW_UserLog'
ALTER TABLE [dbo].[VW_UserLog]
ADD CONSTRAINT [PK_VW_UserLog]
    PRIMARY KEY CLUSTERED ([UserLogID] ASC);
GO

-- Creating primary key on [InvoiceId] in table 'Invoices'
ALTER TABLE [dbo].[Invoices]
ADD CONSTRAINT [PK_Invoices]
    PRIMARY KEY CLUSTERED ([InvoiceId] ASC);
GO

-- Creating primary key on [InvoceItemID] in table 'InvoceItems'
ALTER TABLE [dbo].[InvoceItems]
ADD CONSTRAINT [PK_InvoceItems]
    PRIMARY KEY CLUSTERED ([InvoceItemID] ASC);
GO

-- Creating primary key on [InvoceItemID], [InvoceItemCount], [InvoiceID], [InvoceItemFeePurche], [InvoceItemFeeSell], [ProudactID], [ProductName] in table 'VW_InvoiceItem'
ALTER TABLE [dbo].[VW_InvoiceItem]
ADD CONSTRAINT [PK_VW_InvoiceItem]
    PRIMARY KEY CLUSTERED ([InvoceItemID], [InvoceItemCount], [InvoiceID], [InvoceItemFeePurche], [InvoceItemFeeSell], [ProudactID], [ProductName] ASC);
GO

-- Creating primary key on [UserID], [CoustoemrId], [InvoiceId], [CoustomerName] in table 'VW_InvoiceShow'
ALTER TABLE [dbo].[VW_InvoiceShow]
ADD CONSTRAINT [PK_VW_InvoiceShow]
    PRIMARY KEY CLUSTERED ([UserID], [CoustoemrId], [InvoiceId], [CoustomerName] ASC);
GO

-- Creating primary key on [SystemPartID] in table 'SystemParts'
ALTER TABLE [dbo].[SystemParts]
ADD CONSTRAINT [PK_SystemParts]
    PRIMARY KEY CLUSTERED ([SystemPartID] ASC);
GO

-- Creating primary key on [SystemPartID] in table 'VW_SystemPart'
ALTER TABLE [dbo].[VW_SystemPart]
ADD CONSTRAINT [PK_VW_SystemPart]
    PRIMARY KEY CLUSTERED ([SystemPartID] ASC);
GO

-- Creating primary key on [UserAccessID] in table 'UserAccesses'
ALTER TABLE [dbo].[UserAccesses]
ADD CONSTRAINT [PK_UserAccesses]
    PRIMARY KEY CLUSTERED ([UserAccessID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserID] in table 'Coustomers'
ALTER TABLE [dbo].[Coustomers]
ADD CONSTRAINT [FK_Coustomer_Users]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Coustomer_Users'
CREATE INDEX [IX_FK_Coustomer_Users]
ON [dbo].[Coustomers]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Product_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_User'
CREATE INDEX [IX_FK_Product_User]
ON [dbo].[Products]
    ([UserID]);
GO

-- Creating foreign key on [ProductID] in table 'ProductPrices'
ALTER TABLE [dbo].[ProductPrices]
ADD CONSTRAINT [FK_ProductPrice_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductPrice_Product'
CREATE INDEX [IX_FK_ProductPrice_Product]
ON [dbo].[ProductPrices]
    ([ProductID]);
GO

-- Creating foreign key on [UserID] in table 'ProductPrices'
ALTER TABLE [dbo].[ProductPrices]
ADD CONSTRAINT [FK_ProductPrice_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductPrice_User'
CREATE INDEX [IX_FK_ProductPrice_User]
ON [dbo].[ProductPrices]
    ([UserID]);
GO

-- Creating foreign key on [ProductID] in table 'Inventories'
ALTER TABLE [dbo].[Inventories]
ADD CONSTRAINT [FK_Inventory_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Inventory_Product'
CREATE INDEX [IX_FK_Inventory_Product]
ON [dbo].[Inventories]
    ([ProductID]);
GO

-- Creating foreign key on [UserID] in table 'Inventories'
ALTER TABLE [dbo].[Inventories]
ADD CONSTRAINT [FK_Inventory_Users]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Inventory_Users'
CREATE INDEX [IX_FK_Inventory_Users]
ON [dbo].[Inventories]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'UserLogs'
ALTER TABLE [dbo].[UserLogs]
ADD CONSTRAINT [FK_UserLog_Users]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLog_Users'
CREATE INDEX [IX_FK_UserLog_Users]
ON [dbo].[UserLogs]
    ([UserID]);
GO

-- Creating foreign key on [CoustoemrId] in table 'Invoices'
ALTER TABLE [dbo].[Invoices]
ADD CONSTRAINT [FK_Invoice_Coustomer]
    FOREIGN KEY ([CoustoemrId])
    REFERENCES [dbo].[Coustomers]
        ([CoutomerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Invoice_Coustomer'
CREATE INDEX [IX_FK_Invoice_Coustomer]
ON [dbo].[Invoices]
    ([CoustoemrId]);
GO

-- Creating foreign key on [UserID] in table 'Invoices'
ALTER TABLE [dbo].[Invoices]
ADD CONSTRAINT [FK_Invoice_Users]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Invoice_Users'
CREATE INDEX [IX_FK_Invoice_Users]
ON [dbo].[Invoices]
    ([UserID]);
GO

-- Creating foreign key on [InvoiceID] in table 'InvoceItems'
ALTER TABLE [dbo].[InvoceItems]
ADD CONSTRAINT [FK_InvoceItem_Invoice]
    FOREIGN KEY ([InvoiceID])
    REFERENCES [dbo].[Invoices]
        ([InvoiceId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoceItem_Invoice'
CREATE INDEX [IX_FK_InvoceItem_Invoice]
ON [dbo].[InvoceItems]
    ([InvoiceID]);
GO

-- Creating foreign key on [ProudactID] in table 'InvoceItems'
ALTER TABLE [dbo].[InvoceItems]
ADD CONSTRAINT [FK_InvoceItem_Product]
    FOREIGN KEY ([ProudactID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoceItem_Product'
CREATE INDEX [IX_FK_InvoceItem_Product]
ON [dbo].[InvoceItems]
    ([ProudactID]);
GO

-- Creating foreign key on [SystemPartID] in table 'UserAccesses'
ALTER TABLE [dbo].[UserAccesses]
ADD CONSTRAINT [fk_UserAccess_SystemPartID]
    FOREIGN KEY ([SystemPartID])
    REFERENCES [dbo].[SystemParts]
        ([SystemPartID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_UserAccess_SystemPartID'
CREATE INDEX [IX_fk_UserAccess_SystemPartID]
ON [dbo].[UserAccesses]
    ([SystemPartID]);
GO

-- Creating foreign key on [UserID] in table 'UserAccesses'
ALTER TABLE [dbo].[UserAccesses]
ADD CONSTRAINT [fk_UserAccess_UserID]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_UserAccess_UserID'
CREATE INDEX [IX_fk_UserAccess_UserID]
ON [dbo].[UserAccesses]
    ([UserID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------