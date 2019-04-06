using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class IngredientsMasterDetail
    {
        
        #region Private Fields
        private Guid _IngredientsID;
        private string _IngredientName;
        private int _IngredientsMasterDetail_Id;
        public int IsUPStream { get; set; } = 0;
        public List<IngredientUnitTypeDetail> IngredientUnitTypeDetail { get; set; }
        private string _Mode;
        #endregion

        #region Public Properties

        public Guid IngredientsID
        {
            get { return _IngredientsID; }
            set { _IngredientsID = value; }
        }
        public string IngredientName
        {
            get { return _IngredientName; }
            set { _IngredientName = value; }
        }
        public int IngredientsMasterDetail_Id
        {
            get { return _IngredientsMasterDetail_Id; }
            set { _IngredientsMasterDetail_Id = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
