using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;




namespace WhiskyGaloreAdmin.Models
{
    public class Manager
    {
        public Manager(string query)
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

        public DataTable dt { get; set; }
    }
}