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
using System.Web.Mvc;
using System.Collections;

namespace WhiskyGaloreAdmin.Models
{
    public class Product
    {

        public enum Featured
        {
            False = 0,
            True
        }
        public enum StaffPick
        {
            False = 0,
            True
        }
        public string categoryName { get; set; }

        public int catId { get; set; }
        
        //product table fields
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(255, ErrorMessage = "can not exceed 255 characters")]
        [DisplayName("Product Name*")]
        public string productName { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(100, ErrorMessage = "can not exceed 100 characters")]
        [DisplayName("Barcode*")]
        public string barcode { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(1000, ErrorMessage = "can not exceed 1000 characters")]
        [DisplayName("Product Description*")]
        public string productDesc { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "*invalid format")]
        [DisplayName("Unit Price £(00.00)*")]
        public decimal unitPrice { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "*invalid format")]
        [DisplayName("RRP Price £(00.00)*")]
        public decimal mrrp { get; set; }
        [Required(ErrorMessage = "can not be blank")]
        [DisplayName("Units in stock*")]
        public int unitsInStock { get; set; }
        [Required(ErrorMessage = "can not be blank")]
        [DisplayName("Unit weight kg*")]
        public float unitWeight { get; set; }
        [Required(ErrorMessage = "can not be blank")]
        [DisplayName("Quantity per unit*")]
        public int qPerUnit { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [StringLength(250, ErrorMessage = "can not exceed 250 characters")]
        [DisplayName("Picture URL*")]
        public string picUrl { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Featured product?*")]
        public Featured featured { get; set; }
        [Required(ErrorMessage = "*can not be blank!")]
        [DisplayName("Staff pick?*")]
        public StaffPick staffPick { get; set; }
        public int productId { get; set; }

        public DataTable dt { get; set; }
        public DataTable dtcat { get; set; }
        private String con_str = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString.ToString();
        [DisplayName("Category Name*")]
        public SortedDictionary<uint, string> cat { get; set; }
        public string SelectedCat { get; set; }


        //used to return all product details
        public Product()
        {
            this.dt = new DataTable();
            this.dtcat = new DataTable();
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getMinProductDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(dt);
                    cmd.ExecuteNonQuery();

                }
                using (MySqlCommand cmd = new MySqlCommand("getAllCategories", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    this.cat = new SortedDictionary<uint, string>();

                    while (reader.Read())
                    {
                        categoryName = reader.GetString("categoryName");
                        catId = reader.GetInt32("categoryId");
                        cat.Add(Convert.ToUInt32(catId), categoryName.ToString());

                    }
                    reader.Close();
                    con.Close();
                }
            }

        }

        public void getAllCat()
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getAllCategories", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    this.cat = new SortedDictionary<uint, string>();

                    while (reader.Read())
                    {
                        categoryName = reader.GetString("categoryName");
                        catId = reader.GetInt32("categoryId");
                        cat.Add(Convert.ToUInt32(catId), categoryName.ToString());

                    }
                    reader.Close();
                    con.Close();
                }
            }
        }

        //used to return all details specified by product
        public Product(int _productId)
        {
            this.dt = new DataTable();
            getAllCat();
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("getAllProductDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_productId", _productId);
                    MySqlDataReader reader = null;
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        //catId = reader.GetInt32("categoryId");
                        categoryName = reader.GetString("categoryName");
                        //categoryDescrip = reader.GetString("catDescription");
                        //int i = reader.GetInt32("categoryInUse");
                        //catInUse = (Category)i;
                        
                        productId = reader.GetInt32("productId");
                        barcode = reader.GetString("barcode");
                        productName = reader.GetString("productName");
                        productDesc = reader.GetString("prodDescription");
                        unitPrice = reader.GetDecimal("unitPrice");
                        mrrp = reader.GetDecimal("mrrp");
                        unitsInStock = reader.GetInt32("unitsInStock");
                        unitWeight = reader.GetFloat("unitWeight");
                        qPerUnit = reader.GetInt32("quantityPerUnit");
                        picUrl = reader.GetString("picUrl");
                        int j = reader.GetInt32("featured");
                        featured = (Featured)j;
                        int k = reader.GetInt32("staffPick");
                        staffPick = (StaffPick)k;

                    }

                    reader.Close();
                    con.Close();

                }
            }
        }

        public void insertProduct(Product p)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("insertNewProduct", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //params for insert into category
                    //cmd.Parameters.AddWithValue("_categoryName", p.categoryName);
                    //cmd.Parameters.AddWithValue("@_catDescription", p.categoryDescrip);
                    //cmd.Parameters.AddWithValue("@_categoryInUse", p.catInUse);
                    cmd.Parameters.AddWithValue("@_catId", p.SelectedCat);

                    //params for insert into product
                    cmd.Parameters.AddWithValue("@_barcode", p.barcode);
                    cmd.Parameters.AddWithValue("@_productName", p.productName);
                    cmd.Parameters.AddWithValue("@_prodDescription", p.productDesc);
                    cmd.Parameters.AddWithValue("@_unitPrice", p.unitPrice);
                    cmd.Parameters.AddWithValue("@_mrrp", p.mrrp);
                    cmd.Parameters.AddWithValue("@_unitsInStock", p.unitsInStock);
                    cmd.Parameters.AddWithValue("@_unitWeight", p.unitWeight);
                    cmd.Parameters.AddWithValue("@_quantityPerUnit", p.qPerUnit);
                    cmd.Parameters.AddWithValue("@_picUrl", p.picUrl);
                    cmd.Parameters.AddWithValue("@_featured", p.featured);
                    cmd.Parameters.AddWithValue("@_staffPic", p.staffPick);
                    
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        public void updateProduct(Product p)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("updateProduct", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    //params for insert into category
                   // cmd.Parameters.AddWithValue("_categoryName", p.categoryName);
                    //cmd.Parameters.AddWithValue("@_catDescription", p.categoryDescrip);
                    //cmd.Parameters.AddWithValue("@_categoryInUse", p.catInUse);
                    cmd.Parameters.AddWithValue("@_catId", p.SelectedCat);

                    //params for insert into product
                    cmd.Parameters.AddWithValue("@_barcode", p.barcode);
                    cmd.Parameters.AddWithValue("@_productName", p.productName);
                    cmd.Parameters.AddWithValue("@_prodDescription", p.productDesc);
                    cmd.Parameters.AddWithValue("@_unitPrice", p.unitPrice);
                    cmd.Parameters.AddWithValue("@_mrrp", p.mrrp);
                    cmd.Parameters.AddWithValue("@_unitsInStock", p.unitsInStock);
                    cmd.Parameters.AddWithValue("@_unitWeight", p.unitWeight);
                    cmd.Parameters.AddWithValue("@_quantityPerUnit", p.qPerUnit);
                    cmd.Parameters.AddWithValue("@_picUrl", p.picUrl);
                    cmd.Parameters.AddWithValue("@_featured", p.featured);
                    cmd.Parameters.AddWithValue("@_staffPic", p.staffPick);
                    cmd.Parameters.AddWithValue("@_prodId", p.productId);

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }

        public void deleteProduct(int _productId)
        {
            using (MySqlConnection con = new MySqlConnection(con_str))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("deleteProduct", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@_prodId", _productId);
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
        }
    }
}