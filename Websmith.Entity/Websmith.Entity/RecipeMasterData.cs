using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Websmith.Entity
{
    public class RecipeMasterData
    {
        #region Private Fields
        private Guid _ProductID;
        private string _ProductName;
        private Guid _RecipeID;
        private string _RecipeText;
        private int _RecipeMasterData_Id;
        private string _Mode;
        public List<RecipeMasterDetail> RecipeMasterDetail { get; set; }
        #endregion

        #region Public Properties

        public Guid ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        public Guid RecipeID
        {
            get { return _RecipeID; }
            set { _RecipeID = value; }
        }
        public string RecipeText
        {
            get { return _RecipeText; }
            set { _RecipeText = value; }
        }
        public int RecipeMasterData_Id
        {
            get { return _RecipeMasterData_Id; }
            set { _RecipeMasterData_Id = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        #endregion
    }
}
