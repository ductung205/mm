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

namespace ClothesBadmintonManagent
{
    public partial class Form3 : Form
    {
        string connectstring = @"Data Source=LAPTOP-U00M2T5F\MSSQLSERVER02;Initial Catalog=ASM;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt = new DataTable();
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectstring);
            try
            {
                con.Open();
                cmd = cmd = new SqlCommand("select * from Customer", con);
                adt = new SqlDataAdapter(cmd);
                adt.Fill(dt);
                grView_hienthi.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grView_hienthi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure that the click is on a valid row (not header or empty)
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = grView_hienthi.Rows[e.RowIndex];

                // Retrieve values from the selected row and display them on the form
                txtB_idCustomer.Text = selectedRow.Cells[0].Value.ToString();         // ID Customer
                txtB_nameCustomer.Text = selectedRow.Cells[1].Value.ToString();       // Name Customer
                if (selectedRow.Cells[2].Value.ToString() == "FEMALE")
                {
                    rad_fema.Checked = true;  // Set Male gender radio button
                }
                else if (selectedRow.Cells[2].Value.ToString() == "MALE")
                {
                    rad_male.Checked = true;  // Set Female gender radio button
                }
                txtB_dateCustomer.Text = selectedRow.Cells[3].Value.ToString();        // Date of Birth
                txtB_phoneCustomer.Text = selectedRow.Cells[4].Value.ToString();      // Phone Number
                txtB_emailCustomer.Text = selectedRow.Cells[5].Value.ToString();      // Email
            }
        }

        private void btn_addCustomer_Click(object sender, EventArgs e)
        {
            string CustomerName = txtB_nameCustomer.Text;
            string Gender = (rad_fema.Checked) ? "FEMALE" : "MALE";
            DateTime BirthofDate;
            string Phone = txtB_phoneCustomer.Text;
            string Gmail = txtB_emailCustomer.Text;

            // Chuyển đổi chuỗi ngày thành kiểu DateTime
            if (!DateTime.TryParse(txtB_dateCustomer.Text, out BirthofDate))
            {
                MessageBox.Show("Invalid date format. Please use yyyy-MM-dd.");
                return;
            }

            string insertQuery = "INSERT INTO Customer (CustomerName, Gender, BirthofDate, Phone, Gmail) " +
                                 "VALUES (@CustomerName, @Gender, @BirthofDate, @Phone, @Gmail); " +
                                 "SELECT SCOPE_IDENTITY();";

            using (SqlCommand cmd = new SqlCommand(insertQuery, con))
            {
                cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@BirthofDate", BirthofDate);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Gmail", Gmail);

                try
                {
                    con.Open();
                    // Lấy giá trị ID mới được thêm vào
                    int newCustomerID = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();

                    // Hiển thị thông báo và làm mới DataGridView
                    MessageBox.Show($"Customer added successfully! New CustomerID: {newCustomerID}");
                    LoadCustomerData();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL Error: " + ex.Message);
                }
            }
        }
        private void btn_updateCustomer_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các trường đầu vào
            string CustomerID = txtB_idCustomer.Text.Trim();
            string CustomerName = txtB_nameCustomer.Text.Trim();
            string Gender = (rad_fema.Checked) ? "FEMALE" : "MALE";
            DateTime BirthofDate;
            string Phone = txtB_phoneCustomer.Text.Trim();
            string Gmail = txtB_emailCustomer.Text.Trim();

            // Kiểm tra CustomerID (bắt buộc nhập)
            if (string.IsNullOrWhiteSpace(CustomerID))
            {
                MessageBox.Show("Please enter the CustomerID to update!");
                return;
            }

            // Kiểm tra các trường không được để trống
            if (string.IsNullOrWhiteSpace(CustomerName) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Gmail))
            {
                MessageBox.Show("Please fill in all required fields!");
                return;
            }

            // Chuyển đổi chuỗi ngày thành kiểu DateTime
            if (!DateTime.TryParse(txtB_dateCustomer.Text, out BirthofDate))
            {
                MessageBox.Show("Invalid date format. Please use yyyy-MM-dd.");
                return;
            }

            // Truy vấn SQL để cập nhật dữ liệu
            string updateQuery = @"
        UPDATE Customer 
        SET 
            CustomerName = @CustomerName, 
            Gender = @Gender, 
            BirthofDate = @BirthofDate, 
            Phone = @Phone, 
            Gmail = @Gmail 
        WHERE 
            CustomerID = @CustomerID";

            // Kết nối tới cơ sở dữ liệu
            using (SqlConnection con = new SqlConnection(connectstring))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        // Gắn các tham số với giá trị
                        cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                        cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
                        cmd.Parameters.AddWithValue("@Gender", Gender);
                        cmd.Parameters.AddWithValue("@BirthofDate", BirthofDate);
                        cmd.Parameters.AddWithValue("@Phone", Phone);
                        cmd.Parameters.AddWithValue("@Gmail", Gmail);

                        // Thực thi truy vấn
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra kết quả
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer updated successfully!");

                            // Làm mới DataGridView để hiển thị dữ liệu mới
                            LoadCustomerData();
                        }
                        else
                        {
                            MessageBox.Show("No customer found with the specified CustomerID.");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }


        private void btn_deleCustomer_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu người dùng đã nhập CustomerID
            string CustomerID = txtB_idCustomer.Text;
            if (string.IsNullOrWhiteSpace(CustomerID))
            {
                MessageBox.Show("Please enter a valid Customer ID", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Nếu không có CustomerID, thoát khỏi hàm
            }

            // Xác nhận trước khi xóa
            var confirmResult = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                // Tạo câu lệnh SQL DELETE
                string deleteQuery = "DELETE FROM Customer WHERE CustomerID = @CustomerID";

                using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID); // Sử dụng CustomerID nhập vào TextBox

                    try
                    {
                        con.Open(); // Mở kết nối
                        cmd.ExecuteNonQuery(); // Thực thi lệnh xóa
                        con.Close(); // Đóng kết nối

                        // Làm mới lại DataGridView
                        LoadCustomerData(); // Giả sử bạn có phương thức này để làm mới dữ liệu
                        MessageBox.Show("Customer deleted successfully!");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("SQL Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LoadCustomerData()
        {
            dt.Clear();
            adt.Fill(dt);
            grView_hienthi.DataSource = dt;
        }

        private void btn_exitCustomer_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Are you want to exit..?",
               "Warning",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);

            if (exit == DialogResult.No)
            {
                MessageBox.Show("STAY..!");
            }
            else
            {
                Hide();
            }
        }
    }
}
