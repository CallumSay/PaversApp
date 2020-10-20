using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaversApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginScreen loginscreen = new LoginScreen();
            loginscreen.Show();
            Application.Run();
        }
        public static bool Login(string username, string password)
        {
            DataTable dt = new DataTable();
            string connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Callum\Documents\PaversProject\PaversApp\PaversApp\PaversDB.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionstring);
            string loginQuery = "SELECT * FROM Users WHERE AccountID='" + username + "' AND Password= '" + password + "'";
            SqlCommand cmd = new SqlCommand(loginQuery, con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

    }
}
