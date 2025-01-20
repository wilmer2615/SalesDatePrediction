USE [StoreSample]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Sales].[InsertOrder]
    @custid INT,
    @empid INT,
    @orderdate DATETIME,
    @requireddate DATETIME,
    @shippeddate DATETIME = NULL,
    @shipperid INT,
    @freight MONEY,
    @shipname NVARCHAR(40),
    @shipaddress NVARCHAR(60),
    @shipcity NVARCHAR(15),
    @shipregion NVARCHAR(15) = NULL,
    @shippostalcode NVARCHAR(10) = NULL,
    @shipcountry NVARCHAR(15),
    @OrderDetails [Sales].[OrderDetailsType] READONLY,
    @OrderId INT OUTPUT
AS
BEGIN
    SET XACT_ABORT ON;
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insertar en Sales.Orders
        INSERT INTO [Sales].[Orders] (
            [custid], [empid], [orderdate], [requireddate], [shippeddate],
            [shipperid], [freight], [shipname], [shipaddress],
            [shipcity], [shipregion], [shippostalcode], [shipcountry]
        )
        VALUES (
            @custid, @empid, @orderdate, @requireddate, @shippeddate,
            @shipperid, @freight, @shipname, @shipaddress,
            @shipcity, @shipregion, @shippostalcode, @shipcountry
        );

        -- Obtener el ID de la orden recién insertada
        SET @OrderId = SCOPE_IDENTITY();

        -- Insertar en Sales.OrderDetails
        INSERT INTO [Sales].[OrderDetails] (
            [orderid], [productid], [unitprice], [qty], [discount]
        )
        SELECT 
            @OrderId, [productid], [unitprice], [qty], [discount]
        FROM @OrderDetails;

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO