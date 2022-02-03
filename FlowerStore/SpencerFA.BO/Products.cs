using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpencerFA.BO
{
    public abstract class Products
    {
        // fields
        string iD;
        string name;
        float price;
        string description;



        protected Products(string iD, string name, float price, string description)
        {
            this.iD = iD;
            this.name = name;
            this.price = price;
            this.description = description;
        }

        public string ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public float Price { get => price; set => price = value; }
        public string Description { get => description; set => description = value; }

        public override string ToString()
        {
            return $"{ID,-6}{price,-6} {name,-20} {description,-25}";
        }
    }

    public class Flowers : Products
    {
        //string category = "flowers";
        public Flowers(string ID, string name, float price, string description)
            : base(ID,name,price,description)
        {

        }
        

    }

    public class Gifts : Products
    {
        //string category = "gifts";
        public Gifts(string ID, string name, float price, string description)
            : base(ID, name, price, description)
        {

        }
    }
    public class Christmas : Products
    {
        //string category = "christmas";
        public Christmas(string ID, string name, float price, string description)
            : base(ID, name, price, description) 
        {

        }
    }

    

}
