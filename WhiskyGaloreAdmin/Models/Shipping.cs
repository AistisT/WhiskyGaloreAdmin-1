using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            Administrator = 1,
            Manager,
            Warehouse
        }
        [DisplayName("Order Status")]
        public orderStatus oStatus { get; set; }
        [DisplayName("Date")]
        public DateTime currentDate { get; set; }

        public Shipping()
        {
            String shippersUsername = System.Web.HttpContext.Current.Session["loginName"].ToString();
            this.dt = new DataTable();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = constr;
                con.Open();
                MySqlCommand cmd = new MySqlCommand("getOdersForShipper", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sUsername", shippersUsername);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);

                sda.Fill(dt);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("fail !");
            }
        }

        public DataTable dt { get; set; }

        public void getData(orderStatus oStatus)
        {
            currentDate = DateTime.Now;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = constr;
                con.Open();
                MySqlCommand cmd = new MySqlCommand("getOrderDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@oStatus", oStatus);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string s = reader.GetString("orderStatus");
                    oStatus = (orderStatus)Enum.Parse(typeof(orderStatus), s);
                }
                reader.Close();
                con.Close();
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("fail !");
            }
        }
    }
}
