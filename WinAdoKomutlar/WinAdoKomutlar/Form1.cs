using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAdoKomutlar
{
    public partial class frmADONET : Form
    {
        public frmADONET()
        {
            InitializeComponent();
        }

        SqlConnection cnn = new SqlConnection("Server=.;Database=NORTHWND;Trusted_Connection=True;");
        private void frmADONET_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Products", cnn);
            cnn.Open();
            int productCount = (int)cmd.ExecuteScalar();
            lblProductCount.Text = productCount.ToString();
            cnn.Close();
        }

        private void btnGetCategories_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Categories", cnn);
            cnn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            List<Categories> categoryList = new List<Categories>();
            
            while (dr.Read())
            {
                categoryList.Add(new Categories() { CategoryID = (int)dr["CategoryID"],
                    CategoryName = dr["CategoryName"].ToString() });
            }

            cnn.Close();

            cmbCategories.DataSource = categoryList;
            cmbCategories.DisplayMember = "CategoryName";
            cmbCategories.ValueMember = "CategoryID";
        }

        class Categories
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            Categories selectedCategory = (Categories)combo.SelectedItem;

            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Products WHERE CategoryID=" + 
                selectedCategory.CategoryID, cnn);
            cnn.Open();
            int productCount = (int)cmd.ExecuteScalar();
            lblProductCount.Text = productCount.ToString();
            cnn.Close();
        }
    }
}
