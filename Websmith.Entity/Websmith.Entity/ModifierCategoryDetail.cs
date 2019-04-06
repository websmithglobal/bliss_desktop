using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class ModifierCategoryDetail
    {
        #region Private Fields
        private Guid _ModifierCategoryID;
        private string _ModifierCategoryName;
        private bool _IsForced;
        private Guid _ProductID;
        private int _Sort;
        private int _ModifierCategoryDetail_Id;
        private string _Mode;
        public int IsUPStream { get; set; } = 0;
        #endregion

        #region Public Properties

        public Guid ModifierCategoryID
        {
            get { return _ModifierCategoryID; }
            set { _ModifierCategoryID = value; }
        }
        public string ModifierCategoryName
        {
            get { return _ModifierCategoryName; }
            set { _ModifierCategoryName = value; }
        }
        public bool IsForced
        {
            get { return _IsForced; }
            set { _IsForced = value; }
        }
        public Guid ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        public int Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }
        public int ModifierCategoryDetail_Id
        {
            get { return _ModifierCategoryDetail_Id; }
            set { _ModifierCategoryDetail_Id = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }

}
