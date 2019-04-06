using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class ItemChefMapping
    {
        #region Private Fields

        private Guid _ItemChefMappingID;
        private Guid _CategoryID;
        private Guid _ProductID;
        private Guid _EmployeeID;
        private string _Mode;
        #endregion

        #region Public Properties

        public Guid ItemChefMappingID
        {
            get { return _ItemChefMappingID; }
            set { _ItemChefMappingID = value; }
        }
        public Guid CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        public Guid ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        public Guid EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
