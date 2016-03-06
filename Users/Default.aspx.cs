using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace Users
{
	public partial class Default : System.Web.UI.Page
	{
		private static List<UserInfoBoxButton> m_InfoControls;
		//private static TableCell tc;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Properties.Pageloaded)
			{
				Controller.DBController db = new Controller.DBController();
				string result = db.TestDBStatus();

				Status_Label.Text = result;
				Properties.Pageloaded = true;
				m_InfoControls = new List<UserInfoBoxButton>();
				//tc = this.User_TableCell;

				updateUserList();
			}
			User_TableCell.Controls.Clear();
			foreach (Control control in m_InfoControls)
			{
				User_TableCell.Controls.Add(control);
				User_TableCell.Controls.Add(new LiteralControl("<br />"));
			}
		}

		protected void Connection_Button_Click(object sender, EventArgs e)
		{
			Properties.DBName = DB_TextBox.Text;
			Properties.User = User_TextBox.Text;
			Properties.Password = Pass_TextBox.Value;
			Controller.DBController db = new Controller.DBController();
			string result = db.TestDBStatus();

			Status_Label.Text = result;
		}

		protected void AddChangeUserName_Button_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(UserName_TextBox.Text))
			{
				return;
			}
			UserInfoBoxButton rad = new UserInfoBoxButton();
			if (m_InfoControls.Exists(m => m.UserTableName == tableName.User))
			{
				UserInfoBoxButton bt = m_InfoControls.Find(m => m.UserTableName == tableName.User);
				bt.UserValue = UserName_TextBox.Text;
				bt.Text = "User Name:" + UserName_TextBox.Text;
			}
			else
			{
				rad.UserTableName = tableName.User;
				rad.UserUID = -1;
				rad.UserValue = UserName_TextBox.Text;
				rad.Text = "User Name:" + UserName_TextBox.Text;

				m_InfoControls.Add(rad);
			}

			this.Page_Load(sender, e);
		}

		protected void NewMail_Button_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(NewMail_TextBox.Text))
			{
				return;
			}

			UserInfoBoxButton rad = new UserInfoBoxButton();
			rad.UserTableName = tableName.Email;
			rad.UserUID = -1;
			rad.UserValue = NewMail_TextBox.Text;
			rad.Text = "User mail:" + NewMail_TextBox.Text;
			m_InfoControls.Add(rad);

			this.Page_Load(sender, e);
		}

		protected void NewPhone_Button_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(NewPhone_TextBox.Text))
			{
				return;
			}
			UserInfoBoxButton rad = new UserInfoBoxButton();
			rad.UserTableName = tableName.Phone;
			rad.UserUID = -1;
			rad.UserValue = NewPhone_TextBox.Text;
			rad.Text = "User phone:" + NewPhone_TextBox.Text;
			m_InfoControls.Add(rad);

			this.Page_Load(sender, e);
		}

		protected void AddRecord_Button_Click(object sender, EventArgs e)
		{
			if (Properties.UserDBStatus == dbStatus.NeedCreateDB)
			{
				DBData.CreateDB cDB = new DBData.CreateDB();
				cDB.CreateUserDB();
				Status_Label.Text = "Database UserDB created";
			}
			string UserName = null;
			int newUser = -2;
			List<string> UserMail = new List<string>();
			List<string> UserPhone = new List<string>();
			foreach (var boxControl in User_TableCell.Controls)
			{
				if (boxControl is UserInfoBoxButton)
				{
					UserInfoBoxButton box = (UserInfoBoxButton)boxControl;
					if (box.UserTableName == tableName.Email && box.UserUID == -1)
					{
						UserMail.Add(box.UserValue);
					}
					else if (box.UserTableName == tableName.Phone && box.UserUID == -1)
					{
						UserPhone.Add(box.UserValue);
					}
					else if (box.UserTableName == tableName.User)
					{
						UserName = box.UserValue;
						newUser = box.UserUID;
					}
				}
			}
			if (newUser == -1)
			{
				DBData.UserDataChange db = new DBData.UserDataChange();
				db.NewUser(UserName, UserMail, UserPhone);
			}
			if (newUser > -1)
			{
				DBData.UserDataChange db = new DBData.UserDataChange();
				db.OldUser(newUser, UserMail, UserPhone, UserName);
			}
			m_InfoControls = new List<UserInfoBoxButton>();
			updateUserList();
			this.Page_Load(sender, e);
		}

		protected void LoadUser_Button_Click(object sender, EventArgs e)
		{
			DBData.UserData userData = new DBData.UserData();
			List<UserInfoBoxButton>  userInfo = userData.GetUserData(int.Parse(Users_DropDownList.SelectedItem.Value));
			m_InfoControls = new List<UserInfoBoxButton>();
			UserInfoBoxButton rad = new UserInfoBoxButton();
			rad.UserTableName = tableName.User;
			rad.UserUID = int.Parse(Users_DropDownList.SelectedItem.Value);
			rad.UserValue = Users_DropDownList.SelectedItem.Text;
			rad.Text = "User Name:" + Users_DropDownList.SelectedItem.Text;

			m_InfoControls.Add(rad);
			m_InfoControls.AddRange(userInfo);

			this.Page_Load(sender, e);
		}
		
		protected void DeleteSelected_Button_Click(object sender, EventArgs e)
		{
			DBData.UserDataChange userDataChange = new DBData.UserDataChange();
			List<int> UserMail = new List<int>();
			List<int> UserPhone = new List<int>();
			int currentUser = -1;
			foreach (var boxControl in User_TableCell.Controls)
			{
				if (boxControl is UserInfoBoxButton)
				{
					UserInfoBoxButton box = (UserInfoBoxButton)boxControl;
					if (box.UserTableName == tableName.Email && box.UserUID > -1 && box.Checked)
					{
						UserMail.Add(box.UserUID);
					}
					else if (box.UserTableName == tableName.Phone && box.UserUID > -1 && box.Checked)
					{
						UserPhone.Add(box.UserUID);
					}
					else if(box.UserTableName == tableName.User)
					{
						if (box.Checked)
						{
							userDataChange.DeleteUser(box.UserUID);
							m_InfoControls = new List<UserInfoBoxButton>();
							updateUserList();
							this.Page_Load(sender, e);
							return;
						}
						else
						{
							currentUser = box.UserUID;
						}
					}
				}
			}

			userDataChange.DeleteUserData(UserMail, true);
			userDataChange.DeleteUserData(UserPhone, false);

			if (currentUser > -1)
			{
				DBData.UserData userData = new DBData.UserData();
				List<UserInfoBoxButton> userInfo = userData.GetUserData(currentUser);
				m_InfoControls = null;
				m_InfoControls = new List<UserInfoBoxButton>();
				UserInfoBoxButton rad = new UserInfoBoxButton();
				rad.UserTableName = tableName.User;
				rad.UserUID = int.Parse(Users_DropDownList.SelectedItem.Value);
				rad.UserValue = Users_DropDownList.SelectedItem.Text;
				rad.Text = "User Name:" + Users_DropDownList.SelectedItem.Text;

				m_InfoControls.Add(rad);
				m_InfoControls.AddRange(userInfo);
			}

			updateUserList();
			this.Page_Load(sender, e);
		}

		private void updateUserList()
		{
			DBData.UserData userData = new DBData.UserData();
			Dictionary<int, string> users = userData.GetAllUsers();
			if(users == null)
			{
				return;
			}
			ListItem userItem;
			Users_DropDownList.Items.Clear();
			foreach (KeyValuePair<int, string> user in users)
			{
				userItem = new ListItem();
				userItem.Text = user.Value;
				userItem.Value = user.Key.ToString();
				Users_DropDownList.Items.Add(userItem);
			}
		}
	}
}