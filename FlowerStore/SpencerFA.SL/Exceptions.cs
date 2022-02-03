using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpencerFA.SL
{
    internal class MailException : Exception

    {
        public MailException(): base("Email is invalid") {

        }
    } //Mail exception method
    internal class NameException : Exception
    {
        public NameException() : base("The name/lastname is invalid, please write only alphabetical characters, and" +
            "length of at least three characters.")
        {

        }
    } //Name exception method
    internal class UsernameException : Exception
    {
        public UsernameException() : base("The username is already taken, or it was not written properly")
        {

        }
    } //Username exception method
    internal class ExceptionPassword : Exception
    {
        public ExceptionPassword(): base("That is not a valid password,please introduce a password with" +
            "more than 5 character and a special character and a number")
        {

        }
    }// Password exception method
    internal class AddressException: Exception
    {
        public AddressException(): base("That is not a valid address, remember to have a length of at least 7 characters")
        {

        }
    } //Address exception method
    public class TelephoneException: Exception
    {
        public TelephoneException() : base("That is not a valid telephone, please write a 10 numbers phone number")
        {

        }
    } // Telephone exception method
    internal class IntegerException: Exception
    {
        public IntegerException() : base("Please remember only to use integers")
        {

        }
    } //Integer exception method
    internal class CreditCardException : Exception

    {
        public CreditCardException() : base("There was a problem, pleasy try again")
        {

        }
    } //Mail exception method
}
