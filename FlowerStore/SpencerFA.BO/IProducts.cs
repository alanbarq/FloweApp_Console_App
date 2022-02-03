using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpencerFA.BO
{
    public interface IAccess
    {
        void PrintQuery(int category);
        void EditQuery(int category);
        void AddQuery(int category);
        void DeleteQuery(int category);
    }
}
