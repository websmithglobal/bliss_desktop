USE [BLISS]
GO
/****** Object:  StoredProcedure [dbo].[ClearDatabase]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ClearDatabase] 
AS
BEGIN
	Truncate Table TimeSheetWiseDiscount
	Truncate Table ModifierCategoryDetail
	Truncate Table ModifierDetail
	Truncate Table IngredientsMasterDetail
	Truncate Table ShiftMasterDetail
	Truncate Table ModuleMasterDetail
	Truncate Table FeatureDetail
	Truncate Table SubFeatureDetail
	Truncate Table RightDetail
	Truncate Table TableMasterDetail
	Truncate Table IngredientUnitTypeDetail
	Truncate Table RecipeMasterData
	Truncate Table DiscountMasterDetail
	Truncate Table TaxGroupDetail
	Truncate Table PaymentGatewayMaster
	Truncate Table CategoryMaster
	Truncate Table CustomerMasterData
	Truncate Table EmployeeMasterList
	Truncate Table LoginResponse
	Truncate Table VendorMasterData
	Truncate Table BranchMasterSetting
	Truncate Table BranchSettingDetail
	Truncate Table EmployeeShift
	Truncate Table CategoryWiseProduct
	Truncate Table ComboDetail
	Truncate Table ComboProductDetail
	Truncate Table RootObject
	Truncate Table VersionDetail
	Truncate Table ShiftMaster
	Truncate Table RoleMasterDetail
	Truncate Table ModuleAppIDDetail
	Truncate Table ProductClassMasterDetail
	Truncate Table RecipeMasterDetail
END

GO
/****** Object:  StoredProcedure [dbo].[GetBranchMasterSetting]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetBranchMasterSetting]
	@BranchID uniqueidentifier = null,
	@Mode nvarchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    [BranchMasterSetting] 
	ORDER BY BranchName;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM [BranchMasterSetting]
	WHERE BranchID = @BranchID;
END

IF(@Mode = 'GetBranchMasterAndSettingByID')
BEGIN
	SELECT BranchMasterSetting.BranchID, BranchMasterSetting.RestaurantID, BranchMasterSetting.ContactNoForService, BranchMasterSetting.DeliveryCharges, BranchMasterSetting.DeliveryTime, BranchMasterSetting.PickupTime, 
		BranchMasterSetting.CurrencyName, BranchMasterSetting.CurrencySymbol, BranchMasterSetting.WorkingDays, BranchMasterSetting.TagLine, BranchMasterSetting.Footer, BranchMasterSetting.DeliveryAreaRedius, 
		BranchMasterSetting.DeliveryAreaTitle, BranchMasterSetting.DistanceType, BranchMasterSetting.DistanceName, BranchMasterSetting.FreeDeliveryUpto, BranchMasterSetting.BranchName, 
		BranchMasterSetting.BranchEmailID, BranchMasterSetting.MobileNo, BranchMasterSetting.LastSyncDate, BranchMasterSetting.VatNo, BranchMasterSetting.CSTNo, BranchMasterSetting.ServiceTaxNo, 
		BranchMasterSetting.TinGSTNo, BranchMasterSetting.Address, BranchMasterSetting.SubAreaStreet, BranchMasterSetting.PinCode, BranchMasterSetting.VersionCode, BranchMasterSetting.BranchMasterSetting_Id, 
		BranchSettingDetail.IsFranchise, BranchSettingDetail.IsReservationOn, BranchSettingDetail.IsOrderBookingOn, BranchSettingDetail.IsAutoAcceptOrderOn, BranchSettingDetail.IsAutoRoundOffTotalOn, 
		BranchSettingDetail.TaxGroupId, BranchSettingDetail.IsDemoVersion, BranchSettingDetail.ExpireDate
	FROM BranchMasterSetting INNER JOIN
		BranchSettingDetail ON BranchMasterSetting.BranchID = BranchSettingDetail.BranchID
	WHERE BranchMasterSetting.BranchID = @BranchID;
END


GO
/****** Object:  StoredProcedure [dbo].[GetBranchSettingDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetBranchSettingDetail]
	@BranchID uniqueidentifier = null,
	@Mode nvarchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    [BranchSettingDetail] 
	ORDER BY BranchID;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM [BranchSettingDetail]
	WHERE BranchID = @BranchID;
END

IF(@Mode = 'GetBranchMasterAndSettingByID')
BEGIN
	SELECT BranchMasterSetting.BranchID, BranchMasterSetting.RestaurantID, BranchMasterSetting.ContactNoForService, BranchMasterSetting.DeliveryCharges, BranchMasterSetting.DeliveryTime, BranchMasterSetting.PickupTime, 
		BranchMasterSetting.CurrencyName, BranchMasterSetting.CurrencySymbol, BranchMasterSetting.WorkingDays, BranchMasterSetting.TagLine, BranchMasterSetting.Footer, BranchMasterSetting.DeliveryAreaRedius, 
		BranchMasterSetting.DeliveryAreaTitle, BranchMasterSetting.DistanceType, BranchMasterSetting.DistanceName, BranchMasterSetting.FreeDeliveryUpto, BranchMasterSetting.BranchName, 
		BranchMasterSetting.BranchEmailID, BranchMasterSetting.MobileNo, BranchMasterSetting.LastSyncDate, BranchMasterSetting.VatNo, BranchMasterSetting.CSTNo, BranchMasterSetting.ServiceTaxNo, 
		BranchMasterSetting.TinGSTNo, BranchMasterSetting.Address, BranchMasterSetting.SubAreaStreet, BranchMasterSetting.PinCode, BranchMasterSetting.VersionCode, BranchMasterSetting.BranchMasterSetting_Id, 
		BranchSettingDetail.IsFranchise, BranchSettingDetail.IsReservationOn, BranchSettingDetail.IsOrderBookingOn, BranchSettingDetail.IsAutoAcceptOrderOn, BranchSettingDetail.IsAutoRoundOffTotalOn, 
		BranchSettingDetail.TaxGroupId, BranchSettingDetail.IsDemoVersion, BranchSettingDetail.ExpireDate
	FROM BranchMasterSetting INNER JOIN
		BranchSettingDetail ON BranchMasterSetting.BranchID = BranchSettingDetail.BranchID
	WHERE BranchMasterSetting.BranchID = @BranchID;
END



GO
/****** Object:  StoredProcedure [dbo].[GetCategoryDetails]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetCategoryDetails]
	@CategoryID uniqueidentifier = null,
	@Mode nvarchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    [CategoryDetails] 
	ORDER BY CategoryName;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM [CategoryDetails]
	WHERE CategoryID = @CategoryID;
END




GO
/****** Object:  StoredProcedure [dbo].[GetCategoryMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetCategoryMaster]
	@CategoryID uniqueidentifier = null,
	@ParentID uniqueidentifier = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    [CategoryMaster] 
	ORDER BY CategoryName;
END

IF(@Mode = 'GetRecordByCategoryID')
BEGIN
	SELECT * 
	FROM [CategoryMaster]
	WHERE CategoryID = @CategoryID;
END

IF(@Mode = 'GetRecordByParentID')
BEGIN
	SELECT a.CategoryID, a.CategoryName, ISNULL(Deriv1.Count,0) As Count, a.ImgPath, a.IsCategory, a.Price
	FROM ViewCategoryWiseProduct a LEFT OUTER JOIN 
	(SELECT ParentID, COUNT(*) AS Count FROM ViewCategoryWiseProduct GROUP BY ParentID) 
	Deriv1 ON a.CategoryID = Deriv1.ParentID WHERE a.ParentID = @ParentID;
END

IF(@Mode = 'GetRecordByCategoryID')
BEGIN
	select * from viewCategorywiseproduct 
	WHERE CategoryID = @CategoryID;
END

IF(@Mode = 'GetAddCategoryProduct')
BEGIN
	SELECT a.CategoryID, a.CategoryName, ISNULL(Deriv1.Count,0) As Count, a.ImgPath, a.IsCategory, a.Price
	FROM ViewCategoryWiseProduct a LEFT OUTER JOIN 
	(SELECT ParentID, COUNT(*) AS Count FROM ViewCategoryWiseProduct GROUP BY ParentID) 
	Deriv1 ON a.CategoryID = Deriv1.ParentID WHERE a.ParentID = @ParentID
	UNION 
	select '11111111-1111-1111-1111-111111111111','Add Category', 0, '', 3,0.00
	UNION 
	select '22222222-2222-2222-2222-222222222222','Add Product', 0, '', 4,0.00
	order by IsCategory
END

IF(@Mode = 'GetCategoryByParentID')
BEGIN
	SELECT TOP 1 * 
	FROM [CategoryMaster]
	WHERE ParentID = @ParentID;
END



GO
/****** Object:  StoredProcedure [dbo].[GetCategoryWiseProduct]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetCategoryWiseProduct]
	@ProductID uniqueidentifier = null,
	@CategoryID uniqueidentifier = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  * FROM    [CategoryWiseProduct] 
	ORDER BY ProductName;
END

IF(@Mode = 'GetRecordByProductID')  -- used
BEGIN
	SELECT * FROM [CategoryWiseProduct]
	WHERE ProductID = @ProductID;
END

IF(@Mode = 'GetRecordByCategoryWise')
BEGIN
	SELECT * FROM [CategoryWiseProduct]
	WHERE CategoryID = @CategoryID;
END

IF(@Mode = 'GetForItemChefMapping')
BEGIN
	SELECT	CategoryWiseProduct.ProductID, CategoryWiseProduct.CategoryID, CategoryWiseProduct.ProductName, CategoryMaster.CategoryName
	FROM	CategoryWiseProduct INNER JOIN
				CategoryMaster ON CategoryWiseProduct.CategoryID = CategoryMaster.CategoryID
	ORDER BY CategoryName,ProductName
END




GO
/****** Object:  StoredProcedure [dbo].[GetChefKDSMapping]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetChefKDSMapping]
	@ChefKDSMappingID uniqueidentifier = null,
	@EmployeeID uniqueidentifier = null,
	@DeviceID uniqueidentifier = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAll')
BEGIN
	SELECT			ChefKDSMapping.ChefKDSMappingID, ChefKDSMapping.EmployeeID, EmployeeMasterList.EmpName, ChefKDSMapping.DeviceID, DeviceMaster.DeviceName
	FROM			ChefKDSMapping INNER JOIN
						EmployeeMasterList ON ChefKDSMapping.EmployeeID = EmployeeMasterList.EmployeeID INNER JOIN
						DeviceMaster ON ChefKDSMapping.DeviceID = DeviceMaster.DeviceID
END

IF(@Mode = 'GetByID')
BEGIN
	SELECT			ChefKDSMapping.ChefKDSMappingID, ChefKDSMapping.EmployeeID, EmployeeMasterList.EmpName, ChefKDSMapping.DeviceID, DeviceMaster.DeviceName
	FROM			ChefKDSMapping INNER JOIN
						EmployeeMasterList ON ChefKDSMapping.EmployeeID = EmployeeMasterList.EmployeeID INNER JOIN
						DeviceMaster ON ChefKDSMapping.DeviceID = DeviceMaster.DeviceID
	WHERE			ChefKDSMapping.ChefKDSMappingID = @ChefKDSMappingID
END

IF(@Mode = 'GetByEmployeeID')
BEGIN
	SELECT			ChefKDSMapping.ChefKDSMappingID, ChefKDSMapping.EmployeeID, EmployeeMasterList.EmpName, ChefKDSMapping.DeviceID, DeviceMaster.DeviceName
	FROM			ChefKDSMapping INNER JOIN
						EmployeeMasterList ON ChefKDSMapping.EmployeeID = EmployeeMasterList.EmployeeID INNER JOIN
						DeviceMaster ON ChefKDSMapping.DeviceID = DeviceMaster.DeviceID
	WHERE			ChefKDSMapping.EmployeeID = @EmployeeID
END

IF(@Mode = 'GetByDeviceID')
BEGIN
	SELECT			ChefKDSMapping.ChefKDSMappingID, ChefKDSMapping.EmployeeID, EmployeeMasterList.EmpName, ChefKDSMapping.DeviceID, DeviceMaster.DeviceName
	FROM			ChefKDSMapping INNER JOIN
						EmployeeMasterList ON ChefKDSMapping.EmployeeID = EmployeeMasterList.EmployeeID INNER JOIN
						DeviceMaster ON ChefKDSMapping.DeviceID = DeviceMaster.DeviceID
	WHERE			ChefKDSMapping.DeviceID = @DeviceID
END




GO
/****** Object:  StoredProcedure [dbo].[GetComboDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetComboDetail]
	@ComboSetID uniqueidentifier = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    [ComboDetail] 
	ORDER BY ComboSetName;
END

IF(@Mode = 'GetRecordByComboSetID')
BEGIN
	SELECT * 
	FROM [ComboDetail]
	WHERE ComboSetID = @ComboSetID;
END




GO
/****** Object:  StoredProcedure [dbo].[GetComboProductDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetComboProductDetail]
	@ProductID nvarchar(50) = null,
	@Mode nvarchar(50) = null
AS

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    [ComboProductDetail] 
	ORDER BY ProductName;
END

IF(@Mode = 'GetRecordByProductID')
BEGIN
	SELECT * 
	FROM [ComboProductDetail]
	WHERE ProductID = @ProductID;
END




GO
/****** Object:  StoredProcedure [dbo].[GetCustomerMasterData]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetCustomerMasterData]
	@CustomerID uniqueidentifier = null,
	@Mode nvarchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	--Select * From CustomerMaster
	SELECT  *
	FROM    [CustomerMasterData] 
	ORDER BY Name;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM [CustomerMasterData]
	WHERE CustomerID = @CustomerID;
END



GO
/****** Object:  StoredProcedure [dbo].[GetDeviceMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetDeviceMaster]
	@DeviceID uniqueidentifier = null,
	@DeviceTypeID int = null,
	@DeviceStatus varchar(50) = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAll')
BEGIN
	SELECT	DeviceMaster.DeviceID, DeviceMaster.DeviceName, DeviceMaster.DeviceIP, DeviceMaster.DeviceTypeID, DeviceMaster.DeviceStatus, 
		CASE WHEN DeviceMaster.DeviceTypeID=1 THEN 'POS' 
		WHEN DeviceMaster.DeviceTypeID=2 THEN 'KDS'
		WHEN DeviceMaster.DeviceTypeID=3 THEN 'PRINTER' END AS DeviceType
	FROM	DeviceMaster 
	ORDER BY DeviceName;
END

IF(@Mode = 'GetByID')
BEGIN
	SELECT	DeviceMaster.DeviceID, DeviceMaster.DeviceName, DeviceMaster.DeviceIP, DeviceMaster.DeviceTypeID, DeviceMaster.DeviceStatus, 
		CASE WHEN DeviceMaster.DeviceTypeID=1 THEN 'POS' 
		WHEN DeviceMaster.DeviceTypeID=2 THEN 'KDS'
		WHEN DeviceMaster.DeviceTypeID=3 THEN 'PRINTER' END AS DeviceType
	FROM	DeviceMaster 
	WHERE DeviceMaster.DeviceID = @DeviceID;
END

IF(@Mode = 'GetByStatus')
BEGIN
	SELECT	DeviceMaster.DeviceID, DeviceMaster.DeviceName, DeviceMaster.DeviceIP, DeviceMaster.DeviceTypeID, DeviceMaster.DeviceStatus, 
		CASE WHEN DeviceMaster.DeviceTypeID=1 THEN 'POS' 
		WHEN DeviceMaster.DeviceTypeID=2 THEN 'KDS'
		WHEN DeviceMaster.DeviceTypeID=3 THEN 'PRINTER' END AS DeviceType
	FROM	DeviceMaster 
	WHERE DeviceMaster.DeviceStatus = @DeviceStatus
	ORDER BY DeviceName;
END

IF(@Mode = 'GetByTypeID')
BEGIN
	SELECT	DeviceMaster.DeviceID, DeviceMaster.DeviceName, DeviceMaster.DeviceIP, DeviceMaster.DeviceTypeID, DeviceMaster.DeviceStatus, 
		CASE WHEN DeviceMaster.DeviceTypeID=1 THEN 'POS' 
		WHEN DeviceMaster.DeviceTypeID=2 THEN 'KDS'
		WHEN DeviceMaster.DeviceTypeID=3 THEN 'PRINTER' END AS DeviceType
	FROM	DeviceMaster 
	WHERE DeviceMaster.DeviceTypeID = @DeviceTypeID
	ORDER BY DeviceName;
END



GO
/****** Object:  StoredProcedure [dbo].[GetDeviceTypeMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetDeviceTypeMaster]
	@DeviceTypeID uniqueidentifier = null,
	@DeviceStatus varchar(50) = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAll')
BEGIN
	SELECT  *
	FROM    [DeviceTypeMaster] 
	ORDER BY DeviceType;
END

IF(@Mode = 'GetByID')
BEGIN
	SELECT * 
	FROM [DeviceTypeMaster]
	WHERE DeviceTypeID = @DeviceTypeID;
END

IF(@Mode = 'GetByStatus')
BEGIN
	SELECT * 
	FROM [DeviceTypeMaster]
	WHERE DeviceStatus = @DeviceStatus
	ORDER BY DeviceType;
END



GO
/****** Object:  StoredProcedure [dbo].[GetDiscountMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetDiscountMasterDetail]
	@DiscountID uniqueidentifier = null,
	@Mode varchar(50) = null
AS

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    [DiscountMasterDetail] 
	ORDER BY DiscountName;
END

IF(@Mode = 'GetRecordByDiscountID')
BEGIN
	SELECT * 
	FROM [DiscountMasterDetail]
	WHERE DiscountID = @DiscountID;
END

IF(@Mode = 'GetListForSelectDiscount')
BEGIN
	SELECT  DiscountID, DiscountName, DiscountType, CASE WHEN DiscountType=1 THEN 'Amount' WHEN DiscountType=2 THEN 'Per(%)' ELSE 'Other' END AS DiscountTypeName, AmountOrPercentage, AutoApply
	FROM    [DiscountMasterDetail] 
	ORDER BY DiscountName;
END




GO
/****** Object:  StoredProcedure [dbo].[GetEmployeeMasterList]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetEmployeeMasterList]
	@EmployeeID uniqueidentifier = null,
	@EmpCode varchar(50) = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  * FROM    [EmployeeMasterList] 
	ORDER BY EmpName;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * FROM [EmployeeMasterList]
	WHERE EmployeeID = @EmployeeID;
END

IF(@Mode = 'GetRecordByEmpCode')
BEGIN
	SELECT * FROM [EmployeeMasterList]
	WHERE EmpCode = @EmpCode;
END

IF(@Mode = 'GetByIsDisplayInKDS')
BEGIN
	SELECT [EmployeeID],[EmpName]
	FROM [BLISS].[dbo].[EmployeeMasterList]
	WHERE [IsDisplayInKDS]=1
END

IF(@Mode = 'GetStaffList')
BEGIN
	SELECT [EmployeeID],[EmpCode],[EmpName],[Mobile],[Email],[RoleName],[SalaryAmt],
	CASE WHEN [SalaryType]=1 THEN 'Monthly' WHEN [SalaryType]=2 THEN 'Weekly' WHEN [SalaryType]=3 THEN 'Hourly' END AS SalaryTypeName,
	[Address],[JoinDate],[IsDisplayInKDS],
	CASE WHEN [Gender]=1 THEN 'Male' WHEN [Gender]=2 THEN 'Female' WHEN [Gender]=3 THEN 'Other' END AS GenderName,
	[TotalHourInADay]
	FROM [dbo].[EmployeeMasterList]
	ORDER BY [EmpName];
END



GO
/****** Object:  StoredProcedure [dbo].[GetEmployeeShift]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetEmployeeShift]
	@EmployeeID nvarchar(50) = null,
	@Mode nvarchar(50) = null
AS

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    [EmployeeShift] 
	ORDER BY EmployeeID;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM [EmployeeShift]
	WHERE EmployeeID = @EmployeeID;
END




GO
/****** Object:  StoredProcedure [dbo].[GetFeatureDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetFeatureDetail]
	@FeatureDetail_Id int = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    FeatureDetail 
	ORDER BY FeatureName;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM FeatureDetail
	WHERE FeatureDetail_Id = @FeatureDetail_Id;
END




GO
/****** Object:  StoredProcedure [dbo].[GetGeneralSetting]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetGeneralSetting]
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAll')
BEGIN
	SELECT	*
	FROM		GeneralSetting
END


GO
/****** Object:  StoredProcedure [dbo].[GetIngredientsMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetIngredientsMasterDetail]
	@IngredientsID nvarchar(50) = null,
	@Mode nvarchar(50) = null
AS

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    IngredientsMasterDetail 
	ORDER BY IngredientName;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM IngredientsMasterDetail
	WHERE IngredientsID = @IngredientsID;
END




GO
/****** Object:  StoredProcedure [dbo].[GetIngredientUnitTypeDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetIngredientUnitTypeDetail]
	@UnitTypeID uniqueidentifier = null,
	@Mode nvarchar(50) = null
AS

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    IngredientUnitTypeDetail 
	ORDER BY UnitType;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM IngredientUnitTypeDetail
	WHERE UnitTypeID = @UnitTypeID;
END




GO
/****** Object:  StoredProcedure [dbo].[GetItemChefMapping]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetItemChefMapping]
	@ItemChefMappingID uniqueidentifier = null,
	@CategoryID uniqueidentifier = null,
	@ProductID uniqueidentifier = null,
	@EmployeeID uniqueidentifier = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAll')
BEGIN
	SELECT			ItemChefMapping.ItemChefMappingID, ItemChefMapping.CategoryID, ItemChefMapping.ProductID, CategoryWiseProduct.ProductName, CategoryMaster.CategoryName, ItemChefMapping.EmployeeID, 
						EmployeeMasterList.EmpName
	FROM           ItemChefMapping INNER JOIN
						EmployeeMasterList ON ItemChefMapping.EmployeeID = EmployeeMasterList.EmployeeID INNER JOIN
						CategoryWiseProduct ON ItemChefMapping.ProductID = CategoryWiseProduct.ProductID INNER JOIN
						CategoryMaster ON CategoryWiseProduct.CategoryID = CategoryMaster.CategoryID
END

IF(@Mode = 'GetByID')
BEGIN
	SELECT			ItemChefMapping.ItemChefMappingID, ItemChefMapping.CategoryID, ItemChefMapping.ProductID, CategoryWiseProduct.ProductName, CategoryMaster.CategoryName, ItemChefMapping.EmployeeID, 
						EmployeeMasterList.EmpName
	FROM           ItemChefMapping INNER JOIN
						EmployeeMasterList ON ItemChefMapping.EmployeeID = EmployeeMasterList.EmployeeID INNER JOIN
						CategoryWiseProduct ON ItemChefMapping.ProductID = CategoryWiseProduct.ProductID INNER JOIN
						CategoryMaster ON CategoryWiseProduct.CategoryID = CategoryMaster.CategoryID
	WHERE			ItemChefMapping.ItemChefMappingID = @ItemChefMappingID
END

IF(@Mode = 'GetByCategoryID')
BEGIN
	SELECT			ItemChefMapping.ItemChefMappingID, ItemChefMapping.CategoryID, ItemChefMapping.ProductID, CategoryWiseProduct.ProductName, CategoryMaster.CategoryName, ItemChefMapping.EmployeeID, 
						EmployeeMasterList.EmpName
	FROM           ItemChefMapping INNER JOIN
						EmployeeMasterList ON ItemChefMapping.EmployeeID = EmployeeMasterList.EmployeeID INNER JOIN
						CategoryWiseProduct ON ItemChefMapping.ProductID = CategoryWiseProduct.ProductID INNER JOIN
						CategoryMaster ON CategoryWiseProduct.CategoryID = CategoryMaster.CategoryID
	WHERE			ItemChefMapping.CategoryID = @CategoryID
END

IF(@Mode = 'GetByProductID')
BEGIN
	SELECT			ItemChefMapping.ItemChefMappingID, ItemChefMapping.CategoryID, ItemChefMapping.ProductID, CategoryWiseProduct.ProductName, CategoryMaster.CategoryName, ItemChefMapping.EmployeeID, 
						EmployeeMasterList.EmpName
	FROM           ItemChefMapping INNER JOIN
						EmployeeMasterList ON ItemChefMapping.EmployeeID = EmployeeMasterList.EmployeeID INNER JOIN
						CategoryWiseProduct ON ItemChefMapping.ProductID = CategoryWiseProduct.ProductID INNER JOIN
						CategoryMaster ON CategoryWiseProduct.CategoryID = CategoryMaster.CategoryID
	WHERE			ItemChefMapping.ProductID = @ProductID
END

IF(@Mode = 'GetByEmployeeID')
BEGIN
	SELECT			ItemChefMapping.ItemChefMappingID, ItemChefMapping.CategoryID, ItemChefMapping.ProductID, CategoryWiseProduct.ProductName, CategoryMaster.CategoryName, ItemChefMapping.EmployeeID, 
						EmployeeMasterList.EmpName
	FROM           ItemChefMapping INNER JOIN
						EmployeeMasterList ON ItemChefMapping.EmployeeID = EmployeeMasterList.EmployeeID INNER JOIN
						CategoryWiseProduct ON ItemChefMapping.ProductID = CategoryWiseProduct.ProductID INNER JOIN
						CategoryMaster ON CategoryWiseProduct.CategoryID = CategoryMaster.CategoryID
	WHERE			ItemChefMapping.EmployeeID = @EmployeeID
END



GO
/****** Object:  StoredProcedure [dbo].[GetMergeTable]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetMergeTable]
	@OrderID uniqueidentifier = null,
	@TableID uniqueidentifier = null,
	@Mode nvarchar(50) = null
AS

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    [MergeTable]
END

IF(@Mode = 'GetRecordByOrderID')
BEGIN
	SELECT *
	FROM [MergeTable] 
	WHERE OrderID = @OrderID
END

IF(@Mode = 'GetTableByOrderID')
BEGIN
	SELECT        MergeTable.ID, MergeTable.OrderID, MergeTable.TableID, TableMasterDetail.TableName
	FROM            MergeTable INNER JOIN
                         TableMasterDetail ON MergeTable.TableID = TableMasterDetail.TableID
	WHERE MergeTable.OrderID = @OrderID
END




GO
/****** Object:  StoredProcedure [dbo].[GetModifierCategoryDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetModifierCategoryDetail]
	@ModifierCategoryID uniqueidentifier = null,
	@ProductID uniqueidentifier = null,
	@Mode nvarchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    ModifierCategoryDetail 
	ORDER BY ModifierCategoryName;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM ModifierCategoryDetail
	WHERE ModifierCategoryID = @ModifierCategoryID;
END

IF(@Mode = 'GetRecordByProductID')
BEGIN
	SELECT * 
	FROM ModifierCategoryDetail
	WHERE ProductID = @ProductID
	ORDER BY ModifierCategoryName;
END



GO
/****** Object:  StoredProcedure [dbo].[GetModifierDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetModifierDetail]
	@IngredientsID uniqueidentifier = null,
	@ModifierCategoryDetail_Id int = null,
	@Mode nvarchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    ModifierDetail 
	ORDER BY Name;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM ModifierDetail
	WHERE IngredientsID = @IngredientsID;
END

IF(@Mode = 'GetRecordByMCDID')
BEGIN
	SELECT DISTINCT * 
	FROM ModifierDetail
	WHERE ModifierCategoryDetail_Id = @ModifierCategoryDetail_Id;
END




GO
/****** Object:  StoredProcedure [dbo].[GetModuleAppIDDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[GetModuleAppIDDetail]
	@AppID varchar(50) = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    ModuleAppIDDetail 
	ORDER BY DeviceName;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM ModuleAppIDDetail
	WHERE AppID = @AppID;
END




GO
/****** Object:  StoredProcedure [dbo].[GetModuleMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetModuleMasterDetail]
	@ModuleID uniqueidentifier = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    ModuleMasterDetail 
	ORDER BY ModuleName;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM ModuleMasterDetail
	WHERE ModuleID = @ModuleID;
END




GO
/****** Object:  StoredProcedure [dbo].[GetOrder]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetOrder]
	@OrderID uniqueidentifier = null,
	@TableID uniqueidentifier = null,
	@EmployeeID uniqueidentifier = null,
	@DeliveryType int = null,
	@OrderDateFrom varchar(25) = null,
	@OrderDateTo varchar(25) = null,
	@SearchKey varchar(50) = null,
	@Mode varchar(50) = null
AS

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    [Order] 
	ORDER BY OrderNo;
END

IF(@Mode = 'GetRecordByOrderID')
BEGIN
	SELECT			[Order].OrderID, [Order].OrderNo, [Order].OrderDate, [Order].EmployeeID, [Order].CustomerID, 
						CustomerMasterData.Name, CustomerMasterData.MobileNo, [Order].DeliveryType, [Order].DeliveryTypeName, 
						[Order].TableID, TableMasterDetail.TableName, [Order].SubTotal, [Order].ExtraCharge, [Order].TaxLabel1, [Order].TaxPercent1, [Order].SGSTAmount, 
						[Order].TaxLabel2, [Order].TaxPercent2, [Order].CGSTAmount, [Order].TotalTax, [Order].Discount, [Order].DiscountType, [Order].DiscountPer, [Order].TipGratuity, [Order].PayableAmount, 
						[Order].OrderActions, [Order].OrderSpecialRequest, [Order].DeliveryCharge
	FROM           [Order] INNER JOIN
                        CustomerMasterData ON [Order].CustomerID = CustomerMasterData.CustomerID LEFT OUTER JOIN
                        TableMasterDetail ON [Order].TableID = TableMasterDetail.TableID
	WHERE	OrderID = @OrderID;
END

IF(@Mode = 'GetRecordByOrderAndTableID')
BEGIN
	SELECT [Order].OrderID, [Order].OrderNo, [Order].OrderDate, [Order].EmployeeID, [Order].CustomerID, 
				CustomerMasterData.Name, CustomerMasterData.MobileNo, [Order].DeliveryType, [Order].DeliveryTypeName, 
				[MergeTable].TableID, TableMasterDetail.TableName, [Order].SubTotal, [Order].ExtraCharge, [Order].TaxLabel1, [Order].TaxPercent1, [Order].SGSTAmount, 
				[Order].TaxLabel2, [Order].TaxPercent2, [Order].CGSTAmount, [Order].TotalTax, [Order].Discount, [Order].DiscountType, [Order].DiscountPer, [Order].TipGratuity, [Order].PayableAmount, 
				[Order].OrderActions, [Order].DeliveryCharge
	FROM   [Order] INNER JOIN
				CustomerMasterData ON [Order].CustomerID = CustomerMasterData.CustomerID INNER JOIN
				MergeTable ON [Order].OrderID = MergeTable.OrderID INNER JOIN
				TableMasterDetail ON MergeTable.TableID = TableMasterDetail.TableID
	WHERE	[Order].OrderID = @OrderID AND MergeTable.TableID = @TableID;
END

IF(@Mode = 'GetAllRecordByEmployeeID')
BEGIN
	SELECT			[Order].OrderID, [Order].OrderNo, [Order].OrderDate, [Order].EmployeeID, [Order].CustomerID, CustomerMasterData.Name, [Order].DeliveryType, [Order].DeliveryTypeName, [Order].TableID, TableMasterDetail.TableName, 
                        [Order].SubTotal, [Order].ExtraCharge, [Order].TaxLabel1, [Order].TaxPercent1, [Order].SGSTAmount, [Order].TaxLabel2, [Order].TaxPercent2, [Order].CGSTAmount, [Order].TotalTax, [Order].Discount, [Order].DiscountType, [Order].DiscountPer, [Order].TipGratuity, [Order].PayableAmount, [Order].OrderActions,
						CASE WHEN [Order].OrderActions=1 THEN 'PAY' WHEN [Order].OrderActions=2 THEN 'PAID' WHEN [Order].OrderActions=3 THEN 'CANCEL' END AS OrderActionsName, [Order].DeliveryCharge
	FROM           [Order] INNER JOIN
						CustomerMasterData ON [Order].CustomerID = CustomerMasterData.CustomerID LEFT OUTER JOIN
						TableMasterDetail ON [Order].TableID = TableMasterDetail.TableID
	WHERE	[Order].EmployeeID = @EmployeeID
	ORDER BY OrderNo DESC;
END

IF(@Mode = 'GetRecordByDeliveryType')
BEGIN
	SELECT			[Order].OrderID, [Order].OrderNo, [Order].OrderDate, [Order].EmployeeID, [Order].CustomerID, CustomerMasterData.Name, [Order].DeliveryType, [Order].DeliveryTypeName, [Order].TableID, TableMasterDetail.TableName, 
                        [Order].SubTotal, [Order].ExtraCharge, [Order].TaxLabel1, [Order].TaxPercent1, [Order].SGSTAmount, [Order].TaxLabel2, [Order].TaxPercent2, [Order].CGSTAmount, [Order].TotalTax, [Order].Discount, [Order].DiscountType, [Order].DiscountPer, [Order].TipGratuity, [Order].PayableAmount, [Order].OrderActions,
						CASE WHEN [Order].OrderActions=1 THEN 'PAY' WHEN [Order].OrderActions=2 THEN 'PAID' WHEN [Order].OrderActions=3 THEN 'CANCEL' END AS OrderActionsName, [Order].DeliveryCharge
	FROM           [Order] INNER JOIN
						CustomerMasterData ON [Order].CustomerID = CustomerMasterData.CustomerID LEFT OUTER JOIN
						TableMasterDetail ON [Order].TableID = TableMasterDetail.TableID
	WHERE	[Order].EmployeeID = @EmployeeID AND [Order].DeliveryType = @DeliveryType
	ORDER BY OrderNo DESC;
END

IF(@Mode = 'GetAllRecordByDateFilter')
BEGIN
	SELECT			[Order].OrderID, [Order].OrderNo, [Order].OrderDate, [Order].EmployeeID, [Order].CustomerID, CustomerMasterData.Name, [Order].DeliveryType, [Order].DeliveryTypeName, [Order].TableID, TableMasterDetail.TableName, 
                        [Order].SubTotal, [Order].ExtraCharge, [Order].TaxLabel1, [Order].TaxPercent1, [Order].SGSTAmount, [Order].TaxLabel2, [Order].TaxPercent2, [Order].CGSTAmount, [Order].TotalTax, [Order].Discount, [Order].DiscountType, [Order].DiscountPer, [Order].TipGratuity, [Order].PayableAmount, [Order].OrderActions,
						CASE WHEN [Order].OrderActions=1 THEN 'PAY' WHEN [Order].OrderActions=2 THEN 'PAID' WHEN [Order].OrderActions=3 THEN 'CANCEL' END AS OrderActionsName, [Order].DeliveryCharge
	FROM           [Order] INNER JOIN
						CustomerMasterData ON [Order].CustomerID = CustomerMasterData.CustomerID LEFT OUTER JOIN
						TableMasterDetail ON [Order].TableID = TableMasterDetail.TableID
	WHERE	[Order].EmployeeID = @EmployeeID AND 
						DATEADD(DAY, DATEDIFF(DAY, 0, [Order].OrderDate), 0) BETWEEN @OrderDateFrom AND @OrderDateTo
	ORDER BY OrderNo DESC;
END

IF(@Mode = 'GetRecordByTypeAndDateFilter')
BEGIN
	SELECT			[Order].OrderID, [Order].OrderNo, [Order].OrderDate, [Order].EmployeeID, [Order].CustomerID, CustomerMasterData.Name, [Order].DeliveryType, [Order].DeliveryTypeName, [Order].TableID, TableMasterDetail.TableName, 
                        [Order].SubTotal, [Order].ExtraCharge, [Order].TaxLabel1, [Order].TaxPercent1, [Order].SGSTAmount, [Order].TaxLabel2, [Order].TaxPercent2, [Order].CGSTAmount, [Order].TotalTax, [Order].Discount, [Order].DiscountType, [Order].DiscountPer, [Order].TipGratuity, [Order].PayableAmount, [Order].OrderActions,
						CASE WHEN [Order].OrderActions=1 THEN 'PAY' WHEN [Order].OrderActions=2 THEN 'PAID' WHEN [Order].OrderActions=3 THEN 'CANCEL' END AS OrderActionsName, [Order].DeliveryCharge
	FROM           [Order] INNER JOIN
						CustomerMasterData ON [Order].CustomerID = CustomerMasterData.CustomerID LEFT OUTER JOIN
						TableMasterDetail ON [Order].TableID = TableMasterDetail.TableID
	WHERE	[Order].EmployeeID = @EmployeeID AND [Order].DeliveryType = @DeliveryType AND 
						DATEADD(DAY, DATEDIFF(DAY, 0, [Order].OrderDate), 0) BETWEEN @OrderDateFrom AND @OrderDateTo
	ORDER BY OrderNo DESC;
END

IF(@Mode = 'GetAllRecordBySearch')
BEGIN
	SELECT			[Order].OrderID, [Order].OrderNo, [Order].OrderDate, [Order].EmployeeID, [Order].CustomerID, CustomerMasterData.Name, [Order].DeliveryType, [Order].DeliveryTypeName, [Order].TableID, TableMasterDetail.TableName, 
                        [Order].SubTotal, [Order].ExtraCharge, [Order].TaxLabel1, [Order].TaxPercent1, [Order].SGSTAmount, [Order].TaxLabel2, [Order].TaxPercent2, [Order].CGSTAmount, [Order].TotalTax, [Order].Discount, [Order].DiscountType, [Order].DiscountPer, [Order].TipGratuity, [Order].PayableAmount, [Order].OrderActions,
						CASE WHEN [Order].OrderActions=1 THEN 'PAY' WHEN [Order].OrderActions=2 THEN 'PAID' WHEN [Order].OrderActions=3 THEN 'CANCEL' END AS OrderActionsName, [Order].DeliveryCharge
	FROM           [Order] INNER JOIN
						CustomerMasterData ON [Order].CustomerID = CustomerMasterData.CustomerID LEFT OUTER JOIN
						TableMasterDetail ON [Order].TableID = TableMasterDetail.TableID
	WHERE	[Order].EmployeeID = @EmployeeID AND (CustomerMasterData.Name LIKE @SearchKey+'%' or [Order].OrderNo LIKE @SearchKey+'%') AND 
						DATEADD(DAY, DATEDIFF(DAY, 0, [Order].OrderDate), 0) BETWEEN @OrderDateFrom AND @OrderDateTo
	ORDER BY OrderNo DESC;
END




GO
/****** Object:  StoredProcedure [dbo].[GetOrderTransaction]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetOrderTransaction]
	@OrderID uniqueidentifier = null,
	@Mode nvarchar(50) = null
AS

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    [Transaction] 
	ORDER BY Sort;
END

IF(@Mode = 'GetRecordByOrderID')
BEGIN
	SELECT        [Transaction].TransactionID, [Transaction].OrderID, [Transaction].CategoryID, [Transaction].ProductID, 
						CategoryWiseProduct.ProductName, [Transaction].Quantity, [Transaction].Rate, [Transaction].TotalAmount, 
						[Transaction].Sort, [Transaction].SpecialRequest
	FROM            [Transaction] INNER JOIN
                         CategoryWiseProduct ON [Transaction].ProductID = CategoryWiseProduct.ProductID AND
						 [Transaction].CategoryID = CategoryWiseProduct.CategoryID
	WHERE [Transaction].OrderID = @OrderID
	ORDER BY Sort;
END




GO
/****** Object:  StoredProcedure [dbo].[GetOrderWiseModifier]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetOrderWiseModifier]
	@ModifierID uniqueidentifier = null,
	@OrderID uniqueidentifier = null,
	@TransactionID uniqueidentifier = null,
	@ProductID uniqueidentifier = null,
	@IngredientsID uniqueidentifier = null,
	@Mode nvarchar(50) = null
AS

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    [OrderWiseModifier] 
	ORDER BY OrderID;
END
IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT    DISTINCT     OrderWiseModifier.ModifierID, OrderWiseModifier.OrderID, OrderWiseModifier.TransactionID, OrderWiseModifier.ProductID, OrderWiseModifier.IngredientsID, ModifierDetail.Name, OrderWiseModifier.Quantity, 
                        OrderWiseModifier.Price, OrderWiseModifier.ModifierOption, OrderWiseModifier.Total
	FROM           OrderWiseModifier INNER JOIN
                        ModifierDetail ON OrderWiseModifier.IngredientsID = ModifierDetail.IngredientsID
	WHERE			ModifierID = @ModifierID
END
IF(@Mode = 'GetRecordByOrderAndProductID')
BEGIN
	SELECT    DISTINCT     OrderWiseModifier.ModifierID, OrderWiseModifier.OrderID, OrderWiseModifier.TransactionID, OrderWiseModifier.ProductID, OrderWiseModifier.IngredientsID, ModifierDetail.Name, OrderWiseModifier.Quantity, 
                        OrderWiseModifier.Price, OrderWiseModifier.ModifierOption, OrderWiseModifier.Total
	FROM           OrderWiseModifier INNER JOIN
                        ModifierDetail ON OrderWiseModifier.IngredientsID = ModifierDetail.IngredientsID
	WHERE			OrderID = @OrderID AND ProductID = @ProductID AND TransactionID = @TransactionID
END
IF(@Mode = 'GetByOrderProductTransactionIngredientsID')
BEGIN
	SELECT    DISTINCT     OrderWiseModifier.ModifierID, OrderWiseModifier.OrderID, OrderWiseModifier.TransactionID, OrderWiseModifier.ProductID, OrderWiseModifier.IngredientsID, ModifierDetail.Name, OrderWiseModifier.Quantity, 
                        OrderWiseModifier.Price, OrderWiseModifier.ModifierOption, OrderWiseModifier.Total
	FROM           OrderWiseModifier INNER JOIN
                        ModifierDetail ON OrderWiseModifier.IngredientsID = ModifierDetail.IngredientsID
	WHERE			OrderWiseModifier.OrderID = @OrderID AND OrderWiseModifier.ProductID = @ProductID AND 
						OrderWiseModifier.TransactionID = @TransactionID AND OrderWiseModifier.IngredientsID = @IngredientsID
END




GO
/****** Object:  StoredProcedure [dbo].[GetPaymentGatewayMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetPaymentGatewayMaster]
	@PaymentGatewayID uniqueidentifier = null,
	@Mode nvarchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    PaymentGatewayMaster 
	ORDER BY PaymentGatewayName;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM PaymentGatewayMaster
	WHERE PaymentGatewayID = @PaymentGatewayID;
END




GO
/****** Object:  StoredProcedure [dbo].[GetPrinterMapping]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetPrinterMapping]
	@PrinterMappingID uniqueidentifier = null,
	@DeviceID uniqueidentifier = null,
	@PrinterID uniqueidentifier = null,
	@PartID int = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAll')
BEGIN
	SELECT PrinterMapping.PrinterMappingID, PrinterMapping.DeviceID, DeviceMaster.DeviceName, PrinterMapping.PrinterID, PrinterMaster.DeviceName AS PrinterName, PrinterMapping.PartID
	FROM	PrinterMapping INNER JOIN
				DeviceMaster ON PrinterMapping.DeviceID = DeviceMaster.DeviceID INNER JOIN
				DeviceMaster AS PrinterMaster ON PrinterMapping.PrinterID = PrinterMaster.DeviceID
	WHERE	PrinterMapping.PartID = @PartID
END

IF(@Mode = 'GetByID')
BEGIN
	SELECT PrinterMapping.PrinterMappingID, PrinterMapping.DeviceID, DeviceMaster.DeviceName, PrinterMapping.PrinterID, PrinterMaster.DeviceName AS PrinterName, PrinterMapping.PartID
	FROM	PrinterMapping INNER JOIN
				DeviceMaster ON PrinterMapping.DeviceID = DeviceMaster.DeviceID INNER JOIN
				DeviceMaster AS PrinterMaster ON PrinterMapping.PrinterID = PrinterMaster.DeviceID
	WHERE	PrinterMapping.PartID = @PartID AND 
				PrinterMapping.PrinterMappingID = @PrinterMappingID
END

IF(@Mode = 'GetByPrinterID')
BEGIN
	SELECT PrinterMapping.PrinterMappingID, PrinterMapping.DeviceID, DeviceMaster.DeviceName, PrinterMapping.PrinterID, PrinterMaster.DeviceName AS PrinterName, PrinterMapping.PartID
	FROM	PrinterMapping INNER JOIN
				DeviceMaster ON PrinterMapping.DeviceID = DeviceMaster.DeviceID INNER JOIN
				DeviceMaster AS PrinterMaster ON PrinterMapping.PrinterID = PrinterMaster.DeviceID
	WHERE	PrinterMapping.PartID = @PartID AND 
				PrinterMapping.PrinterID = @PrinterID
END

IF(@Mode = 'GetByDeviceID')
BEGIN
	SELECT PrinterMapping.PrinterMappingID, PrinterMapping.DeviceID, DeviceMaster.DeviceName, PrinterMapping.PrinterID, PrinterMaster.DeviceName AS PrinterName, PrinterMapping.PartID
	FROM	PrinterMapping INNER JOIN
				DeviceMaster ON PrinterMapping.DeviceID = DeviceMaster.DeviceID INNER JOIN
				DeviceMaster AS PrinterMaster ON PrinterMapping.PrinterID = PrinterMaster.DeviceID
	WHERE	PrinterMapping.PartID = @PartID AND 
				PrinterMapping.DeviceID = @DeviceID
END




GO
/****** Object:  StoredProcedure [dbo].[GetProductClassMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetProductClassMasterDetail]
	@ClassID uniqueidentifier = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    ProductClassMasterDetail 
	ORDER BY ClassName;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM ProductClassMasterDetail
	WHERE ClassID = @ClassID;
END




GO
/****** Object:  StoredProcedure [dbo].[GetRecipeMasterData]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRecipeMasterData]
	@RecipeID uniqueidentifier = null,
	@ProductID uniqueidentifier = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    RecipeMasterData 
	ORDER BY ProductName;
END

IF(@Mode = 'GetRecordByRecipeID')
BEGIN
	SELECT * 
	FROM RecipeMasterData
	WHERE RecipeID = @RecipeID;
END

IF(@Mode = 'GetRecordByProductID')
BEGIN
	SELECT * 
	FROM RecipeMasterData
	WHERE ProductID = @ProductID;
END




GO
/****** Object:  StoredProcedure [dbo].[GetRecipeMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRecipeMasterDetail]
	@IngredientsID uniqueidentifier = null,
	@Mode nvarchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    RecipeMasterDetail 
	ORDER BY Name;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM RecipeMasterDetail
	WHERE IngredientsID = @IngredientsID;
END




GO
/****** Object:  StoredProcedure [dbo].[GetRightDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRightDetail]
	@RightCode int,
	@SubFeatureDetail_Id int,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    RightDetail 
	ORDER BY RightName;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM RightDetail
	WHERE @SubFeatureDetail_Id = @SubFeatureDetail_Id AND RightCode = @RightCode;
END




GO
/****** Object:  StoredProcedure [dbo].[GetRoleMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetRoleMasterDetail]
	@RoleID uniqueidentifier = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    RoleMasterDetail 
	ORDER BY RoleName;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM RoleMasterDetail
	WHERE RoleID = @RoleID;
END




GO
/****** Object:  StoredProcedure [dbo].[GetSalesReport]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetSalesReport]
	@OrderID uniqueidentifier = null,
	@TableID uniqueidentifier = null,
	@EmployeeID uniqueidentifier = null,
	@DeliveryType int = null,
	@OrderDateFrom varchar(25) = null,
	@OrderDateTo varchar(25) = null,
	@Mode varchar(50) = null
AS

IF(@Mode = 'GetAll')
BEGIN
	SELECT			[Order].OrderID, [Order].OrderNo, [Order].OrderDate, [Order].EmployeeID, [Order].CustomerID, CustomerMasterData.Name, CustomerMasterData.MobileNo, [Order].DeliveryType, [Order].DeliveryTypeName, [Order].TableID, TableMasterDetail.TableName, 
                        [Order].SubTotal, [Order].ExtraCharge, [Order].SGSTAmount, [Order].CGSTAmount, [Order].TotalTax, [Order].Discount, [Order].TipGratuity, [Order].PayableAmount, [Order].OrderActions, [Order].OrderSpecialRequest
	FROM           [Order] INNER JOIN
                        CustomerMasterData ON [Order].CustomerID = CustomerMasterData.CustomerID LEFT OUTER JOIN
                        TableMasterDetail ON [Order].TableID = TableMasterDetail.TableID
	ORDER BY [Order].OrderDate;
END

IF(@Mode = 'GetByEmployeeID')
BEGIN
	SELECT			[Order].OrderNo, [Order].OrderDate, CustomerMasterData.Name, [Order].DeliveryTypeName, 
                        [Order].SubTotal, [Order].ExtraCharge, [Order].SGSTAmount, [Order].CGSTAmount, [Order].Discount, 
						[Order].TipGratuity, [Order].PayableAmount, 
						CASE WHEN CheckOutDetail.PaymentMethod = 1 THEN 'CASH' WHEN CheckOutDetail.PaymentMethod = 2 THEN 'CARD' WHEN CheckOutDetail.PaymentMethod = 3 THEN 'CHEQUE' END AS PaymentMethod
	FROM           [Order] INNER JOIN
                        CustomerMasterData ON [Order].CustomerID = CustomerMasterData.CustomerID LEFT OUTER JOIN
                        TableMasterDetail ON [Order].TableID = TableMasterDetail.TableID LEFT JOIN
						CheckOutDetail ON [Order].OrderID = CheckOutDetail.OrderID 
	WHERE [Order].EmployeeID = @EmployeeID AND
				DATEADD(DAY, DATEDIFF(DAY, 0, [Order].OrderDate), 0) BETWEEN @OrderDateFrom AND @OrderDateTo AND 
				[Order].OrderActions = 2
	ORDER BY [Order].OrderDate;
END
GO
/****** Object:  StoredProcedure [dbo].[GetShiftMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetShiftMaster]
	@ShiftID uniqueidentifier = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    ShiftMaster 
	ORDER BY ShiftName;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM ShiftMaster
	WHERE ShiftID = @ShiftID;
END




GO
/****** Object:  StoredProcedure [dbo].[GetShiftMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetShiftMasterDetail]
	@ShiftDetailsID uniqueidentifier = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    ShiftMasterDetail 
	ORDER BY ShiftMaster_ID;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM ShiftMasterDetail
	WHERE ShiftDetailsID = @ShiftDetailsID;
END




GO
/****** Object:  StoredProcedure [dbo].[GetSubCategoryDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetSubCategoryDetail]
	@CategoryID uniqueidentifier = null,
	@MainCategoryID uniqueidentifier = null,
	@Mode nvarchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    [SubCategoryDetail] 
	ORDER BY CategoryName;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM [SubCategoryDetail]
	WHERE CategoryID = @CategoryID;
END

IF(@Mode = 'GetRecordByMainCategoryID')
BEGIN
	SELECT * 
	FROM [SubCategoryDetail]
	WHERE MainCategoryID = @MainCategoryID;
END



GO
/****** Object:  StoredProcedure [dbo].[GetSubFeatureDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetSubFeatureDetail]
	@SubFeatureDetail_Id int = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    SubFeatureDetail 
	ORDER BY SubFeatureName;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM SubFeatureDetail
	WHERE FeatureDetail_Id = @SubFeatureDetail_Id;
END




GO
/****** Object:  StoredProcedure [dbo].[GetTableInfo]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTableInfo] 
	@TABLE_NAME Varchar(50) = null	
AS
BEGIN
	SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = @TABLE_NAME
END

GO
/****** Object:  StoredProcedure [dbo].[GetTableMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetTableMasterDetail]
	@TableID uniqueidentifier = null,
	@EmployeeID uniqueidentifier = null,
	@StatusID int = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    TableMasterDetail 
	ORDER BY TableName;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM TableMasterDetail
	WHERE TableID = @TableID;
END

IF(@Mode = 'GetRecordByEmployeeID')
BEGIN
	SELECT * 
	FROM TableMasterDetail
	WHERE EmployeeID = @EmployeeID;
END

IF(@Mode = 'GetTableForViewByEmpID')
BEGIN
	--2)
	SELECT [Order].OrderID, TableMasterDetail.TableID, TableMasterDetail.TableName, [Order].OrderDate, CustomerMasterData.Name AS CustomerName, [Order].PayableAmount, TableMasterDetail.StatusID
	FROM [Order] LEFT OUTER JOIN
	MergeTable ON [Order].OrderID = MergeTable.OrderID RIGHT OUTER JOIN
	TableMasterDetail ON MergeTable.TableID = TableMasterDetail.TableID LEFT OUTER JOIN
	CustomerMasterData ON [Order].CustomerID = CustomerMasterData.CustomerID
	WHERE TableMasterDetail.EmployeeID = @EmployeeID 
	AND [order].OrderActions in (1,3) AND  TableMasterDetail.StatusID = 2

	UNION ALL

	SELECT [Order].OrderID, TableMasterDetail.TableID, TableMasterDetail.TableName, [Order].OrderDate, CustomerMasterData.Name AS CustomerName, [Order].PayableAmount, TableMasterDetail.StatusID
	FROM [Order] LEFT OUTER JOIN
	MergeTable ON [Order].OrderID = MergeTable.OrderID RIGHT OUTER JOIN
	TableMasterDetail ON MergeTable.TableID = TableMasterDetail.TableID LEFT OUTER JOIN
	CustomerMasterData ON [Order].CustomerID = CustomerMasterData.CustomerID
	WHERE TableMasterDetail.EmployeeID = @EmployeeID 
	AND TableMasterDetail.StatusID = 1
	ORDER BY TableMasterDetail.TableName;


	--3) SELECT temp.OrderID,TableMasterDetail.TableID,TableMasterDetail.TableName,temp.OrderDate,temp.CustomerName, temp.PayableAmount,TableMasterDetail.StatusID 
	--FROM TableMasterDetail LEFT JOIN 
	--MergeTable ON MergeTable.TableID = TableMasterDetail.TableID left join
	--(SELECT [Order].OrderID,[MergeTable].TableID,[Order].OrderDate,CustomerMasterData.Name AS CustomerName,[Order].PayableAmount
	--FROM [Order] INNER JOIN 
	--CustomerMasterData ON [Order].CustomerID = CustomerMasterData.CustomerID LEFT JOIN 
	--MergeTable ON MergeTable.OrderID = [Order].OrderID where [Order].OrderActions IN (1,3)) 
	--AS temp ON MergeTable.OrderID = temp.OrderID AND
	--TableMasterDetail.TableID= temp.TableID

	--1) SELECT [Order].OrderID, TableMasterDetail.TableID, TableMasterDetail.TableName, [Order].OrderDate, CustomerMasterData.Name AS CustomerName, [Order].PayableAmount, TableMasterDetail.StatusID
	--FROM [Order] LEFT OUTER JOIN
--   MergeTable ON [Order].OrderID = MergeTable.OrderID RIGHT OUTER JOIN
--   TableMasterDetail ON MergeTable.TableID = TableMasterDetail.TableID LEFT OUTER JOIN
--   CustomerMasterData ON [Order].CustomerID = CustomerMasterData.CustomerID
	--WHERE TableMasterDetail.EmployeeID = @EmployeeID 
	--ORDER BY TableMasterDetail.TableName;
END

IF(@Mode = 'GetRecordByEmpIDForCombo')
BEGIN
	SELECT * 
	FROM TableMasterDetail
	WHERE EmployeeID = @EmployeeID AND StatusID = 1;
END




GO
/****** Object:  StoredProcedure [dbo].[GetTaxGroupDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetTaxGroupDetail]
	@TaxGroupID uniqueidentifier = null,
	@ParentID uniqueidentifier = null,
	@Mode nvarchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    TaxGroupDetail 
	ORDER BY Name;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM TaxGroupDetail
	WHERE TaxGroupID = @TaxGroupID;
END

IF(@Mode = 'GetRecordByParentID')
BEGIN
	SELECT * 
	FROM TaxGroupDetail
	WHERE ParentID = @ParentID
	ORDER BY Name;
END




GO
/****** Object:  StoredProcedure [dbo].[GetTillManage]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetTillManage]
	@TillID uniqueidentifier = null,
	@StartDateTime varchar(50) = null,
	@EndDateTime varchar(50) = null,
	@EnrtyDate varchar(50) = null,
	@IsTillDone bit = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAll')
BEGIN
	SELECT	*
	FROM	TillManage 
END

IF(@Mode = 'GetByID')
BEGIN
	SELECT	*
	FROM	TillManage 
	WHERE [TillID] = @TillID;
END

IF(@Mode = 'GetByIsTillDone')
BEGIN
	SELECT	*
	FROM	TillManage 
	WHERE IsTillDone = @IsTillDone;
END



GO
/****** Object:  StoredProcedure [dbo].[GetTillPayIn]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetTillPayIn]
	@PayInID uniqueidentifier = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAll')
BEGIN
	SELECT	*
	FROM	TillPayIn 
END

IF(@Mode = 'GetByID')
BEGIN
	SELECT	*
	FROM	TillPayIn 
	WHERE [PayInID] = @PayInID;
END





GO
/****** Object:  StoredProcedure [dbo].[GetTillPayOut]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetTillPayOut]
	@PayOutID uniqueidentifier = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAll')
BEGIN
	SELECT	*
	FROM	TillPayOut 
END

IF(@Mode = 'GetByID')
BEGIN
	SELECT	*
	FROM	TillPayOut 
	WHERE [PayOutID] = @PayOutID;
END





GO
/****** Object:  StoredProcedure [dbo].[GetTimeSheetWiseDiscount]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetTimeSheetWiseDiscount]
	@DiscountMasterDetail_Id int = null,
	@Mode nvarchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    [TimeSheetWiseDiscount] 
	ORDER BY [Day];
END

IF(@Mode = 'GetRecordByDiscountMasterDetailID')
BEGIN
	SELECT  *
	FROM    [TimeSheetWiseDiscount] 
	WHERE DiscountMasterDetail_Id = @DiscountMasterDetail_Id;
END



GO
/****** Object:  StoredProcedure [dbo].[GetVendorMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetVendorMaster]
	@VendorID uniqueidentifier = null,
	@Mode varchar(50) = null
as

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    [VendorMasterData] 
	ORDER BY VendorName;
END

IF(@Mode = 'GetRecordByCategoryID')
BEGIN
	SELECT * 
	FROM [VendorMasterData]
	WHERE VendorID = @VendorID;
END




GO
/****** Object:  StoredProcedure [dbo].[GetVersionDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetVersionDetail]
	@Version_Code nvarchar(50) = null,
	@Mode nvarchar(50) = null
AS

IF(@Mode = 'GetAllRecord')
BEGIN
	SELECT  *
	FROM    [VersionDetail] 
	ORDER BY Version_Code;
END

IF(@Mode = 'GetRecordByID')
BEGIN
	SELECT * 
	FROM [VersionDetail]
	WHERE Version_Code = @Version_Code;
END




GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteBranchMasterSetting]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteBranchMasterSetting]
	@BranchID uniqueidentifier = null,
	@RestaurantID uniqueidentifier = null,
	@ContactNoForService varchar(50) = null,
	@DeliveryCharges int = null,
	@DeliveryTime varchar(10) = null,
	@PickupTime varchar(10) = null,
	@CurrencyName varchar(50) = null,
	@CurrencySymbol varchar(50) = null,
	@WorkingDays varchar(50) = null,
	@TagLine varchar(50) = null,
	@Footer varchar(50) = null,
	@DeliveryAreaRedius int = null,
	@DeliveryAreaTitle varchar(50) = null,
	@DistanceType int = null,
	@DistanceName varchar(50) = null,
	@FreeDeliveryUpto int = null,
	@BranchName varchar(50) = null,
	@BranchEmailID varchar(50) = null,
	@MobileNo varchar(50) = null,
	@LastSyncDate varchar(50) = null,
	@VatNo varchar(50) = null,
	@CSTNo varchar(50) = null,
	@ServiceTaxNo varchar(50) = null,
	@TinGSTNo varchar(50) = null,
	@Address varchar(250) = null,
	@SubAreaStreet varchar(50) = null,
	@PinCode varchar(50) = null,
	@VersionCode varchar(50) = null,
	@BranchMasterSetting_Id int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into [BranchMasterSetting]
		([BranchID],[RestaurantID],[ContactNoForService],[DeliveryCharges],[DeliveryTime],[PickupTime],[CurrencyName],[CurrencySymbol],[WorkingDays],[TagLine],[Footer],[DeliveryAreaRedius],[DeliveryAreaTitle],[DistanceType],[DistanceName],[FreeDeliveryUpto],[BranchName],[BranchEmailID],[MobileNo],[LastSyncDate],[VatNo],[CSTNo],[ServiceTaxNo],[TinGSTNo],[Address],[SubAreaStreet],[PinCode],[VersionCode],[BranchMasterSetting_Id])
	Values
		(@BranchID,@RestaurantID,@ContactNoForService,@DeliveryCharges,@DeliveryTime,@PickupTime,@CurrencyName,@CurrencySymbol,@WorkingDays,@TagLine,@Footer,@DeliveryAreaRedius,@DeliveryAreaTitle,@DistanceType,@DistanceName,@FreeDeliveryUpto,@BranchName,@BranchEmailID,@MobileNo,@LastSyncDate,@VatNo,@CSTNo,@ServiceTaxNo,@TinGSTNo,@Address,@SubAreaStreet,@PinCode,@VersionCode,@BranchMasterSetting_Id)

END

IF(@Mode ='UPDATE')
BEGIN
	Update [BranchMasterSetting]
	Set
		[RestaurantID] = @RestaurantID,
		[ContactNoForService] = @ContactNoForService,
		[DeliveryCharges] = @DeliveryCharges,
		[DeliveryTime] = @DeliveryTime,
		[PickupTime] = @PickupTime,
		[CurrencyName] = @CurrencyName,
		[CurrencySymbol] = @CurrencySymbol,
		[WorkingDays] = @WorkingDays,
		[TagLine] = @TagLine,
		[Footer] = @Footer,
		[DeliveryAreaRedius] = @DeliveryAreaRedius,
		[DeliveryAreaTitle] = @DeliveryAreaTitle,
		[DistanceType] = @DistanceType,
		[DistanceName] = @DistanceName,
		[FreeDeliveryUpto] = @FreeDeliveryUpto,
		[BranchName] = @BranchName,
		[BranchEmailID] = @BranchEmailID,
		[MobileNo] = @MobileNo,
		[LastSyncDate] = @LastSyncDate,
		[VatNo] = @VatNo,
		[CSTNo] = @CSTNo,
		[ServiceTaxNo] = @ServiceTaxNo,
		[TinGSTNo] = @TinGSTNo,
		[Address] = @Address,
		[SubAreaStreet] = @SubAreaStreet,
		[PinCode] = @PinCode,
		[VersionCode] = @VersionCode,
		[BranchMasterSetting_Id] = @BranchMasterSetting_Id
		Where		
			[BranchID] = @BranchID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [BranchMasterSetting]
	WHERE
		[BranchID] = @BranchID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteBranchSettingDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE Procedure [dbo].[InsertUpdateDeleteBranchSettingDetail]
	@IsFranchise bit,
	@IsReservationOn bit,
	@IsOrderBookingOn bit,
	@IsAutoAcceptOrderOn bit,
	@IsAutoRoundOffTotalOn bit,
	@TaxGroupId int,
	@IsDemoVersion bit,
	@ExpireDate varchar(100),
	@BranchID uniqueidentifier,
	@Mode varchar(50)
As
Begin
	IF(@Mode = 'ADD')
	BEGIN
		Insert Into [BranchSettingDetail]
			([IsFranchise],[IsReservationOn],[IsOrderBookingOn],[IsAutoAcceptOrderOn],[IsAutoRoundOffTotalOn],[TaxGroupId],[IsDemoVersion],[ExpireDate],[BranchID])
		Values
			(@IsFranchise,@IsReservationOn,@IsOrderBookingOn,@IsAutoAcceptOrderOn,@IsAutoRoundOffTotalOn,@TaxGroupId,@IsDemoVersion,@ExpireDate,@BranchID)
	END
	
	IF(@Mode = 'UPDATE')
	BEGIN
		Update [BranchSettingDetail]
		Set
			[IsFranchise] = @IsFranchise,
			[IsReservationOn] = @IsReservationOn,
			[IsOrderBookingOn] = @IsOrderBookingOn,
			[IsAutoAcceptOrderOn] = @IsAutoAcceptOrderOn,
			[IsAutoRoundOffTotalOn] = @IsAutoRoundOffTotalOn,
			[TaxGroupId] = @TaxGroupId,
			[IsDemoVersion] = @IsDemoVersion,
			[ExpireDate] = @ExpireDate
		Where		
			[BranchID] = @BranchID
	END

	IF(@Mode = 'DELETE')
	BEGIN
		Delete [BranchSettingDetail]
		Where
			[BranchID] = @BranchID
	END
End

GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteCategoryMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create Procedure [dbo].[InsertUpdateDeleteCategoryMaster]
	@CategoryID uniqueidentifier,
	@CategoryName varchar(50),
	@ImgPath varchar(MAX),
	@ParentID uniqueidentifier,
	@ClassMasterID uniqueidentifier,
	@Priority int,
	@IsCategory int,
	@Mode varchar(50)
As
Begin
	IF(@Mode = 'ADD')
	BEGIN
		Insert Into CategoryMaster
			([CategoryID],[CategoryName],[ImgPath],[ParentID],[ClassMasterID],[Priority],[IsCategory])
		Values
			(@CategoryID,@CategoryName,@ImgPath,@ParentID,@ClassMasterID,@Priority,@IsCategory)
	END
	
	IF(@Mode = 'UPDATE')
	BEGIN
		Update CategoryMaster
		Set
			[CategoryName] = @CategoryName,
			[ImgPath] = @ImgPath,
			[ParentID] = @ParentID,
			[ClassMasterID] = @ClassMasterID,
			[Priority] = @Priority,
			[IsCategory] = @IsCategory
		Where		
			[CategoryID] = @CategoryID
	END

	IF(@Mode = 'DELETE')
	BEGIN
		Delete CategoryMaster
		Where
			[CategoryID] = @CategoryID
	END
End

GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteCheckOutDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteCheckOutDetail]
	@TransactionID uniqueidentifier = null,
	@OrderID uniqueidentifier = null,
	@CustomerID uniqueidentifier = null,
	@TableID uniqueidentifier = null,
	@CRMMethod int = null,
	@PaymentMethod int = null,
	@OrderAmount numeric(12,2) = null,
	@PaidAmount numeric(12,2) = null,
	@ChangeAmount numeric(12,2) = null,
	@ChequeNo varchar(25) = null,
	@ChequeDate varchar(25) = null,
	@CardHolderName varchar(50) = null,
	@CardNumber varchar(50) = null,
	@EntryDateTime varchar(25) = null,
	@OrderActions int = null,
	@TableStatusID int = null,
	@Mode varchar(50) = null
As
IF(@Mode ='ADD')
BEGIN
	INSERT INTO CheckOutDetail
		([TransactionID],[OrderID],[CustomerID],[TableID],[CRMMethod],[PaymentMethod],[OrderAmount],[PaidAmount],[ChangeAmount],[ChequeNo],[ChequeDate],[CardHolderName],[CardNumber],[EntryDateTime])
	VALUES
		(@TransactionID,@OrderID,@CustomerID,@TableID,@CRMMethod,@PaymentMethod,@OrderAmount,@PaidAmount,@ChangeAmount,@ChequeNo,@ChequeDate,@CardHolderName,@CardNumber,@EntryDateTime)

	UPDATE [Order]
	SET
		OrderActions = @OrderActions
	WHERE
		OrderID = @OrderID

	UPDATE TableMasterDetail
	SET
		StatusID = @TableStatusID
	WHERE
		TableID = @TableID

END

IF(@Mode ='UPDATE')
BEGIN
	UPDATE CheckOutDetail
	SET
		[OrderID] = @OrderID,
		[CustomerID] = @CustomerID,
		[TableID] = @TableID,
		[CRMMethod] = @CRMMethod,
		[PaymentMethod] = @PaymentMethod,
		[OrderAmount] = @OrderAmount,
		[PaidAmount] = @PaidAmount,
		[ChangeAmount] = @ChangeAmount,
		[ChequeNo] = @ChequeNo,
		[ChequeDate] = @ChequeDate,
		[CardHolderName] = @CardHolderName,
		[CardNumber] = @CardNumber,
		[EntryDateTime] = @EntryDateTime
	WHERE		
		[TransactionID] = @TransactionID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM CheckOutDetail
	WHERE
		[TransactionID] = @TransactionID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteChefKDSMapping]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteChefKDSMapping]
	@ChefKDSMappingID uniqueidentifier = null,
	@EmployeeID uniqueidentifier = null,
	@DeviceID uniqueidentifier = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into [ChefKDSMapping]
		([ChefKDSMappingID],[EmployeeID],[DeviceID])
	Values
		(@ChefKDSMappingID,@EmployeeID,@DeviceID)

END

IF(@Mode ='UPDATE')
BEGIN
	Update [ChefKDSMapping]
	Set
		[DeviceID] = @DeviceID
	Where		
			[EmployeeID] = @EmployeeID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [ChefKDSMapping]
	WHERE
		[EmployeeID] = @EmployeeID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteComboDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteComboDetail]
	@ComboSetID uniqueidentifier = null,
	@ComboSetName varchar(100) = null,
	@CategoryID uniqueidentifier = null,
	@CProductID uniqueidentifier = null,
	@ComboDetail_Id int = null,
	@CategoryWiseProduct_Id int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into [ComboDetail]
		([ComboSetID],[ComboSetName],[CategoryID],[CProductID],[ComboDetail_Id],[CategoryWiseProduct_Id])
	Values
		(@ComboSetID,@ComboSetName,@CategoryID,@CProductID,@ComboDetail_Id,@CategoryWiseProduct_Id)

END

IF(@Mode ='UPDATE')
BEGIN
	Update [ComboDetail]
	Set
		[ComboSetName] = @ComboSetName,
		[CategoryID] = @CategoryID,
		[CProductID] = @CProductID,
		[ComboDetail_Id] = @ComboDetail_Id,
		[CategoryWiseProduct_Id] = @CategoryWiseProduct_Id
	Where		
			[ComboSetID] = @ComboSetID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [ComboDetail]
	WHERE
		[ComboSetID] = @ComboSetID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteComboProductDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteComboProductDetail]
	@ProductID uniqueidentifier = null,
	@IsDefault bit = null,
	@ProductName varchar(50) = null,
	@ComboDetail_Id int = null,
	@Mode varchar(50)
AS
IF(@Mode ='ADD')
Begin
	Insert Into ComboProductDetail
		([ProductID],[IsDefault],[ProductName],[ComboDetail_Id])
	Values
		(@ProductID,@IsDefault,@ProductName,@ComboDetail_Id)
END

IF(@Mode ='UPDATE')
BEGIN
	Update ComboProductDetail
	Set
		[IsDefault] = @IsDefault,
		[ProductName] = @ProductName,
		[ComboDetail_Id] = @ComboDetail_Id
	Where		
			[ProductID] = @ProductID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM ComboProductDetail
	WHERE
		[ProductID] = @ProductID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteCustomer]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteCustomer]
	@CustomerID uniqueidentifier=null,
	@Name varchar(50)=null,
	@MobileNo varchar(50)=null,
	@EmailID varchar(50)=null,
	@Address varchar(150)=null,
	@CardNo varchar(50) = null,
	@ShippingAddress varchar(150)=null,
	@Birthdate varchar(50)=null,
    @RUserID uniqueidentifier=null,
	@RUserType int=null,
	@Mode varchar(50)=null

AS
IF(@Mode ='ADD')
Begin
	INSERT INTO [CustomerMasterData] (
		[CustomerID],
		[Name],
		[MobileNo],
		[EmailID],
		[Address],
		[CardNo],
		[ShippingAddress],
		[Birthdate],
		[RUserID],
		[RUserType])
	VALUES (
		@CustomerID,
		@Name,
		@MobileNo,
		@EmailID,
		@Address,
		@CardNo,
		@ShippingAddress,
		@Birthdate,
		@RUserID,
		@RUserType)
END

IF(@Mode ='UPDATE')
BEGIN
	Update [CustomerMasterData]
	Set
		[Name] = @Name,
		[MobileNo] = @MobileNo,
		[EmailID] = @EmailID,
		[Address] = @Address,
		[CardNo] = @CardNo,
		[ShippingAddress] = @ShippingAddress,
		[Birthdate] = @Birthdate,
		[RUserID] = @RUserID,
		[RUserType] = @RUserType
	Where		
		[CustomerID] = @CustomerID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [CustomerMasterData]
	WHERE
		[CustomerID] = @CustomerID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteDeviceMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteDeviceMaster]
	@DeviceID uniqueidentifier = null,
	@DeviceName varchar(50) = null,
	@DeviceIP varchar(50) = null,
	@DeviceTypeID int = null,
	@DeviceStatus int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into [DeviceMaster]
		([DeviceID],[DeviceName],[DeviceIP],[DeviceTypeID],[DeviceStatus])
	Values
		(@DeviceID,@DeviceName,@DeviceIP,@DeviceTypeID,@DeviceStatus)

END

IF(@Mode ='UPDATE')
BEGIN
	Update [DeviceMaster]
	Set
		[DeviceName] = @DeviceName,
		[DeviceIP] = @DeviceIP,
		[DeviceTypeID] = @DeviceTypeID
	Where		
			[DeviceID] = @DeviceID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [DeviceMaster]
	WHERE
		[DeviceID] = @DeviceID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteDeviceTypeMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteDeviceTypeMaster]
	@DeviceTypeID uniqueidentifier = null,
	@DeviceType varchar(50) = null,
	@DeviceStatus int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into [DeviceTypeMaster]
		([DeviceTypeID],[DeviceType],[DeviceStatus])
	Values
		(@DeviceTypeID,@DeviceType,@DeviceStatus)

END

IF(@Mode ='UPDATE')
BEGIN
	Update [DeviceTypeMaster]
	Set
		[DeviceType] = @DeviceType
	Where		
			[DeviceTypeID] = @DeviceTypeID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [DeviceTypeMaster]
	WHERE
		[DeviceTypeID] = @DeviceTypeID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteDiscountMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteDiscountMasterDetail]
	@DiscountID varchar(50) = null,
	@DiscountName varchar(50) = null,
	@DiscountType int = null,
	@AmountOrPercentage numeric(12,2) = null,
	@QualificationType int = null,
	@IsTaxed bit = null,
	@Barcode varchar(50) = null,
	@DiscountCode varchar(50) = null,
	@PasswordRequired bit = null,
	@DisplayOnPOS bit = null,
	@AutoApply bit = null,
	@DisplayToCustomer bit = null,
	@IsTimeBase bit = null,
	@IsLoyaltyRewards bit = null,
	@DiscountMasterDetail_Id int = null,
	@RootObject_Id int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into [DiscountMasterDetail]
		([DiscountID],[DiscountName],[DiscountType],[AmountOrPercentage],[QualificationType],[IsTaxed],[Barcode],[DiscountCode],[PasswordRequired],[DisplayOnPOS],[AutoApply],[DisplayToCustomer],[IsTimeBase],[IsLoyaltyRewards],[DiscountMasterDetail_Id])
	Values
		(@DiscountID,@DiscountName,@DiscountType,@AmountOrPercentage,@QualificationType,@IsTaxed,@Barcode,@DiscountCode,@PasswordRequired,@DisplayOnPOS,@AutoApply,@DisplayToCustomer,@IsTimeBase,@IsLoyaltyRewards,@DiscountMasterDetail_Id)

END

IF(@Mode ='UPDATE')
BEGIN
	Update [DiscountMasterDetail]
	Set
		[DiscountName] = @DiscountName,
		[DiscountType] = @DiscountType,
		[AmountOrPercentage] = @AmountOrPercentage,
		[QualificationType] = @QualificationType,
		[IsTaxed] = @IsTaxed,
		[Barcode] = @Barcode,
		[DiscountCode] = @DiscountCode,
		[PasswordRequired] = @PasswordRequired,
		[DisplayOnPOS] = @DisplayOnPOS,
		[AutoApply] = @AutoApply,
		[DisplayToCustomer] = @DisplayToCustomer,
		[IsTimeBase] = @IsTimeBase,
		[IsLoyaltyRewards] = @IsLoyaltyRewards,
		[DiscountMasterDetail_Id] = @DiscountMasterDetail_Id
	Where		
			[DiscountID] = @DiscountID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [DiscountMasterDetail]
	WHERE
		[DiscountID] = @DiscountID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteEmployee]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteEmployee]
			@EmployeeID uniqueidentifier = null
           ,@EmpCode varchar(50) = null
           ,@EmpName varchar(50) = null
           ,@Password varchar(50) = null
           ,@Mobile varchar(50) = null
           ,@Email varchar(50) = null
           ,@RepotingTo uniqueidentifier = null
           ,@RoleID uniqueidentifier = null
           ,@RoleName varchar(50) = null
		   ,@SalaryAmt numeric(12,2) = null
           ,@SalaryType int = null
		   ,@ShiftID uniqueidentifier = null
		   ,@Address varchar(50) = null
           ,@JoinDate varchar(50) = null
           ,@IsDisplayInKDS int = null
           ,@ClassID uniqueidentifier = null
           ,@Gender int = null
           ,@TotalHourInADay int = null
		   ,@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	INSERT INTO [EmployeeMasterList]
           ([EmployeeID]
           ,[EmpCode]
           ,[EmpName]
           ,[Password]
           ,[Mobile]
           ,[Email]
           ,[RepotingTo]
           ,[RoleID]
		   ,[RoleName]
           ,[SalaryAmt]
           ,[SalaryType]
		   ,[ShiftID]
		   ,[Address]
           ,[JoinDate]
           ,[IsDisplayInKDS]
           ,[ClassID]
           ,[Gender]
           ,[TotalHourInADay])
     VALUES
           (@EmployeeID
           ,@EmpCode
           ,@EmpName
           ,@Password 
           ,@Mobile 
           ,@Email 
           ,@RepotingTo 
           ,@RoleID 
		   ,@RoleName
           ,@SalaryAmt 
           ,@SalaryType
		   ,@ShiftID
		   ,@Address 
           ,@JoinDate 
           ,@IsDisplayInKDS 
           ,@ClassID 
           ,@Gender 
           ,@TotalHourInADay )
END

IF(@Mode ='UPDATE')
BEGIN
	UPDATE [EmployeeMasterList]
	   SET [EmpCode] = @EmpCode
		  ,[EmpName] = @EmpName
		  ,[Password] = @Password
		  ,[Mobile] = @Mobile
		  ,[Email] = @Email
		  ,[RepotingTo] = @RepotingTo
		  ,[RoleID] = @RoleID
		  ,[RoleName] = @RoleName
		  ,[SalaryAmt] = @SalaryAmt
		  ,[SalaryType] = @SalaryType
		  ,[ShiftID] = @ShiftID
		  ,[Address] = @Address
		  ,[JoinDate] = @JoinDate
		  ,[IsDisplayInKDS] = @IsDisplayInKDS
		  ,[ClassID] = @ClassID
		  ,[Gender] = @Gender
		  ,[TotalHourInADay] = @TotalHourInADay
		Where		
			[EmployeeID] = @EmployeeID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [EmployeeMasterList]
	WHERE
		[EmployeeID] = @EmployeeID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteEmployeeShift]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteEmployeeShift]
		@ShiftID varchar(50),
		@EmployeeID uniqueidentifier,
		@Mode varchar(50)
AS
IF(@Mode ='ADD')
Begin
	Insert Into [EmployeeShift]
		([ShiftID],[EmployeeID])
	Values
		(@ShiftID,@EmployeeID)
END

IF(@Mode ='UPDATE')
BEGIN
	Update [EmployeeShift]
	Set
		[ShiftID] = @ShiftID
	Where		
			[EmployeeID] = @EmployeeID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [EmployeeShift]
	WHERE
		[EmployeeID] = @EmployeeID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteFeatureDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteFeatureDetail]
	@FeatureCode int = null,
	@FeatureName varchar(50) = null,
	@FeatureDetail_Id int = null,
	@RoleMasterDetail_Id int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into FeatureDetail
		([FeatureCode],[FeatureName],[FeatureDetail_Id],[RoleMasterDetail_Id])
	Values
		(@FeatureCode,@FeatureName,@FeatureDetail_Id,@RoleMasterDetail_Id)
END

IF(@Mode ='UPDATE')
BEGIN
	Update FeatureDetail
	Set
		[FeatureCode] = @FeatureCode,
		[FeatureName] = @FeatureName,
		[RoleMasterDetail_Id] = @RoleMasterDetail_Id
	Where		
			[FeatureDetail_Id] = @FeatureDetail_Id
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM FeatureDetail
	WHERE
		[FeatureDetail_Id] = @FeatureDetail_Id
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteGeneralSetting]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteGeneralSetting]
	@PaymentGatewayID uniqueidentifier = null,
	@PaymentGatewayName varchar(50) = null,
	@PrintHeader nvarchar(250) = null,
	@PrintFooter nvarchar(250) = null,
	@DuplicatePrint bit = null,
	@KOTCount int = null,
	@OrderPrefix varchar(50) = null,
	@KOTFontSize int = null,
	@KOTServerName bit = null,
	@KOTDateTime bit = null,
	@KOTOrderType bit = null,
	@KDSWithoutDisplay bit = null,
	@RoundingTotal bit = null,
	@DisplayCardNo bit = null,
	@PrintOnPaymentDone bit = null,
	@RunningOrderDisplayOnKOT bit = null,
	@KDSWithoutPrinter bit = null,
	@CustomerNameOnKOT bit = null,
	@DateTimeFormat varchar(50) = null,
	@Language varchar(50) = null,
	@TillCur1 varchar(50) = null,
	@TillCur2 varchar(50) = null,
	@TillCur3 varchar(50) = null,
	@TillCur4 varchar(50) = null,
	@TillCur5 varchar(50) = null,
	@DineIn bit = null,
	@TakeOut bit = null,
	@Delivery bit = null,
	@OrderAhead bit = null,
	@Queue bit = null,
	@PartyEvent bit = null,
	@TaxLabel1 varchar(50) = null,
	@TaxPercentage1 numeric(12,2) = null,
	@TaxLabel2 varchar(50) = null,
	@TaxPercentage2 numeric(12,2) = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into GeneralSetting
		([PaymentGatewayID],[PaymentGatewayName],[PrintHeader],[PrintFooter],[DuplicatePrint],[KOTCount],[OrderPrefix],
		[KOTFontSize],[KOTServerName],[KOTDateTime],[KOTOrderType],[KDSWithoutDisplay],[RoundingTotal],[DisplayCardNo],
		[PrintOnPaymentDone],[RunningOrderDisplayOnKOT],[KDSWithoutPrinter],[CustomerNameOnKOT],[DateTimeFormat],
		[Language],[TillCur1],[TillCur2],[TillCur3],[TillCur4],[TillCur5],[DineIn],[TakeOut],[Delivery],[OrderAhead],[Queue],
		[PartyEvent],[TaxLabel1],[TaxPercentage1],[TaxLabel2],[TaxPercentage2])
	Values
		(@PaymentGatewayID,@PaymentGatewayName,@PrintHeader,@PrintFooter,@DuplicatePrint,@KOTCount,@OrderPrefix,
		@KOTFontSize,@KOTServerName,@KOTDateTime,@KOTOrderType,@KDSWithoutDisplay,@RoundingTotal,@DisplayCardNo,
		@PrintOnPaymentDone,@RunningOrderDisplayOnKOT,@KDSWithoutPrinter,@CustomerNameOnKOT,@DateTimeFormat,
		@Language,@TillCur1,@TillCur2,@TillCur3,@TillCur4,@TillCur5,@DineIn,@TakeOut,@Delivery,@OrderAhead,@Queue,
		@PartyEvent,@TaxLabel1,@TaxPercentage1,@TaxLabel2,@TaxPercentage2)
END

IF(@Mode ='UPDATE')
BEGIN
	Update GeneralSetting
	Set
		[PaymentGatewayID] = @PaymentGatewayID,
		[PaymentGatewayName] = @PaymentGatewayName,
		[PrintHeader] = @PrintHeader,
		[PrintFooter] = @PrintFooter,
		[DuplicatePrint] = @DuplicatePrint,
		[KOTCount] = @KOTCount,
		[OrderPrefix] = @OrderPrefix,
		[KOTFontSize] = @KOTFontSize,
		[KOTServerName] = @KOTServerName,
		[KOTDateTime] = @KOTDateTime,
		[KOTOrderType] = @KOTOrderType,
		[KDSWithoutDisplay] = @KDSWithoutDisplay,
		[RoundingTotal] = @RoundingTotal,
		[DisplayCardNo] = @DisplayCardNo,
		[PrintOnPaymentDone] = @PrintOnPaymentDone,
		[RunningOrderDisplayOnKOT] = @RunningOrderDisplayOnKOT,
		[KDSWithoutPrinter] = @KDSWithoutPrinter,
		[CustomerNameOnKOT] = @CustomerNameOnKOT,
		[DateTimeFormat] = @DateTimeFormat,
		[Language] = @Language,
		[TillCur1] = @TillCur1,
		[TillCur2] = @TillCur2,
		[TillCur3] = @TillCur3,
		[TillCur4] = @TillCur4,
		[TillCur5] = @TillCur5,
		[DineIn] = @DineIn,
		[TakeOut] = @TakeOut,
		[Delivery] = @Delivery,
		[OrderAhead] = @OrderAhead,
		[Queue] = @Queue,
		[PartyEvent] = @PartyEvent,
		[TaxLabel1] = @TaxLabel1,
		[TaxPercentage1] = @TaxPercentage1,
		[TaxLabel2] = @TaxLabel2,
		[TaxPercentage2] = @TaxPercentage2
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [GeneralSetting]
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteIngredientsMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteIngredientsMasterDetail]
	@IngredientsID uniqueidentifier = null,
	@IngredientName varchar(50) = null,
	@IngredientsMasterDetail_Id int = null,
	@Mode varchar(50)
AS
IF(@Mode ='ADD')
Begin
	Insert Into IngredientsMasterDetail
		([IngredientsID],[IngredientName],[IngredientsMasterDetail_Id])
	Values
		(@IngredientsID,@IngredientName,@IngredientsMasterDetail_Id)
END

IF(@Mode ='UPDATE')
BEGIN
	Update IngredientsMasterDetail
	Set
		[IngredientName] = @IngredientName,
		[IngredientsMasterDetail_Id] = @IngredientsMasterDetail_Id
	Where		
			[IngredientsID] = @IngredientsID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM IngredientsMasterDetail
	WHERE
		[IngredientsID] = @IngredientsID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteIngredientUnitTypeDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteIngredientUnitTypeDetail]
	@UnitTypeID uniqueidentifier = null,
	@UnitType varchar(50) = null,
	@Qty numeric(12,2) = null,
	@IngredientsMasterDetail_Id int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into IngredientUnitTypeDetail
		([UnitTypeID],[UnitType],[Qty],[IngredientsMasterDetail_Id])
	Values
		(@UnitTypeID,@UnitType,@Qty,@IngredientsMasterDetail_Id)
END

IF(@Mode ='UPDATE')
BEGIN
	Update IngredientUnitTypeDetail
	Set
		[UnitType] = @UnitType,
		[Qty] = @Qty,
		[IngredientsMasterDetail_Id] = @IngredientsMasterDetail_Id
	Where		
			[UnitTypeID] = @UnitTypeID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM IngredientUnitTypeDetail
	WHERE
		[UnitTypeID] = @UnitTypeID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteItemChefMapping]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteItemChefMapping]
	@ItemChefMappingID uniqueidentifier = null,
	@CategoryID uniqueidentifier = null,
	@ProductID uniqueidentifier = null,
	@EmployeeID uniqueidentifier = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into [ItemChefMapping]
		([ItemChefMappingID],[CategoryID],[ProductID],[EmployeeID])
	Values
		(@ItemChefMappingID,@CategoryID,@ProductID,@EmployeeID)

END

IF(@Mode ='UPDATE')
BEGIN
	Update [ItemChefMapping]
	Set
		[EmployeeID] = @EmployeeID
	Where		
			[CategoryID] = @CategoryID AND
			[ProductID] = @ProductID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [ItemChefMapping]
	WHERE
		[CategoryID] = @CategoryID AND
			[ProductID] = @ProductID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteLoginResponse]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteLoginResponse]
	@Code varchar(50),
	@Message varchar(50),
	@RootObject_Id int,
	@ErrorMsg varchar(50),
	@ShiftWiseEmployee varchar(50),
	@Mode varchar(50)
AS
IF(@Mode ='ADD')
Begin
	Insert Into LoginResponse
		([Code],[Message],[RootObject_Id],[ErrorMsg],[ShiftWiseEmployee])
	Values
		(@Code,@Message,@RootObject_Id,@ErrorMsg,@ShiftWiseEmployee)
END

IF(@Mode ='UPDATE')
BEGIN
	Update LoginResponse
	Set
		[Code] = @Code,
		[Message] = @Message,
		[RootObject_Id] = @RootObject_Id,
		[ErrorMsg] = @ErrorMsg,
		[ShiftWiseEmployee] = @ShiftWiseEmployee
		Where		
			[Code] = @Code
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [LoginResponse]
	WHERE
		[Code] = @Code
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteMergeTable]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteMergeTable]
	@ID uniqueidentifier = null,
	@OrderID uniqueidentifier = null,
	@TableID uniqueidentifier = null,
	@OldTableID uniqueidentifier = null,
	@TableStatusID int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into [MergeTable]
		([ID],[OrderID],[TableID])
	Values
		(@ID,@OrderID,@TableID)

	Update [TableMasterDetail]
	Set
		[StatusID] = @TableStatusID
	Where
		[TableID] = @TableID
END

IF(@Mode ='UPDATE')
BEGIN
	Update [MergeTable]
	Set
		[TableID] = @TableID
	Where		
			[OrderID] = @OrderID AND [TableID] = @OldTableID

	Update [Order]
	Set
		[TableID] = @TableID
	Where		
			[OrderID] = @OrderID

	Update [TableMasterDetail]
	Set
		[StatusID] = @TableStatusID
	Where
		[TableID] = @TableID

	Update [TableMasterDetail]
	Set
		[StatusID] = 1
	Where
		[TableID] = @OldTableID
END

IF(@Mode ='DELETE_BY_ORDERID')
BEGIN
	DELETE FROM [MergeTable]
	WHERE
		[OrderID] = @OrderID
END

IF(@Mode ='DELETE_BY_ORDERID_TABLEID')
BEGIN
	DELETE FROM [MergeTable]
	WHERE
		[OrderID] = @OrderID AND [TableID] = @TableID

	Update [TableMasterDetail]
	Set
		[StatusID] = @TableStatusID
	Where
		[TableID] = @TableID
END

IF(@Mode ='DELETE_BY_ID')
BEGIN
	DELETE FROM [MergeTable]
	WHERE
		[ID] = @ID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteModifierCategoryDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteModifierCategoryDetail]
	@ModifierCategoryID uniqueidentifier,
	@ModifierCategoryName varchar(50),
	@IsForced bit,
	@ProductID uniqueidentifier,
	@Sort int,
	@ModifierCategoryDetail_Id int,
	@Mode varchar(50)
AS
IF(@Mode ='ADD')
Begin
	Insert Into ModifierCategoryDetail
		([ModifierCategoryID],[ModifierCategoryName],[IsForced],[ProductID],[Sort],[ModifierCategoryDetail_Id])
	Values
		(@ModifierCategoryID,@ModifierCategoryName,@IsForced,@ProductID,@Sort,@ModifierCategoryDetail_Id)

END

IF(@Mode ='UPDATE')
BEGIN
	Update ModifierCategoryDetail
	Set
		[ModifierCategoryName] = @ModifierCategoryName,
		[IsForced] = @IsForced,
		[ProductID] = @ProductID,
		[Sort] = @Sort,
		[ModifierCategoryDetail_Id] = @ModifierCategoryDetail_Id
		Where		
			[ModifierCategoryID] = @ModifierCategoryID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM ModifierCategoryDetail
	WHERE
		[ModifierCategoryID] = @ModifierCategoryID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteModifierDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteModifierDetail]
	@IngredientsID uniqueidentifier = null,
	@Name varchar(50) = null,
	@Qty int = null,
	@Price numeric(12,2) = null,
	@IsDefault bit = null,
	@IsQty bit = null,
	@UnitTypeID uniqueidentifier = null,
	@UnitType varchar(50) = null,
	@Sort int = null,
	@ModifierCategoryDetail_Id int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into ModifierDetail
		([IngredientsID],[Name],[Qty],[Price],[IsDefault],[IsQty],[UnitTypeID],[UnitType],[Sort],[ModifierCategoryDetail_Id])
	Values
		(@IngredientsID,@Name,@Qty,@Price,@IsDefault,@IsQty,@UnitTypeID,@UnitType,@Sort,@ModifierCategoryDetail_Id)

END

IF(@Mode ='UPDATE')
BEGIN
	Update ModifierDetail
	Set
		[Name] = @Name,
		[Qty] = @Qty,
		[Price] = @Price,
		[IsDefault] = @IsDefault,
		[IsQty] = @IsQty,
		[UnitTypeID] = @UnitTypeID,
		[UnitType] = @UnitType,
		[Sort] = @Sort,
		[ModifierCategoryDetail_Id] = @ModifierCategoryDetail_Id
		Where		
			[IngredientsID] = @IngredientsID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM ModifierDetail
	WHERE
		[IngredientsID] = @IngredientsID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteModuleAppIDDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[InsertUpdateDeleteModuleAppIDDetail]
	@AppID varchar(50) = null,
	@DeviceName varchar(50) = null,
	@ModuleMasterDetail_Id int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into ModuleAppIDDetail
		([AppID],[DeviceName],[ModuleMasterDetail_Id])
	Values
		(@AppID,@DeviceName,@ModuleMasterDetail_Id)
END

IF(@Mode ='UPDATE')
BEGIN
	Update ModuleAppIDDetail
	Set
		[DeviceName] = @DeviceName,
		[ModuleMasterDetail_Id] = @ModuleMasterDetail_Id
	Where		
			[AppID] = @AppID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM ModuleAppIDDetail
	WHERE
		[AppID] = @AppID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteModuleMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteModuleMasterDetail]
	@ModuleID uniqueidentifier = null,
	@ModuleName varchar(50) = null,
	@NoOfModule int = null,
	@ModuleMasterDetail_Id int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into ModuleMasterDetail
		([ModuleID],[ModuleName],[NoOfModule],[ModuleMasterDetail_Id])
	Values
		(@ModuleID,@ModuleName,@NoOfModule,@ModuleMasterDetail_Id)
END

IF(@Mode ='UPDATE')
BEGIN
	Update ModuleMasterDetail
	Set
		[ModuleName] = @ModuleName,
		[NoOfModule] = @NoOfModule,
		[ModuleMasterDetail_Id] = @ModuleMasterDetail_Id
	Where		
			[ModuleID] = @ModuleID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM ModuleMasterDetail
	WHERE
		[ModuleID] = @ModuleID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteOrder]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteOrder]
	@OrderID uniqueidentifier = null,
	@OrderNo varchar(15) = null,
	@OrderDate varchar(50) = null,
	@CustomerID uniqueidentifier = null,
	@EmployeeID uniqueidentifier = null,
	@DeliveryType int = null,
	@DeliveryTypeName varchar(50) = null,
	@TableID uniqueidentifier = null,
	@SubTotal numeric(12,2) = null,
	@ExtraCharge numeric(12,2) = null,
	@TaxLabel1 varchar(50) = null,
	@TaxPercent1 numeric(12,2) = null,
	@SGSTAmount numeric(12,2) = null,
	@TaxLabel2 varchar(50) = null,
	@TaxPercent2 numeric(12,2) = null,
	@CGSTAmount numeric(12,2) = null,
	@TotalTax numeric(12,2) = null,
	@DiscountPer numeric(12,2) = null,
	@Discount numeric(12,2) = null,
	@TipGratuity numeric(12,2) = null,
	@PayableAmount numeric(12,2) = null,
	@TableStatusID int = null,
	@OrderActions int = null,
	@OrderSpecialRequest varchar(250) = null,
	@DiscountType int = null,
	@DiscountRemark varchar(50) = null,
	@DeliveryCharge numeric(12,2) = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into [Order]
		([OrderID],[OrderNo],[OrderDate],[EmployeeID],[CustomerID],[DeliveryType],[DeliveryTypeName],[TableID],[SubTotal],[ExtraCharge],[TaxLabel1],[TaxPercent1],[SGSTAmount],[TaxLabel2],[TaxPercent2],[CGSTAmount],[TotalTax],[DiscountPer],[Discount],[TipGratuity],[PayableAmount],[OrderActions],[OrderSpecialRequest],[DiscountType],[DiscountRemark],[DeliveryCharge])
	Values
		(@OrderID,@OrderNo,@OrderDate,@EmployeeID,@CustomerID,@DeliveryType,@DeliveryTypeName,@TableID,@SubTotal,@ExtraCharge,@TaxLabel1,@TaxPercent1,@SGSTAmount,@TaxLabel2,@TaxPercent2,@CGSTAmount,@TotalTax,@DiscountPer,@Discount,@TipGratuity,@PayableAmount,@OrderActions,@OrderSpecialRequest,@DiscountType,@DiscountRemark,@DeliveryCharge)

	IF(@TableID != '00000000-0000-0000-0000-000000000000')
	Begin
		Insert Into [MergeTable]
			([ID],[OrderID],[TableID])
		Values
			(NEWID(),@OrderID,@TableID)
	End

	Update [TableMasterDetail]
	Set
		[StatusID] = @TableStatusID
	Where
		[TableID] = @TableID
END

IF(@Mode ='UPDATE')
BEGIN
	Update [Order]
	Set
		[OrderNo] = @OrderNo,
		[OrderDate] = @OrderDate,
		[EmployeeID] = @EmployeeID,
		[CustomerID] = @CustomerID,
		[DeliveryType] = @DeliveryType,
		[DeliveryTypeName] = @DeliveryTypeName,
		[TableID] = @TableID,
		[SubTotal] = @SubTotal,
		[ExtraCharge] = @ExtraCharge,
		[TaxLabel1] = @TaxLabel1,
		[TaxPercent1] = @TaxPercent1,
		[SGSTAmount] = @SGSTAmount,
		[TaxLabel2] = @TaxLabel2,
		[TaxPercent2] = @TaxPercent2,
		[CGSTAmount] = @CGSTAmount,
		[TotalTax] = @TotalTax,
		[DiscountPer] = @DiscountPer,
		[Discount] = @Discount,
		[TipGratuity] = @TipGratuity,
		[PayableAmount] = @PayableAmount,
		[OrderSpecialRequest] = @OrderSpecialRequest
		--[DeliveryCharge] = @DeliveryCharge
		--[DiscountType] = @DiscountType,
		--[DiscountRemark] = @DiscountRemark
	Where		
			[OrderID] = @OrderID
END

IF(@Mode ='UPDATE_DISCOUNT')
BEGIN
	Update [Order]
	Set
		[SubTotal] = @SubTotal,
		[ExtraCharge] = @ExtraCharge,
		[DiscountPer] = @DiscountPer,
		[Discount] = @Discount,
		[DiscountType] = @DiscountType,
		[TaxLabel1] = @TaxLabel1,
		[TaxPercent1] = @TaxPercent1,
		[SGSTAmount] = @SGSTAmount,
		[TaxLabel2] = @TaxLabel2,
		[TaxPercent2] = @TaxPercent2,
		[CGSTAmount] = @CGSTAmount,
		[TotalTax] = @TotalTax,
		[TipGratuity] = @TipGratuity,
		[PayableAmount] = @PayableAmount,
		[DiscountRemark] = @DiscountRemark,
		[DeliveryCharge] = @DeliveryCharge
	Where		
			[OrderID] = @OrderID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [Order]
	WHERE
		[OrderID] = @OrderID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteOrderTransaction]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteOrderTransaction]
	@TransactionID uniqueidentifier = null,
	@CategoryID uniqueidentifier = null,
	@OrderID uniqueidentifier = null,
	@ProductID uniqueidentifier = null,
	@Quantity int = null,
	@Rate numeric(12,2) = null,
	@TotalAmount numeric(12,2) = null,
	@Sort int = null,
	@SpecialRequest varchar(250) = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into [Transaction]
		([TransactionID],[CategoryID],[OrderID],[ProductID],[Quantity],[Rate],[TotalAmount],[Sort])
	Values
		(@TransactionID,@CategoryID,@OrderID,@ProductID,@Quantity,@Rate,@TotalAmount,@Sort)
END

IF(@Mode ='UPDATE')
BEGIN
	Update [Transaction]
	Set
		[CategoryID] = @CategoryID,
		[ProductID] = @ProductID,
		[Quantity] = @Quantity,
		[Rate] = @Rate,
		[TotalAmount] = @TotalAmount,
		[Sort] = @Sort
	Where		
		[OrderID] = @OrderID AND TransactionID = @TransactionID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [Transaction]
	WHERE
		[OrderID] = @OrderID AND TransactionID = @TransactionID
END

IF(@Mode ='UPDATE_SP_REQ')
BEGIN
	Update [Transaction]
	Set
		[SpecialRequest] = @SpecialRequest
	Where		
		[OrderID] = @OrderID AND TransactionID = @TransactionID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteOrderWiseModifier]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteOrderWiseModifier]
	@ModifierID uniqueidentifier = null,
	@OrderID uniqueidentifier = null,
	@TransactionID uniqueidentifier = null,
	@ProductID uniqueidentifier = null,
	@IngredientsID uniqueidentifier = null,
	@Quantity int = null,
	@Price numeric(12,2) = null,
	@Total numeric(12,2) = null,
	@ModifierOption varchar(50) = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into OrderWiseModifier
		([ModifierID],[OrderID],[TransactionID],[ProductID],[IngredientsID],[Quantity],[Price],[ModifierOption],[Total])
	Values
		(@ModifierID,@OrderID,@TransactionID,@ProductID,@IngredientsID,@Quantity,@Price,@ModifierOption,@Total)
END

IF(@Mode ='UPDATE')
BEGIN
	Update OrderWiseModifier
	Set
		[OrderID] = @OrderID,
		[TransactionID] = @TransactionID,
		[ProductID] = @ProductID,
		[IngredientsID] = @IngredientsID,
		[Quantity] = @Quantity,
		[Price] = @Price,
		[ModifierOption] = @ModifierOption,
		[Total] = @Total
	Where		
			[ModifierID] = @ModifierID
END

IF(@Mode ='UPDATE_QTY')
BEGIN
	Update OrderWiseModifier
	Set
		[Quantity] = @Quantity,
		[Total] = @Total
	Where		
			[OrderID] = @OrderID AND
			[TransactionID] = @TransactionID AND
			[ProductID] = @ProductID AND
			[IngredientsID] = @IngredientsID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM OrderWiseModifier
	WHERE
		[ModifierID] = @ModifierID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeletePaymentGatewayMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeletePaymentGatewayMaster]
	@PaymentGatewayID uniqueidentifier = null,
	@PaymentGatewayName varchar(50) = null,
	@MerchantID varchar(50) = null,
	@TokenKey varchar(MAX) = null,
	@UserName varchar(50) = null,
	@Password varchar(50) = null,
	@ResponseUrl varchar(MAX) = null,
	@ATOMTransactionType varchar(50) = null,
	@Productid varchar(50) = null,
	@Version varchar(50) = null,
	@ServiceID varchar(50) = null,
	@ApplicationProfileId varchar(50) = null,
	@MerchantProfileId varchar(50) = null,
	@MerchantProfileName varchar(50) = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into PaymentGatewayMaster
		([PaymentGatewayID],[PaymentGatewayName],[MerchantID],[TokenKey],[UserName],[Password],[ResponseUrl],[ATOMTransactionType],[Productid],[Version],[ServiceID],[ApplicationProfileId],[MerchantProfileId],[MerchantProfileName])
	Values
		(@PaymentGatewayID,@PaymentGatewayName,@MerchantID,@TokenKey,@UserName,@Password,@ResponseUrl,@ATOMTransactionType,@Productid,@Version,@ServiceID,@ApplicationProfileId,@MerchantProfileId,@MerchantProfileName)

END

IF(@Mode ='UPDATE')
BEGIN
	Update PaymentGatewayMaster
	Set
		[PaymentGatewayName] = @PaymentGatewayName,
		[MerchantID] = @MerchantID,
		[TokenKey] = @TokenKey,
		[UserName] = @UserName,
		[Password] = @Password,
		[ResponseUrl] = @ResponseUrl,
		[ATOMTransactionType] = @ATOMTransactionType,
		[Productid] = @Productid,
		[Version] = @Version,
		[ServiceID] = @ServiceID,
		[ApplicationProfileId] = @ApplicationProfileId,
		[MerchantProfileId] = @MerchantProfileId,
		[MerchantProfileName] = @MerchantProfileName
	Where		
		[PaymentGatewayID] = @PaymentGatewayID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM PaymentGatewayMaster
	WHERE
		[PaymentGatewayID] = @PaymentGatewayID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeletePrinterMapping]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeletePrinterMapping]
	@PrinterMappingID uniqueidentifier = null,
	@DeviceID uniqueidentifier = null,
	@PrinterID uniqueidentifier = null,
	@PartID int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into [PrinterMapping]
		([PrinterMappingID],[DeviceID],[PrinterID],[PartID])
	Values
		(@PrinterMappingID,@DeviceID,@PrinterID,@PartID)

END

IF(@Mode ='UPDATE')
BEGIN
	Update [PrinterMapping]
	Set
		[PrinterID] = @PrinterID
	Where		
			[DeviceID] = @DeviceID AND 
			[PartID] = @PartID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [PrinterMapping]
	WHERE
		[DeviceID] = @DeviceID AND 
			[PartID] = @PartID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteProduct]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteProduct]
	@DiscountID uniqueidentifier = null,
	@ProductID uniqueidentifier = null,
	@CategoryID uniqueidentifier = null,
	@ProductName varchar(200) = null,
	@Price numeric(12,2) = null,
	@ImgPath varchar(MAX) = null,
	@IsUrl bit = null,
	@Calorie varchar(50) = null,
	@ShortDescription varchar(MAX) = null,
	@IsNonVeg bit = null,
	@IsTrendingItem bit = null,
	@ApproxCookingTime varchar(50) = null,
	@IsAellergic bit = null,
	@Extras varchar(50) = null,
	@IsVisibleToB2C bit = null,
	@ExpiryDateFrom varchar(50) = null,
	@ExpiryDateTo varchar(50) = null,
	@StationID uniqueidentifier = null,
	@SuggestiveItems varchar(250) = null,
	@IsCold bit = null,
	@IsDrink bit = null,
	@DiningOptions int = null,
	@AllowPriceOverride int = null,
	@IsAgeValidation bit = null,
	@AgeForValidation int = null,
	@OverridePrice numeric(12,2) = null,
	@IsCombo bit = null,
	@IsDisplayModifire bit = null,
	@ProductCode varchar(50) = null,
	@TaxPercentage numeric(12,2) = null,
	@Sort int = null,
	@Priority int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into CategoryWiseProduct
		([DiscountID],[ProductID],[CategoryID],[ProductName],[Price],[ImgPath],[IsUrl],[Calorie],[ShortDescription],[IsNonVeg],[IsTrendingItem],[ApproxCookingTime],[IsAellergic],[Extras],[IsVisibleToB2C],[ExpiryDateFrom],[ExpiryDateTo],[StationID],[SuggestiveItems],[IsCold],[IsDrink],[DiningOptions],[AllowPriceOverride],[IsAgeValidation],[AgeForValidation],[OverridePrice],[IsCombo],[IsDisplayModifire],[ProductCode],[TaxPercentage],[Sort],[Priority])
	Values
		(@DiscountID,@ProductID,@CategoryID,@ProductName,@Price,@ImgPath,@IsUrl,@Calorie,@ShortDescription,@IsNonVeg,@IsTrendingItem,@ApproxCookingTime,@IsAellergic,@Extras,@IsVisibleToB2C,@ExpiryDateFrom,@ExpiryDateTo,@StationID,@SuggestiveItems,@IsCold,@IsDrink,@DiningOptions,@AllowPriceOverride,@IsAgeValidation,@AgeForValidation,@OverridePrice,@IsCombo,@IsDisplayModifire,@ProductCode,@TaxPercentage,@Sort,@Priority)

END

IF(@Mode ='UPDATE')
BEGIN
	Update CategoryWiseProduct
	Set
		[DiscountID] = @DiscountID,
		[ProductName] = @ProductName,
		[Price] = @Price,
		[ImgPath] = @ImgPath,
		[IsUrl] = @IsUrl,
		[Calorie] = @Calorie,
		[ShortDescription] = @ShortDescription,
		[IsNonVeg] = @IsNonVeg,
		[IsTrendingItem] = @IsTrendingItem,
		[ApproxCookingTime] = @ApproxCookingTime,
		[IsAellergic] = @IsAellergic,
		[Extras] = @Extras,
		[IsVisibleToB2C] = @IsVisibleToB2C,
		[ExpiryDateFrom] = @ExpiryDateFrom,
		[ExpiryDateTo] = @ExpiryDateTo,
		[StationID] = @StationID,
		[SuggestiveItems] = @SuggestiveItems,
		[IsCold] = @IsCold,
		[IsDrink] = @IsDrink,
		[DiningOptions] = @DiningOptions,
		[AllowPriceOverride] = @AllowPriceOverride,
		[IsAgeValidation] = @IsAgeValidation,
		[AgeForValidation] = @AgeForValidation,
		[OverridePrice] = @OverridePrice,
		[IsCombo] = @IsCombo,
		[IsDisplayModifire] = @IsDisplayModifire,
		[ProductCode] = @ProductCode,
		[TaxPercentage] = @TaxPercentage,
		[Sort] = @Sort,
		[Priority] = @Priority
	Where		
			[ProductID] = @ProductID AND
			[CategoryID] = @CategoryID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [CategoryWiseProduct]
	WHERE
		[ProductID] = @ProductID AND
		[CategoryID] = @CategoryID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteProductClassMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteProductClassMasterDetail]
	@ClassID uniqueidentifier = null,
	@ClassName varchar(50) = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into ProductClassMasterDetail
		([ClassID],[ClassName])
	Values
		(@ClassID,@ClassName)
END

IF(@Mode ='UPDATE')
BEGIN
	Update ProductClassMasterDetail
	Set
		[ClassName] = @ClassName
	Where		
		[ClassID] = @ClassID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM ProductClassMasterDetail
	WHERE
		[ClassID] = @ClassID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteRecipeMasterData]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteRecipeMasterData]
	@ProductID uniqueidentifier = null,
	@ProductName varchar(250) = null,
	@RecipeID uniqueidentifier = null,
	@RecipeText varchar(MAX) = null,
	@RecipeMasterData_Id int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into RecipeMasterData
		([ProductID],[ProductName],[RecipeID],[RecipeText],[RecipeMasterData_Id])
	Values
		(@ProductID,@ProductName,@RecipeID,@RecipeText,@RecipeMasterData_Id)
END

IF(@Mode ='UPDATE')
BEGIN
	Update RecipeMasterData
	Set
		[ProductID] = @ProductID,
		[ProductName] = @ProductName,
		[RecipeText] = @RecipeText,
		[RecipeMasterData_Id] = @RecipeMasterData_Id
	Where		
			[RecipeID] = @RecipeID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM RecipeMasterData
	WHERE
		[RecipeID] = @RecipeID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteRecipeMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteRecipeMasterDetail]
	@IngredientsID uniqueidentifier = null,
	@Name varchar(50) = null,
	@Qty int = null,
	@Price numeric(12,2) = null,
	@IsDefault bit = null,
	@IsQty bit = null,
	@UnitTypeID uniqueidentifier = null,
	@UnitType varchar(50) = null,
	@RecipeMasterData_Id int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into RecipeMasterDetail
		([IngredientsID],[Name],[Qty],[Price],[IsDefault],[IsQty],[UnitTypeID],[UnitType],[RecipeMasterData_Id])
	Values
		(@IngredientsID,@Name,@Qty,@Price,@IsDefault,@IsQty,@UnitTypeID,@UnitType,@RecipeMasterData_Id)

END

IF(@Mode ='UPDATE')
BEGIN
	Update RecipeMasterDetail
	Set
		[Name] = @Name,
		[Qty] = @Qty,
		[Price] = @Price,
		[IsDefault] = @IsDefault,
		[IsQty] = @IsQty,
		[UnitTypeID] = @UnitTypeID,
		[UnitType] = @UnitType,
		[RecipeMasterData_Id] = @RecipeMasterData_Id
		Where		
			[IngredientsID] = @IngredientsID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM RecipeMasterDetail
	WHERE
		[IngredientsID] = @IngredientsID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteRightDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteRightDetail]
	@RightCode int = null,
	@RightName varchar(50) = null,
	@SubFeatureDetail_Id int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into RightDetail
		([RightCode],[RightName],[SubFeatureDetail_Id])
	Values
		(@RightCode,@RightName,@SubFeatureDetail_Id)
END

IF(@Mode ='UPDATE')
BEGIN
	Update RightDetail
	Set
		[RightName] = @RightName
	Where		
			[RightCode] = @RightCode AND [SubFeatureDetail_Id] = @SubFeatureDetail_Id
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM RightDetail
	WHERE
		[RightCode] = @RightCode AND [SubFeatureDetail_Id] = @SubFeatureDetail_Id
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteRoleMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteRoleMasterDetail]
	@RoleID uniqueidentifier = null,
	@RoleName varchar(50) = null,
	@RoleMasterDetail_Id int = null,
	@ModuleMasterDetail_Id int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into RoleMasterDetail
		([RoleID],[RoleName],[RoleMasterDetail_Id],[ModuleMasterDetail_Id])
	Values
		(@RoleID,@RoleName,@RoleMasterDetail_Id,@ModuleMasterDetail_Id)
END

IF(@Mode ='UPDATE')
BEGIN
	Update RoleMasterDetail
	Set
		[RoleName] = @RoleName,
		[RoleMasterDetail_Id] = @RoleMasterDetail_Id,
		[ModuleMasterDetail_Id] = @ModuleMasterDetail_Id
	Where		
			[RoleID] = @RoleID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM RoleMasterDetail
	WHERE
		[RoleID] = @RoleID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteShiftMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteShiftMaster]
	@ShiftID uniqueidentifier = null,
	@ShiftName varchar(50) = null,
	@ShiftMaster_Id int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into ShiftMaster
		([ShiftID],[ShiftName],[ShiftMaster_Id])
	Values
		(@ShiftID,@ShiftName,@ShiftMaster_Id)
END

IF(@Mode ='UPDATE')
BEGIN
	Update ShiftMaster
	Set
		[ShiftName] = @ShiftName,
		[ShiftMaster_Id] = @ShiftMaster_Id
	Where		
			[ShiftID] = @ShiftID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM ShiftMaster
	WHERE
		[ShiftID] = @ShiftID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteShiftMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteShiftMasterDetail]
	@ShiftDetailsID uniqueidentifier = null,
	@ShiftFromTime time = null,
	@ShiftToTime time = null,
	@ShiftDay int = null,
	@FirstSlot numeric(12,2) = null,
	@SecondSlot numeric(12,2) = null,
	@FinalSlot numeric(12,2) = null,
	@ShiftDiff varchar(50) = null,
	@ShiftMaster_ID int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into ShiftMasterDetail
		([ShiftDetailsID],[ShiftFromTime],[ShiftToTime],[ShiftDay],[FirstSlot],[SecondSlot],[FinalSlot],[ShiftDiff],[ShiftMaster_ID])
	Values
		(@ShiftDetailsID,@ShiftFromTime,@ShiftToTime,@ShiftDay,@FirstSlot,@SecondSlot,@FinalSlot,@ShiftDiff,@ShiftMaster_ID)

END

IF(@Mode ='UPDATE')
BEGIN
	Update ShiftMasterDetail
	Set
		[ShiftFromTime] = @ShiftFromTime,
		[ShiftToTime] = @ShiftToTime,
		[ShiftDay] = @ShiftDay,
		[FirstSlot] = @FirstSlot,
		[SecondSlot] = @SecondSlot,
		[FinalSlot] = @FinalSlot,
		[ShiftDiff] = @ShiftDiff,
		[ShiftMaster_ID] = @ShiftMaster_ID
	Where		
			[ShiftDetailsID] = @ShiftDetailsID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM ShiftMasterDetail
	WHERE
		[ShiftDetailsID] = @ShiftDetailsID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteSubFeatureDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteSubFeatureDetail]
	@SubFeatureCode int = null,
	@SubFeatureName varchar(50) = null,
	@SubFeatureDetail_Id int = null,
	@FeatureDetail_Id int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into SubFeatureDetail
		([SubFeatureCode],[SubFeatureName],[SubFeatureDetail_Id],[FeatureDetail_Id])
	Values
		(@SubFeatureCode,@SubFeatureName,@SubFeatureDetail_Id,@FeatureDetail_Id)
END

IF(@Mode ='UPDATE')
BEGIN
	Update SubFeatureDetail
	Set
		[SubFeatureCode] = @SubFeatureCode,
		[SubFeatureName] = @SubFeatureName,
		[FeatureDetail_Id] = @FeatureDetail_Id
	Where		
			[SubFeatureDetail_Id] = @SubFeatureDetail_Id
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM SubFeatureDetail
	WHERE
		[SubFeatureDetail_Id] = @SubFeatureDetail_Id
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteTableMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteTableMasterDetail]
	@TableID uniqueidentifier = null,
	@EmployeeID uniqueidentifier = null,
	@TableName varchar(50) = null,
	@NoOfSeats int = null,
	@Location varchar(50) = null,
	@ClassID uniqueidentifier = null,
	@Sort int = null,
	@StatusID int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into TableMasterDetail
		([TableID],[EmployeeID],[TableName],[NoOfSeats],[Location],[ClassID],[Sort],[StatusID])
	Values
		(@TableID,@EmployeeID,@TableName,@NoOfSeats,@Location,@ClassID,@Sort,@StatusID)
END

IF(@Mode ='UPDATE')
BEGIN
	Update TableMasterDetail
		Set
			[EmployeeID] = @EmployeeID,
			[TableName] = @TableName,
			[NoOfSeats] = @NoOfSeats,
			[Location] = @Location,
			[ClassID] = @ClassID,
			[Sort] = @Sort,
			[StatusID] = @StatusID
		Where		
			[TableID] = @TableID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM TableMasterDetail
	WHERE
		[TableID] = @TableID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteTaxGroupDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteTaxGroupDetail]
	@TaxGroupID uniqueidentifier = null,
	@Name varchar(50) = null,
	@Percentage numeric(12,2) = null,
	@ParentID uniqueidentifier = null,
	@PartnerID uniqueidentifier = null,
	@Action varchar(50) = null,
	@Sign varchar(50) = null,
	@Sort int = null,
	@TaxOnProductType int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into TaxGroupDetail
		([TaxGroupID],[Name],[Percentage],[ParentID],[PartnerID],[Action],[Sign],[Sort],[TaxOnProductType])
	Values
		(@TaxGroupID,@Name,@Percentage,@ParentID,@PartnerID,@Action,@Sign,@Sort,@TaxOnProductType)
END

IF(@Mode ='UPDATE')
BEGIN
	Update TaxGroupDetail
	Set
		[Name] = @Name,
		[Percentage] = @Percentage,
		[ParentID] = @ParentID,
		[PartnerID] = @PartnerID,
		[Action] = @Action,
		[Sign] = @Sign,
		[Sort] = @Sort,
		[TaxOnProductType] = @TaxOnProductType
	Where		
			[TaxGroupID] = @TaxGroupID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [TaxGroupDetail]
	WHERE
		[TaxGroupID] = @TaxGroupID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteTillManage]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteTillManage]
	@TillID uniqueidentifier = null,
	@EmployeeID uniqueidentifier = null,
	@PayIn numeric(12,2) = null,
	@PayOut numeric(12,2) = null,
	@Cash numeric(12,2) = null,
	@Currency5 int = null,
	@Currency10 int = null,
	@Currency20 int = null,
	@Currency50 int = null,
	@Currency100 int = null,
	@Currency200 int = null,
	@Currency500 int = null,
	@Currency1000 int = null,
	@Currency2000 int = null,
	@StartCash numeric(12,2) = null,
	@ExpectedCash numeric(12,2) = null,
	@EndCash numeric(12,2) = null,
	@Difference numeric(12,2) = null,
	@StartDateTime datetime = null,
	@EndDateTime datetime = null,
	@EnrtyDate datetime = null,
	@IsTillDone bit = null,
	@UpStreamStatus bit = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into TillManage
		([TillID],[EmployeeID],[PayIn],[PayOut],[Cash],[Currency5],[Currency10],[Currency20],[Currency50],[Currency100],[Currency200],[Currency500],[Currency1000],[Currency2000],[StartCash],[ExpectedCash],[EndCash],[Difference],[StartDateTime],[EndDateTime],[EnrtyDate],[IsTillDone],[UpStreamStatus])
	Values
		(@TillID,@EmployeeID,@PayIn,@PayOut,@Cash,@Currency5,@Currency10,@Currency20,@Currency50,@Currency100,@Currency200,@Currency500,@Currency1000,@Currency2000,@StartCash,@ExpectedCash,@EndCash,@Difference,@StartDateTime,@EndDateTime,@EnrtyDate,@IsTillDone,@UpStreamStatus)
END

IF(@Mode ='UPDATE')
BEGIN
	Update TillManage
	Set
		[StartCash] = @StartCash,
		[StartDateTime] = @StartDateTime,
		[EnrtyDate] = @EnrtyDate,
		[IsTillDone] = @IsTillDone
	Where		
			[TillID] = @TillID AND [EmployeeID] = @EmployeeID

	Update TillManage
	Set
		[Difference] = (StartCash+PayIn) - PayOut
	Where [TillID] = @TillID and EmployeeID = @EmployeeID
END

IF(@Mode ='UPDATE_CHECKOUT')
BEGIN
	Update TillManage
	Set
		[Currency5] = @Currency5,
		[Currency10] = @Currency10,
		[Currency20] = @Currency20,
		[Currency50] = @Currency50,
		[Currency100] = @Currency100,
		[Currency200] = @Currency200,
		[Currency500] = @Currency500,
		[Currency1000] = @Currency1000,
		[Currency2000] = @Currency2000,
		[EndCash] = @EndCash,
		[EndDateTime] = @EndDateTime,
		[IsTillDone] = @IsTillDone
	Where		
			[TillID] = @TillID AND [EmployeeID] = @EmployeeID

	Update TillManage
	Set
		[Difference] = (StartCash+PayIn) - PayOut
	Where [TillID] = @TillID and EmployeeID = @EmployeeID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM TillManage
	WHERE
		[TillID] = @TillID AND [EmployeeID] = @EmployeeID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteTillPayIn]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteTillPayIn]
	@PayInID uniqueidentifier,
	@TillID uniqueidentifier = null,
	@EmployeeID uniqueidentifier,
	@Amount numeric(12,2),
	@Reason varchar(150),
	@EntryDateTime datetime = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
		Insert Into TillPayIn
		([PayInID],[TillID],[EmployeeID],[Amount],[Reason],[EntryDateTime] )
	Values
		(@PayInID,@TillID,@EmployeeID,@Amount,@Reason,@EntryDateTime)

	Update TillManage
		Set
			PayIn = (select sum([Amount]) as a from TillPayIn where TillID = @TillID and EmployeeID = @EmployeeID)
		Where [TillID] = @TillID and EmployeeID = @EmployeeID

	Update TillManage
		Set
			[Difference] = (StartCash+PayIn) - PayOut
		Where [TillID] = @TillID and EmployeeID = @EmployeeID
END

IF(@Mode ='UPDATE')
BEGIN
	Update TillPayIn
	Set
		[TillID] = @TillID,
		[Amount] = @Amount,
		[Reason] = @Reason,
		[EntryDateTime] = @EntryDateTime
	Where		
			[PayInID] = @PayInID and [EmployeeID] = @EmployeeID

	Update TillManage
		Set
			PayIn = (select sum([Amount]) as a from TillPayIn where TillID = @TillID and EmployeeID = @EmployeeID)
		Where [TillID] = @TillID and EmployeeID = @EmployeeID

	Update TillManage
		Set
			[Difference] = (StartCash+PayIn) - PayOut
		Where [TillID] = @TillID and EmployeeID = @EmployeeID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM TillPayIn
	WHERE
		[PayInID] = @PayInID and [EmployeeID] = @EmployeeID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteTillPayOut]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteTillPayOut]
	@PayOutID uniqueidentifier = null,
	@TillID uniqueidentifier = null,
	@EmployeeID uniqueidentifier = null,
	@Amount numeric(12,2) = null,
	@Reason varchar(150) = null,
	@EntryDateTime datetime = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
		Insert Into TillPayOut
			([PayOutID],[TillID],[EmployeeID],[Amount],[Reason],[EntryDateTime])
		Values
			(@PayOutID,@TillID,@EmployeeID,@Amount,@Reason,@EntryDateTime)

		Update TillManage
		Set
			PayOut = (select sum([Amount]) as a from TillPayOut where TillID = @TillID and EmployeeID = @EmployeeID)
		Where [TillID] = @TillID and EmployeeID = @EmployeeID

		Update TillManage
		Set
			[Difference] = (StartCash+PayIn) - PayOut
		Where [TillID] = @TillID and EmployeeID = @EmployeeID
END

IF(@Mode ='UPDATE')
BEGIN
	Update TillPayOut
	Set
		[TillID] = @TillID,
		[Amount] = @Amount,
		[Reason] = @Reason,
		[EntryDateTime] = @EntryDateTime
	Where		
			[PayOutID] = @PayOutID AND [EmployeeID] = @EmployeeID

	Update TillManage
		Set
			PayOut = (select sum([Amount]) as a from TillPayOut where TillID = @TillID and EmployeeID = @EmployeeID)
		Where [TillID] = @TillID and EmployeeID = @EmployeeID

	Update TillManage
		Set
			[Difference] = (StartCash+PayIn) - PayOut
		Where [TillID] = @TillID and EmployeeID = @EmployeeID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM TillPayOut
	WHERE
		[PayOutID] = @PayOutID AND [EmployeeID] = @EmployeeID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteTimeSheetWiseDiscount]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteTimeSheetWiseDiscount]
	@FromTime varchar(50) = null,
	@ToTime varchar(50) = null,
	@StartDate varchar(50) = null,
	@EndDate varchar(50) = null,
	@Day int = null,
	@DiscountMasterDetail_Id int = null,
	@Mode varchar(50) = null
AS
IF(@Mode ='ADD')
Begin
	Insert Into TimeSheetWiseDiscount
		([FromTime],[ToTime],[StartDate],[EndDate],[Day],[DiscountMasterDetail_Id])
	Values
		(@FromTime,@ToTime,@StartDate,@EndDate,@Day,@DiscountMasterDetail_Id)
END

IF(@Mode ='UPDATE')
BEGIN
	Update TimeSheetWiseDiscount
	Set
		[FromTime] = @FromTime,
		[ToTime] = @ToTime,
		[StartDate] = @StartDate,
		[EndDate] = @EndDate,
		[Day] = @Day		
	Where		
			[DiscountMasterDetail_Id] = @DiscountMasterDetail_Id
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [TimeSheetWiseDiscount]
	WHERE
		[DiscountMasterDetail_Id] = @DiscountMasterDetail_Id
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteVendorMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteVendorMaster]
	@VendorID uniqueidentifier,
	@VendorName varchar(50),
	@Mode varchar(50)
AS
IF(@Mode ='ADD')
Begin
	Insert Into VendorMasterData
		([VendorID],[VendorName])
	Values
		(@VendorID,@VendorName)
END

IF(@Mode ='UPDATE')
BEGIN
	Update VendorMasterData
	Set
		[VendorName] = @VendorName
	Where		
		[VendorID] = @VendorID
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [VendorMasterData]
	WHERE
		[VendorID] = @VendorID
END


GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateDeleteVersionDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateDeleteVersionDetail]
	@Version_Code varchar(50),
	@IsMandtory bit,
	@Mode varchar(50)
AS
IF(@Mode ='ADD')
Begin
	Insert Into [VersionDetail]
		([Version_Code],[IsMandtory])
	Values
		(@Version_Code,@IsMandtory)
END

IF(@Mode ='UPDATE')
BEGIN
	Update [VersionDetail]
	Set
		[IsMandtory] = @IsMandtory
	Where		
			[Version_Code] = @Version_Code
END

IF(@Mode ='DELETE')
BEGIN
	DELETE FROM [VersionDetail]
	WHERE
		[Version_Code] = @Version_Code
END


GO
/****** Object:  Table [dbo].[BranchMasterSetting]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BranchMasterSetting](
	[BranchID] [uniqueidentifier] NULL,
	[RestaurantID] [uniqueidentifier] NULL,
	[ContactNoForService] [varchar](50) NULL,
	[DeliveryCharges] [int] NULL,
	[DeliveryTime] [time](7) NULL,
	[PickupTime] [time](7) NULL,
	[CurrencyName] [varchar](50) NULL,
	[CurrencySymbol] [varchar](50) NULL,
	[WorkingDays] [varchar](50) NULL,
	[TagLine] [varchar](50) NULL,
	[Footer] [varchar](50) NULL,
	[DeliveryAreaRedius] [int] NULL,
	[DeliveryAreaTitle] [varchar](50) NULL,
	[DistanceType] [int] NULL,
	[DistanceName] [varchar](50) NULL,
	[FreeDeliveryUpto] [int] NULL,
	[BranchName] [varchar](50) NULL,
	[BranchEmailID] [varchar](50) NULL,
	[MobileNo] [varchar](50) NULL,
	[LastSyncDate] [datetime] NULL,
	[VatNo] [varchar](50) NULL,
	[CSTNo] [varchar](50) NULL,
	[ServiceTaxNo] [varchar](50) NULL,
	[TinGSTNo] [varchar](50) NULL,
	[Address] [varchar](250) NULL,
	[SubAreaStreet] [varchar](50) NULL,
	[PinCode] [varchar](50) NULL,
	[VersionCode] [varchar](50) NULL,
	[BranchMasterSetting_Id] [int] NULL,
	[RootObject_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BranchSettingDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BranchSettingDetail](
	[IsFranchise] [bit] NULL,
	[IsReservationOn] [bit] NULL,
	[IsOrderBookingOn] [bit] NULL,
	[IsAutoAcceptOrderOn] [bit] NULL,
	[IsAutoRoundOffTotalOn] [bit] NULL,
	[TaxGroupId] [int] NULL,
	[IsDemoVersion] [bit] NULL,
	[ExpireDate] [varchar](100) NULL,
	[BranchMasterSetting_Id] [int] NULL,
	[BranchID] [uniqueidentifier] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CategoryMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CategoryMaster](
	[CategoryID] [uniqueidentifier] NOT NULL,
	[CategoryName] [varchar](50) NULL,
	[ImgPath] [varchar](max) NULL,
	[ParentID] [uniqueidentifier] NULL,
	[ClassMasterID] [uniqueidentifier] NULL,
	[Priority] [int] NULL,
	[IsCategory] [int] NULL,
 CONSTRAINT [PK_CategoryMaster] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CategoryWiseProduct]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CategoryWiseProduct](
	[DiscountID] [uniqueidentifier] NULL,
	[ProductID] [uniqueidentifier] NULL,
	[CategoryID] [uniqueidentifier] NULL,
	[ProductName] [varchar](200) NULL,
	[Price] [numeric](12, 2) NULL,
	[ImgPath] [varchar](max) NULL,
	[IsUrl] [bit] NULL,
	[Calorie] [varchar](50) NULL,
	[ShortDescription] [varchar](max) NULL,
	[IsNonVeg] [bit] NULL,
	[IsTrendingItem] [bit] NULL,
	[ApproxCookingTime] [time](7) NULL,
	[IsAellergic] [bit] NULL,
	[Extras] [time](7) NULL,
	[IsVisibleToB2C] [bit] NULL,
	[ExpiryDateFrom] [datetime] NULL,
	[ExpiryDateTo] [datetime] NULL,
	[StationID] [uniqueidentifier] NULL,
	[SuggestiveItems] [varchar](250) NULL,
	[IsCold] [bit] NULL,
	[IsDrink] [bit] NULL,
	[DiningOptions] [int] NULL,
	[AllowPriceOverride] [int] NULL,
	[IsAgeValidation] [bit] NULL,
	[AgeForValidation] [int] NULL,
	[OverridePrice] [numeric](12, 2) NULL,
	[IsCombo] [bit] NULL,
	[IsDisplayModifire] [bit] NULL,
	[ProductCode] [varchar](50) NULL,
	[TaxPercentage] [numeric](12, 2) NULL,
	[Sort] [int] NULL,
	[Priority] [int] NULL,
	[CategoryWiseProduct_Id] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CheckOutDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CheckOutDetail](
	[TransactionID] [uniqueidentifier] NULL,
	[OrderID] [uniqueidentifier] NULL,
	[CustomerID] [uniqueidentifier] NULL,
	[TableID] [uniqueidentifier] NULL,
	[CRMMethod] [int] NULL,
	[PaymentMethod] [int] NULL,
	[OrderAmount] [numeric](12, 2) NULL,
	[PaidAmount] [numeric](12, 2) NULL,
	[ChangeAmount] [numeric](12, 2) NULL,
	[ChequeNo] [varchar](25) NULL,
	[ChequeDate] [datetime] NULL,
	[CardHolderName] [varchar](50) NULL,
	[CardNumber] [varchar](50) NULL,
	[EntryDateTime] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChefKDSMapping]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChefKDSMapping](
	[ChefKDSMappingID] [uniqueidentifier] NULL,
	[EmployeeID] [uniqueidentifier] NULL,
	[DeviceID] [uniqueidentifier] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ComboDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ComboDetail](
	[ComboSetID] [uniqueidentifier] NULL,
	[ComboSetName] [varchar](100) NULL,
	[CategoryID] [uniqueidentifier] NULL,
	[CProductID] [uniqueidentifier] NULL,
	[ComboDetail_Id] [int] NULL,
	[CategoryWiseProduct_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ComboProductDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ComboProductDetail](
	[ProductID] [uniqueidentifier] NULL,
	[IsDefault] [bit] NULL,
	[ProductName] [varchar](50) NULL,
	[ComboDetail_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CustomerMasterData]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerMasterData](
	[CustomerID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NULL,
	[MobileNo] [varchar](50) NULL,
	[EmailID] [varchar](50) NULL,
	[Birthdate] [datetime] NULL,
	[Address] [varchar](150) NULL,
	[CardNo] [varchar](50) NULL,
	[ShippingAddress] [varchar](150) NULL,
	[RUserID] [uniqueidentifier] NULL,
	[RUserType] [int] NULL,
	[RootObject_Id] [int] NULL,
 CONSTRAINT [PK_CustomerMasterData] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DeviceMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DeviceMaster](
	[DeviceID] [uniqueidentifier] NULL,
	[DeviceName] [varchar](50) NULL,
	[DeviceIP] [varchar](50) NULL,
	[DeviceTypeID] [int] NULL,
	[DeviceStatus] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DeviceTypeMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DeviceTypeMaster](
	[DeviceTypeID] [uniqueidentifier] NULL,
	[DeviceType] [varchar](50) NULL,
	[DeviceStatus] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DiscountMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DiscountMasterDetail](
	[DiscountID] [uniqueidentifier] NULL,
	[DiscountName] [varchar](50) NULL,
	[DiscountType] [int] NULL,
	[AmountOrPercentage] [numeric](12, 2) NULL,
	[QualificationType] [int] NULL,
	[IsTaxed] [bit] NULL,
	[Barcode] [varchar](50) NULL,
	[DiscountCode] [varchar](50) NULL,
	[PasswordRequired] [bit] NULL,
	[DisplayOnPOS] [bit] NULL,
	[AutoApply] [bit] NULL,
	[DisplayToCustomer] [bit] NULL,
	[IsTimeBase] [bit] NULL,
	[IsLoyaltyRewards] [bit] NULL,
	[DiscountMasterDetail_Id] [int] NULL,
	[RootObject_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmployeeMasterList]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmployeeMasterList](
	[EmployeeID] [uniqueidentifier] NOT NULL,
	[EmpCode] [varchar](50) NULL,
	[EmpName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Mobile] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[RepotingTo] [uniqueidentifier] NULL,
	[RoleID] [uniqueidentifier] NULL,
	[RoleName] [varchar](50) NULL,
	[SalaryAmt] [numeric](12, 2) NULL,
	[SalaryType] [int] NULL,
	[Address] [varchar](250) NULL,
	[JoinDate] [datetime] NULL,
	[IsDisplayInKDS] [int] NULL,
	[ClassID] [uniqueidentifier] NULL,
	[ShiftID] [uniqueidentifier] NULL,
	[Gender] [int] NULL,
	[TotalHourInADay] [int] NULL,
	[EmployeeMasterList_Id] [int] NULL,
	[RootObject_Id] [int] NULL,
 CONSTRAINT [PK_EmployeeMasterList] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmployeeShift]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeShift](
	[ShiftID] [uniqueidentifier] NULL,
	[EmployeeMasterList_Id] [int] NULL,
	[EmployeeID] [uniqueidentifier] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FeatureDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FeatureDetail](
	[FeatureCode] [int] NULL,
	[FeatureName] [varchar](50) NULL,
	[FeatureDetail_Id] [int] NULL,
	[RoleMasterDetail_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GeneralSetting]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GeneralSetting](
	[PaymentGatewayID] [uniqueidentifier] NULL,
	[PaymentGatewayName] [varchar](50) NULL,
	[PrintHeader] [nvarchar](250) NULL,
	[PrintFooter] [nvarchar](250) NULL,
	[DuplicatePrint] [bit] NULL,
	[KOTCount] [int] NULL,
	[OrderPrefix] [varchar](50) NULL,
	[KOTFontSize] [int] NULL,
	[KOTServerName] [bit] NULL,
	[KOTDateTime] [bit] NULL,
	[KOTOrderType] [bit] NULL,
	[KDSWithoutDisplay] [bit] NULL,
	[RoundingTotal] [bit] NULL,
	[DisplayCardNo] [bit] NULL,
	[PrintOnPaymentDone] [bit] NULL,
	[RunningOrderDisplayOnKOT] [bit] NULL,
	[KDSWithoutPrinter] [bit] NULL,
	[CustomerNameOnKOT] [bit] NULL,
	[DateTimeFormat] [varchar](50) NULL,
	[Language] [varchar](50) NULL,
	[TillCur1] [varchar](50) NULL,
	[TillCur2] [varchar](50) NULL,
	[TillCur3] [varchar](50) NULL,
	[TillCur4] [varchar](50) NULL,
	[TillCur5] [varchar](50) NULL,
	[DineIn] [bit] NULL,
	[TakeOut] [bit] NULL,
	[Delivery] [bit] NULL,
	[OrderAhead] [bit] NULL,
	[Queue] [bit] NULL,
	[PartyEvent] [bit] NULL,
	[TaxLabel1] [varchar](50) NULL,
	[TaxPercentage1] [numeric](12, 2) NULL,
	[TaxLabel2] [varchar](50) NULL,
	[TaxPercentage2] [numeric](12, 2) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IngredientsMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IngredientsMasterDetail](
	[IngredientsID] [uniqueidentifier] NULL,
	[IngredientName] [varchar](50) NULL,
	[IngredientsMasterDetail_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IngredientUnitTypeDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IngredientUnitTypeDetail](
	[UnitTypeID] [uniqueidentifier] NULL,
	[UnitType] [varchar](50) NULL,
	[Qty] [numeric](12, 2) NULL,
	[IngredientsMasterDetail_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ItemChefMapping]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemChefMapping](
	[ItemChefMappingID] [uniqueidentifier] NULL,
	[CategoryID] [uniqueidentifier] NULL,
	[ProductID] [uniqueidentifier] NULL,
	[EmployeeID] [uniqueidentifier] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoginResponse]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoginResponse](
	[Code] [varchar](50) NULL,
	[Message] [varchar](50) NULL,
	[RootObject_Id] [int] NULL,
	[ErrorMsg] [varchar](50) NULL,
	[ShiftWiseEmployee] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MergeTable]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MergeTable](
	[ID] [uniqueidentifier] NULL,
	[OrderID] [uniqueidentifier] NULL,
	[TableID] [uniqueidentifier] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ModifierCategoryDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ModifierCategoryDetail](
	[ModifierCategoryID] [uniqueidentifier] NULL,
	[ModifierCategoryName] [varchar](50) NULL,
	[IsForced] [bit] NULL,
	[ProductID] [uniqueidentifier] NULL,
	[Sort] [int] NULL,
	[ModifierCategoryDetail_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ModifierDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ModifierDetail](
	[IngredientsID] [uniqueidentifier] NULL,
	[Name] [varchar](50) NULL,
	[Qty] [int] NULL,
	[Price] [numeric](12, 2) NULL,
	[IsDefault] [bit] NULL,
	[IsQty] [bit] NULL,
	[UnitTypeID] [uniqueidentifier] NULL,
	[UnitType] [varchar](50) NULL,
	[Sort] [int] NULL,
	[ModifierCategoryDetail_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ModuleAppIDDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ModuleAppIDDetail](
	[AppID] [varchar](50) NULL,
	[DeviceName] [varchar](50) NULL,
	[ModuleMasterDetail_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ModuleMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ModuleMasterDetail](
	[ModuleID] [uniqueidentifier] NULL,
	[ModuleName] [varchar](50) NULL,
	[NoOfModule] [int] NULL,
	[ModuleMasterDetail_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [uniqueidentifier] NOT NULL,
	[OrderNo] [varchar](15) NULL,
	[OrderDate] [datetime] NULL,
	[EmployeeID] [uniqueidentifier] NULL,
	[CustomerID] [uniqueidentifier] NULL,
	[DeliveryType] [int] NULL,
	[DeliveryTypeName] [varchar](50) NULL,
	[TableID] [uniqueidentifier] NULL,
	[SubTotal] [numeric](12, 2) NULL,
	[ExtraCharge] [numeric](12, 2) NULL,
	[TaxLabel1] [varchar](50) NULL,
	[TaxPercent1] [numeric](12, 2) NULL,
	[SGSTAmount] [numeric](12, 2) NULL,
	[TaxLabel2] [varchar](50) NULL,
	[TaxPercent2] [numeric](12, 2) NULL,
	[CGSTAmount] [numeric](12, 2) NULL,
	[TotalTax] [numeric](12, 2) NULL,
	[DiscountPer] [numeric](12, 2) NULL,
	[Discount] [numeric](12, 2) NULL,
	[TipGratuity] [numeric](12, 2) NULL,
	[DeliveryCharge] [numeric](12, 2) NULL,
	[PayableAmount] [numeric](12, 2) NULL,
	[OrderActions] [int] NULL,
	[OrderSpecialRequest] [nvarchar](250) NULL,
	[DiscountType] [int] NULL,
	[DiscountRemark] [varchar](50) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrderWiseModifier]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrderWiseModifier](
	[ModifierID] [uniqueidentifier] NULL,
	[OrderID] [uniqueidentifier] NULL,
	[TransactionID] [uniqueidentifier] NULL,
	[ProductID] [uniqueidentifier] NULL,
	[IngredientsID] [uniqueidentifier] NULL,
	[Quantity] [int] NULL,
	[Price] [numeric](12, 2) NULL,
	[ModifierOption] [varchar](50) NULL,
	[Total] [numeric](12, 2) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PaymentGatewayMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PaymentGatewayMaster](
	[PaymentGatewayID] [uniqueidentifier] NULL,
	[PaymentGatewayName] [varchar](50) NULL,
	[MerchantID] [varchar](50) NULL,
	[TokenKey] [varchar](max) NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[ResponseUrl] [varchar](max) NULL,
	[ATOMTransactionType] [varchar](50) NULL,
	[Productid] [varchar](50) NULL,
	[Version] [varchar](50) NULL,
	[ServiceID] [varchar](50) NULL,
	[ApplicationProfileId] [varchar](50) NULL,
	[MerchantProfileId] [varchar](50) NULL,
	[MerchantProfileName] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrinterMapping]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrinterMapping](
	[PrinterMappingID] [uniqueidentifier] NULL,
	[DeviceID] [uniqueidentifier] NULL,
	[PrinterID] [uniqueidentifier] NULL,
	[PartID] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductClassMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductClassMasterDetail](
	[ClassID] [uniqueidentifier] NOT NULL,
	[ClassName] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RecipeMasterData]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RecipeMasterData](
	[ProductID] [uniqueidentifier] NULL,
	[ProductName] [varchar](250) NULL,
	[RecipeID] [uniqueidentifier] NULL,
	[RecipeText] [varchar](max) NULL,
	[RecipeMasterData_Id] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RecipeMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RecipeMasterDetail](
	[IngredientsID] [uniqueidentifier] NULL,
	[Name] [varchar](50) NULL,
	[Qty] [int] NULL,
	[Price] [numeric](12, 2) NULL,
	[IsDefault] [bit] NULL,
	[IsQty] [bit] NULL,
	[UnitTypeID] [uniqueidentifier] NULL,
	[UnitType] [varchar](50) NULL,
	[RecipeMasterData_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RightDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RightDetail](
	[RightCode] [int] NULL,
	[RightName] [varchar](50) NULL,
	[SubFeatureDetail_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoleMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RoleMasterDetail](
	[RoleID] [uniqueidentifier] NULL,
	[RoleName] [varchar](50) NULL,
	[RoleMasterDetail_Id] [int] NULL,
	[ModuleMasterDetail_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RootObject]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RootObject](
	[Code] [varchar](50) NULL,
	[Message] [varchar](50) NULL,
	[RootObject_Id] [int] NULL,
	[ErrorMsg] [varchar](50) NULL,
	[ShiftWiseEmployee] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ShiftMaster]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShiftMaster](
	[ShiftID] [uniqueidentifier] NOT NULL,
	[ShiftName] [varchar](50) NULL,
	[ShiftMaster_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ShiftMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShiftMasterDetail](
	[ShiftDetailsID] [uniqueidentifier] NULL,
	[ShiftFromTime] [time](7) NULL,
	[ShiftToTime] [time](7) NULL,
	[ShiftDay] [int] NULL,
	[FirstSlot] [numeric](12, 2) NULL,
	[SecondSlot] [numeric](12, 2) NULL,
	[FinalSlot] [numeric](12, 2) NULL,
	[ShiftDiff] [varchar](50) NULL,
	[ShiftMaster_ID] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SubFeatureDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SubFeatureDetail](
	[SubFeatureCode] [int] NULL,
	[SubFeatureName] [varchar](50) NULL,
	[SubFeatureDetail_Id] [int] NULL,
	[FeatureDetail_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TableMasterDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TableMasterDetail](
	[TableID] [uniqueidentifier] NULL,
	[EmployeeID] [uniqueidentifier] NULL,
	[TableName] [varchar](50) NULL,
	[NoOfSeats] [int] NULL,
	[Location] [varchar](50) NULL,
	[ClassID] [uniqueidentifier] NULL,
	[Sort] [int] NULL,
	[StatusID] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TaxGroupDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaxGroupDetail](
	[TaxGroupID] [uniqueidentifier] NULL,
	[Name] [varchar](50) NULL,
	[Percentage] [numeric](12, 2) NULL,
	[ParentID] [uniqueidentifier] NULL,
	[PartnerID] [uniqueidentifier] NULL,
	[Action] [varchar](50) NULL,
	[Sign] [varchar](50) NULL,
	[Sort] [int] NULL,
	[TaxOnProductType] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[test]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[test](
	[testdate] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TillManage]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TillManage](
	[TillID] [uniqueidentifier] NULL,
	[EmployeeID] [uniqueidentifier] NULL,
	[StartCash] [numeric](12, 2) NULL,
	[PayIn] [numeric](12, 2) NULL,
	[PayOut] [numeric](12, 2) NULL,
	[EndCash] [numeric](12, 2) NULL,
	[Difference] [numeric](12, 2) NULL,
	[Cash] [numeric](12, 2) NULL,
	[ExpectedCash] [numeric](12, 2) NULL,
	[Currency5] [int] NULL,
	[Currency10] [int] NULL,
	[Currency20] [int] NULL,
	[Currency50] [int] NULL,
	[Currency100] [int] NULL,
	[Currency200] [int] NULL,
	[Currency500] [int] NULL,
	[Currency1000] [int] NULL,
	[Currency2000] [int] NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
	[EnrtyDate] [datetime] NULL,
	[IsTillDone] [bit] NULL,
	[UpStreamStatus] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TillPayIn]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TillPayIn](
	[PayInID] [uniqueidentifier] NULL,
	[TillID] [uniqueidentifier] NULL,
	[EmployeeID] [uniqueidentifier] NULL,
	[Amount] [numeric](12, 2) NULL,
	[Reason] [varchar](150) NULL,
	[EntryDateTime] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TillPayOut]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TillPayOut](
	[PayOutID] [uniqueidentifier] NULL,
	[TillID] [uniqueidentifier] NULL,
	[EmployeeID] [uniqueidentifier] NULL,
	[Amount] [numeric](12, 2) NULL,
	[Reason] [varchar](150) NULL,
	[EntryDateTime] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TimeSheetWiseDiscount]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSheetWiseDiscount](
	[FromTime] [time](7) NULL,
	[ToTime] [time](7) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Day] [int] NULL,
	[DiscountMasterDetail_Id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[TransactionID] [uniqueidentifier] NULL,
	[OrderID] [uniqueidentifier] NULL,
	[CategoryID] [uniqueidentifier] NULL,
	[ProductID] [uniqueidentifier] NULL,
	[Quantity] [int] NULL,
	[Rate] [numeric](12, 2) NULL,
	[TotalAmount] [numeric](12, 2) NULL,
	[Sort] [int] NULL,
	[SpecialRequest] [nvarchar](250) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VendorMasterData]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VendorMasterData](
	[VendorID] [uniqueidentifier] NOT NULL,
	[VendorName] [varchar](50) NULL,
	[RootObject_Id] [int] NULL,
 CONSTRAINT [PK_VendorMasterData] PRIMARY KEY CLUSTERED 
(
	[VendorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VersionDetail]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VersionDetail](
	[Version_Code] [varchar](50) NULL,
	[IsMandtory] [varchar](50) NULL,
	[RootObject_Id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[ViewCategoryWiseProduct]    Script Date: 18/12/2017 03:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewCategoryWiseProduct]
AS
SELECT        CategoryID, CategoryName, ParentID, ImgPath, IsCategory, Price
FROM            (SELECT        CategoryID, CategoryName, ParentID, ImgPath, IsCategory, 0 AS Price
                          FROM            dbo.CategoryMaster
                          UNION ALL
                          SELECT        ProductID, ProductName, CategoryID, ImgPath, 0 AS IsCategory, Price
                          FROM            dbo.CategoryWiseProduct) AS ViewCategoryWiseProduct_1

GO
ALTER TABLE [dbo].[OrderWiseModifier] ADD  CONSTRAINT [DF_OrderWiseModifier_Total]  DEFAULT ((0)) FOR [Total]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ViewCategoryWiseProduct_1"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 178
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewCategoryWiseProduct'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewCategoryWiseProduct'
GO
