using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpencerFA.BO;
using SpencerFA.DL;

namespace SpencerFA.SL
{
    public class ProductSL: IAccess
    {
        public  List<Tuple<int,string>> ProductCategories()
        {
            return ProductsDAL.ProductStringsSQL();
        }

        public List<Products> RecoverProducts(string category)
        {

            if (category == "flowers") {
                return ProductsDAL.RecoverProductsFromSQL("flowers");
            }
            else if (category == "gifts")
            {
                return ProductsDAL.RecoverProductsFromSQL("gifts");
            }
            else if (category == "christmas")
            {
                return ProductsDAL.RecoverProductsFromSQL("christmas");
            }
            else
            {
                Console.WriteLine("There was no access to the database");
                return null;
            }

        }

        public void AddQuery(int category)
        {
            try
            {
                PrintQuery(category);
                string ID = ValidateString();
                Console.WriteLine("Please write the name of the product");
                string name = Console.ReadLine();
                Console.WriteLine("Please insert the price of the product");
                double price = ProductsDAL.ValidatePriceSQL();
                Console.WriteLine("Please insert a description of the product");
                string description = Console.ReadLine();
                int quantity = ProductsDAL.AddAProductSQL(category, ID, name, price, description);
                if (quantity == 1)
                {
                    Console.Clear();
                    Console.WriteLine("The product was added successfully");
                }
                else Console.WriteLine("The product was not added");
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("There was a problem connecting with the database");
                Console.WriteLine(e.Message);
            }
        }
        public  void DeleteQuery(int category)
        {
            try
            {
                PrintQuery(category);
                string ID = ProductsBO.ValidateID();
                int quantity = ProductsDAL.DeleteProduct(category,ID);
                if (quantity == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Product was deleted successfully");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("The product was not deleted, did you check that the ID was written correctly?");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem connecting with the database");
                Console.WriteLine(e.Message);
            }

        }

        public  void EditQuery(int category)
        {
            try
            {

                PrintQuery(category);
                string ID = ProductsBO.ValidateID();
                
                List<string> values = ProductsDAL.QueryProduct(ID, category);
                if (values != null)
                {

                    string name;
                    double price;
                    string description;


                    Console.WriteLine("Do you want to change the name of the product(y)?");
                    string des_name = Console.ReadLine();
                    if (des_name == "y")
                    {
                        Console.WriteLine("Please write the new name of the product");
                        name = Console.ReadLine();
                    }
                    else
                    {
                        name = values[1];
                    }
                    Console.WriteLine("Do you want to change the price of the product(y)?");
                    string des_price = Console.ReadLine();
                    if (des_price == "y")
                    {
                        Console.WriteLine("Please write the new price for the product");
                        price = Convert.ToDouble(Console.ReadLine());
                    }
                    else
                    {
                        price = Convert.ToDouble(values[2]);
                    }
                    Console.WriteLine("Do you want to change the description of the product?(y)");
                    string des_description = Console.ReadLine();
                    if (des_description == "y")
                    {
                        Console.WriteLine("Please write the new description of the product");
                        description = Console.ReadLine();
                    }
                    else
                    {
                        description = values[3];
                    }
                    int quantity = ProductsDAL.EditAProduct(ID,name,price,description);
                    if (quantity == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Product was edited successfully");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("The product was not edited, did you check that the ID was written correctly?");
                    }

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("The ID is incorrect, please try again");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem connecting with the database");
                e.ToString();
            }
        }

        public  void PrintQuery(int category)
        {

            int products_counter = 1;
            DataTable products_data = ProductsDAL.PrintProducts(category);
            if (products_data == null)
            {
                Console.WriteLine("There was no access to the database");
            }
            else
            {
                Console.WriteLine("NO ID    PRICE  NAME                 DESCRIPTION");
                for (int i = 0; i < products_data.Rows.Count; i++)
                {
                    if (category == products_data.Rows[i].Field<int>("ID_Category"))
                    {
                        string ID = products_data.Rows[i].Field<string>("Code_product");
                        string name = products_data.Rows[i].Field<string>("Name");
                        double price = products_data.Rows[i].Field<double>("Price");
                        string description = products_data.Rows[i].Field<string>("Description");
                        //Users new_user = new Users(name, lastname, username, mail, password, address, telephone);
                        Console.WriteLine($"{products_counter}  {ID,-6}{price,-6} {name,-20} {description,-25}");
                        products_counter += 1;
                    }
                }

            }

        }
        public string ValidateString()
        {
            Console.WriteLine("Please introduce the ID");
            string ID = Console.ReadLine();
            DataTable products_data = ProductsDAL.ValidateStringSQL();
            if (products_data == null)
            {
                Console.WriteLine("There was no access to the database");
                return null;
            }
            else
            {
                for (int i = 0; i < products_data.Rows.Count; i++)
                {
                    if (ID == products_data.Rows[i].Field<string>("Code_product"))
                    {
                        Console.WriteLine("The ID already exists, try again");
                        return ValidateString();
                    }
                }
                return ID;

            }
            
        }
        }
    }

