using System.Collections.Generic;
using System.Data.SqlClient;

namespace Users.DBData
{
	public class UserDataChange
	{
		SqlConnection m_DBConnection;

		#region Methods

		/// <summary>
		/// Add new user with data
		/// </summary>
		/// <param name="name">User name</param>
		/// <param name="mail">User Enmails</param>
		/// <param name="phone">User Phones</param>
		public void NewUser(string name, List<string> mail, List<string> phone)
		{
			ConnectToDB();
			m_DBConnection.Open();
			SqlCommand command = new SqlCommand();
			command.Connection = m_DBConnection;
			command.CommandText = "INSERT INTO [User] (name) VALUES ('" + name + "') ;SELECT CAST(scope_identity() as int)";
			int userUID = (int)command.ExecuteScalar();
			OldUser(userUID, mail, phone, null, command);
		}

		/// <summary>
		/// Add userInfrmation
		/// </summary>
		/// <param name="userUID">User to add to</param>
		/// <param name="mails">User mails</param>
		/// <param name="phones">User phones</param>
		/// <param name="command">Connection</param>
		public void OldUser(int userUID, List<string> mails, List<string> phones, string nameChange = null,  SqlCommand command = null)
		{
			if (command == null)
			{
				ConnectToDB();
				m_DBConnection.Open();
				command = new SqlCommand();
				command.Connection = m_DBConnection;
			}
			if (!string.IsNullOrEmpty(nameChange))
			{
				string sqlString = CombineAddStrings(mails, userUID.ToString());
				command.CommandText = "UPDATE [User] SET NAME = '" + nameChange + "' WHERE UID = " + userUID;
				command.ExecuteNonQuery();
			}
			if (mails.Count > 0)
			{
				string sqlString = CombineAddStrings(mails, userUID.ToString());
				command.CommandText = "INSERT INTO [Email] (USERKEY, email) VALUES " + sqlString;
				command.ExecuteNonQuery();
			}
			if (phones.Count > 0)
			{
				string sqlString = CombineAddStrings(phones, userUID.ToString());
				command.CommandText = "INSERT INTO [Phone] (USERKEY, phone) VALUES " + sqlString;
				command.ExecuteNonQuery();
			}
			m_DBConnection.Close();
		}

		public void DeleteUser(int userUID)
		{
			ConnectToDB();
			m_DBConnection.Open();
			SqlCommand command = new SqlCommand();
			command.Connection = m_DBConnection;
			command.CommandText = "DELETE FROM[User] where[User].UID =" + userUID +
										"DELETE FROM[Email] where[Email].USERKEY = " + userUID +
										"DELETE FROM[Phone] where[Phone].USERKEY = " + userUID;
			command.ExecuteNonQuery();
			m_DBConnection.Close();
		}

		public void DeleteUserData(List<int> dataUID, bool isMail)
		{
			if (dataUID.Count < 1)
			{
				return; 
			}
			ConnectToDB();
			m_DBConnection.Open();
			SqlCommand command = new SqlCommand();
			command.Connection = m_DBConnection;
			string table = "[Phone]";
			if(isMail)
			{
				table = "[Email]";
			}
			command.CommandText = "DELETE " + table + " WHERE UID in (" + CombineDeleteStrings(dataUID);
			command.ExecuteNonQuery();
			m_DBConnection.Close();
		}
		#endregion


		#region helpers

		private void ConnectToDB()
		{
			m_DBConnection = new SqlConnection("user id= " + Properties.User +
												";password=" + Properties.Password + ";server=" + Properties.DBName +
												";database=UserDB; ");

		}

		/// <summary>
		/// Combine to format (key, string1),(key, string2)...
		/// </summary>
		/// <param name="stringLIst"></param>
		/// <param name="userKey"></param>
		/// <returns></returns>
		private string CombineAddStrings(List<string> stringLIst, string userKey)
		{
			string sqlString;
			sqlString = "(" + userKey + ",'";
			sqlString += string.Join("'),(" + userKey + ",'", stringLIst);
			sqlString += "')";
			return sqlString;
		}

		private string CombineDeleteStrings(List<int> uidList)
		{
			string sqlString;
			sqlString = string.Join(",", uidList);
			sqlString += ")";
			return sqlString;
		}

		#endregion
	}
}