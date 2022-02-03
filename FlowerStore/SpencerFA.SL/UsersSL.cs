using SpencerFA.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpencerFA.DL;
using System.Data;

namespace SpencerFA.SL
{
    public static class UsersSL
    {

        public static void CreatingNewUser(List<Users> users)
        {
            UsersDAL usersDAL = new UsersDAL();
            UsersBO usersBO = new UsersBO();

            string name = usersBO.ValidateName();
            string lastname = usersBO.ValidateLastName();
            string username = usersBO.ValidateUsername(users);
            string mail = usersBO.ValidateMail(users);
            string password = usersBO.ValidatePassword();
            string address = usersBO.ValidateAddress();
            string telephone = usersBO.ValidatePhoneNumber();

            Users new_user = new Users(name, lastname, username, mail, password, address, telephone);
            // Save the employee in the SQL database
            if (usersDAL.SaveUser(new_user))
            {
                usersDAL.RecoverUsersFromSQL();
                Console.WriteLine("User saved");
            }
            else
            {
                Console.WriteLine("There was no access to the database");
                Console.WriteLine("The user was not saved in the SQL database");
            }
        }

        public static List<Users> GetUsers()
        {
            UsersDAL usersDAL = new UsersDAL();
            return usersDAL.RecoverUsersFromSQL();
        }

        public static void UpdateTheUser(Users user)
        {
            UsersBO usersBO = new UsersBO();
            string name;
            string lastname;
            string address;
            string password;
            string telephone;
            try
            {

                Console.WriteLine("Do you want to change the name(y)");
                string des_name = Console.ReadLine();
                if (des_name == "y")
                {
                    name = usersBO.ValidateName();
                }
                else
                {
                    name = user.Name;
                }
                Console.WriteLine("Do you want to change the lastname(y)");
                string des_lastname = Console.ReadLine();
                if (des_lastname == "y")
                {
                    lastname = usersBO.ValidateLastName();
                }
                else
                {
                    lastname = user.Lastname;
                }
                Console.WriteLine("Do you want to change the password(y)");
                string des_password = Console.ReadLine();
                if (des_password == "y")
                {
                    password = usersBO.ValidatePassword();
                }
                else
                {
                    password = user.Password;
                }
                Console.WriteLine("Do you want to change the Address(y)");
                string des_address = Console.ReadLine();
                if (des_address == "y")
                {
                    address = usersBO.ValidateAddress();
                }
                else
                {
                    address = user.Address;
                }
                Console.WriteLine("Do you want to change the Telephone(y)");
                string des_telephone = Console.ReadLine();
                if (des_telephone == "y")
                {
                    telephone = usersBO.ValidatePhoneNumber();
                }
                else
                {
                    telephone = user.Cellphone;
                }

                int update = UsersDAL.UpdateUser(user,name,lastname,address,password,telephone);
                if (update == 1)
                {
                    Console.WriteLine("The user was updated successfully");
                }
                else
                {
                    Console.WriteLine("The user ws not updated");
                }

            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void PrintTheUsers()
        {
            
            int num_of_customers = 1;
            DataTable user_data = UsersDAL.PrintUsers();
            if(user_data == null)
            {
                Console.WriteLine("There was no access to the database");
            }
            else
            {
                Console.WriteLine("************************************************************************************************************************");
                Console.WriteLine("No  Name     LastName          Username           Mail                  Telephone       Address");
                for (int i = 0; i < user_data.Rows.Count; i++)
                {
                    string username = user_data.Rows[i].Field<string>("username");
                    var name = user_data.Rows[i].Field<string>("name");
                    var lastname = user_data.Rows[i].Field<string>("lastname");
                    //var password = user_data.Rows[i].Field<string>("password");
                    var address = user_data.Rows[i].Field<string>("address");
                    var mail = user_data.Rows[i].Field<string>("mail");
                    var telephone = user_data.Rows[i].Field<string>("telephone");
                    //Users new_user = new Users(name, lastname, username, mail, password, address, telephone);
                    Console.WriteLine($"{num_of_customers}   {name,-8} {lastname,-14}    {username,-14}     {mail,-22}{telephone,-14}  {address}");

                    num_of_customers++;
                }
                Console.WriteLine("************************************************************************************************************************");
            }
            
        }

        public static void DeleteTheUser(string username)
        {
            try
            {
                UsersDAL con = new UsersDAL();

                int quantity = UsersDAL.DeleteUser(username);
                if (quantity == 1)
                {
                    con.Disconnect();
                    Console.WriteLine("User was deleted successfully");
                }
                else
                {
                    con.Disconnect();
                    Console.WriteLine("User was not deleted");
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void QueryTheUser(string username)
        {
            try
            {
                
                List<string> data_user = UsersDAL.QueryUser(username);
                if (data_user == null)
                {
                    Console.WriteLine("There is not a user with that username");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("USER FOUND");
                    Console.WriteLine(" ");
                    Console.WriteLine("************************************************************************************************************************");
                    Console.WriteLine("No  Name     LastName          Username           Mail                  Telephone       Address");

                    //Users new_user = new Users(name, lastname, username, mail, password, address, telephone);
                    Console.WriteLine($"1   {data_user[1],-8} {data_user[2],-14}    {data_user[0],-14}     {data_user[5],-22}{data_user[6],-14}  {data_user[4]}");
                    Console.WriteLine("************************************************************************************************************************");
                }
                
            }catch(Exception e)
            {
                Console.WriteLine("There was not access to database");
                e.ToString();
            }
        }
    }
}
