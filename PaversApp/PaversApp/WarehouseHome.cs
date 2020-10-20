using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace PaversApp
{
    public partial class WarehouseHome : Form
    {
        string connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Callum\Documents\PaversProject\PaversApp\PaversApp\PaversDB.mdf;Integrated Security=True";
        public void FillListbox()
        {
            InventoryListBox.Items.Clear();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string getdata = "SELECT * FROM Inventory";
                SqlCommand cmd = new SqlCommand(getdata, con);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    InventoryListBox.Items.Add(dr["SKU"]);
                }
            }
        }
        public WarehouseHome()
        {
            InitializeComponent();
            FillListbox();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SearchTextBox_Enter(object sender, EventArgs e)
        {
            if (SearchTextBox.Text.Length == 0)
            {
                SearchTextBox.Text = "Search";
                SearchTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void SearchTextBox_Leave(object sender, EventArgs e)
        {
            if (SearchTextBox.Text == "Search")
            {
                SearchTextBox.Text = "";
                SearchTextBox.ForeColor = SystemColors.WindowText;
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (SearchTextBox.Text == "")
            {
                FillListbox();
                return;
            }
            InventoryListBox.Items.Clear();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string searchitem = "SELECT * FROM Inventory WHERE SKU='" + SearchTextBox.Text + "'OR Colour='" + SearchTextBox.Text + "' OR Size='" + SearchTextBox.Text + "' OR Sex='" + SearchTextBox.Text + "' OR Material='" + SearchTextBox.Text + "'";
                SqlCommand cmd = new SqlCommand(searchitem, con);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    InventoryListBox.Items.Add(dr["SKU"]);
                }
                if(InventoryListBox.Items.Count == 0)
                {
                    MessageBox.Show("ERROR\nItem not found");
                    FillListbox();
                }
            }
            InventoryListBox.Update();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            this.Close();
            Form loginform = new LoginScreen();
            loginform.Show();
        }

        private void ModifyButton_Click(object sender, EventArgs e)
        {
            if (InventoryListBox.SelectedItem == null)
            {
                MessageBox.Show("Select an item to modify");
                return;
            }
            this.Close();
            Form modifyform = new ModifyItem(InventoryListBox.SelectedItem.ToString());
            modifyform.Show();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if(InventoryListBox.SelectedItem == null)
            {
                MessageBox.Show("Select an item to remove");
                return;
            }
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string removeitem = "DELETE FROM Inventory WHERE SKU='" + InventoryListBox.SelectedItem + "'";
                SqlCommand cmd = new SqlCommand(removeitem, con);
                con.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    FillListbox();
                    MessageBox.Show("SUCCESS\nSelected item removed");
                }
                catch
                {
                    MessageBox.Show("ERROR\nFailed to remove selected item");
                }
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            this.Close();
            Form addform = new AddItem();
            addform.Show();
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                DataTable dt = new DataTable();
                string iteminfo = "SELECT * FROM Inventory WHERE SKU='"+InventoryListBox.SelectedItem+"'";
                SqlCommand cmd = new SqlCommand(iteminfo, con);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                string message = "";
                foreach(DataRow dr in dt.Rows)
                {
                    message += "Material: "+dr["Material"]+"\nColour: "+dr["Colour"]+"\nSize: "+dr["Size"]+"\nSex: "+dr["Sex"];
                }
                MessageBox.Show(message);
            }
        }
    }
}
