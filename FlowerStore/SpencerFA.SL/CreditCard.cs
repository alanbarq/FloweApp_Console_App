using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpencerFA.BO;

namespace SpencerFA.SL
{
    public class CreditCard
    {

        public List<string> NewCreditCard(Users users)
        {
            
            List<string> cardData = new List<string>();
            UsersBO obj_usersBO = new UsersBO(); 
            string name;
            string address; 
            Console.WriteLine("Are you the owner of the card?(y)");
            string desicion = Console.ReadLine();
            if (desicion == "y")
            {
                name = users.Name + " " + users.Lastname;
            }
            else
            {
                Console.WriteLine("Please introduce the card name's owner");
                string first_name = obj_usersBO.ValidateName();
                Console.WriteLine("Please introduce the card lastname's owner");
                string last_name = obj_usersBO.ValidateLastName();

                name = first_name + " " + last_name;
            }
            cardData.Add(name);

            string digits = ValidateSixteenNumbers();
            cardData.Add(digits);
            Console.WriteLine("Please enter the month of the Expiration date of the card");
            int montExp = ValidateMonth();
            cardData.Add(Convert.ToString(montExp));
            Console.WriteLine("Please enter the year of the Expiration date of the card");
            int yearExp = ValidateYear();
            cardData.Add(Convert.ToString(yearExp));

            Console.WriteLine("Please introduce the (CVV) of the card");
            string CVV = ValidateCVVNumbers();
            cardData.Add(CVV);
            Console.WriteLine("Is the credit card's address the same one as your address?(y)");
            string desicionAddress = Console.ReadLine();
            if (desicionAddress == "y")
            {
                address = users.Address;
            }
            else
            {
                address = obj_usersBO.ValidateAddress();
            }
            cardData.Add(address);
            return cardData;


        }




        public void SelectPaymentCard()
        {
            Console.Clear();
            Console.WriteLine("If you want to add a new card, please enter 1");
            Console.WriteLine("If you want to pay with a saved card, please enter 2");
            Console.WriteLine("If you want to return, enter 0");

        }

        public string ValidateSixteenNumbers()
        {
            
            try
            {
                Console.WriteLine("Please introduce the sixteen number of the card");
                string sixteen = Console.ReadLine();
                if (sixteen.Length == 16 && sixteen.All(Char.IsDigit))
                {
                    return sixteen;
                }
                else
                {
                    Console.WriteLine("Please check again, remember to use only digits and 16 numbers");
                    return ValidateSixteenNumbers();
                }
            }
            catch (CreditCardException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidateSixteenNumbers();
            }
        }

        public string ValidateCVVNumbers()
        {

            try
            {

                string sixteen = Console.ReadLine();
                if (sixteen.Length == 3 && sixteen.All(Char.IsDigit))
                {
                    return sixteen;
                }
                else
                {
                    Console.WriteLine("Please check again, remember to use only digits and 3 numbers");
                    return ValidateCVVNumbers();
                }
            }
            catch (CreditCardException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidateCVVNumbers();
            }
        }

        public int ValidateMonth()
        {
            UsersBO obj_usersBO = new UsersBO();
            int month = obj_usersBO.ValidateInteger();
            if (month < 0 || month > 12)
            {
                Console.WriteLine("That is not a valid month, please try again");
                return ValidateMonth();
            }
            return month;
        }

        public int ValidateYear()
        {
            UsersBO obj_usersBO = new UsersBO();
            int year = obj_usersBO.ValidateInteger();
            if (year < 21)
            {
                Console.WriteLine("That is not a valid year, please try again, remember only using two digits");
                return ValidateYear();
            }
            return year;
        }
        public string SelectAddress(Users user)
        {
            Console.WriteLine("Is the shipping address the same one as yours?(y)");
            string decision = Console.ReadLine();
            if (decision == "y")
            {
                return user.Address;
            }
            else
            {
                Console.WriteLine("Please introduce the new address");
                string address = Console.ReadLine();
                return address;
            }
        }
    }
    
}
