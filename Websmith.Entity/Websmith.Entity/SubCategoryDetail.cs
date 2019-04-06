using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class SubCategoryDetail
    {
        #region Constructors

        public SubCategoryDetail()
        {
        }
        #endregion

        #region Private Fields

        private string _CategoryID;
        private string _CategoryName;
        private string _ImgPath;
        private string _MainCategoryID;
        private string _ClassMasterID;
        private int _Priority;
        private int _SubCategoryDetail_Id;
        private int _SubCategoryDetail_Id_0;
        private int _CategoryDetails_Id;
        private string _mode;
        #endregion

        #region Public Properties

        public string CategoryID
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
        public string MainCategoryID
        {
            get { return _MainCategoryID; }
            set { _MainCategoryID = value; }
        }
        public string ClassMasterID
        {
            get { return _ClassMasterID; }
            set { _ClassMasterID = value; }
        }
        public int Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }
        public int SubCategoryDetail_Id
        {
            get { return _SubCategoryDetail_Id; }
            set { _SubCategoryDetail_Id = value; }
        }
        public int SubCategoryDetail_Id_0
        {
            get { return _SubCategoryDetail_Id_0; }
            set { _SubCategoryDetail_Id_0 = value; }
        }
        public int CategoryDetails_Id
        {
            get { return _CategoryDetails_Id; }
            set { _CategoryDetails_Id = value; }
        }
        public string Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }
        #endregion
    }
}
