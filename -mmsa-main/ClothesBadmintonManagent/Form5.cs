using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClothesBadmintonManagent
{
    public partial class Form5 : Form
    {
        // Chuỗi kết nối đến cơ sở dữ liệu
        string connectstring = @"Data Source=LAPTOP-U00M2T5F\MSSQLSERVER02;Initial Catalog=ASM;Integrated Security=True;TrustServerCertificate=True";

        SqlConnection con; // Đối tượng kết nối SQL
        SqlCommand cmd; // Đối tượng lệnh SQL
        SqlDataAdapter adt; // Đối tượng để lấy dữ liệu từ DB
        DataTable dt = new DataTable(); // Bảng dữ liệu để lưu trữ sản phẩm
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectstring); // Khởi tạo kết nối với cơ sở dữ liệu
            try
            {
                con.Open(); // Mở kết nối
                cmd = new SqlCommand("select * from Products", con); // Tạo lệnh SQL để lấy tất cả sản phẩm
                adt = new SqlDataAdapter(cmd); // Tạo SqlDataAdapter từ lệnh
                adt.Fill(dt); // Điền dữ liệu vào DataTable
                GridV_hienthi6.DataSource = dt; // Gán DataTable cho DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Hiển thị thông báo lỗi nếu có
            }
            finally
            {
                con.Close(); // Đóng kết nối
            }
        }

        private void GridV_hienthi6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra nếu chỉ số dòng hợp lệ
            {
                DataGridViewRow selectedRow = GridV_hienthi6.Rows[e.RowIndex];

                try
                {
                    // Gán giá trị từ dòng được chọn vào các TextBox tương ứng theo đúng thứ tự SQL
                    txtB_idProduct.Text = selectedRow.Cells["ProductID"].Value.ToString();       // ProductID
                    txtB_nameProduct.Text = selectedRow.Cells["ProductName"].Value.ToString();   // ProductName

                    if (selectedRow.Cells["ProductImage"].Value != DBNull.Value)                // ProductImage
                    {
                        byte[] imageData = (byte[])selectedRow.Cells["ProductImage"].Value;
                        if (imageData != null && imageData.Length > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                //picB_imageProduct.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            //picB_imageProduct.Image = null;
                        }
                    }
                    else
                    {
                        //picB_imageProduct.Image = null;
                    }

                    //cbB_sizeProduct.Text = selectedRow.Cells["SizeProduct"].Value.ToString();   // SizeProduct
                    txtB_inputPrice.Text = selectedRow.Cells["InputPrice"].Value.ToString();    // InputPrice
                    txtB_inventProduct.Text = selectedRow.Cells["InventoryPrice"].Value.ToString(); // InventoryPrice
                    txtB_priceProduct.Text = selectedRow.Cells["SellingPrice"].Value.ToString(); // SellingPrice
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message); // Hiển thị thông báo lỗi nếu có
                }
            }
        }

        private void btn_addProduct_Click(object sender, EventArgs e)
        {
            // Kiểm tra tính hợp lệ của dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txtB_idProduct.Text) || // Kiểm tra ProductID
                string.IsNullOrWhiteSpace(txtB_nameProduct.Text) || // Kiểm tra ProductName
                //string.IsNullOrWhiteSpace(cbB_sizeProduct.Text) || // Kiểm tra SizeProduct
                !decimal.TryParse(txtB_inputPrice.Text, out decimal InputPrice) || // Kiểm tra InputPrice
                !decimal.TryParse(txtB_inventProduct.Text, out decimal InventoryPrice) || // Kiểm tra InventoryPrice
                !decimal.TryParse(txtB_priceProduct.Text, out decimal SellingPrice)) // Kiểm tra SellingPrice
            {
                MessageBox.Show("Please fill in all fields with valid data.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Thoát nếu dữ liệu không hợp lệ
            }

            // Chuyển đổi đường dẫn hình ảnh thành mảng byte
            byte[] productImage = PathToByteArray(this.Text); // Đảm bảo this.Text chứa đường dẫn hình ảnh hoặc sửa thành đường dẫn cụ thể.

            // Kết nối cơ sở dữ liệu
            using (SqlConnection con = new SqlConnection(connectstring))
            {
                try
                {
                    con.Open(); // Mở kết nối

                    // Tạo lệnh SQL INSERT để thêm sản phẩm
                    string query = @"INSERT INTO Products (ProductID, ProductName, SizeProduct, ProductImage, InputPrice, InventoryPrice, SellingPrice) 
                             VALUES (@ProductID, @ProductName, @SizeProduct, @ProductImage, @InputPrice, @InventoryPrice, @SellingPrice)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Gán tham số cho câu lệnh SQL
                        cmd.Parameters.AddWithValue("@ProductID", int.Parse(txtB_idProduct.Text)); // Lấy ProductID
                        cmd.Parameters.AddWithValue("@ProductName", txtB_nameProduct.Text); // Lấy ProductName
                        //cmd.Parameters.AddWithValue("@SizeProduct", cbB_sizeProduct.Text); // Lấy SizeProduct
                        cmd.Parameters.AddWithValue("@ProductImage", productImage); // Lấy ProductImage
                        cmd.Parameters.AddWithValue("@InputPrice", InputPrice); // Lấy InputPrice
                        cmd.Parameters.AddWithValue("@InventoryPrice", InventoryPrice); // Lấy InventoryPrice
                        cmd.Parameters.AddWithValue("@SellingPrice", SellingPrice); // Lấy SellingPrice

                        cmd.ExecuteNonQuery(); // Thực thi câu lệnh INSERT
                    }

                    // Thông báo thành công
                    MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Tải lại danh sách sản phẩm
                    LoadProducts();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Chuyển đổi file hình ảnh thành mảng byte
        byte[] PathToByteArray(string path)
        {
            MemoryStream m = new MemoryStream(); // Tạo MemoryStream để lưu trữ hình ảnh
            Image img = Image.FromFile(path); // Tải hình ảnh từ file
            img.Save(m, System.Drawing.Imaging.ImageFormat.Png); // Lưu hình ảnh vào MemoryStream
            return m.ToArray(); // Trả về mảng byte
        }

        private void LoadProducts()
        {
            dt.Clear(); // Xóa dữ liệu cũ trong DataTable
            using (SqlConnection con = new SqlConnection(connectstring)) // Tạo kết nối mới
            {
                try
                {
                    con.Open(); // Mở kết nối
                    cmd = new SqlCommand("SELECT * FROM Products", con); // Tạo lệnh SQL để lấy tất cả sản phẩm
                    adt = new SqlDataAdapter(cmd); // Tạo SqlDataAdapter từ lệnh
                    adt.Fill(dt); // Điền dữ liệu vào DataTable
                    GridV_hienthi6.DataSource = dt; // Gán DataTable cho DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); // Hiển thị thông báo lỗi nếu có
                }
            }
        }

        private void picB_imageProduct_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog(); // Tạo hộp thoại mở file
            if (open.ShowDialog() == DialogResult.OK) // Kiểm tra xem người dùng đã chọn file chưa
            {
                //picB_imageProduct.Image = Image.FromFile(open.FileName); // Hiển thị hình ảnh đã chọn
                this.Text = open.FileName; // Hiển thị đường dẫn file cho mục đích gỡ lỗi
            }
        }
        private void btn_updProduct_Click(object sender, EventArgs e)
        {
            // Kiểm tra tính hợp lệ của dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txtB_idProduct.Text) || // Kiểm tra ProductID
                string.IsNullOrWhiteSpace(txtB_nameProduct.Text) || // Kiểm tra ProductName
                !decimal.TryParse(txtB_inputPrice.Text, out decimal InputPrice) || // Kiểm tra InputPrice
                !decimal.TryParse(txtB_inventProduct.Text, out decimal InventoryPrice) || // Kiểm tra InventoryPrice
                !decimal.TryParse(txtB_priceProduct.Text, out decimal SellingPrice)) // Kiểm tra SellingPrice
            {
                MessageBox.Show("Please enter valid values for Product ID, Product Name, Input Price, Inventory Price, and Selling Price.",
                                "Invalid Input",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return; // Thoát nếu dữ liệu không hợp lệ
            }

            // Chuyển đổi đường dẫn hình ảnh thành mảng byte
            byte[] productImage = PathToByteArray(this.Text); // Đảm bảo this.Text chứa đường dẫn hình ảnh hợp lệ.

            // Mở kết nối với cơ sở dữ liệu
            using (SqlConnection con = new SqlConnection(connectstring)) // Tạo kết nối mới
            {
                try
                {
                    con.Open(); // Mở kết nối

                    // Câu lệnh UPDATE
                    string query = @"UPDATE Products 
                             SET ProductName = @ProductName, 
                                 ProductImage = @ProductImage, 
                                 SizeProduct = @SizeProduct, 
                                 InputPrice = @InputPrice, 
                                 InventoryPrice = @InventoryPrice, 
                                 SellingPrice = @SellingPrice 
                             WHERE ProductID = @ProductID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Thêm tham số theo đúng thứ tự cột
                        cmd.Parameters.AddWithValue("@ProductID", int.Parse(txtB_idProduct.Text)); // ProductID
                        cmd.Parameters.AddWithValue("@ProductName", txtB_nameProduct.Text); // ProductName
                        cmd.Parameters.AddWithValue("@ProductImage", productImage); // ProductImage
                                                                                    // Thêm tham số SizeProduct nếu có, nếu không thì bỏ qua
                                                                                    // cmd.Parameters.AddWithValue("@SizeProduct", cbB_sizeProduct.Text); // Uncomment nếu cần sử dụng SizeProduct
                        cmd.Parameters.AddWithValue("@InputPrice", InputPrice); // InputPrice
                        cmd.Parameters.AddWithValue("@InventoryPrice", InventoryPrice); // InventoryPrice
                        cmd.Parameters.AddWithValue("@SellingPrice", SellingPrice); // SellingPrice

                        // Thực thi lệnh
                        cmd.ExecuteNonQuery(); // Cập nhật sản phẩm
                    }

                    MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts(); // Tải lại danh sách sản phẩm sau khi cập nhật
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_deleProduct_Click(object sender, EventArgs e)
        {
            if (GridV_hienthi6.SelectedRows.Count > 0) // Kiểm tra xem có dòng nào được chọn không
            {
                // Lấy dòng được chọn
                var selectedRow = GridV_hienthi6.SelectedRows[0];
                int productIdToDelete = Convert.ToInt32(selectedRow.Cells["ProductID"].Value); // Lấy ProductID từ dòng đã chọn

                // Xác nhận trước khi xóa
                var confirmResult = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(connectstring)) // Tạo kết nối mới
                    {
                        try
                        {
                            con.Open(); // Mở kết nối

                            // Xóa các bản ghi liên quan trong các bảng khác nếu có
                            string deleteRelatedQuery = "DELETE FROM OrderDetails WHERE ProductID = @ProductID";
                            using (SqlCommand cmdRelated = new SqlCommand(deleteRelatedQuery, con))
                            {
                                cmdRelated.Parameters.AddWithValue("@ProductID", productIdToDelete);
                                cmdRelated.ExecuteNonQuery();
                            }

                            // Tạo câu lệnh DELETE để xóa sản phẩm
                            string deleteProductQuery = "DELETE FROM Products WHERE ProductID = @ProductID";
                            using (SqlCommand cmdProduct = new SqlCommand(deleteProductQuery, con))
                            {
                                cmdProduct.Parameters.AddWithValue("@ProductID", productIdToDelete); // Thêm tham số ProductID
                                int rowsAffected = cmdProduct.ExecuteNonQuery(); // Thực thi câu lệnh DELETE

                                if (rowsAffected > 0)
                                {
                                    // Xóa dòng khỏi DataGridView sau khi xóa thành công trong cơ sở dữ liệu
                                    GridV_hienthi6.Rows.RemoveAt(selectedRow.Index);
                                    MessageBox.Show("Product deleted successfully!");
                                }
                                else
                                {
                                    MessageBox.Show("No product found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show($"SQL Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Hiển thị lỗi cơ sở dữ liệu nếu có
                        }
                        finally
                        {
                            con.Close(); // Đóng kết nối
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete."); // Thông báo nếu không có dòng nào được chọn
            }
        }


        private void btn_search_Click(object sender, EventArgs e)
        {
            // Lấy ID sản phẩm từ TextBox
            string productId = txtB_search.Text.Trim();

            // Kiểm tra ID không rỗng
            if (!string.IsNullOrEmpty(productId))
            {
                using (SqlConnection con = new SqlConnection(connectstring)) // Tạo kết nối mới
                {
                    try
                    {
                        con.Open(); // Mở kết nối

                        // Truy vấn thông tin sản phẩm theo ProductID
                        string query = @"SELECT ProductID, ProductName, SizeProduct, InputPrice, InventoryPrice, SellingPrice, ProductImage 
                                 FROM Products 
                                 WHERE ProductID = @ProductID";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", productId); // Thêm tham số ProductID
                            SqlDataReader reader = cmd.ExecuteReader(); // Thực thi truy vấn

                            if (reader.Read()) // Nếu có dữ liệu trả về
                            {
                                // Lấy thông tin sản phẩm
                                string id = reader["ProductID"].ToString();
                                string name = reader["ProductName"].ToString();
                                string size = reader["SizeProduct"].ToString();
                                decimal inputPrice = reader.GetDecimal(reader.GetOrdinal("InputPrice")); // Giá nhập
                                decimal inventoryPrice = reader.GetDecimal(reader.GetOrdinal("InventoryPrice")); // Giá tồn kho
                                decimal sellingPrice = reader.GetDecimal(reader.GetOrdinal("SellingPrice")); // Giá bán
                                // Hiển thị thông tin sản phẩm
                                MessageBox.Show($"Product information:\n" +
                                $"ID: {id}\n" +
                                $"Name {name}\n" +
                                $"Size: {size}\n" +
                                $"Input Price: {inputPrice:C}\n" +
                                $"Inventory Price: {inventoryPrice:C}\n" +
                                $"Selling Price: {sellingPrice:C}",
                                "Product Information",MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            }
                            else
                            {
                            // Thông báo nếu không tìm thấy
                            MessageBox.Show($"No product found with ID: {productId}",
                            "No found",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                    // Hiển thị lỗi cơ sở dữ liệu nếu có
                    MessageBox.Show($"Database error: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Thông báo nếu ID rỗng
            MessageBox.Show("Please enter product ID to search.",
            "Notification",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
