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

namespace ManageTask
{
    public partial class recipt : Form
    {
        public recipt()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-IS912V6;Initial Catalog=dairyFarm;Integrated Security=True");

        private void materialLabel12_Click(object sender, EventArgs e)
        {

        }
        public void loadReceipt()
        {

        }
        private void recipt_Load(object sender, EventArgs e)
        {

        }
    }
}
