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
        [DisplayName("Order Date")]
        public DateTime orderDate { get; set; }
        [DisplayName("Required Date")]
        public Nullable<DateTime> requiredDate { get; set; }
        [DisplayName("Processed Date")]
        public Nullable<DateTime> processedDate { get; set; }
        [DisplayName("Dispatched Date")]
        public Nullable<DateTime> dispatchedDate { get; set; }
        [DisplayName("Delivered Date")]
        public Nullable<DateTime> deliveredDate { get; set; }
        [DisplayName("Shipping Cost")]
        public float sCost { get; set; }
        [DisplayName("Customer Type")]
        public int type { get; set; }
        [DisplayName("Name")]
        public String name { get; set; }
        [DisplayName("Contact Number")]
        public String fNumber { get; set; }
        [DisplayName("E-mail")]
        public String email { get; set; }
        [DisplayName("E-mail")]
        /*public String email { get; set; }
        [DisplayName("E-mail")]
        public String email { get; set; }
        [DisplayName("E-mail")]
        public String email { get; set; }
        [DisplayName("E-mail")]
        public String email { get; set; }*/

        [Required(ErrorMessage = "*can not be blank!")]
        //[DisplayName("Date")]
        public DateTime currentDate { get; set; }


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
                while (reader.Read())
                {
                    id = orderId;
                    oStatus = (orderStatus)Enum.Parse(typeof(orderStatus), reader.GetString("orderStatus"));
                    orderDate = reader.GetDateTime("orderDate");
                    if (!reader.IsDBNull(reader.GetOrdinal("requiredDate"))){requiredDate = reader.GetDateTime("requiredDate");} else {requiredDate = null;}
                    if (!reader.IsDBNull(reader.GetOrdinal("processedDate"))) { processedDate = reader.GetDateTime("processedDate"); } else { processedDate = null; }
                    if (!reader.IsDBNull(reader.GetOrdinal("dispatchedDate"))) { dispatchedDate = reader.GetDateTime("dispatchedDate"); } else { dispatchedDate = null; }
                    if (!reader.IsDBNull(reader.GetOrdinal("deliveredDate"))) { deliveredDate = reader.GetDateTime("deliveredDate"); } else { deliveredDate = null; }
                    sCost = reader.GetFloat("shippingCost");
                    if (!reader.IsDBNull(reader.GetOrdinal("consumer_consumerid")))
                    {
                        //ct = 0;
                           // reader.GetString("forename") + " " + reader.GetString("surname");
                    }
                    else
                    {
                        //consumer = 0;
                       // branch = reader.GetInt32("branch_branchId");
                    }
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
                cmd.Parameters.AddWithValue("@d", currentDate.ToString("yyyy-MM-dd"));

                System.Diagnostics.Debug.WriteLine("id "+id.GetType() + " "+ id);
                System.Diagnostics.Debug.WriteLine("date "+currentDate.ToString("yyyy-MM-dd"));
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
