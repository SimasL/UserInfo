using System.Web.UI.WebControls;

namespace Users
{
    public class UserInfoBoxButton : CheckBox
    {
        #region Properties

        public string UserValue;
        public int UserUID;
        public tableName UserTableName;

        #endregion

		public UserInfoBoxButton()
		{
			this.AutoPostBack = true;
		}

    }

    public enum tableName
    {
        User,
        Email,
        Phone
    }
}