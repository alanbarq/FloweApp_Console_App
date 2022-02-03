using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SpencerFA.BO;
using System.Configuration;

namespace SpencerFA.DL
{
    public class UsersDAL
    {
        readonly SqlConnection con;
        public UsersDAL()
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

            }catch(Exception e)
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
        public bool SaveUser(Users user)
        {
            try
            {
                UsersDAL con = new UsersDAL();
                SqlCommand command = new SqlCommand("sp_CreateUser", con.Connect())
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@username", SqlDbType.NVarChar).Value = user.Username;
                command.Parameters.AddWithValue("@name", SqlDbType.NVarChar).Value = user.Name;
                command.Parameters.AddWithValue("@lastname", SqlDbType.NVarChar).Value = user.Lastname;
                command.Parameters.AddWithValue("@address", SqlDbType.NVarChar).Value = user.Address;
                command.Parameters.AddWithValue("@password", SqlDbType.NVarChar).Value = user.Password;
                command.Parameters.AddWithValue("@mail", SqlDbType.NVarChar).Value = user.Mail;
                command.Parameters.AddWithValue("@telephone", SqlDbType.NChar).Value = user.Cellphone;
                int quantity = command.ExecuteNonQuery(); //Not a selection query, just to check;
                con.Disconnect();
                if (quantity == 1)
                {
                    return true;
                }
                else return false;


            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }
        }
        public DataTable ListUsers()
        {
            try
            {
                UsersDAL con = new UsersDAL();
                SqlCommand command = new SqlCommand("sp_SelectAllUsers", con.Connect())
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);//Not a selection query, just to check;
                DataTable dt = new DataTable();
                dt.Load(dr);
                con.Disconnect();
                return dt;


            }
            catch (Exception e)
            {
                e.ToString();
                return null;
            }
        }

        public List<Users> RecoverUsersFromSQL()
        {
            UsersDAL usersdal = new UsersDAL();
            List<Users> users = new List<Users>();
            DataTable user_data = usersdal.ListUsers();
            if (user_data == null)
            {
                return null;
            }
            else
            {
                for(int i = 0; i < user_data.Rows.Count; i++)
                {
                    string username = user_data.Rows[i].Field<string>("username");
                    var name = user_data.Rows[i].Field<string>("name");
                    var lastname = user_data.Rows[i].Field<string>("lastname");
                    var password = user_data.Rows[i].Field<string>("password");
                    var address = user_data.Rows[i].Field<string>("address");
                    var mail = user_data.Rows[i].Field<string>("mail");
                    var telephone = user_data.Rows[i].Field<string>("telephone");
                    //Users new_user = new Users(name, lastname, username, mail, password, address, telephone);
                    users.Add(new Users(name, lastname, username, mail, password, address, telephone));
                }
                return users;
            }


        }

        public static DataTable PrintUsers()
        {
            UsersDAL usersDAL = new UsersDAL();

            DataTable user_data = usersDAL.ListUsers();
            if (user_data == null) return null;
            else return user_data;
        }

        public static List<string> QueryUser(string usern)
        {

            List<string> data_user = new List<string>();
            UsersDAL con = new UsersDAL();
            SqlCommand command = new SqlCommand("sp_QueryUser", con.Connect())
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@username", SqlDbType.NVarChar).Value = usern;
            SqlDataReader dr = command.ExecuteReader();//Not a selection query, just to check;

            if (dr.Read()) // if the user exists
            {
                string username = dr["username"].ToString();
                data_user.Add(username);
                var name = dr["name"].ToString();
                data_user.Add(name);
                var lastname = dr["lastname"].ToString();
                data_user.Add(lastname);
                var password = dr["password"].ToString();
                data_user.Add(password);
                var address = dr["address"].ToString();
                data_user.Add(address);
                var mail = dr["mail"].ToString();
                data_user.Add(mail);
                var telephone = dr["telephone"].ToString();
                data_user.Add(telephone);
                con.Disconnect();
                return data_user;
                    
                    
            }
            else
            {
                    
                con.Disconnect();
                return null;
            }
       
        }
        public static int UpdateUser(Users user,string name,string lastname,string address,string password,string telephone)
        {
            

            
            UsersDAL con = new UsersDAL();
            SqlCommand command = new SqlCommand("sp_UpdateUser", con.Connect())
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@username", SqlDbType.NVarChar).Value = user.Username;
            command.Parameters.AddWithValue("@name", SqlDbType.NVarChar).Value = name;
            command.Parameters.AddWithValue("@lastname", SqlDbType.NVarChar).Value = lastname;
            command.Parameters.AddWithValue("@address", SqlDbType.NVarChar).Value = address;
            command.Parameters.AddWithValue("@password", SqlDbType.NVarChar).Value = password;
            command.Parameters.AddWithValue("@telephone", SqlDbType.NChar).Value = telephone;
            int quantity = command.ExecuteNonQuery(); //Not a selection query, just to check;

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

        public static int DeleteUser(string username)
        {

            UsersDAL con = new UsersDAL();
            SqlCommand command = new SqlCommand("sp_DeleteUser", con.Connect())
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@username", SqlDbType.NVarChar).Value = username;
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

    }


}
