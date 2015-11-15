using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;




namespace WhiskyGaloreAdmin.Models
{
    public class Manager
    {
        public void getData(string query)
        {
            this.dt = new DataTable();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = constr;
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                if (query.Equals("getStaffDataWithDailyHours"))
                {
                    System.DateTime currentDate = System.DateTime.Now;
                    cmd.Parameters.AddWithValue("@currentDate", System.Convert.ToDateTime(currentDate).ToString("yyyy-MM-dd"));
                }
                sda.Fill(dt);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("fail !");
            }
        }

        public void calculateDailyFinances(System.DateTime dailyDate)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = constr;
                con.Open();
                MySqlCommand cmd = new MySqlCommand("updateDailyFinances", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idate", System.Convert.ToDateTime(dailyDate).ToString("yyyy-MM-dd"));
                System.Diagnostics.Debug.WriteLine("Date " + System.Convert.ToDateTime(dailyDate).ToString("yyyy-MM-dd"));
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