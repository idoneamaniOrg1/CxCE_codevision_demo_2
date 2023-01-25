using System;
using System.Data;
using System.Data.SqlClient;
using system.Web.Security.AntiXss.Sanitize;


namespace CxCE_Demo
{
    public partial class sqli : System.Web.UI.Page
    {
        static string username = String.Empty;
        static int age = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string ID = Request.QueryString[0];
                getName(ID);
            }
            catch { }
        }

        private void getName(string ID)
        {
            string username = "No name";
            SqlConnection conn = new SqlConnection("Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=" + Constants.DB_PASSWORD + ";");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            

            //cmd.CommandText = "SELECT NAME FROM Users WHERE ID = " + ID;
            // adding prepared statements to fix sqlI
            cmd.CommandText = "SELECT NAME FROM Users WHERE ID = " + @ID;
            cmd.Parameters.AddWithValue("@ID", ID);
            // finish prepared statement
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            conn.Open();

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                username = reader["NAME"].ToString();
                age = getAge(username);
            }

            message.Text = "Welcome " + AntiXss.Sanitize(username);
            conn.Close();
        }

        private int getAge(string name)
        {
            SqlConnection conn = new SqlConnection("Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=" + Constants.DB_PASSWORD + ";");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT AGE FROM Users WHERE NAME = '" + name + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            conn.Open();

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
               return(reader.GetInt32(0));

            conn.Close();
            return -1;
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            getName(name.Text);
        }
    }
}
