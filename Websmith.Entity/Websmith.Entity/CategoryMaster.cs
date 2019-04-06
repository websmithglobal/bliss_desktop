using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class CategoryMaster
    {

        #region Constructors
        public CategoryMaster()
        {
        }
        #endregion

        #region Private Fields

        private Guid _CategoryID;
        private string _CategoryName;
        private string _ImgPath;
        private Guid _ParentID;
        private Guid _ClassMasterID;
        private int _Priority;
        private int _IsCategory;
        private string _Mode;
        private int _Count;
        private decimal _Price;
        private Guid _RUserID;
        private int _RUserType;
        public int IsUPStream { get; set; } = 0;
        public Guid MainCategoryID { get; set; }
        #endregion

        #region Public Properties

        public Guid CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }
        public string ImgPath
        {
            get { return _ImgPath; }
            set { _ImgPath = value; }
        }
        public Guid ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        public Guid ClassMasterID
        {
            get { return _ClassMasterID; }
            set { _ClassMasterID = value; }
        }
        public int Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }
        public int IsCategory
        {
            get { return _IsCategory; }
            set { _IsCategory = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        public int Count
        {
            get{ return _Count; }
            set{ _Count = value; }
        }
        public decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        public Guid RUserID
        {
            get { return _RUserID; }
            set { _RUserID = value; }
        }
        public int RUserType
        {
            get { return _RUserType; }
            set { _RUserType = value; }
        }
        #endregion
    }

    public class CategoryAddress
    {
        public int Index { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
