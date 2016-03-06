using System;
using System.Data;
using System.Data.SqlClient;


namespace Users.DBData
{
    public class CreateDB
    {

        public bool CreateUserDB()
        {
            SqlConnection dbConnection = new SqlConnection("user id= " + Properties.User +
                                       ";password=" + Properties.Password + ";server=" + Properties.DBName);
            try
            {
                dbConnection.Open();
                SqlCommand commands = new SqlCommand();
                commands.Connection = dbConnection;
                commands.CommandText ="CREATE DATABASE UserDB";
                commands.ExecuteNonQuery();
                dbConnection.Close();

                dbConnection = new SqlConnection("user id= " + Properties.User +
                                       ";password=" + Properties.Password + ";server=" + Properties.DBName + "; database=UserDB; ");
                dbConnection.Open();
                commands.Connection = dbConnection;

                commands.CommandText = "CREATE TABLE [User] ([UID][int] IDENTITY(1,1) PRIMARY KEY not null, [NAME] [nchar](100) NOT NULL)";
                commands.ExecuteNonQuery();
                commands.CommandText = "CREATE TABLE [Email] ([UID][int] IDENTITY(1,1) PRIMARY KEY not null, [USERKEY][int] not null, [EMAIL] [nchar](100) NOT NULL)";
                commands.ExecuteNonQuery();
                commands.CommandText = "CREATE TABLE [Phone] ([UID][int] IDENTITY(1,1) PRIMARY KEY not null, [USERKEY][int] not null, [PHONE] [nchar](100) NOT NULL)";
                commands.ExecuteNonQuery();
                //SqlDataAdapter adapter = new SqlDataAdapter(commands);
                //DataSet ds = new DataSet();
                //adapter.FillSchema(ds, SchemaType.Source, "User");
                //adapter.FillSchema(ds, SchemaType.Source, "Email");

                //DataColumn User, Email, Phone;
                //User = ds.Tables["User"].Columns["UID"];
                //Email = ds.Tables["Email"].Columns["USERKEY"];
                //DataRelation dr = new DataRelation("UserMails", User, Email);
                //ds.Relations.Add(dr);
                //adapter.Fill(ds);

                //ds = new DataSet();
                //adapter.FillSchema(ds, SchemaType.Source, "User");
                //adapter.FillSchema(ds, SchemaType.Source, "Phone");
                //User = ds.Tables["User"].Columns["UID"];
                //Phone = ds.Tables["Phone"].Columns["USERKEY"];
                //dr = new DataRelation("UserPhones", User, Email);
                //ds.Relations.Add(dr);

                dbConnection.Close();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}