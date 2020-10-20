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
    public partial class ModifyItem : Form
    {

        string sku;
        public ModifyItem(string SKU)
        {           
            InitializeComponent();
            
            sku = SKU;

            var ColourDict = new Dictionary<string, string>
            {
                {"Beige","Beige"},
                {"Black","Black"},
                {"Blue","Blue"},
                {"Brown","Brown"},
                {"Burgundy","Burgundy"},
                {"Grey","Grey"},
                {"Red","Red"}
            };
            ColourComboBox.DataSource = new BindingSource(ColourDict, null);
            ColourComboBox.DisplayMember = "Value";
            ColourComboBox.ValueMember = "Key";
            ColourComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            var SexDict = new Dictionary<string, string>
            {
                {"Male","Male"},
                {"Female","Female"}
            };
            SexComboBox.DataSource = new BindingSource(SexDict, null);
            SexComboBox.DisplayMember = "Value";
            SexComboBox.ValueMember = "Key";
            SexComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            var SizeDict = new Dictionary<int, string>
            {
                {6,"6"},
                {7,"7"},
                {8,"8"},
                {9,"9"},
                {10,"10"},
                {11,"11"},
                {12,"12"},
                {13,"13"},
                {14,"14"}
            };
            SizeComboBox.DataSource = new BindingSource(SizeDict, null);
            SizeComboBox.DisplayMember = "Value";
            SizeComboBox.ValueMember = "Key";
            SizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            var MaterialDict = new Dictionary<string, string>
            {
                {"Leather","Leather"},
                {"Canavs","Canvas"},
                {"Plastic","Plastic"}
            };
            MaterialComboBox.DataSource = new BindingSource(MaterialDict, null);
            MaterialComboBox.DisplayMember = "Value";
            MaterialComboBox.ValueMember = "Key";
            MaterialComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ModifyItem_Load(object sender, EventArgs e)
        {

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            string colour = ColourComboBox.SelectedValue.ToString();
            string sex = SexComboBox.SelectedValue.ToString();
            string size = SizeComboBox.SelectedValue.ToString();
            string material = MaterialComboBox.SelectedValue.ToString();

            string connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Callum\Documents\PaversProject\PaversApp\PaversApp\PaversDB.mdf;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string updatequery = "UPDATE Inventory SET Colour=@val1, Sex=@val2, Size=@val3, Material=@val4 WHERE SKU='"+sku+"'";
                using (SqlCommand cmd = new SqlCommand(updatequery, con))
                {
                    cmd.Parameters.AddWithValue("@val1", colour);
                    cmd.Parameters.AddWithValue("@val2", sex);
                    cmd.Parameters.AddWithValue("@val3", size);
                    cmd.Parameters.AddWithValue("@val4", material);
                    con.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("SUCCESS\nSelected item has been modified");
                    }
                    catch
                    {
                        MessageBox.Show("ERROR\nFailed to modify item");
                        con.Close();
                        return;
                    }
                    con.Close();
                    this.Close();
                    Form warehouse = new WarehouseHome();
                    warehouse.Show();
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
            Form warehouse = new WarehouseHome();
            warehouse.Show();
        }
    }
}
