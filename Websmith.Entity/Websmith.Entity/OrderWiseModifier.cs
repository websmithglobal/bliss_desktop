using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class OrderWiseModifier
    {
        #region Private Fields

        private Guid _ModifierID;
        private Guid _OrderID;
        private Guid _TransactionID;
        private Guid _ProductID;
        private Guid _IngredientsID;
        private string _Name;
        private int _Quantity;
        private decimal _Price;
        private decimal _Total;
        private string _ModifierOption;
        private string _Mode;
        public Guid RUserID { get; set; }
        public int RUserType { get; set; }
        #endregion

        #region Public Properties

        public Guid ModifierID
        {
            get { return _ModifierID; }
            set { _ModifierID = value; }
        }
        public Guid OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }
        public Guid TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        public Guid ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
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
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }
        public string ModifierOption
        {
            get { return _ModifierOption; }
            set { _ModifierOption = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        #endregion
    }
}
