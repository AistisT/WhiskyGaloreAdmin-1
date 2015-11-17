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
    public class Shipping
    {
        public enum orderStatus
        {
            Dispatched = 1,
            Delivered
        }
        [DisplayName("ID")]
        public int id { get; set; }

        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Order Status")]
        public orderStatus oStatus { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Date")]
        public DateTime currentDate { get; set; }

        [DisplayName("Order Date")]
        public String orderDate { get; set; }
        [DisplayName("Required Date")]
        public String requiredDate { get; set; }
        [DisplayName("Processed Date")]
        public String processedDate { get; set; }
        [DisplayName("Dispatched Date")]
        public String dispatchedDate { get; set; }
        [DisplayName("Delivered Date")]
        public String deliveredDate { get; set; }
        [DisplayName("Shipping Cost")]
        public float sCost { get; set; }

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
            String shippersUsername = System.Web.HttpContext.Current.Session["loginName"].ToString();
            this.dt = new DataTable();
            try
            {
                MySqlConnection con = new MySqlConnection(constr);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("getOrdersForShipper", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sUsername", shippersUsername);
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
                MySqlConnection con = new MySqlConnection(constr);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("getOrderDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@orderID", orderId);
                MySqlDataReader reader = cmd.ExecuteReader();
                
                MySqlCommand cmd1 = new MySqlCommand("getBranchRetailer", con);
                cmd1.CommandType = CommandType.StoredProcedure;

                while (reader.Read())
                {
                    id = orderId;
                    oStatus = (orderStatus)Enum.Parse(typeof(orderStatus), reader.GetString("orderStatus"));
                    sCost = reader.GetFloat("shippingCost");
                    orderDate = reader.GetDateTime("orderDate").ToString("yyyy-MM-dd");
                    if (!reader.IsDBNull(reader.GetOrdinal("requiredDate"))) { requiredDate = reader.GetDateTime("requiredDate").ToString("yyyy-MM-dd"); } else { requiredDate = null; }
                    if (!reader.IsDBNull(reader.GetOrdinal("processedDate"))) { processedDate = reader.GetDateTime("processedDate").ToString("yyyy-MM-dd"); } else { processedDate = null; }
                    if (!reader.IsDBNull(reader.GetOrdinal("dispatchedDate"))) { dispatchedDate = reader.GetDateTime("dispatchedDate").ToString("yyyy-MM-dd"); } else { dispatchedDate = null; }
                    if (!reader.IsDBNull(reader.GetOrdinal("deliveredDate"))) { deliveredDate = reader.GetDateTime("deliveredDate").ToString("yyyy-MM-dd"); } else { deliveredDate = null; }
                    if (!reader.IsDBNull(reader.GetOrdinal("consumer_consumerid")))
                    {
                        type = "Individual Customer"; 
                    }
                    else
                    {
                        int branchID = reader.GetInt32("branch_branchId");
                        cmd1.Parameters.AddWithValue("@branchID", branchID);
                        MySqlDataReader reader1 = cmd1.ExecuteReader();
                        while (reader1.Read())
                        {
                            type = reader1.GetString("companyName") + "-" + reader1.GetString("branchName");
                        }
                    }

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

                    System.Diagnostics.Debug.WriteLine("email: " + email);
                    System.Diagnostics.Debug.WriteLine("address: " + address);
                }
                reader.Close();
                con.Close();
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
                cmd.Parameters.AddWithValue("@iorderStatus", oStatus.ToString());
                cmd.Parameters.AddWithValue("@d", currentDate);

                System.Diagnostics.Debug.WriteLine("id "+id.GetType() + " "+ id);
                System.Diagnostics.Debug.WriteLine("date "+currentDate);
                System.Diagnostics.Debug.WriteLine("status "+oStatus.ToString());

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
