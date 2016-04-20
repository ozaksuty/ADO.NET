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
    public partial class frmDelete : Form
    {
        public frmDelete()
        {
            InitializeComponent();
        }

        SqlConnection cnn = new SqlConnection("Server=.;Database=NORTHWND;Trusted_Connection=True;");
        private void frmDelete_Load(object sender, EventArgs e)
        {
            BindCategories();
        }

        void BindCategories()
        {
            cmbCategories.DataSource = null;
            cmbCategories.Items.Clear();

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

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            Categories selectedCategory = (Categories)cmbCategories.SelectedItem;
            if (selectedCategory.CategoryID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Categories WHERE CategoryID=@CategoryID"
                    , cnn);

                cmd.Parameters.AddWithValue("@CategoryID", selectedCategory.CategoryID);

                cnn.Open();

                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted");

                cnn.Close(); //Unutmuyoruz!

                BindCategories();
            }
            else
            {
                MessageBox.Show("Select a Category!");
            }
        }
    }
}
