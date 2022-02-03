using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SpencerFA.BO;
using System.Configuration;

namespace SpencerFA.DL
{
    public class ProductsDAL
    {
        readonly SqlConnection con;
        public ProductsDAL()
        {
            string conStr = ConfigurationManager.ConnectionStrings["Test"].ToString();
            //con = new SqlConnection("Server=ASPLAPLTM032\\SQLEXPRESS;Database=FlowerApp;integrated security=true");
            con = new SqlConnection(conStr);
        }

        public SqlConnection Connect()
        {
            try
            {
                con.Open();
                return con;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }
        public bool Disconnect()
        {
            try
            {
                con.Close();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

        public static DataTable ListOfProductsSQL(string datatable)
        {
            try
            {
                ProductsDAL con = new ProductsDAL();
                SqlCommand command = new SqlCommand("sp_TableSelect", con.Connect())
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Tablename", SqlDbType.NVarChar).Value = datatable;
                SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);//Not a selection query, just to check;
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Disconnect();
                return dt;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static List<Products> RecoverProductsFromSQL(string category)
        {
            List<Products> products = new List<Products>();
            DataTable products_data = ProductsDAL.ListOfProductsSQL("tbl_products");
            if (products_data == null)
            {
                
                return null;
            }
            else
            {
                for (int i = 0; i < products_data.Rows.Count; i++)
                {
                    string ID = products_data.Rows[i].Field<string>("Code_product");
                    var name = products_data.Rows[i].Field<string>("Name");
                    var price = products_data.Rows[i].Field<double>("Price");
                    var description = products_data.Rows[i].Field<string>("Description");
                    int cat_product = products_data.Rows[i].Field<int>("ID_Category");
                    if (cat_product == 1 && category == "flowers")

                    {
                        products.Add(new Flowers(ID, name, (float)price, description));
                    }
                    else if (cat_product == 2 && category == "gifts")
                    {
                        products.Add(new Gifts(ID, name, (float)price, description));
                    }
                    else if (cat_product == 3 && category == "christmas")
                    {
                        products.Add(new Christmas(ID, name, (float)price, description));
                    }

                }
                return products;
            }
        }
        public static int AddAProductSQL(int category,string ID, string name, double price, string description)
        {

                

                ProductsDAL con = new ProductsDAL();
                //string sql = $"INSERT INTO tbl_products (ID_product,[Name],Price,[Description],Category) VALUES('" + ID + "','" + name + "','" + price + "','" + description + "','"+category+"');";
                SqlCommand command = new SqlCommand("sp_AddAProduct", con.Connect())
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Code_product", SqlDbType.Char).Value = ID;
                command.Parameters.AddWithValue("@NAME", SqlDbType.NVarChar).Value = name;
                command.Parameters.AddWithValue("@pric", SqlDbType.Float).Value = price;
                command.Parameters.AddWithValue("@Descript", SqlDbType.NVarChar).Value = description;
                command.Parameters.AddWithValue("@cat", SqlDbType.Int).Value = category;
                int quantity = command.ExecuteNonQuery(); //Not a selection query, just to check;
                con.Disconnect();
                if (quantity == 1)
                {
                    return 1;
                    
                }
                else return 0; 


 
        }

        public static int DeleteProduct(int category,string ID)
        {

            ProductsDAL con = new ProductsDAL();
            //string sql = $"DELETE FROM tb_products where ID_product='" + ID + "' and Category = '"+ category +"';";
            SqlCommand command = new SqlCommand("sp_DeleteAProduct", con.Connect())
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@ID_Category", SqlDbType.Int).Value = category;
            command.Parameters.AddWithValue("@Code_product", SqlDbType.Char).Value = ID;
            int quantity = command.ExecuteNonQuery();
            if (quantity == 1)
            {
                con.Disconnect();
                return 1;

            }
            else
            {
                con.Disconnect();
                return 0;

            }

        }

        public static List<Tuple<int,string>> ProductStringsSQL()
        {
            List<Tuple<int,string>> products = new List<Tuple<int,string>>();
            DataTable products_data = ProductsDAL.ListOfProductsSQL("tbl_categories");
            if (products_data == null)
            {
                Console.WriteLine("There was no access to the database");
                return null;
            }
            else
            {
                for (int i = 0; i < products_data.Rows.Count; i++)
                {
                    Tuple<int,string> aux_tuple = new Tuple <int,string>(products_data.Rows[i].Field<int>("ID_category"), products_data.Rows[i].Field<string>("category"));
                    products.Add(aux_tuple);
                }
                return products;
            }
        }

        public static List<string> QueryProduct(string productID, int category)
        {
            try
            {
                List<string> fields = new List<string>();
                ProductsDAL con = new ProductsDAL();
                //string sql = $"SELECT * FROM tb_products where ID_product = '" + productID + "' and Category = '"+category+"';";
                SqlCommand command = new SqlCommand("sp_QueryAProduct", con.Connect())
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ID_Category", SqlDbType.Int).Value = category;
                command.Parameters.AddWithValue("@Code_product", SqlDbType.Char).Value = productID;
                
                //command.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = command.ExecuteReader();//Not a selection query, just to check;

                if (dr.Read()) // if the product exists
                {
                    Console.WriteLine("Product FOUND");
                    Console.WriteLine(" ");
                    fields.Add(dr["Code_product"].ToString());
                    fields.Add(dr["Name"].ToString());
                    fields.Add(dr["Price"].ToString());
                    fields.Add(dr["Description"].ToString());


                    con.Disconnect();
                    return fields;
                }
                else
                {

                    con.Disconnect();
                    return null;
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static int EditAProduct(string ID,string name,double price, string description)
        {

            ProductsDAL con = new ProductsDAL();
            //string sql = $"UPDATE tb_products SET Name ='" + name + "',Price = '" + price + "',Description='" + description + "'  where ID_product = '" + ID + "';";
            SqlCommand command = new SqlCommand("sp_EditAProduct", con.Connect())
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Code_product", SqlDbType.Char).Value = ID;
            command.Parameters.AddWithValue("@NAME", SqlDbType.NVarChar).Value = name;
            command.Parameters.AddWithValue("@pric", SqlDbType.Float).Value = price;
            command.Parameters.AddWithValue("@Descript", SqlDbType.NVarChar).Value = description;
            int quantity = command.ExecuteNonQuery(); //Not a selection query, just to check;
            con.Disconnect();
            if (quantity == 1)
            {
                return 1;
                
            }
            else return 0;
                
        }

        public static DataTable PrintProducts(int category)
        {

            DataTable products_data = ProductsDAL.ListOfProductsSQL("tbl_products");
            if (products_data == null)
            {
                return null;
            }
            else
            {
                return products_data;

            }

        }
        public static DataTable ValidateStringSQL()
        {
            DataTable products_data = ProductsDAL.ListOfProductsSQL("tbl_products");
            if (products_data == null)
            {
               
                return null;
            }
            else
            {
                return products_data;
                

            }
            

        }
        public static double ValidatePriceSQL()
        {
            try
            {      
                double price = Convert.ToDouble(Console.ReadLine());
                return price;

            }catch(Exception e)
            {
                Console.WriteLine("Please introduce the correct price sintax (integer or decimal)");
                e.ToString();
                return ValidatePriceSQL();
            }

        }

    }


}
