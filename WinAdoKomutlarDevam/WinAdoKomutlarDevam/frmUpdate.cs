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

namespace WinAdoKomutlarDevam
{
    public partial class frmUpdate : Form
    {
        public frmUpdate()
        {
            InitializeComponent();
        }

        SqlConnection cnn = new SqlConnection("Server=.;Database=NORTHWND;Trusted_Connection=True;");
        private void frmUpdate_Load(object sender, EventArgs e)
        {
            BindCategories();
        }

        void BindCategories()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Categories", cnn);
            cnn.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            List<Categories> categoryList = new List<Categories>();

            categoryList.Add(new Categories()
            {
                CategoryID = 0,
                CategoryName = "Select a Category"
            });
            while (dr.Read())
            {
                categoryList.Add(new Categories()
                {
                    CategoryID = (int)dr["CategoryID"],
                    CategoryName = dr["CategoryName"].ToString(),
                    Description = dr["Description"].ToString()
                });
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
            public string Description { get; set; }
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            Categories selectedCategory = (Categories)combo.SelectedItem;
            if (selectedCategory.CategoryID > 0)
            {
                txtCategoryName.Text = selectedCategory.CategoryName;
                txtDescription.Text = selectedCategory.Description;
            }
            else
            {
                txtCategoryName.Text = "";
                txtDescription.Text = "";
            }
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Categories SET CategoryName=@CategoryName, Description=@Description WHERE CategoryID=@CategoryID"
                , cnn);

            Categories selectedCategory = (Categories)cmbCategories.SelectedItem;

            cmd.Parameters.AddWithValue("@CategoryID", selectedCategory.CategoryID);
            cmd.Parameters.AddWithValue("@CategoryName", txtCategoryName.Text);
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

            cnn.Open();

            cmd.ExecuteNonQuery();
            MessageBox.Show("Updated");
            BindCategories();

            cnn.Close(); //Unutmuyoruz!
        }
    }
}
