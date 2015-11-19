using System;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WhiskyGaloreAdmin.Models
{
    public class DailyHours
    {
        [Required(ErrorMessage = "*can not be blank!")]
        [RegularExpression("([0-9]+)", ErrorMessage = "*only positive numbers")]
        [DisplayName("Hours worked")]
        public double hours { get; set; }

        [DisplayName("Date")]
        public string currentDate { get; set; }

        [Required(ErrorMessage = "*can not be blank!, please select member of staff .")]
        [DisplayName("Staff ID")]
        public int staffId { get; set; }

        [DisplayName("Staff Name")]
        public SortedDictionary<uint, string> staffFullNames { get; set; }

        [DisplayName("Staff Name")]
        public  string staffFullname { get; set; }

        public DataTable dt { get; set; }

        public void getNames()
        {
            currentDate = DateTime.Now.ToString("dd/MM/yyyy");
            dt = new DataTable();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = constr;
                con.Open();
                MySqlCommand cmd = new MySqlCommand("getStaffIdNameWithoutDailyHours", con);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@icurrentDate", Convert.ToDateTime(currentDate).ToString("yyyy-MM-dd"));
                sda.Fill(dt);
                cmd.ExecuteNonQuery();
                con.Close();

                this.staffFullNames = new SortedDictionary<uint, string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    staffFullNames.Add(Convert.ToUInt32(dt.Rows[i]["staffId"].ToString()), dt.Rows[i]["forename"].ToString() + " " + dt.Rows[i]["surname"].ToString());
                }
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("fail !");
            }
        }

        public void getData(int staffId)
        {
            currentDate = DateTime.Now.ToString("dd/MM/yyyy");
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = constr;
                con.Open();
                MySqlCommand cmd = new MySqlCommand("getSingleStaffHours", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@staffid", staffId);
                cmd.Parameters.AddWithValue("@currentDate", Convert.ToDateTime(currentDate).ToString("yyyy-MM-dd"));
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    staffFullname = reader.GetString("forename") + " " + reader.GetString("surname");
                    this.staffId = reader.GetInt32("staffId");
                    hours = reader.GetFloat("hoursworked");
                }
                reader.Close();
                con.Close();
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("fail !");
            }
        }



        public void InsertDailyhours(DailyHours s)
        {
            currentDate = DateTime.Now.ToString("dd/MM/yyyy");
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = constr;
                con.Open();
                MySqlCommand cmd = new MySqlCommand("insertNewDailyHours", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@icurrentDate", Convert.ToDateTime(s.currentDate).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@ihoursWorked", (double)s.hours);
                cmd.Parameters.AddWithValue("@iStaff_staffId", s.staffId);

                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("fail !");
            }

        }

        public void UpdateDailyHours(DailyHours s)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = constr;
                con.Open();
                MySqlCommand cmd = new MySqlCommand("updateDailyHours", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@icurrentDate", Convert.ToDateTime(s.currentDate).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@ihoursWorked", (double)s.hours);
                cmd.Parameters.AddWithValue("@iStaff_staffId", s.staffId);

                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("fail !");
            }

        }


        public void DeleteDailyHours(DailyHours s)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString;
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = constr;
                con.Open();
                MySqlCommand cmd = new MySqlCommand("deleteDailyHours", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@icurrentDate", Convert.ToDateTime(s.currentDate).ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@iStaff_staffId", s.staffId);

                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("fail !");
            }

        }
    }
}
