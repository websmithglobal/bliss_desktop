using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class IngredientUnitTypeDetail
    {

        #region Constructors

        public IngredientUnitTypeDetail()
        {
        }
        #endregion

        #region Private Fields

        private Guid _UnitTypeID;
        private string _UnitType;
        private decimal _Qty;
        private int _IngredientsMasterDetail_Id;
        public int IsUPStream { get; set; } = 0;
        private string _Mode;
        #endregion

        #region Public Properties

        public Guid UnitTypeID
        {
            get { return _UnitTypeID; }
            set { _UnitTypeID = value; }
        }
        public string UnitType
        {
            get { return _UnitType; }
            set { _UnitType = value; }
        }
        public decimal Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
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
