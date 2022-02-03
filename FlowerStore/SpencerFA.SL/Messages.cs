using SpencerFA.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpencerFA.SL
{
    public static class Messages
    {
        public static void InitialMessages()
        {
            Console.WriteLine("Welcome to Spencer Flower Shop");
            Console.WriteLine("If you are a registered user, please enter 0");
            Console.WriteLine("If you are a new user, please enter 1");
            Console.WriteLine("If you want to exit, please enter 2");
            Console.WriteLine("");
        }

        public static void SecondMessages(Users obj)
        {
            Console.WriteLine("Welcome to Spencer Flower Shop {0}",obj.Username);
            Console.WriteLine("To see the flowers menu, enter 1");
            Console.WriteLine("To see the gifts menu, enter 2");
            Console.WriteLine("To see our christmas specials, enter 3");
            Console.WriteLine("To check your cart, enter 4");
            Console.WriteLine("To check your deliveries status, enter 5");
            Console.WriteLine("If you want to edit your information, enter 6");
            Console.WriteLine("To log out, enter 0");
            Console.WriteLine("");
        }


        public static void ThirdMessages() 
        {
            Console.WriteLine("If you want to see all our products, enter 1");
            Console.WriteLine("If you want to search for a product, enter 2");
            Console.WriteLine("To buy a product, enter 3");
            Console.WriteLine("If you want to return to main menu, enter 0");
            
        }
        public static void CartMessages(double total)
        {
            Console.WriteLine("Your total will be of {0}", total);
            Console.WriteLine(" ");
            Console.WriteLine("If you are agree with the purchase, enter 1");
            Console.WriteLine("If you want to edit a product, enter 2");
            Console.WriteLine("If you want to return to previous menu, enter 0");
        }

        public static void NotValidOption()
        {
            Console.WriteLine("That is not a valid option, please select a valid option");
            Console.WriteLine(" ");
        }

        public static void DeliveriesMessages()
        {
            Console.WriteLine("If you want to check your deliveries, enter 1");
            Console.WriteLine("If you want to return to previous menu, enter 0");
        }
        public static void SuperUserMenu()
        {
            Console.WriteLine("What do you want to do today?");
            Console.WriteLine("If you want to recover the XML files of the products, enter 1");
            Console.WriteLine("If you want to add a product to a category, enter 2");
            Console.WriteLine("If you want to remove a product of a category, enter 3");
            Console.WriteLine("If you want to edit a product of a category, enter 4");
            Console.WriteLine("If you want to recover the XML file of the users, enter 5");
            Console.WriteLine("If you want to print the registered users, enter 6");
            Console.WriteLine("If you want to check for a user by username, enter 7");
            Console.WriteLine("To delete a user by username, enter 8");
            Console.WriteLine("If you want to quit, enter 0");
        }
    }
}
