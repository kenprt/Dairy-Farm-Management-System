using MaterialSkin;
using MaterialSkin.Controls;
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
using Microsoft.Reporting.WinForms;

namespace ManageTask
{
    public partial class Form1 : MaterialForm
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-IS912V6;Initial Catalog=dairyFarm;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue800, Primary.Blue900, Primary.Blue500, Accent.Blue200, TextShade.WHITE);

        }
        public void loadChart()
        {
            try
            {
              
                {
                    SqlCommand command = new SqlCommand("Select address as Name, count(id) as Total from farmer group by address", con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    chart7.DataSource = dt;
                    chart7.Series["Series1"].XValueMember = "Name";
                    chart7.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                    chart7.Series["Series1"].YValueMembers = "Total";
                    chart7.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public void loadSpesific()
        {
            try
            {

                {
                    SqlCommand command = new SqlCommand("Select date as Name, sum(quantity) as Total from collection where nationalnum ='"+txtfilter.Text+"' group by date", con);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    chart1.DataSource = dt;
                    chart1.Series["Series1"].XValueMember = "Name";
                    chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                    chart1.Series["Series1"].YValueMembers = "Total";
                    chart1.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void countFarmers()
        {
            SqlCommand cmd = new SqlCommand("Select count (id) as total from farmer", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                lblfarmers.Text = dt.Rows[0]["total"].ToString();
            }
        }
        public void countMilk()
        {
            SqlCommand cmd = new SqlCommand("Select sum (quantity) as total from collection", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                lblmilk.Text = dt.Rows[0]["total"].ToString();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dairyFarmDataSet3.collection' table. You can move, or remove it, as needed.
            this.collectionTableAdapter1.Fill(this.dairyFarmDataSet3.collection);
            // TODO: This line of code loads data into the 'dairyFarmDataSet1.collection' table. You can move, or remove it, as needed.
            this.collectionTableAdapter.Fill(this.dairyFarmDataSet1.collection);
            // TODO: This line of code loads data into the 'dairyFarmDataSet.farmer' table. You can move, or remove it, as needed.
            this.farmerTableAdapter.Fill(this.dairyFarmDataSet.farmer);
            materialComboBox1.Text = "Category";
            bindFarmer();
            findFarmer();
            loadChart();
            countFarmers();
            bindCollection();
            countMilk();
            find();
            loadGeneralReports();
            this.reportViewer1.RefreshReport();
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void materialCard8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }
        public void loadGeneralReports()
        {
            SqlCommand cmd = new SqlCommand("select * from collection", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = @"C:\Users\Ken Waweru\source\repos\ManageTask\ManageTask\collectionrpt.rdlc";
            ReportDataSource source = new ReportDataSource("DataSet1", table);
            reportViewer1.LocalReport.DataSources.Add(source);
            reportViewer1.RefreshReport();
        }
        public void bindFarmer()
        {
            SqlCommand cmd = new SqlCommand("Select * from farmer", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            bunifuDataGridView1.DataSource = table;
            
        }
        public void clearFarmer()
        {
            txtid.Text = "";
            txtname.Text = "";
            txtaddress.Text = "";
            txphone.Text = "";
        }
        public void findFarmer()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select * FROM farmer WHERE id LIKE '" + this.txtfindfarmer.Text + "%'OR name LIKE '" + this.txtfindfarmer.Text + "%' OR address LIKE '" + this.txtfindfarmer.Text + "%' OR phone LIKE '" + this.txtfindfarmer.Text + "%'", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            bunifuDataGridView1.DataSource = dt;
        }
        public void find()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select * FROM collection WHERE nationalnum LIKE '" + this.txtfind.Text + "%'", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            bunifuDataGridView2.DataSource = dt;
        }
        private void materialButton1_Click(object sender, EventArgs e)
        {
            if (txtid.Text == "" || txtname.Text == ""|| txtaddress.Text == "" || txphone.Text == "" )
            {
                MessageBox.Show("All Fields are required", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }else if(txphone.Text.Length != 10)
            {
                MessageBox.Show("Invalid phone Number", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    if(con.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand command = new SqlCommand("Select * from farmer where id = '" + txtid.Text + "'", con);
                        SqlDataAdapter sda = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if(dt.Rows.Count > 0)
                        {
                            SqlCommand doUpdate = new SqlCommand("Update farmer set name = '" + txtname.Text + "', address = '" + txtaddress.Text + "', phone = '" + txphone.Text + "' where id = '" + txtid.Text + "'", con);
                            doUpdate.ExecuteNonQuery();
                            MessageBox.Show("Farmer's details Updated Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bindFarmer();
                            loadChart();
                            countFarmers();
                            clearFarmer();
                        }
                        else
                        {
                            SqlCommand addFarmer = new SqlCommand("Insert into farmer(id, name, address, phone, date)Values('" + txtid.Text + "','" + txtname.Text + "', '" + txtaddress.Text + "', '" + txphone.Text + "','"+dateTimePicker1.Text+"')", con);
                            addFarmer.ExecuteNonQuery();
                            MessageBox.Show("New Farmer Added Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bindFarmer();
                            loadChart();
                            countFarmers();
                            clearFarmer();
                        }
                    }

                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void txtfindfarmer_TextChanged(object sender, EventArgs e)
        {
            findFarmer();
        }
        public void bindCollection()
        {
            SqlCommand cmd = new SqlCommand("Select * from collection", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            bunifuDataGridView2.DataSource = table;
        }
        public void clearCollection()
        {
            txtnatioanl.Text = "";
            txtquatity.Text = "";
            materialComboBox1.Text = "";
        }
        private void materialButton2_Click(object sender, EventArgs e)
        {
            if(txtnatioanl.Text == "" || txtquatity.Text == "" || materialComboBox1.Text == "")
            {
                MessageBox.Show("All fields are required", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    con.Open();
                    if(con.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand search = new SqlCommand("Select * from category where category = '"+materialComboBox1.SelectedItem+"'", con);
                        SqlDataAdapter sda = new SqlDataAdapter(search);
                        DataTable dta = new DataTable();
                        sda.Fill(dta);
                        if(dta.Rows.Count > 0)
                        {
                            lblap.Text = dta.Rows[0][2].ToString();
                            lblmultiply.Text = (float.Parse(lblap.Text) * float.Parse(txtquatity.Text)).ToString();
                        }
                        SqlCommand cmd = new SqlCommand("Select * from farmer where id ='" + txtnatioanl.Text + "'", con);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            SqlCommand command = new SqlCommand("Insert into collection(nationalnum, category, quantity, date, total)Values('"+txtnatioanl.Text+"','"+materialComboBox1.SelectedItem+ "','" + txtquatity.Text + "', '"+bunifuDatePicker1.Text+"', '"+lblmultiply.Text+"')", con);
                            command.ExecuteNonQuery();
                            bindCollection();
                            clearCollection();
                            countMilk();
                        }
                        else
                        {
                            MessageBox.Show("This Farmer is Not recorded in the System", "Kindly Add Farmer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void txtfind_TextChanged(object sender, EventArgs e)
        {
            find();
        }
        public void upComming()
        {
            SqlCommand calc = new SqlCommand("Select sum(payment) as total from payment where id = '" + txtfilter.Text + "'", con);
            SqlDataAdapter sdac = new SqlDataAdapter(calc);
            DataTable dtac = new DataTable();
            sdac.Fill(dtac);
            if (dtac.Rows.Count > 0)
            {
                label3.Text = dtac.Rows[0]["total"].ToString();

                 lblupcoming.Text = (float.Parse(label3.Text) - float.Parse(label2.Text)).ToString();
            }
            else
            {
                //lblupcoming.Text = label2.Text;
                //lblpayout.Text = "0.00";
                MessageBox.Show("There are currently no payouts for this farmer", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void calc()
        {
            SqlCommand pay = new SqlCommand("Select sum(total) as Total from collection where nationalnum = '" + txtfilter.Text + "'", con);
            SqlDataAdapter sda = new SqlDataAdapter(pay);
            DataTable dta = new DataTable();
            sda.Fill(dta);
            if (dta.Rows.Count > 0)
            {
                label2.Text = dta.Rows[0]["Total"].ToString();

            }
            else
            {
                MessageBox.Show("Milk for this farmer farmer is not collected Yet");
            }
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            if(txtfilter.Text == "")
            {
                MessageBox.Show("The search field should not be Empty!","", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    if(con.State == System.Data.ConnectionState.Open)
                    {
                        
                        SqlCommand cmd = new SqlCommand("Select * From farmer where id = '" + txtfilter.Text + "'", con);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        if(table.Rows.Count > 0)
                        {
                            lblname.Text = table.Rows[0][1].ToString();
                            loadSpesific();
                            upComming();
                            calc();
                           

                        }

                        else
                        {
                            MessageBox.Show("This Farmer's id does Not exist", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                       


                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }

            }
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuCircleProgress1_ProgressChanged(object sender, Bunifu.UI.WinForms.BunifuCircleProgress.ProgressChangedEventArgs e)
        {

        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from collection where date between '"+bunifuDatePicker2.Text+"' AND '"+bunifuDatePicker3.Text+"'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = @"C:\Users\Ken Waweru\source\repos\ManageTask\ManageTask\collectionrpt.rdlc";
            ReportDataSource source = new ReportDataSource("DataSet1", table);
            reportViewer1.LocalReport.DataSources.Add(source);
            reportViewer1.RefreshReport();
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select date as Name, sum(quantity) as Total from collection where nationalnum ='" + txtfilter.Text + "' and date between '"+ bunifuDatePicker4 .Text+ "' and '"+ bunifuDatePicker5 .Text+ "'group by date", con);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            chart1.DataSource = dt;
            chart1.Series["Series1"].XValueMember = "Name";
            chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            chart1.Series["Series1"].YValueMembers = "Total";
            chart1.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
        }
        

        private void materialButton7_Click(object sender, EventArgs e)
        {
            if(txtsearchpay.Text == "")
            {
                MessageBox.Show("The search field is required", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    con.Open();
                    if(con.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand pay = new SqlCommand("Select sum(total) as t from collection where nationalnum = '" + txtfilter.Text + "'", con);
                        SqlDataAdapter sda = new SqlDataAdapter(pay);
                        DataTable dta = new DataTable();
                        sda.Fill(dta);
                        if (dta.Rows.Count > 0)
                        {
                            label5.Text = dta.Rows[0]["t"].ToString();
                            SqlCommand calc = new SqlCommand("Select sum(payment) as total from payment where id = '" + txtfilter.Text + "'", con);
                            SqlDataAdapter sdac = new SqlDataAdapter(calc);
                            DataTable dtac = new DataTable();
                            sdac.Fill(dtac);
                            if (dtac.Rows.Count > 0)
                            {
                                label4.Text = dtac.Rows[0]["total"].ToString();

                                lblpay.Text = (float.Parse(label5.Text) - float.Parse(label3.Text)).ToString();
                            }
                            else
                            {
                                lblpay.Text = label5.Text;
                                MessageBox.Show("There are currently no payouts for this farmer", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                        
                        //SqlCommand pay = new SqlCommand("Select sum()")
                    }

                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void materialButton8_Click(object sender, EventArgs e)
        {
            if(txtsearchpay.Text == "")
            {
                MessageBox.Show("The search field is required", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }else if(lblpay.Text == "0.00")
            {
                MessageBox.Show("This payment is null", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    con.Open();
                    if(con.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand cmd = new SqlCommand("Insert into payement(id, payment, date)Values('" + txtsearchpay.Text + "', '" + lblpay.Text + "', '" + dateTimePicker2.Text + "')", con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Payment caried out Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
