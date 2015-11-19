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
    public class Category
    {
        //category table fields
        public enum CategoryIn
        {
            False = 0,
            True
        }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(45, ErrorMessage = "can not exceed 45 characters")]
        [DisplayName("Category*")]
        public string categoryName { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(1000, ErrorMessage = "can not exceed 1000 characters")]
        [DisplayName("Category Description*")]
        public string categoryDescrip { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Category in use?*")]
        public CategoryIn catInUse { get; set; }
        public int catId { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Picture URL*")]
        public string picURL { get; set; }

        public DataTable dt { get; set; }
        private String con_str = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString.ToString();

        //used to return all product details
        public Category()
        {
            this.dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getMinCategoryDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(dt);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
  
            }
        }

        //used to return all details specified by product
        public Category(int _categoryId)
        {
            this.dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getAllCategoryDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_categoryId", _categoryId);
                    MySqlDataReader reader = null;
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        catId = reader.GetInt32("categoryId");
                        categoryName = reader.GetString("categoryName");
                        categoryDescrip = reader.GetString("catDescription");
                        if (!reader.IsDBNull(reader.GetOrdinal("picUrl"))) { picURL = reader.GetString("picUrl"); } else { picURL = null; }
                        int i = reader.GetInt32("categoryInUse");
                        catInUse = (CategoryIn)i;

                    }

                    reader.Close();
                    con.Close();

                }
            }
        }

        public void insertCategory(Category c)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("insertNewCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //params for insert into category
                    cmd.Parameters.AddWithValue("@_catName", c.categoryName);
                    cmd.Parameters.AddWithValue("@_catDescription", c.categoryDescrip);
                    cmd.Parameters.AddWithValue("@_catInUse", c.catInUse);
                    cmd.Parameters.AddWithValue("@_picUrl", c.picURL);
                    
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        public void updateCategory(Category c)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("updateCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    //params for insert into category
                    cmd.Parameters.AddWithValue("@_categoryName", c.categoryName);
                    cmd.Parameters.AddWithValue("@_catDescription", c.categoryDescrip);
                    cmd.Parameters.AddWithValue("@_categoryInUse", c.catInUse);
                    cmd.Parameters.AddWithValue("@_catId", c.catId);
                    cmd.Parameters.AddWithValue("@_picUrl", c.picURL);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

    }
}