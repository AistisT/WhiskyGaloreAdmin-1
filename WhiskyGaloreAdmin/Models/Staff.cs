using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using WhiskyGaloreAdmin.Libs;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace WhiskyGaloreAdmin.Models
{
    public class Staff
    {
        public enum AccountType
        {
            Administrator = 1,
            Manager,
            Warehouse
        }
        public enum Title
        {
            Mr = 1,
            Mrs,
            Miss,
            Ms,
            Dr,
            Prof
        }
        //username table fields
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(50, ErrorMessage = "can not exceed 50 characters")]
        [DisplayName("Username*")]
        public string username { get; set; }
        [DisplayName("Username")]
        public string usernameR { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "must be between 6-16 chars")]
        [DisplayName("Password*")]
        public string password { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Account type*")]
        public AccountType acctype { get; set; }
        //staff table fields
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Role*")]
        public string role { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "*invalid format")]
        [DisplayName("Hourly pay (00.00)*")]
        public decimal hourlyRate { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Start Date*")]
        public DateTime startDate { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "must be 9 characters long")]
        [DisplayName("National Insurance no.*")]
        public string ni { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Department*")]
        public string department { get; set; }
        //contact table fields
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Title*")]
        public Title title { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("First Name*")]
        public string forename { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Last Name*")]
        public string surname { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(18, MinimumLength = 11, ErrorMessage = "must be between 11-18 digits")]
        [DisplayName("Primary Phone no.*")]
        public string firstNumber { get; set; }
        [StringLength(18, MinimumLength = 11, ErrorMessage = "must be between 11-18 digits")]
        [DisplayName("Secondary Phone no.")]
        public string secondaryNumber { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Email*")]
        public string email { get; set; }
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Fax")]
        public string fax { get; set; }
        //address table fields
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("First Line of Address*")]
        public string firstLine { get; set; }
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Second Line of Address")]
        public string secondLine { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Town*")]
        public string town { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(10, ErrorMessage = "can not exceed 10 characters")]
        [DisplayName("Postcode*")]
        public string postcode { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(50, ErrorMessage = "can not exceed 50 characters")]
        [DisplayName("Region*")]
        public string region { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Country*")]
        public string country { get; set; }
        //bankdetails table fields
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Account Number*")]
        public int accountNumber { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Sort Code*")]
        public int sortCode { get; set; }
        public DataTable dt { get; set; }
        private String con_str = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString.ToString();

        //used to return all employee details
        public Staff()
        {
            this.dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getMinStaffDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(dt);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
 
        }

        //used to return all details specified by username
        public Staff(string username)
        {
            this.dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getAllStaffDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user", username);
                    MySqlDataReader reader = null;
                    reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        username = reader.GetString("username");
                        password = Encryption.Decrypt(reader.GetString("password"));
                        Debug.WriteLine("PASSWORD " + password);
                        string s = reader.GetString("accountType");
                        acctype = (AccountType) Enum.Parse(typeof(AccountType), s);
                        role = reader.GetString("role");
                        hourlyRate = reader.GetDecimal("hourlyRate");
                        startDate = reader.GetDateTime("startDate");
                        ni = reader.GetString("ni");
                        department = reader.GetString("department");
                        firstLine = reader.GetString("firstLine");
                        if (!reader.IsDBNull(reader.GetOrdinal("secondLine")))
                        {
                            secondLine = reader.GetString("secondLine");
                        }
                        else
                        {
                            secondLine = null;
                        }
                        town = reader.GetString("town");
                        postcode = reader.GetString("postcode");
                        region = reader.GetString("region");
                        country = reader.GetString("country");
                        sortCode = reader.GetInt32("sortCode");
                        accountNumber = reader.GetInt32("accountNumber");
                        string t = reader.GetString("Title");
                        title = (Title) Enum.Parse(typeof(Title), t);
                        forename = reader.GetString("forename");
                        surname = reader.GetString("surname");
                        firstNumber = reader.GetString("firstNumber");
                        if (!reader.IsDBNull(reader.GetOrdinal("secondaryNumber")))
                        {
                            secondaryNumber = reader.GetString("secondaryNumber");
                        }
                        else
                        {
                            secondaryNumber = null;
                        }
                        email = reader.GetString("email");
                        if (!reader.IsDBNull(reader.GetOrdinal("fax")))
                        {
                            fax = reader.GetString("fax");
                        }
                        else
                        {
                            fax = null;
                        }
                        
                    }

                    reader.Close();
                    con.Close();
                    
                }
            }
        }

        public void insertEmployee(Staff s)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("insertNewEmployee", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //params for insert into username
                    cmd.Parameters.AddWithValue("@username", s.username);
                    cmd.Parameters.AddWithValue("@password", Encryption.Encrypt(s.password));
                    cmd.Parameters.AddWithValue("@accountType", s.acctype.ToString());

                    //params for insert into address
                    cmd.Parameters.AddWithValue("@firstLine", s.firstLine);
                    if (s.secondLine != null)
                    {
                        cmd.Parameters.AddWithValue("@secondLine", s.secondLine);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@secondLine", null);
                    }
                    cmd.Parameters.AddWithValue("@town", s.town);
                    cmd.Parameters.AddWithValue("@postcode", s.postcode);
                    cmd.Parameters.AddWithValue("@region", s.region);
                    cmd.Parameters.AddWithValue("@country", s.country);

                    //params for insert into bankDetails
                    cmd.Parameters.AddWithValue("@sortCode", s.sortCode);
                    cmd.Parameters.AddWithValue("@accountNumber", s.accountNumber);

                    //params for insert into contact
                    cmd.Parameters.AddWithValue("@title", s.title.ToString());
                    cmd.Parameters.AddWithValue("@forename", s.forename);
                    cmd.Parameters.AddWithValue("@surname", s.surname);
                    cmd.Parameters.AddWithValue("@firstNumber", s.firstNumber);
                    if (s.secondaryNumber != null)
                    {
                        cmd.Parameters.AddWithValue("@secondaryNumber", s.secondaryNumber);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@secondaryNumber", null);
                    }
                    cmd.Parameters.AddWithValue("@email", s.email);
                    if (s.fax != null)
                    {
                        cmd.Parameters.AddWithValue("@fax", s.fax);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@fax", null);
                    }

                    //params for insert into staff
                    cmd.Parameters.AddWithValue("@role", s.role);
                    cmd.Parameters.AddWithValue("@hourlyRate", s.hourlyRate);
                    cmd.Parameters.AddWithValue("@startDate", s.startDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@ni", s.ni);
                    cmd.Parameters.AddWithValue("@department", s.department);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        public void updateEmployee(Staff s)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("updateEmployee", con))
                {
                    Debug.WriteLine("updating " + s.username);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //params for updating username table
                    cmd.Parameters.AddWithValue("@_username", s.username);
                    cmd.Parameters.AddWithValue("@password", Encryption.Encrypt(s.password));
                    cmd.Parameters.AddWithValue("@accountType", s.acctype.ToString());

                    //params for updating address table
                    cmd.Parameters.AddWithValue("@firstLine", s.firstLine);
                    if (s.secondLine != null)
                    {
                        cmd.Parameters.AddWithValue("@secondLine", s.secondLine);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@secondLine", null);
                    }
                    cmd.Parameters.AddWithValue("@town", s.town);
                    cmd.Parameters.AddWithValue("@postcode", s.postcode);
                    cmd.Parameters.AddWithValue("@region", s.region);
                    cmd.Parameters.AddWithValue("@country", s.country);

                    //params for updating bankDetails table
                    cmd.Parameters.AddWithValue("@sortCode", s.sortCode);
                    cmd.Parameters.AddWithValue("@accountNumber", s.accountNumber);

                    //params for updating contact table
                    cmd.Parameters.AddWithValue("@title", s.title.ToString());
                    cmd.Parameters.AddWithValue("@forename", s.forename);
                    cmd.Parameters.AddWithValue("@surname", s.surname);
                    cmd.Parameters.AddWithValue("@firstNumber", s.firstNumber);
                    if (s.secondaryNumber != null)
                    {
                        cmd.Parameters.AddWithValue("@secondaryNumber", s.secondaryNumber);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@secondaryNumber", null);
                    }
                    cmd.Parameters.AddWithValue("@email", s.email);
                    if (s.fax != null)
                    {
                        cmd.Parameters.AddWithValue("@fax", s.fax);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@fax", null);
                    }

                    //params for updating staff table
                    cmd.Parameters.AddWithValue("@role", s.role);
                    cmd.Parameters.AddWithValue("@hourlyRate", s.hourlyRate);
                    cmd.Parameters.AddWithValue("@startDate", s.startDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@ni", s.ni);
                    cmd.Parameters.AddWithValue("@department", s.department);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        public void deleteEmployee(string username)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("deleteEmployee", con))
                {
                    Debug.WriteLine("updating " + username);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_username", username);
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }
    }
}