using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace SpencerFA.DL
{
    public class TicketDAL
    {
        public int CreatingNewDeliverySQL(string ticket, string username, DateTime datetime, string address)
        {

            ProductsDAL obj_productsDAL = new ProductsDAL();
            //string sql = "INSERT INTO tb_ticket(ID_ticket,username,datetime) VALUES('" + ticket + "','" + username + "','" + datetime + "');";
            SqlCommand command = new SqlCommand("sp_CreatingNewDelivery", obj_productsDAL.Connect())
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@TICKET", SqlDbType.NVarChar).Value = ticket;
            command.Parameters.AddWithValue("@USERNAME", SqlDbType.NVarChar).Value = username;
            command.Parameters.AddWithValue("@DATETIME", SqlDbType.NVarChar).Value = datetime;
            command.Parameters.AddWithValue("@ADDRESS", SqlDbType.NVarChar).Value = address;
            int quantity = command.ExecuteNonQuery(); //Not a selection query, just to check;
            obj_productsDAL.Disconnect();
            if (quantity == 1)
            {
                return 1;
                   
            }
            else return 0;


        }

        public int AddingPurchase(string ticket,string ID,int val)
        {

            ProductsDAL obj_productsDAL = new ProductsDAL();

            SqlCommand command = new SqlCommand("sp_AddingPurchase", obj_productsDAL.Connect())
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@TICKET", SqlDbType.NVarChar).Value = ticket;
            command.Parameters.AddWithValue("@ID", SqlDbType.Char).Value = ID;
            command.Parameters.AddWithValue("@QUANTITY", SqlDbType.NVarChar).Value = val;
            int quantity = command.ExecuteNonQuery(); //Not a selection query, just to check;
            obj_productsDAL.Disconnect();
            if (quantity != 1)
            {
                obj_productsDAL.Disconnect();
                return 0;
               
            }
            else
            {
                return 1;
            }
                
            

        }

        public DataTable RecoverTicketsFromSQL(string username)
        {
            ProductsDAL con = new ProductsDAL();
            SqlCommand command = new SqlCommand("sp_RecoverTickets", con.Connect())
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@USERNAME", SqlDbType.NVarChar).Value = username;
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);//Not a selection query, just to check;
            DataTable dt = new DataTable();
            dt.Load(dr);
            con.Disconnect();
            return dt;
            
        }

        public Tuple<DataTable,DateTime> RecoverProductsFromTicketSQL(string username, Tuple<string, DateTime> ticket)
        {
           
            ProductsDAL con = new ProductsDAL();
            SqlCommand command = new SqlCommand("sp_RecoverProducts", con.Connect())
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@TICKET", SqlDbType.NVarChar).Value = ticket.Item1;
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);//Not a selection query, just to check;
            DataTable dt_products = new DataTable();
            dt_products.Load(dr);
            con.Disconnect();
            Tuple<DataTable, DateTime> products_details = new Tuple<DataTable, DateTime>(dt_products, ticket.Item2);
            return products_details;
                
            

            
        }
        public int AddingCreditCard(List<string> CreditCardValues, string username)
        {

            ProductsDAL obj_productsDAL = new ProductsDAL();
            //string sql = "INSERT INTO tb_ticket(ID_ticket,username,datetime) VALUES('" + ticket + "','" + username + "','" + datetime + "');";
            SqlCommand command = new SqlCommand("sp_AddCredittCard", obj_productsDAL.Connect())
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@USERNAME", SqlDbType.NVarChar).Value = username;
            command.Parameters.AddWithValue("@NAME", SqlDbType.NVarChar).Value = CreditCardValues[0];
            command.Parameters.AddWithValue("@DIGITS", SqlDbType.NVarChar).Value = CreditCardValues[1];
            command.Parameters.AddWithValue("@MONTH", SqlDbType.NVarChar).Value = CreditCardValues[2];
            command.Parameters.AddWithValue("@YEAR", SqlDbType.NVarChar).Value = CreditCardValues[3];
            //command.Parameters.AddWithValue("@CVV", SqlDbType.NVarChar).Value = CreditCardValues[4];
            command.Parameters.AddWithValue("@ADDRESS", SqlDbType.NVarChar).Value = CreditCardValues[5];
            int quantity = command.ExecuteNonQuery(); //Not a selection query, just to check;
            obj_productsDAL.Disconnect();
            if (quantity == 1)
            {
                return 1;
            }
            else return 0;

        }

        public DataTable RecoverCreditCardSQL(string username)
        {
            ProductsDAL con = new ProductsDAL();
            SqlCommand command = new SqlCommand("sp_RecoverCreditCard", con.Connect())
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@USERNAME", SqlDbType.NVarChar).Value = username;
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);//Not a selection query, just to check;
            DataTable dt = new DataTable();
            dt.Load(dr);
            con.Disconnect();
            return dt;
        }
    }
   
}
