using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class LoginResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public VersionDetail VersionDetail { get; set; }
        public string ErrorMsg { get; set; }
        public BranchMasterSetting BranchMasterSetting { get; set; }
        public List<EmployeeMasterList> EmployeeMasterList { get; set; }
        public List<CategoryDetail> CategoryDetails { get; set; }
        public List<DiscountMasterDetail> DiscountMasterDetail { get; set; }
        public List<ModifierCategoryDetail> ModifierCategoryDetail { get; set; }
        public List<TaxGroupDetail> TaxGroupDetail { get; set; }
        public List<PaymentGatewayMaster> PaymentGatewayMaster { get; set; }
        public List<IngredientsMasterDetail> IngredientsMasterDetail { get; set; }
        public List<object> LanguageMaster { get; set; }
        public List<ShiftMaster> ShiftMaster { get; set; }
        public object ShiftWiseEmployee { get; set; }
        public List<ModuleMasterDetail> ModuleMasterDetail { get; set; }
        public List<TableMasterDetail> TableMasterDetail { get; set; }
        public List<ProductClassMasterDetail> ProductClassMasterDetail { get; set; }
        public List<RecipeMasterData> RecipeMasterData { get; set; }
        public List<VendorMasterData> VendorMasterData { get; set; }
        public List<CustomerMasterData> CustomerMasterData { get; set; }
    }
}
