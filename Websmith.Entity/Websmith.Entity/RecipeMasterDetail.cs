using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class RecipeMasterDetail
    {
        #region Private Fields

        private Guid _IngredientsID;
        private string _Name;
        private int _Qty;
        private decimal _Price;
        private bool _IsDefault;
        private bool _IsQty;
        private Guid _UnitTypeID;
        private string _UnitType;
        private int _RecipeMasterData_Id;
        private string _Mode;
        public int IsUPStream { get; set; } = 0;
        #endregion

        #region Public Properties

        public Guid IngredientsID
        {
            get { return _IngredientsID; }
            set { _IngredientsID = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public int Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }
        public decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        public bool IsDefault
        {
            get { return _IsDefault; }
            set { _IsDefault = value; }
        }
        public bool IsQty
        {
            get { return _IsQty; }
            set { _IsQty = value; }
        }
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
        public int RecipeMasterData_Id
        {
            get { return _RecipeMasterData_Id; }
            set { _RecipeMasterData_Id = value; }
        }
        public string Mode
        {
            get{ return _Mode; }
            set{ _Mode = value; }
        }
        #endregion
    }
}
