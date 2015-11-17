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
    public class Shipper
    {
        public enum Title
        {
            Mr = 1,
            Mrs,
            Miss,
            Ms,
            Dr,
            Prof
        }
        public enum Card
        {
            Visa = 1,
            Mastercard,
            Maestro,
            AmericanExpress
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
        public string acctype = "Shipper"; 
        //shipper table fields
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Company Name*")]
        public string companyName { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "*invalid format")]
        [DisplayName("Rate per kg £(00.00)*")]
        public decimal discount { get; set; }
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
        [Required(ErrorMessage = "can not be blank")]
        [DisplayName("Sort Code*")]
        public int sortCode { get; set; }
        //paymentdetails table fields
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Card Type*")]
        public Card cardType { get; set; }
        [Required(ErrorMessage = "*can not be blank!")] 
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("First Name on card*")]
        public string cardForename { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Surname on card*")]
        public string cardSurname { get; set; }
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "can only enter numbers")]
        [StringLength(16, ErrorMessage = "can not exceed 16 characters")]
        [Required(ErrorMessage = "can not be blank")]
        [DisplayName("Card No.*")]
        public string cardNo { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Start Date*")]
        public DateTime startDate { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("End Date*")]
        public DateTime endDate { get; set; }
        [Required(ErrorMessage = "can not be blank")]
        [DisplayName("CVC.*")]
        public int issueNo { get; set; }

        public DataTable dt { get; set; }
        private String con_str = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString.ToString();

        //used to return all employee details
        public Shipper()
        {
            this.dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getMinShipperDetails", con))
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
        public Shipper(string username)
        {
            this.dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getAllShipperDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_user", username);
                    MySqlDataReader reader = null;
                    reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        username = reader.GetString("username");
                        password = Encryption.Decrypt(reader.GetString("password"));
                        Debug.WriteLine("PASSWORD " + password);
                        string s = reader.GetString("creditCardType");
                        cardType = (Card) Enum.Parse(typeof(Card), s);
                        companyName = reader.GetString("companyName");
                        discount = reader.GetDecimal("rate");
                        startDate = reader.GetDateTime("startDate");
                        endDate = reader.GetDateTime("expiryDate");
                        issueNo = reader.GetInt32("issueNumber");
                        cardNo = Encryption.Decrypt(reader.GetString("cardNumber"));
                        cardForename = reader.GetString("fName");
                        cardSurname = reader.GetString("lName");
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

        public void insertShipper(Shipper s)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("insertNewShipper", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //params for insert into username
                    cmd.Parameters.AddWithValue("@_username", s.username);
                    cmd.Parameters.AddWithValue("@_password", Encryption.Encrypt(s.password));
                    cmd.Parameters.AddWithValue("@_accountType", s.acctype);

                    //params for insert into address
                    cmd.Parameters.AddWithValue("@_firstLine", s.firstLine);
                    if (s.secondLine != null)
                    {
                        cmd.Parameters.AddWithValue("@_secondLine", s.secondLine);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@_secondLine", null);
                    }
                    cmd.Parameters.AddWithValue("@_town", s.town);
                    cmd.Parameters.AddWithValue("@_postcode", s.postcode);
                    cmd.Parameters.AddWithValue("@_region", s.region);
                    cmd.Parameters.AddWithValue("@_country", s.country);

                    //params for insert into bankDetails
                    cmd.Parameters.AddWithValue("@_sortCode", s.sortCode);
                    cmd.Parameters.AddWithValue("@_accountNumber", s.accountNumber);

                    //params for insert into contact
                    cmd.Parameters.AddWithValue("@_title", s.title.ToString());
                    cmd.Parameters.AddWithValue("@_forename", s.forename);
                    cmd.Parameters.AddWithValue("@_surname", s.surname);
                    cmd.Parameters.AddWithValue("@_firstNumber", s.firstNumber);
                    if (s.secondaryNumber != null)
                    {
                        cmd.Parameters.AddWithValue("@_secondaryNumber", s.secondaryNumber);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@_secondaryNumber", null);
                    }
                    cmd.Parameters.AddWithValue("@_email", s.email);
                    if (s.fax != null)
                    {
                        cmd.Parameters.AddWithValue("@_fax", s.fax);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@_fax", null);
                    }

                    //params for insert into shippers
                    cmd.Parameters.AddWithValue("@_companyName", s.companyName);
                    cmd.Parameters.AddWithValue("@_discount", s.discount);

                    //params for insert into paymentdetails
                    cmd.Parameters.AddWithValue("@_creditCardType", s.cardType.ToString());
                    cmd.Parameters.AddWithValue("@_fName", s.cardForename);
                    cmd.Parameters.AddWithValue("@_lName", s.cardSurname);
                    cmd.Parameters.AddWithValue("@_cardNumber", Encryption.Decrypt(s.cardNo));
                    cmd.Parameters.AddWithValue("@_startDate", s.startDate);
                    cmd.Parameters.AddWithValue("@_expiryDate", s.endDate);
                    cmd.Parameters.AddWithValue("@_issueNumber", s.issueNo);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        public void updateShipper(Shipper s)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("updateShiper", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //params for insert into username
                    cmd.Parameters.AddWithValue("@_username", s.username);
                    cmd.Parameters.AddWithValue("@password", Encryption.Encrypt(s.password));
                    cmd.Parameters.AddWithValue("@accountType", s.acctype);

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

                    //params for insert into shippers
                    cmd.Parameters.AddWithValue("@companyName", s.companyName);
                    cmd.Parameters.AddWithValue("@discount", s.discount);

                    //params for insert into paymentdetails
                    cmd.Parameters.AddWithValue("@creditCardType", s.cardType.ToString());
                    cmd.Parameters.AddWithValue("@fName", s.cardForename);
                    cmd.Parameters.AddWithValue("@lName", s.cardSurname);
                    cmd.Parameters.AddWithValue("@cardNumber", Encryption.Encrypt(s.cardNo));
                    cmd.Parameters.AddWithValue("@startDate", s.startDate);
                    cmd.Parameters.AddWithValue("@expiryDate", s.endDate);
                    cmd.Parameters.AddWithValue("@issueNumber", s.issueNo);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        public void deleteShipper(string username)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("deleteShipper", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_username", username);
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }
    }
}