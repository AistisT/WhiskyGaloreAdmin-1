using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace WhiskyGaloreAdmin.Models
{
    public class Warehouse
    {
        public enum changeStatus
        {
            Processed = 1,
        }

        [DisplayName("ID")]
        public int id { get; set; }

        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Change Order Status")]
        public changeStatus cStatus { get; set; }

        public string orderStatus { get; set; }

        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Date")]
        public DateTime currentDate { get; set; }

        [DisplayName("Order Date")]
        public String orderDate { get; set; }

        [DisplayName("Shipper")]
        public String shipper { get; set; }
        [DisplayName("Shipping Cost")]
        public decimal sCost { get; set; }
        [DisplayName("Tax")]
        public decimal tax { get; set; }
        [DisplayName("Total Price")]
        public decimal price { get; set; }

        [DisplayName("Customer Type")]
        public String type { get; set; }
        [DisplayName("Customer Name")]
        public String name { get; set; }
        [DisplayName("First Phone Number")]
        public String fNumber { get; set; }
        [DisplayName("Second Phone Number")]
        public String sNumber { get; set; }
        [DisplayName("E-mail")]
        public String email { get; set; }
        [DisplayName("Fax")]
        public String fax { get; set; }
        [DisplayName("Address")]
        public String address { get; set; }


        string constr = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
        public DataTable dt { get; set; }

        public void ordersTable()
        {
            this.dt = new DataTable();
            try
            {
                MySqlConnection con = new MySqlConnection(constr);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("getOrdersForStaff", con);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);

                sda.Fill(dt);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("ordersTable() fail !");
            }
        }

        public void orderDetails(int orderId)
        {
            currentDate = DateTime.Now;
            try
            {
                int branchID = 0;
                MySqlConnection con = new MySqlConnection(constr);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("getOrderDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@orderID", orderId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    id = orderId;
                    orderStatus = reader.GetString("orderStatus");
                    price = reader.GetDecimal("totalPrice");
                    shipper = reader.GetString("companyName");
                    sCost = reader.GetDecimal("shippingCost");
                    tax = reader.GetDecimal("tax");
                    orderDate = reader.GetDateTime("orderDate").ToString("yyyy-MM-dd");

                    if (!reader.IsDBNull(reader.GetOrdinal("branch_branchId"))) { branchID = reader.GetInt32("branch_branchId"); }

                    name = reader.GetString("title") + " " + reader.GetString("forename") + " " + reader.GetString("surname");
                    fNumber = reader.GetString("firstNumber");
                    if (!reader.IsDBNull(reader.GetOrdinal("secondaryNumber"))) { sNumber = reader.GetString("secondaryNumber"); } else { sNumber = null; }
                    email = reader.GetString("email");
                    if (!reader.IsDBNull(reader.GetOrdinal("fax"))) { fax = reader.GetString("fax"); } else { fax = null; }

                    if (!reader.IsDBNull(reader.GetOrdinal("secondLine")))
                    {
                        address = reader.GetString("firstLine") + ", " + reader.GetString("secondLine") + ", " + reader.GetString("town") + ", " + reader.GetString("region") + ", " + reader.GetString("country") + ", " + reader.GetString("postcode");
                    }
                    else
                    {
                        address = reader.GetString("firstLine") + ", " + reader.GetString("town") + ", " + reader.GetString("region") + ", " + reader.GetString("country") + ", " + reader.GetString("postcode");
                    }
                }
                reader.Close();
                con.Close();

                //get the names of branch and retailer 
                MySqlConnection con1 = new MySqlConnection(constr);
                con1.Open();
                MySqlCommand cmd1 = new MySqlCommand("getBranchRetailer", con1);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@branchID", branchID);
                MySqlDataReader reader1 = cmd1.ExecuteReader();
                if (branchID == 0)
                {
                    type = "Individual Customer";
                }
                else
                {
                    while (reader1.Read())
                    {
                        type = reader1.GetString("companyName") + "-" + reader1.GetString("branchName");

                    }
                    reader1.Close();
                    con1.Close();
                }

                //get products in a single order
                this.dt = new DataTable();
                MySqlConnection con2 = new MySqlConnection(constr);
                con2.Open();
                MySqlCommand cmd2 = new MySqlCommand("getProductsInOrder", con2);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@orderId", orderId);
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd2);

                sda2.Fill(dt);
                cmd2.ExecuteNonQuery();
                con2.Close();


            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("orderDetails(int orderId) fail !");
            }
        }

        public void updateOrderStatus()
        {
            try
            {
                MySqlConnection con = new MySqlConnection(constr);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("updateOrderStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@iorderId", id);
                cmd.Parameters.AddWithValue("@iorderStatus", cStatus.ToString());
                cmd.Parameters.AddWithValue("@d", currentDate.ToString("yyyy-MM-dd"));

                System.Diagnostics.Debug.WriteLine("id " + id.GetType() + " " + id);
                System.Diagnostics.Debug.WriteLine("date " + currentDate.ToString("yyyy-MM-dd"));
                System.Diagnostics.Debug.WriteLine("status " + cStatus.ToString());

                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("updateOrderStatus() fail !");
            }
        } 
    }
}