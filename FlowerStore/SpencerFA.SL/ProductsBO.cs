using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using SpencerFA.BO;
using System.Text.RegularExpressions;

namespace SpencerFA.SL
{
    public static class ProductsBO
    {
        public static Tuple<Products, int> AddProducts(List<Products> products, UsersBO obj_usersBO)

        {
            Console.WriteLine("Please select the number of the product you want to buy");
            int decision0 = obj_usersBO.ValidateInteger();
            if (decision0 >= products.Count || decision0 < 0)
            {
                Console.WriteLine("That is not a valid option, please enter the number of the product you want to buy");
                return AddProducts(products, obj_usersBO);
            }
            Console.WriteLine("Now decide the quantity you want of it.");
            int decision1 = obj_usersBO.ValidateInteger();

            Console.Clear();
            Console.WriteLine("The products were added to the cart");

            return new Tuple<Products, int>(products[decision0], decision1);
        }

        public static void PrintAllProducts(List<Products> objects)
        {
            Console.WriteLine("************************************************");
            Console.WriteLine("NO ID    PRICE  NAME                 DESCRIPTION");

            for (int i = 0; i < objects.Count; i++)
            {
                Console.WriteLine("{0}  {1}", i, objects[i].ToString());
            }
            Console.WriteLine("************************************************");
            Console.WriteLine(" ");

        }

        public static void SearchForAProdcut(List<Products> objects)
        {
            Console.WriteLine("Write the name of the product you want to search");
            string name_product = Console.ReadLine();
            bool isHere = false;
            var product = from pr in objects where pr.Name == name_product select pr;
            

            foreach (Products pr in objects)
            {
                if (Regex.IsMatch(pr.Name.ToLower(),name_product.ToLower()))
                {
                    Console.WriteLine("*********************************************");
                    Console.WriteLine("ID    PRICE  NAME                 DESCRIPTION");
                    Console.WriteLine(pr.ToString());
                    isHere = true;
                    Console.WriteLine("*********************************************");
                }
            }
            if (isHere == false)
            {
                Console.WriteLine("We do not have that product yet");
            }
        }
        public static double FinalPrice(List<Tuple<Products, int>> products)
        {
            double final_price = 0;
            foreach (var pr in products)
            {
                final_price += pr.Item1.Price * pr.Item2;
            }
            return final_price;
        }

        public static string PurchaseTicket()
        {
            Random rand = new Random();

            // Choosing the size of string
            // Using Next() string
            int stringlen = rand.Next(4, 10);
            int randValue;
            string str = "";
            char letter;
            for (int i = 0; i < stringlen; i++)
            {

                // Generating a random number.
                randValue = rand.Next(0, 26);

                // Generating random character by converting
                // the random number into character.
                letter = Convert.ToChar(randValue + 65);

                // Appending the letter to string.
                str += letter;
            }
            return str;
        }

        // SuperUser common methods
        public static int SelectCategory(List<Tuple<int,string>> product_categories, UsersBO obj_usersBO)
        {
            Console.WriteLine("Please select the category you want to edit:");
            Console.WriteLine("No  Category");
            for (int i = 0; i < product_categories.Count; i++)
            {
                Console.WriteLine("{0}   {1}", product_categories[i].Item1, product_categories[i].Item2);
            }
            int decision2 = obj_usersBO.ValidateInteger();
            if (decision2 > product_categories.Count || decision2 <= 0)
            {
                Console.Clear();
                Console.WriteLine("That is not a valid option, please select a valid option");
                return SelectCategory(product_categories, obj_usersBO);
            }

            int catModified2 = decision2;
            return catModified2;
        }

        public static void RecoverXMLs(List<Tuple<int,string>> product_categories, Dictionary<string,List<Products>> allProducts)
        {
            foreach(var pr in product_categories)
            {
                string finalpath = @"C:\Users\erick.barlandas\source\repos\FlowerStore\XMLfiles\" +
                  pr.Item2 + ".XML";
                ProductsBO.CreateXML(finalpath, allProducts[pr.Item2]);
            }
            for (int i = 0; i < allProducts.Count; i++)
            {

                
            }
            Console.WriteLine("You can find the files in the path: ");
            Console.WriteLine(@"C:\Users\erick.barlandas\source\repos\FlowerStore\XMLfiles\");
            Console.WriteLine(" ");
        }
        public static void CreatingTicketXML(Users us, List<Tuple<Products, int>> final_products,string ticket, string addressCC)
        {
            string extra_path = us.Username + "_" + ticket;
            XmlTextWriter xmlWriter = new XmlTextWriter(@"C:\Users\erick.barlandas\source\repos\FlowerStore\Users\Deliveries\" + extra_path + ".XML",
                                                        System.Text.Encoding.UTF8)
            {
                Formatting = Formatting.Indented
            };
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteComment("Purchase " + ticket);
            xmlWriter.WriteComment("DeliveryAddress: " + addressCC);
            //User information
            //Products of the purchase
            xmlWriter.WriteStartElement("Products");
            foreach (var pr in final_products)
            {
                xmlWriter.WriteStartElement("Product");
                xmlWriter.WriteElementString("ID", pr.Item1.ID);
                xmlWriter.WriteElementString("Name", pr.Item1.Name);
                xmlWriter.WriteElementString("Price", Convert.ToString(pr.Item1.Price));
                xmlWriter.WriteElementString("Description", pr.Item1.Description);
                xmlWriter.WriteElementString("Quantity", Convert.ToString(pr.Item2));
                xmlWriter.WriteElementString("Total", Convert.ToString(pr.Item1.Price * pr.Item2));
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();
            //Console.WriteLine("You can find the files in the path: ");
            //Console.WriteLine(@"C:\Users\erick.barlandas\source\repos\FlowerStore\Users\Deliveries\");
            Console.WriteLine("");
            

        }

        public static List<string> RecoverTicketsFromXMLs(Users user)
        {
            string dirPath = @"C:\Users\erick.barlandas\source\repos\FlowerStore\Users\Deliveries";
            DirectoryInfo directory = new DirectoryInfo(dirPath);
            FileInfo[] files = directory.GetFiles(user.Username + "*");
            List<string> files0 = new List<string>();
            foreach (var file in files)
            {
                string x = file.Name.Split('_')[1];
                string y = x.Split('.')[0];
                files0.Add(y);
            }

            return files0;
        }

        public static void CreateXML(string name_of_file, List<Products> products)
        {

            XmlTextWriter xmlWriter = new XmlTextWriter(name_of_file, System.Text.Encoding.UTF8)
            {
                Formatting = Formatting.Indented
            };
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteComment("Products of the stores");
            xmlWriter.WriteStartElement("Products");
            foreach (Products pr in products)
            {
                xmlWriter.WriteStartElement("Product");
                xmlWriter.WriteAttributeString("ID", pr.ID);
                xmlWriter.WriteElementString("ID", pr.ID);
                xmlWriter.WriteElementString("Name", pr.Name);
                xmlWriter.WriteElementString("Price", Convert.ToString(pr.Price));
                xmlWriter.WriteElementString("Description", pr.Description);
                xmlWriter.WriteEndElement();

            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();

        }

        public static string ValidateID()
        {
            Console.WriteLine("Please introduce the ID of the product: ");
            try
            {
                string ID = Console.ReadLine();
                if (ID.Length == 3)
                {
                    return ID;
                }
                else
                {
                    Console.WriteLine("That is not a valid ID,remember use a max length of three");
                    return ValidateID();
                }
            }
            catch (NameException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidateID();
            }
        }



    }   
}
