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
    public partial class frmAdoCrud : Form
    {
        public frmAdoCrud()
        {
            InitializeComponent();
        }

        SqlConnection cnn = new SqlConnection("Server=.;Database=NORTHWND;Trusted_Connection=True;");
        private void btnInsertCategory_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Categories (CategoryName, Description) VALUES(@CategoryName, @Description)"
                , cnn);

            cmd.Parameters.AddWithValue("@CategoryName", txtCategoryName.Text);
            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

            cnn.Open();

            cmd.ExecuteNonQuery();
            MessageBox.Show("Inserted");

            cnn.Close(); //Unutmuyoruz!
        }
    }
}