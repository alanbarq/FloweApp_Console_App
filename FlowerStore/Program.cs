/*
 COMPANY: ASPIRE SYSTEMS
 TITLE: ONLINE SHOPPING APPLICATION -- FLOWER SHOP 
 DEVELOPER: ERICK ALAN BARLANDAS QUINTANA
 CREATION DATE: December 3rd, 2021.  
 REVIEWED BY: DHINESH DEVAMANI AND AKSHAYA RAJAGOPAL.
 LAST MODIFICATION DATE: JANUARY 3rd 2022.

*/
using System;
using SpencerFA.SL;
namespace FlowerStore
{
    class Program
    {
        static void Main(string[] args)
        {
            UserScreen obj_userScreen = new UserScreen();
            obj_userScreen.SpencerShop();
        }
        /*
        static void ups(string[] args)
        {
            // variables to changing menus
            bool entering = false;
            Tuple<int, int> correct_user;
            Users obj_users = new Users();
            UserScreen obj_userScreen = new UserScreen();
            int main_menu = 0;
            List<Users> flower_users = UsersSL.GetUsers();
            UsersBO obj_usersBO = new UsersBO();
            List<string> product_categories = ProductSL.ProductCategories();
            List<Products> flowers = ProductSL.RecoverProducts("flowers");
            List<Products> gifts = ProductSL.RecoverProducts("gifts");
            List<Products> christmas = ProductSL.RecoverProducts("christmas");
            Dictionary<string, List<Products>> allProducts = new Dictionary<string, List<Products>>() { { "flowers", flowers }, { "gifts", gifts }, { "christmas", christmas } };
            List<Tuple<Products, int>> final_products = new List<Tuple<Products, int>>();

            // Enter user to the shop
            do
            {
                Messages.InitialMessages(); // Initial messages of the flower shop
                int user = obj_usersBO.ValidateInteger();
                switch (user)
                {
                    case 0:

                        correct_user = obj_usersBO.CheckUserName(flower_users);

                        if (correct_user.Item1 == 0)
                        {

                            
                            Console.WriteLine("Welcome Super user");
                            Console.WriteLine(" ");
                            entering = true;
                            main_menu = 1;
                            
                            break;
                        }
                        else if (correct_user.Item1 == 1)
                        {
                            Console.WriteLine("Correct Login");
                            obj_users = flower_users[correct_user.Item2]; //Here  I recover the username of the user
                            Console.WriteLine(" ");
                            main_menu = 2;
                            entering = true;
                            break;
                        }
                        break;
                    case 1:
                        UsersSL.CreatingNewUser(flower_users);
                        flower_users = UsersSL.GetUsers();
                        Console.WriteLine("You have been registered as a new user");
                        
                        break;
                    default:
                        Messages.NotValidOption();
                        break;
                }
            } while (entering == false);
            // menu for main user
            if (main_menu == 2)
            {
                bool quit = false;
                do
                {
                    Messages.SecondMessages(obj_users);
                    int des_customer = obj_usersBO.ValidateInteger();
                    switch (des_customer)
                    {
                        case 1: // If the user wants to check the flowers
                            bool option1 = false;

                            Console.WriteLine("Welcome to the flowers section");
                            do
                            {
                                Messages.ThirdMessages();
                                int des_flower = obj_usersBO.ValidateInteger();
                                switch (des_flower)
                                {
                                    case 1:
                                        ProductsBO.PrintAllProducts(flowers);
                                        break;
                                    case 2:
                                        ProductsBO.SearchForAProdcut(flowers);
                                        break;
                                    case 3:
                                        ProductsBO.PrintAllProducts(flowers);
                                        Tuple<Products, int> product_selected = ProductsBO.AddProducts(flowers, obj_usersBO);
                                        final_products.Add(product_selected);
                                        break;
                                    case 0:
                                        option1 = SummarizeCode.LeavingMenu();
                                        break;
                                    default:
                                        Messages.NotValidOption();
                                        break;
                                }
                            } while (option1 == false);
                            break;
                        case 2: // If the user wants to check the gifts section
                            bool option2 = false;
                            Console.WriteLine("Welcome to the gifts section");
                            do
                            {
                                Messages.ThirdMessages();
                                int des_gifts = obj_usersBO.ValidateInteger();
                                switch (des_gifts)
                                {
                                    case 1:
                                        ProductsBO.PrintAllProducts(gifts);
                                        break;
                                    case 2:
                                        ProductsBO.SearchForAProdcut(gifts);
                                        break;
                                    case 3:
                                        ProductsBO.PrintAllProducts(gifts);
                                        Tuple<Products, int> product_selected = ProductsBO.AddProducts(gifts, obj_usersBO);
                                        final_products.Add(product_selected);
                                        break;
                                    case 0:
                                        option2 = SummarizeCode.LeavingMenu();
                                        break;
                                    default:
                                        Messages.NotValidOption();
                                        break;
                                }
                            } while (option2 == false);
                            break;
                        case 3: // If the user wants to chech the christmas section
                            bool option3 = false;
                            Console.WriteLine("Welcome to the christmas section");
                            do
                            {
                                Messages.ThirdMessages();
                                int des_gifts = obj_usersBO.ValidateInteger();
                                switch (des_gifts)
                                {
                                    case 1:
                                        ProductsBO.PrintAllProducts(christmas);
                                        break;
                                    case 2:
                                        ProductsBO.SearchForAProdcut(christmas);
                                        break;
                                    case 3:
                                        ProductsBO.PrintAllProducts(christmas);
                                        Tuple<Products, int> product_selected = ProductsBO.AddProducts(christmas, obj_usersBO);
                                        final_products.Add(product_selected);
                                        break;
                                    case 0:
                                        option3 = SummarizeCode.LeavingMenu();
                                        break;
                                    default:
                                        Messages.NotValidOption();
                                        break;
                                }
                            } while (option3 == false);
                            break;
                        case 4: // If the user wants to check its cart.
                            bool cart = true;
                            do
                            {
                                SummarizeCode.PrintingFinalProducts(final_products);
                                Messages.CartMessages(ProductsBO.FinalPrice(final_products));
                                int des = obj_usersBO.ValidateInteger();
                                switch (des)
                                {
                                    case 1:
                                        string ticket = SummarizeCode.CreatingTicket(); //Creates a random ticket
                                        SummarizeCode.CreatingNewDelivery(ticket, DateTime.Now.AddDays(20)); //Writes the delivery date and the ticket in a .txt file
                                        ProductsBO.CreatingTicketXML(obj_users, final_products, ticket);  // Writes a XML file of the products, with the ticket as the key.
                                        break;
                                    case 2:
                                        Console.WriteLine("Please indicate the name and the new quantity of the product you want to edit");
                                        string name_quantity = Console.ReadLine();
                                        string[] product_details = name_quantity.Split(',');

                                        for (int i = 0; i < final_products.Count; i++)

                                        {
                                            if (final_products[i].Item1.Name == product_details[0])
                                            {


                                                Tuple<Products, int> tuple0 = new Tuple<Products, int>(final_products[i].Item1, Convert.ToInt32(product_details[1]));
                                                final_products.Add(tuple0);
                                                final_products.RemoveAt(i);
                                                break;
                                            }
                                        }
                                        break;
                                    case 3:
                                        cart = SummarizeCode.LeavingMenu();
                                        break;
                                    default:
                                        Messages.NotValidOption();
                                        break;
                                }


                            } while (cart == false);

                            break;
                        case 5: // Deliveries information
                            bool deliveries = false;
                            do
                            {
                                Messages.DeliveriesMessages();
                                int des = obj_usersBO.ValidateInteger();
                                switch (des)
                                {
                                    case 1:
                                        SummarizeCode.PrintingTicketInfo2(obj_users); // Checking the delivery date and the products of the ticket. 
                                        break;
                                    case 0:
                                        deliveries = SummarizeCode.LeavingMenu();
                                        break;
                                    default:
                                        Messages.NotValidOption();
                                        break;
                                }
                            } while (deliveries == false);
                            break;
                        case 6: // Change user information
                            Console.WriteLine("Lets change your information");
                            UsersSL.UpdateTheUser(obj_users);
                            //flower_users = UsersSL.GetUsers();
                            break;
                        case 0:
                            Console.WriteLine("You select quit the program, see you next time");
                            quit = true;
                            break;
                        default:
                            Messages.NotValidOption();
                            break;
                    }
                } while (quit == false);


            }
            // menu for the superuser
            else if (main_menu == 1)
            {
                bool quitting_user = false;
                do
                {
                    Messages.SuperUserMenu();
                    int superuser = obj_usersBO.ValidateInteger();
                    switch (superuser)
                    {
                        case 1: //Recover XML of the products
                            ProductsBO.RecoverXMLs(product_categories, allProducts);
                            break;
                        case 2: //Add a product
                            
                            string catModified = ProductsBO.SelectCategory(product_categories, obj_usersBO);
                            /*
                            ListOfProducts.AddAProduct(catModified);
                            
                            ProductSL.AddAProduct(catModified);
                            break;
                        case 3: //Remove a product
                            string catModified2 = ProductsBO.SelectCategory(product_categories, obj_usersBO);
                            ProductSL.DeleteProduct(catModified2);
                            //ListOfProducts.RemoveProduct(catModified2);
                            break;
                        case 4: // Edit a product
                            string catModified3 = ProductsBO.SelectCategory(product_categories, obj_usersBO);
                            Console.WriteLine("Please write the ID of the product you want to modify");
                            string id_pr = Console.ReadLine();
                            ProductSL.EditProduct(catModified3,id_pr);
                            //ListOfProducts.EditAProduct(catModified3);
                            break;
                        case 5: // Create XML of the users
                            UsersBO.CreateXMLUsers(flower_users);
                            break;
                        case 6: //Print the registered users
                            UsersSL.PrintTheUsers();
                            break;
                        case 7:
                            Console.WriteLine("Please write the username of the user you want to search");
                            string query_username = Console.ReadLine();
                            UsersSL.QueryTheUser(query_username);
                            break;
                        case 8:
                            Console.WriteLine("Please write the username of the user you want to delete");
                            string query_delete = Console.ReadLine();
                            UsersSL.DeleteTheUser(query_delete);
                            break;
                        case 0:
                            Console.WriteLine("You selected, log out");
                            quitting_user = true;
                            break;
                        default:
                            Console.WriteLine("That is not a valid option, please select a valid option");
                            break;
                    }



                } while (quitting_user == false);


            }
        }
        */

    }
}
