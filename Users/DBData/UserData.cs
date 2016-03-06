using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Users.DBData
{
	public class UserData
	{


		public Dictionary<int, string> GetAllUsers()
		{
			if (Properties.UserDBStatus != dbStatus.OK)
			{
				return null;
			}
			Dictionary<int, string> users = new Dictionary<int, string>();
			using (SqlConnection dbconnection = ConnectToDB())
			{
				dbconnection.Open();
				SqlCommand command = new SqlCommand();
				command.Connection = dbconnection;
				command.CommandText = "SELECT * FROM [User]";
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						users.Add((int)reader["UID"], ((string)reader["NAME"]).TrimEnd(' '));
					}
					reader.Close();
				}
			}
			return users;
		}

		public List<UserInfoBoxButton> GetUserData(int userID)
		{
			List<UserInfoBoxButton> useInfo = new List<UserInfoBoxButton>();
			Dictionary<int, string> users = new Dictionary<int, string>();
			UserInfoBoxButton box;
			using (SqlConnection dbconnection = ConnectToDB())
			{
				dbconnection.Open();
				SqlCommand command = new SqlCommand();
				command.Connection = dbconnection;
				command.CommandText = "SELECT UID, email from [Email] where USERKEY = " + userID ;
				DataTable emailTable = new DataTable();
				using (SqlDataAdapter adapter = new SqlDataAdapter(command))
				{
					adapter.Fill(emailTable);
				}
				foreach (DataRow row in emailTable.Rows)
				{
					box = new UserInfoBoxButton();
					box.UserTableName = tableName.Email;
					box.UserUID = row.Field<int>("UID");
					box.UserValue = row.Field<string>("email").TrimEnd(' ');
					box.Text = "User mail:" + row.Field<string>("email").TrimEnd(' ');
					useInfo.Add(box);
				}

				command.CommandText = "SELECT UID, phone from [Phone] where USERKEY = " + userID;
				DataTable phoneTable = new DataTable();
				using (SqlDataAdapter adapter = new SqlDataAdapter(command))
				{
					adapter.Fill(phoneTable);
				}
				foreach (DataRow row in phoneTable.Rows)
				{
					box = new UserInfoBoxButton();
					box.UserTableName = tableName.Phone;
					box.UserUID = row.Field<int>("UID");
					box.UserValue = row.Field<string>("phone").TrimEnd(' ');
					box.Text = "User mail:" + row.Field<string>("phone").TrimEnd(' ');
					useInfo.Add(box);
				}

				return useInfo;
			}

		}


		#region helpers

		private SqlConnection ConnectToDB()
		{
			SqlConnection dbConnection = new SqlConnection("user id= " + Properties.User +
												";password=" + Properties.Password + ";server=" + Properties.DBName +
												";database=UserDB; ");
			return dbConnection;
		}

		#endregion
	}
}