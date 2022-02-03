using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using SpencerFA.BO;
//using FlowerStore.User;

namespace SpencerFA.SL
{
    public class UsersBO
    {
        public UsersBO()
        {
        }
        // check if a user is already registered
        public Tuple<int,int> CheckUserName(List<Users> users)
        {

            Console.WriteLine("Please enter your username");
            string username= Console.ReadLine();
            Console.WriteLine("Now, introduce your password");
            string pass = ReadPassword();
            
            if (username == "aa" && pass == "aa")
            {
                return new Tuple<int,int> (0,0);
            }
            for(int i = 0;i < users.Count; i++)
            {
                if (users[i].Username == username && users[i].Password == pass)
                {
                        return new Tuple<int, int>(1, i);
                }
            }
            Console.WriteLine("");
            Console.WriteLine("The password or username is incorrect. Or the user does not exist");
            return new Tuple<int,int>(2,0);
            
        }
        public static string ReadPassword()
        {
            string password = "";
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        return null;
                    case ConsoleKey.Enter:
                        return password;
                    case ConsoleKey.Backspace:
                        if (password.Length > 0)
                        {
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                        }
                        break;
                    default:
                        password += key.KeyChar;
                        Console.Write("*");
                        break;
                }
            }
        }
        // Creating a new user 
        /*public void creatingNewUser(List<Users> users)
        {
            //string fullpath = @"C:\Users\erick.barlandas\source\repos\FlowerStore\Users\users.txt";
            string name = ValidateName();
            string lastname = ValidateLastName();
            string username = ValidateUsername(users);
            string mail = ValidateMail(users);
            string password = ValidatePassword();
            string address = ValidateAddress();
            string telephone = ValidatePhoneNumber();
            /*
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\erick.barlandas\source\repos\FlowerStore\Users\users.XML");
            XmlElement user = doc.CreateElement("User");
            doc.DocumentElement.AppendChild(user);
            XmlElement name2 = doc.CreateElement("Name");
            name2.InnerText=name;
            user.AppendChild(name2);
            XmlElement elem = doc.CreateElement("LastName");
            elem.InnerText = lastname;
            user.AppendChild(elem);
            elem = doc.CreateElement("Username");
            elem.InnerText = username;
            user.AppendChild(elem);
            elem = doc.CreateElement("Password");
            elem.InnerText = password;
            user.AppendChild(elem);
            elem = doc.CreateElement("Mail");
            elem.InnerText = mail;
            user.AppendChild(elem);
            elem = doc.CreateElement("Telephone");
            elem.InnerText = telephone;
            user.AppendChild(elem);
            elem = doc.CreateElement("Address");
            elem.InnerText = address;
            user.AppendChild(elem);
            //doc.Save(Console.Out);
            doc.Save(@"C:\Users\erick.barlandas\source\repos\FlowerStore\Users\users.XML");
            Console.WriteLine("You can find the XML in the path: ");
            Console.WriteLine(@"C:\Users\erick.barlandas\source\repos\FlowerStore\Users\users.XML");
            Users new_user = new Users(name, lastname, username, mail, password, address, telephone);
            // Save the employee in the SQL database
            if (UsersDAL.SaveEmployee(new_user))
            {
                UsersDAL.RecoverUsersFromSQL();
                Console.WriteLine("User saved");
            }
            else
            {
                Console.WriteLine("The user was not saved in the SQL database");
            }

            /*
            string full_name = $"{name},{lastname},{username},{mail},{password},{address},{telephone}";
            File.AppendAllText(fullpath, full_name + Environment.NewLine);
            

        } */
        public static void CreateXMLUsers(List<Users> users)
        {

            XmlTextWriter xmlWriter = new XmlTextWriter(@"C:\Users\erick.barlandas\source\repos\FlowerStore\Users\users.XML", System.Text.Encoding.UTF8)
            {
                Formatting = Formatting.Indented
            };
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteComment("Registered users");
            xmlWriter.WriteStartElement("Users");
            foreach (Users us in users)
            {
                xmlWriter.WriteStartElement("User");
                xmlWriter.WriteElementString("Name", us.Name);
                xmlWriter.WriteElementString("LastName", us.Lastname);
                xmlWriter.WriteElementString("Username", us.Username);
                xmlWriter.WriteElementString("Password", us.Password);
                xmlWriter.WriteElementString("Mail", us.Mail);
                xmlWriter.WriteElementString("Telephone", us.Cellphone);
                xmlWriter.WriteElementString("Address", us.Address);
                xmlWriter.WriteEndElement();

            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();
            Console.WriteLine("You can find the files in the path: ");
            Console.WriteLine(@"C:\Users\erick.barlandas\source\repos\FlowerStore\Users\");
            Console.WriteLine("");

        }
        /*public static List<Users> RecoverUsers()
        {

            List<Users> users = new List<Users>();
            string name = "";
            string lastname = "";
            string username = "";
            string mail = "";
            string password = "";
            string address = "";
            string telephone = "";
            /*
            XmlDocument xmlDocProdcuts = new XmlDocument();
            xmlDocProdcuts.Load(@"C:\Users\erick.barlandas\source\repos\FlowerStore\Users\users.XML");
            var UsersOnTheShop = xmlDocProdcuts.DocumentElement.ChildNodes;
            foreach (XmlNode productNode in UsersOnTheShop)
            {
                if (productNode.HasChildNodes)
                {
                    foreach (XmlNode field in productNode.ChildNodes)
                    {
                        switch (field.Name)
                        {
                            case "Name":
                                name = field.InnerText;
                                //Console.WriteLine(ID);
                                break;
                            case "LastName":
                                lastname = field.InnerText;
                                //Console.WriteLine(name);
                                break;
                            case "Username":
                                username = field.InnerText;
                                //Console.WriteLine(price);
                                break;
                            case "Password":
                                password = field.InnerText;
                                break;
                            case "Mail":
                                mail = field.InnerText;
                                //Console.WriteLine(description);
                                break;
                            case "Telephone":
                                telephone = field.InnerText;
                                //Console.WriteLine(quantity);
                                break;
                            case "Address":
                                address = field.InnerText;
                                break;

                        }

                    }
                    users.Add(new Users(name, lastname, username, mail, password, address, telephone)); ;
                }
            }

            return users;

        }*/
        /*public void PrintUsers()
        {
            string name = "";
            string lastname = "";
            string username = "";
            string mail = "";
            string password = "";
            string address = "";
            string telephone = "";
            int num_of_customers = 1;
            XmlDocument xmlDocProdcuts = new XmlDocument();
            xmlDocProdcuts.Load(@"C:\Users\erick.barlandas\source\repos\FlowerStore\Users\users.XML");
            var UsersOnTheShop = xmlDocProdcuts.DocumentElement.ChildNodes;
            Console.WriteLine("No  Name      LastName   Username    Mail                  Telephone   Address");
            foreach (XmlNode productNode in UsersOnTheShop)
            {
                if (productNode.HasChildNodes)
                {
                    foreach (XmlNode field in productNode.ChildNodes)
                    {
                        switch (field.Name)
                        {
                            case "Name":
                                name = field.InnerText;
                                //Console.WriteLine(ID);
                                break;
                            case "LastName":
                                lastname = field.InnerText;
                                //Console.WriteLine(name);
                                break;
                            case "Username":
                                username = field.InnerText;
                                //Console.WriteLine(price);
                                break;
                            case "Password":
                                password = field.InnerText;
                                break;
                            case "Mail":
                                mail = field.InnerText;
                                //Console.WriteLine(description);
                                break;
                            case "Telephone":
                                telephone = field.InnerText;
                                //Console.WriteLine(quantity);
                                break;
                            case "Address":
                                address = field.InnerText;
                                break;

                        }

                    }
                    Console.WriteLine($"{num_of_customers} {name,-6} {lastname,-14}{username,-10}{mail,-22}{telephone,-14}  {address}");
                    Console.WriteLine(" ");
                    num_of_customers++;
                }
            }
            /*
            string filePath = @"C:\Users\erick.barlandas\source\repos\FlowerStore\Users\users.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();
            Console.WriteLine("No  Name      LastName   Username    Mail                  Telephone   Address");
            int num_of_customers = 1;
            foreach (var line in lines)
            {
                string[] product_details = line.Split(',');
                Console.WriteLine($"{num_of_customers} {product_details[0],6} {product_details[1],14}{product_details[2],10}{product_details[3],22}{product_details[6],14}  {product_details[5]}");
                Console.WriteLine(" ");
                num_of_customers++;
            }
            

        }*/
        // Validating the user data
        public string ValidateMail(List<Users> users)
        {
            Console.WriteLine("Please introduce your mail (Remember to use the @ character and a length of at least 6 characters.");
           
            try
            {
                string mail = Console.ReadLine();

                bool same_mail = users.Any(user => user.Mail == mail);
                if (same_mail)

                {
                    Console.WriteLine("The mail already exists");
                    return ValidateMail(users);
                }

                if (mail.Contains("@") && mail.Length > 6)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Please re write your mail again");
                    string mailVal = Console.ReadLine();
                    Console.WriteLine("");
                    if (mail == mailVal)
                    {
                        return mail;
                    }
                    else
                    {
                        Console.WriteLine("The mails do not match");
                        return ValidateMail(users);
                    }
                    
                }
                else
                {
                    Console.WriteLine("Your mail must have the @ character, and a length of at least six characters");
                    return ValidateMail(users);
                }

            }
            catch(MailException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidateMail(users);
            }
            catch(FormatException e)
            {
                Console.WriteLine("Please write a string as xxxx@domain.com");
                Console.WriteLine(e.Message);
                return ValidateMail(users);
            }

        }
        public string ValidateName()
        {

            Console.WriteLine("Please introduce your name: ");
            try
            {
                string name = Console.ReadLine();
                if(name.Length >= 3 && name.All(Char.IsLetter))
                {
                    return name;
                }
                else
                {
                    Console.WriteLine("That is not a valid name, remeber the name must be at least three characters and only " +
                        "alphabetical characters");
                    return ValidateName();
                }

            }catch(FormatException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Please do not user numbers, just alphabetical characters");
                return ValidateName();
            }
            catch(NameException e)
            {
                Console.WriteLine(e.Message);
                return ValidateName();
            }

        }
        public string ValidateLastName()
        {

            Console.WriteLine("Please introduce your lastname: ");
            try
            {
                string lastname = Console.ReadLine();
                if (lastname.Length >= 3 && lastname.All(Char.IsLetter))
                {
                    return lastname;
                }
                else
                {
                    Console.WriteLine("That is not a valid lastname, remeber the lastname must be at least three characters and only " +
                        "alphabetical characters");
                    return ValidateLastName();
                }

            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Please do not user numbers, just alphabetical characters");
                return ValidateLastName();
            }
            catch (NameException e)
            {
                Console.WriteLine(e.Message);
                return ValidateLastName();
            }

        }
        public string ValidateUsername(List<Users> users)
        {
            Console.WriteLine("Please introduce your username");
            try
            {
                string username = Console.ReadLine();
                bool same_username = users.Any(user => user.Username == username);

                if (same_username)
                {
                    Console.WriteLine("The username already exists");
                    return ValidateUsername(users);
                }
                if (username.Length > 6)
                {
                    return username;
                }
                else
                {
                    Console.WriteLine("Your username must have a length of at least six characters");
                    return ValidateUsername(users);
                }

            }
            catch (UsernameException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidateUsername(users);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Please write a string as xxxx@domain.com");
                return ValidateUsername(users);
            }

        }
        public string ValidatePassword()
        {
            Console.WriteLine("Please introduce your password: ");
            Console.WriteLine("More than 5 character and a special character and a number");
            try
            {
                string password = ReadPassword();
                bool result = password.Any(c => char.IsLetter(c)) && password.Any(c => char.IsDigit(c) && password.Any(ch => !Char.IsLetterOrDigit(ch)));
                if (password.Length > 5 && result)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Please re-write your password");
                    string password2 = ReadPassword();
                    Console.WriteLine("");
                    if (password == password2)
                    {
                        
                        Console.WriteLine("Password saved");
                        return password;
                    }
                    else
                    {
                        
                        Console.WriteLine("The passwords are different");
                        return ValidatePassword();
                    }
                }
                else
                {
                    Console.WriteLine("That is not a valid password");
                    return ValidatePassword();
                }
            }catch(ExceptionPassword ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatePassword();
            }
        }
        public string ValidateAddress()
        {
            Console.WriteLine("Please introduce your address: ");
            try
            {
                string address = Console.ReadLine();
                if (address.Length > 6)
                {
                    return address;
                }
                else
                {
                    Console.WriteLine("That is not a valid address");
                    return ValidateAddress();
                }
            }
            catch (AddressException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidateAddress();
            }
        }
        public string ValidatePhoneNumber()
        {
            Console.WriteLine("Please introduce your telephone (10 numbers): ");
            try
            {
                string telephone = Console.ReadLine();
                if (telephone.Length == 10 && telephone.All(Char.IsDigit))
                {
                    return telephone;
                }
                else
                {
                    Console.WriteLine("That is not a valid telephone number");
                    return ValidatePhoneNumber();
                }
            }
            catch(TelephoneException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatePhoneNumber();
            }
        }
        public int ValidateInteger()
        {
            try
            {
                int val = int.Parse(Console.ReadLine());
                return val;

            }
            catch(IntegerException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidateInteger();
            }catch(FormatException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Please enter an integer");
                return ValidateInteger();
            }
        }
    }
}
