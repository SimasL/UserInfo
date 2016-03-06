using System.Data.SqlClient;

namespace Users.DBData
{
    public class DBStatusCheck
    {
        #region DBCheck

        public bool DBSERVERConnectionCheck()
        {
            SqlConnection dbConnection = new SqlConnection("user id= "+ Properties.User +
                                       ";password=" + Properties.Password + ";server=" + Properties.DBName);
            try
            {
                dbConnection.Open();
            }
            catch
            {
                return false;
            }
            dbConnection.Close();
            return true;
        }

        public bool DatabaseCheck()
        {
            SqlConnection dbConnection = new SqlConnection("user id= " + Properties.User +
                                       ";password=" + Properties.Password + ";server=" + Properties.DBName +
                                       ";database=UserDB; ");
            try
            {
                dbConnection.Open();
            }
            catch
            {
                return false;
            }
            dbConnection.Close();
            return true;
        }

        #endregion

    }
}