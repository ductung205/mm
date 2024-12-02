using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClothesBadmintonManagent
{
    public partial class Form4 : Form
    {
        string connectstring = @"Data Source=LAPTOP-U00M2T5F\MSSQLSERVER02;Initial Catalog=ASM;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection con;
        SqlDataAdapter adt;
        DataTable dt = new DataTable();

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();
        }

        // Method to load employee data
        private void LoadEmployeeData()
        {
            using (con = new SqlConnection(connectstring))
            {
                try
                {
                    string selectQuery = "SELECT * FROM Employee";
                    adt = new SqlDataAdapter(selectQuery, con);
                    dt.Clear();
                    adt.Fill(dt);
                    grView_hienthi7.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // Method for handling cell click event to populate fields
        private void grView_hienthi7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = grView_hienthi7.Rows[e.RowIndex];
                txtB_idEmployee.Text = selectedRow.Cells[0].Value.ToString();
                txtB_nameEmployee.Text = selectedRow.Cells[1].Value.ToString();
                if (selectedRow.Cells[2].Value.ToString() == "FEMALE")
                {
                    rad_femaEmp.Checked = true;
                }
                else
                {
                    rad_maleEmp.Checked = true;
                }
                txtB_emailEmp.Text = selectedRow.Cells[3].Value.ToString();
                txtB_phoneEmp.Text = selectedRow.Cells[4].Value.ToString();
                txtB_dateEmp.Text = selectedRow.Cells[5].Value.ToString();
            }
        }

        // Method to add a new employee
        private void btn_addEmp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtB_idEmployee.Text) ||
                string.IsNullOrWhiteSpace(txtB_nameEmployee.Text) ||
                string.IsNullOrWhiteSpace(txtB_emailEmp.Text) ||
                string.IsNullOrWhiteSpace(txtB_phoneEmp.Text) ||
                string.IsNullOrWhiteSpace(txtB_dateEmp.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            string ID = txtB_idEmployee.Text;
            string Name = txtB_nameEmployee.Text;
            string Gender = rad_femaEmp.Checked ? "FEMALE" : "MALE";
            string Email = txtB_emailEmp.Text;
            string Phone = txtB_phoneEmp.Text;
            DateTime BirthofDate;

            if (!DateTime.TryParse(txtB_dateEmp.Text, out BirthofDate))
            {
                MessageBox.Show("Invalid date format. Please use yyyy-MM-dd.");
                return;
            }

            string insertQuery = "INSERT INTO Employee (EmployeeID, EmployeeName, Gender, Email, Phone, BirthofDate) VALUES (@EmployeeID, @EmployeeName, @Gender, @Email, @Phone, @BirthofDate)";

            using (con = new SqlConnection(connectstring))
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", ID);
                    cmd.Parameters.AddWithValue("@EmployeeName", Name);
                    cmd.Parameters.AddWithValue("@Gender", Gender);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Phone", Phone);
                    cmd.Parameters.AddWithValue("@BirthofDate", BirthofDate);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Employee added successfully!");
                        LoadEmployeeData();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Database error: " + ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        // Method to update an employee
        private void btn_updateEmp_Click(object sender, EventArgs e)
        {
            string EmployeeID = txtB_idEmployee.Text.Trim();
            string EmployeeName = txtB_nameEmployee.Text.Trim();
            string Gender = rad_femaEmp.Checked ? "FEMALE" : "MALE";
            DateTime BirthofDate;
            string Email = txtB_emailEmp.Text.Trim();
            string Phone = txtB_phoneEmp.Text.Trim();

            if (string.IsNullOrWhiteSpace(EmployeeID))
            {
                MessageBox.Show("Please enter the EmployeeID to update!");
                return;
            }

            if (string.IsNullOrWhiteSpace(EmployeeName) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Email))
            {
                MessageBox.Show("Please fill in all required fields!");
                return;
            }

            if (!DateTime.TryParse(txtB_dateEmp.Text, out BirthofDate))
            {
                MessageBox.Show("Invalid date format. Please use yyyy-MM-dd.");
                return;
            }

            string updateQuery = @"
                UPDATE Employee 
                SET 
                    EmployeeName = @EmployeeName, 
                    Gender = @Gender, 
                    Email = @Email, 
                    Phone = @Phone, 
                    BirthofDate = @BirthofDate 
                WHERE 
                    EmployeeID = @EmployeeID";

            using (con = new SqlConnection(connectstring))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                        cmd.Parameters.AddWithValue("@EmployeeName", EmployeeName);
                        cmd.Parameters.AddWithValue("@Gender", Gender);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Phone", Phone);
                        cmd.Parameters.AddWithValue("@BirthofDate", BirthofDate);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Employee updated successfully!");
                            LoadEmployeeData();
                        }
                        else
                        {
                            MessageBox.Show("No employee found with the specified EmployeeID.");
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

        // Method to delete an employee
        private void btn_deleEmp_Click(object sender, EventArgs e)
        {
            string EmployeeID = txtB_idEmployee.Text;

            if (string.IsNullOrWhiteSpace(EmployeeID))
            {
                MessageBox.Show("Please enter a valid Employee ID", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Are you sure you want to delete this employee?", "Confirm Delete", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(connectstring))
                {
                    try
                    {
                        con.Open();

                        // Xóa các bản ghi liên quan trong bảng Users
                        string deleteUsersQuery = "DELETE FROM Users WHERE EmployeeID = @EmployeeID";
                        using (SqlCommand cmdUsers = new SqlCommand(deleteUsersQuery, con))
                        {
                            cmdUsers.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                            cmdUsers.ExecuteNonQuery();
                        }

                        // Xóa bản ghi trong bảng Employee
                        string deleteEmployeeQuery = "DELETE FROM Employee WHERE EmployeeID = @EmployeeID";
                        using (SqlCommand cmdEmployee = new SqlCommand(deleteEmployeeQuery, con))
                        {
                            cmdEmployee.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                            int rowsAffected = cmdEmployee.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Employee deleted successfully!");
                                LoadEmployeeData();
                            }
                            else
                            {
                                MessageBox.Show("No employee found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("SQL Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        // Method to exit the form
        private void btn_exitEmp_Click(object sender, EventArgs e)
        {
            DialogResult exitDialog = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (exitDialog == DialogResult.Yes)
            {
                this.Hide();
            }
            else
            {
                MessageBox.Show("Stay on the current form.");
            }
        }
    }
}
