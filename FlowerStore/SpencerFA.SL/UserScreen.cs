/*
 COMPANY: ASPIRE SYSTEMS
 TITLE: ONLINE SHOPPING APPLICATION -- FLOWER SHOP 
 DEVELOPER: ERICK ALAN BARLANDAS QUINTANA
 CREATION DATE: December 3rd, 2021.  
 REVIEWED BY: DHINESH DEVAMANI AND AKSHAYA RAJAGOPAL.
 LAST MODIFICATION DATE: JANUARY 3rd 2022.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SpencerFA.BO;
using SpencerFA.SL;


namespace SpencerFA.SL
{
    public class UserScreen

    {
        public void SpencerShop()
        {
            bool entering = false;
            Tuple<int, int> correct_user;
            CreditCard obj_creditCard = new CreditCard();
            Users obj_users;
            ProductSL obj_productSL = new ProductSL();
            TicketSL obj_ticketSL = new TicketSL();
            List<Users> flower_users = UsersSL.GetUsers();
            UsersBO obj_usersBO = new UsersBO();
            List<Tuple<int,string>> product_categories = obj_productSL.ProductCategories();
            List<Products> flowers = obj_productSL.RecoverProducts("flowers");
            List<Products> gifts = obj_productSL.RecoverProducts("gifts");
            List<Products> christmas = obj_productSL.RecoverProducts("christmas");
            Dictionary<string, List<Products>> allProducts = new Dictionary<string, List<Products>>() {
                { "flowers", flowers }, { "gifts", gifts }, { "christmas", christmas } };
            List<Tuple<Products, int>> final_products = new List<Tuple<Products, int>>();

            // Enter user to the shop
            do
            {
                
                Messages.InitialMessages(); // Initial messages of the flower shop
                int user = obj_usersBO.ValidateInteger();
                Console.Clear();
                switch (user)
                
                {
                    case 0:
                        correct_user = obj_usersBO.CheckUserName(flower_users);
                        if (correct_user.Item1 == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Welcome Super user");
                            Console.WriteLine(" ");
                            //entering = true;
                            //main_menu = 2;
                            bool quitting_user = false;
                            do
                            {
                                Messages.SuperUserMenu();
                                int superuser = obj_usersBO.ValidateInteger();
                                switch (superuser)
                                {
                                    case 1: //Recover XML of the products
                                        Console.Clear();
                                        ProductsBO.RecoverXMLs(product_categories, allProducts);
                                        break;
                                    case 2: //Add a product
                                        Console.Clear();
                                        int catModified = ProductsBO.SelectCategory(product_categories, obj_usersBO);
                                        /*
                                        ListOfProducts.AddAProduct(catModified);
                                        */
                                        obj_productSL.AddQuery(catModified);
                                        break;
                                    case 3: //Remove a product
                                        bool deleteCat = false;
                                        do
                                        {
                                            
                                            Console.WriteLine("To delete a product, enter 1");
                                            Console.WriteLine("To go back to main menu, enter 0");
                                            int delCat = obj_usersBO.ValidateInteger();
                                            switch (delCat)
                                            {
                                                case 0:
                                                    Console.Clear();
                                                    deleteCat = true;
                                                    break;
                                                case 1:
                                                    Console.Clear();
                                                    int catModified2 = ProductsBO.SelectCategory(product_categories, obj_usersBO);
                                                    obj_productSL.DeleteQuery(catModified2);
                                                    break;
                                                default:
                                                    Console.WriteLine("That is not a valid option, please select a valid option");
                                                    break;
                                            }
                                            
                                            //ListOfProducts.RemoveProduct(catModified2); }
                                        } while (deleteCat == false);
                                        break;

                                    case 4: // Edit a product
                                        bool editCat = false;
                                        do
                                        {

                                            Console.WriteLine("To edit a product, enter 1");
                                            Console.WriteLine("To go back to main menu, enter 0");
                                            int ediCat = obj_usersBO.ValidateInteger();
                                            switch (ediCat)
                                            {
                                                case 0:
                                                    Console.Clear();
                                                    editCat = true;
                                                    break;
                                                case 1:
                                                    Console.Clear();
                                                    int catModified3 = ProductsBO.SelectCategory(product_categories, obj_usersBO);
                                                    obj_productSL.EditQuery(catModified3);
                                                    break;
                                                default:
                                                    Console.WriteLine("That is not a valid option, please select a valid option");
                                                    break;
                                            }

                                        } while (editCat == false);
                                        break;
                                        //ListOfProducts.EditAProduct(catModified3);

                                    case 5: // Create XML of the users
                                        Console.Clear();
                                        UsersBO.CreateXMLUsers(flower_users);
                                        break;
                                    case 6: //Print the registered users
                                        Console.Clear();
                                        UsersSL.PrintTheUsers();
                                        break;
                                    case 7:
                                        Console.Clear();
                                        Console.WriteLine("Please write the username of the user you want to search");
                                        string query_username = Console.ReadLine();
                                        UsersSL.QueryTheUser(query_username);
                                        break;
                                    case 8:
                                        Console.Clear();
                                        Console.WriteLine("Please write the username of the user you want to delete");
                                        string query_delete = Console.ReadLine();
                                        UsersSL.DeleteTheUser(query_delete);
                                       break;
                                    case 0:
                                        Console.Clear();
                                        Console.WriteLine("You selected, log out");
                                        quitting_user = true;
                                        break;
                                    default:
                                        Console.Clear();
                                        Console.WriteLine("That is not a valid option, please select a valid option");
                                        
                                        break;
                                }



                            } while (quitting_user == false);
                            //break;
                        }
                        else if (correct_user.Item1 == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Correct Login");
                            obj_users = flower_users[correct_user.Item2]; //Here  I recover the username of the user
                            Console.WriteLine(" ");
                            bool quit = false;
                            do
                            {

                                Messages.SecondMessages(obj_users);
                                int des_customer = obj_usersBO.ValidateInteger();
                                switch (des_customer)
                                {
                                    case 1: // If the user wants to check the flowers
                                        bool option1 = false;
                                        Console.Clear();
                                        Console.WriteLine("Welcome to the flowers section");
                                        do
                                        {
                                            Messages.ThirdMessages();
                                            int des_flower = obj_usersBO.ValidateInteger();
                                            switch (des_flower)
                                            {
                                                case 1:
                                                    Console.Clear();
                                                    ProductsBO.PrintAllProducts(flowers);
                                                    break;
                                                case 2:
                                                    Console.Clear();
                                                    ProductsBO.SearchForAProdcut(flowers);
                                                    break;
                                                case 3:
                                                    Console.Clear();
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
                                        Console.Clear();
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
                                        Console.Clear();
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
                                        Console.Clear();
                                        bool cart = false;
                                        do
                                        {
                                            SummarizeCode.PrintingFinalProducts(final_products);
                                            Messages.CartMessages(ProductsBO.FinalPrice(final_products));
                                            int des = obj_usersBO.ValidateInteger();
                                            switch (des)
                                            {
                                                case 1:
                                                    // Credit Card Section *************************
                                                    bool CreditCard = false;
                                                    do
                                                    {
                                                        obj_creditCard.SelectPaymentCard();
                                                        int decCC = obj_usersBO.ValidateInteger();
                                                        
                                                        
                                                        switch (decCC)
                                                        {
                                                            case 1:
                                                                Console.Clear();
                                                                List<string> CCVals = obj_creditCard.NewCreditCard(obj_users);
                                                                Console.WriteLine("Do you want to save the info of your credit card?(y/yes)");
                                                                string desSave = Console.ReadLine();
                                                                if(desSave == "y" || desSave == "yes")
                                                                {
                                                                    obj_ticketSL.SaveCard(CCVals, obj_users.Username);   //AQUI SE VAN A GUARDAR LOS DATOS
                                                                }
                                                                string addressCC = obj_creditCard.SelectAddress(obj_users);
                                                                string ticket = SummarizeCode.CreatingTicket(); //Creates a random ticket
                                                     
                                                                SummarizeCode.CreatingNewDelivery(ticket, DateTime.Now.AddDays(20), addressCC);//Writes the delivery date and the ticket in a .txt file
                                                        
                                                                obj_ticketSL.CreateTicket(ticket, DateTime.Now.AddDays(20), obj_users.Username,addressCC);
                                                             
                                                                obj_ticketSL.CreatePurchase(final_products, ticket);
                                                                
                                                                ProductsBO.CreatingTicketXML(obj_users, final_products, ticket,addressCC); // Writes a XML file of the products, with the ticket as the key.
                                                                final_products.Clear();
                                                                CreditCard = true;
                                                                break;
                                                            case 2:
                                                                string card = obj_ticketSL.RecoverCreditCard(obj_users.Username);
                                                                //Select the creditCard
                                                                Console.Clear();
                                                                Console.WriteLine("You selected the card:" + card);
                                                                addressCC = obj_creditCard.SelectAddress(obj_users);
                                                                ticket = SummarizeCode.CreatingTicket();
                                                                SummarizeCode.CreatingNewDelivery(ticket, DateTime.Now.AddDays(20), addressCC);//Writes the delivery date and the ticket in a .txt file

                                                                obj_ticketSL.CreateTicket(ticket, DateTime.Now.AddDays(20), obj_users.Username, addressCC);
                                                                
                                                                obj_ticketSL.CreatePurchase(final_products, ticket);
                                                                
                                                                ProductsBO.CreatingTicketXML(obj_users, final_products, ticket,addressCC); // Writes a XML file of the products, with the ticket as the key.
                                                                final_products.Clear();
                                                                CreditCard = true;
                                                                break;
                                                            default:
                                                                Console.WriteLine("That is not a valid option, please try again");
                                                                Console.Clear();
                                                                break;

                                                        }


                                                    } while (CreditCard == false);
                                                    // *********************************************
                                                    
                                                    break;
                                                case 2:
                                                    Console.WriteLine("Please indicate the name of the product you want to edit");
                                                    string name_pr = Console.ReadLine();
                                                    Console.WriteLine("Now indicate the new quantity of the product");
                                                    int new_quantity = Convert.ToInt32(Console.ReadLine());
                                                   
                                                    for (int i = 0; i < final_products.Count; i++)
                                                    {
                                                        if (Regex.IsMatch(final_products[i].Item1.Name.ToLower(), name_pr.ToLower()) || final_products[i].Item1.Name.ToLower() == name_pr.ToLower())
                                                        {
                                                            Tuple<Products, int> tuple0 = new Tuple<Products,
                                                                int>(final_products[i].Item1, new_quantity);
                                                            final_products.Add(tuple0);
                                                            final_products.RemoveAt(i);
                                                            
                                                        }
                                                    }
                                                    
                                                    Console.Clear();
                                                    for (int i = 0; i < final_products.Count; i++)
                                                    {
                                                        if(final_products[i].Item2 == 0)
                                                        {
                                                            final_products.RemoveAt(i);
                                                        }
                                                    }
                                                    break;
                                                case 0:
                                                    Console.Clear();
                                                    cart = SummarizeCode.LeavingMenu();
                                                    break;
                                                default:
                                                    Messages.NotValidOption();
                                                    
                                                    break;
                                            }

                                        } while (cart == false);

                                        break;
                                    case 5: // Deliveries information
                                        Console.Clear();
                                        bool deliveries = false;
                                        do
                                        {
                                            Messages.DeliveriesMessages();
                                            int des = obj_usersBO.ValidateInteger();
                                            switch (des)
                                            {
                                                case 1:
                                                    obj_ticketSL.RecoverProducts(obj_users.Username);
                                                    //SummarizeCode.PrintingTicketInfo2(obj_users); // Checking the delivery date and the products of the ticket. 
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
                            //main_menu = 1;
                            //entering = true;
                            break;
                        }
                        break;
                    case 1:
                        Console.Clear();
                        UsersSL.CreatingNewUser(flower_users);
                        flower_users = UsersSL.GetUsers();
                        Console.WriteLine("You have been registered as a new user");   
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("You selected Log Out, see you next time");
                        entering = true;
                        break;
                    default:
                        Console.Clear();
                        Messages.NotValidOption();
                        break;
                
                }
            } while (entering == false);


        }



    }

}
