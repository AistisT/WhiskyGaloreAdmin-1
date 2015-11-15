using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace WhiskyGaloreAdmin.Models
{
    public class Shipping
    {

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
    }
}
