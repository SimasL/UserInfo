using Users.DBData;

namespace Users.Controller
{
    public class DBController
    {
        public string TestDBStatus()
        {
            DBStatusCheck db = new DBStatusCheck();
            if (db.DBSERVERConnectionCheck())
            {
                if (db.DatabaseCheck())
                {
                    Properties.UserDBStatus = dbStatus.OK;
                    return "Connection OK";
                }
                else
                {
                    Properties.UserDBStatus = dbStatus.NeedCreateDB;
                    return "Missing Database on first save Database:UserDB will be created";
                }
            }
            else
            {
                Properties.UserDBStatus = dbStatus.WrongConnection;
                return "Bad Connection";
            }
        }

        public bool CreateUserDB()
        {
            CreateDB db = new CreateDB();
            if (db.CreateUserDB())
            {
                Properties.UserDBStatus = dbStatus.OK;
                return true;
            }
            return false;
        }
    }
}