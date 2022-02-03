using System;
using System.Collections.Generic;
using SpencerFA.DL;
using SpencerFA.BO;
using System.Data;

namespace SpencerFA.SL
{
    public class TicketSL
    {

        public void CreateTicket(string ticket, DateTime datimetime, string username, string address)
        {
            try
            {
                DL.TicketDAL obj_ticketDAL = new DL.TicketDAL();
                int quantity = obj_ticketDAL.CreatingNewDeliverySQL(ticket, username, datimetime, address);
                
                if (quantity == 1)
                {
                    
                    Console.WriteLine("The ticket was created successfully");
                }
                else  Console.WriteLine("The ticket was not created");

            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error accessing to the database");
                Console.WriteLine(e.Message);
            }
            
        }

        public void CreatePurchase(List<Tuple<Products, int>> final_products, string ticket)
        {
            try
            {
                DL.TicketDAL obj_ticketDAL = new DL.TicketDAL();
                ProductsDAL obj_productsDAL = new ProductsDAL();
                foreach (var pr in final_products)
                {

                    int quantity = obj_ticketDAL.AddingPurchase(ticket,pr.Item1.ID,pr.Item2);
                    if (quantity != 1)
                    {
                        Console.WriteLine("There was an error accessing to database");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void RecoverProducts(string username)
        {
            string name;
            string ID;
            double price;
            string description;
            int quantity;
            double total;
            double total_price = 0;
            try
            {

                DL.TicketDAL obj_ticketDAL = new DL.TicketDAL();
                Tuple<string, DateTime> ticket = RecoverTickets(username);
                Tuple<DataTable,DateTime> dt_details = obj_ticketDAL.RecoverProductsFromTicketSQL(username,ticket);
                DataTable dt_products = dt_details.Item1; 
                Console.WriteLine("The delivery date is in: " + dt_details.Item2);
                Console.WriteLine("******************************************************************************************");
                Console.WriteLine("ID     NAME                 PRICE    DESCRIPTION                       QUANTITY      TOTAL");
                for (int i = 0; i < dt_products.Rows.Count; i++)

                {
                    ID = dt_products.Rows[i].Field<string>("Code_product");
                    name = dt_products.Rows[i].Field<string>("Name");
                    price = dt_products.Rows[i].Field<double>("Price");
                    description = dt_products.Rows[i].Field<string>("Description");
                    quantity = dt_products.Rows[i].Field<int>("quantity");
                    total = price * quantity;
                    total_price += total;
                    Console.WriteLine($"{ID,-6} {name,-20} {price,-8} {description,-40} {quantity,-6} {total,-6}");
                }
                Console.WriteLine(" ");
                Console.WriteLine("******************************************************************************************");
                Console.WriteLine("Your total is: " + total_price);
                Console.WriteLine(" ");
                Console.WriteLine("******************************************************************************************");
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error with retrieving the data, please try again");
                Console.WriteLine(e.Message);
            }
        }

        public void SaveCard(List<string> CCValues, string username)
        {
            try
            {
                DL.TicketDAL obj_ticketDAL = new DL.TicketDAL();
                int savecard = obj_ticketDAL.AddingCreditCard(CCValues, username);
                if (savecard != 1)
                {
                    Console.WriteLine("The card was not saved");
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public string RecoverCreditCard(string username)
        {
            try
            {
                UsersBO obj_usersBO = new UsersBO();
                DL.TicketDAL obj_ticketDAL = new DL.TicketDAL();
                DataTable dt = obj_ticketDAL.RecoverCreditCardSQL(username);
                Console.WriteLine("*****************************");
                Console.WriteLine("No    CreditCard number");
                Console.WriteLine("*****************************");
                for (int i = 0; i < dt.Rows.Count; i++)

                {
                    Console.WriteLine($"{i}    {dt.Rows[i].Field<string>("digits")}");
                }
                Console.WriteLine("*********************************");
                Console.WriteLine("Please enter the number of the CARD you want to use");
                int val = obj_usersBO.ValidateInteger();
                if (val >= dt.Rows.Count || val < 0)
                {
                    Console.WriteLine("The selection does not exist, please try again");
                    return RecoverCreditCard(username);
                }
                else
                {

                    return dt.Rows[val].Field<string>("digits");
                }
            }catch(CreditCardException CC)
            {
                Console.WriteLine(CC.ToString());
                return null;
            }
        }

        public Tuple<string, DateTime> RecoverTickets(string username)
        {
            UsersBO usersBO = new UsersBO();
            DL.TicketDAL obj_ticketDAL = new DL.TicketDAL();
            DataTable dt = obj_ticketDAL.RecoverTicketsFromSQL(username);
            Console.WriteLine("***************");
            Console.WriteLine("No    Ticket");
            Console.WriteLine("***************");
            for (int i = 0; i < dt.Rows.Count; i++)

            {
                Console.WriteLine($"{i}    {dt.Rows[i].Field<string>("Ticket")}");
            }
            Console.WriteLine("***************");
            Console.WriteLine("Please enter the number of the ticket you want to check");
            int ticket = usersBO.ValidateInteger();
            if (ticket >= dt.Rows.Count || ticket < 0)
            {
                Console.WriteLine("The ticket does not exist, please try again");
                return RecoverTickets(username);
            }
            else
            {
                Tuple<string, DateTime> tuple_result = new Tuple<string, DateTime>(dt.Rows[ticket].Field<string>("Ticket"), Convert.ToDateTime(dt.Rows[ticket].Field<string>("datetime")));
                return tuple_result;


            }
        }
    }
}
