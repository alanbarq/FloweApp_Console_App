using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using SpencerFA.BO;

namespace SpencerFA.SL
{
    public static class SummarizeCode
    {
        public static bool LeavingMenu()
        {
            Console.WriteLine("You select to return back to main menu");
            Console.WriteLine(" ");
            return true;
        }
        public static string CreatingTicket()
        {
            string ticket = ProductsBO.PurchaseTicket();
            Console.Clear();
            Console.WriteLine("You agree with the purchase'");
            Console.WriteLine("The ticket for your purchase is {0}", ticket);

            return ticket;
        }
        /*
        public static void printingTicketInfo(Dictionary<string, List<Tuple<Products, int>>> purchases,
            Dictionary<string, DateTime> delivery_date)
        {
            Console.WriteLine("Please copy the ticket you want to check: ");
            Console.WriteLine("");
            
            foreach (var pair in purchases)
            {
                Console.WriteLine(pair.Key);
            }
            Console.WriteLine("");
            string key1 = Console.ReadLine();

            var final_list = purchases[key1];
            var final_delivery = delivery_date[key1];
            Console.WriteLine("Product, Quantity");
            foreach (var item in final_list)
            {
                Console.WriteLine("{0}, {1}", item.Item1.Name, item.Item2);
            }
            Console.WriteLine("And the delivery date will be on: {0}", final_delivery);
        }
        */
        public static void PrintingFinalProducts(List<Tuple<Products, int>> final_products)
        {
            Console.WriteLine("**************************************");
            Console.WriteLine("NAME                PRICE     QUANTITY");
            Console.WriteLine("");
            foreach (var item in final_products)
            {
                Console.WriteLine("{0,-20}{1,-10}{2,-6}", item.Item1.Name, item.Item1.Price, item.Item2);
            }
            Console.WriteLine("**************************************");
        }
        public static void PrintingTicketInfo2(Users user)
        {
            string ID = "";
            string name = "";
            double price = 0;
            string description = "";
            string quantity = "";
            double total = 0;

            string datetime = "";
            double total_price = 0;
            Console.WriteLine("Please copy the ticket you want to check: ");
            Console.WriteLine("");
            try
            {
                List<string> purchases = ProductsBO.RecoverTicketsFromXMLs(user);
                foreach (var pair in purchases)
                {
                    Console.WriteLine(pair);
                }
                Console.WriteLine("");
                string key1 = Console.ReadLine();

                string filePath = @"C:\Users\erick.barlandas\source\repos\FlowerStore\Users\Deliveries\deliveries.txt";
                List<string> lines = File.ReadAllLines(filePath).ToList();
                foreach (var line in lines)
                {
                    string[] product_details = line.Split(',');
                    if (product_details[0] == key1)
                    {
                        datetime = Convert.ToString(product_details[1]);
                    }

                }
                Console.WriteLine("The delivery date is in: " + datetime);
                XmlDocument xmlDocProdcuts = new XmlDocument();
                Console.WriteLine("******************************************************************************************");
                Console.WriteLine("ID     NAME                 PRICE    DESCRIPTION                       QUANTITY      TOTAL");
                xmlDocProdcuts.Load(@"C:\Users\erick.barlandas\source\repos\FlowerStore\Users\Deliveries\" + user.Username + "_" + key1 + ".XML");
                var ProductsOnTheShop = xmlDocProdcuts.DocumentElement.ChildNodes;
                foreach (XmlNode productNode in ProductsOnTheShop)
                {
                    if (productNode.HasChildNodes)
                    {
                        foreach (XmlNode field in productNode.ChildNodes)
                        {
                            switch (field.Name)
                            {
                                case "ID":
                                    ID = field.InnerText;
                                    //Console.WriteLine(ID);
                                    break;
                                case "Name":
                                    name = field.InnerText;
                                    //Console.WriteLine(name);
                                    break;
                                case "Price":
                                    price = Convert.ToDouble(field.InnerText);
                                    //Console.WriteLine(price);
                                    break;
                                case "Description":
                                    description = field.InnerText;
                                    //Console.WriteLine(description);
                                    break;
                                case "Quantity":
                                    quantity = field.InnerText;
                                    //Console.WriteLine(quantity);
                                    break;
                                case "Total":
                                    total = Convert.ToDouble(field.InnerText);
                                    total_price += total;
                                    break;

                            }

                        }
                        Console.WriteLine($"{ID,-6} {name,-20} {price,-8} {description,-40} {quantity,-6} {total,-6}");
                    }
                }
                Console.WriteLine(" ");
                Console.WriteLine("******************************************************************************************");
                Console.WriteLine("Your total is: " + total_price);
                Console.WriteLine(" ");
                Console.WriteLine("******************************************************************************************");
            }catch(Exception e)
            {
                Console.WriteLine("THE TICKET DOES NOT EXIST, PLEASE TRY AGAIN");
                e.ToString();
            }
        }

        public static void CreatingNewDelivery(string ticket, DateTime datetime,string dateCC)
        {
            string fullpath = @"C:\Users\erick.barlandas\source\repos\FlowerStore\Users\Deliveries\deliveries.txt";

            string full_product = $"{ticket},{datetime},{dateCC}";

            File.AppendAllText(fullpath, full_product + Environment.NewLine);

        }
    }
}

