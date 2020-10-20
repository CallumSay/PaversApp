CREATE TABLE [dbo].[Inventory] (
    [SKU]         INT          IDENTITY (111111, 1) NOT NULL,
    [Colour]      VARCHAR (50) NOT NULL,
    [Sex]         VARCHAR (50) NOT NULL,
    [Size]        VARCHAR (50) NOT NULL,
    [Material]    VARCHAR (50) NOT NULL,
    [Description] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([SKU] ASC)
);

