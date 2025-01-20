USE [StoreSample]
GO

CREATE TYPE [Sales].[OrderDetailsType] AS TABLE(
	[productid] [int] NOT NULL,
	[unitprice] [money] NOT NULL,
	[qty] [smallint] NOT NULL,
	[discount] [numeric](4, 3) NOT NULL
)
GO