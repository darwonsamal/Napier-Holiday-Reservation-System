using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Guest
    {

        private string _name;
        private string _passportNumber;
        private int _age;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {   
                if(value == "")
                {
                    throw new ArgumentException("Name can't be empty");
                }
                _name = value;
            }
        }

        public string PassportNo
        {
            get
            {
                return _passportNumber;
            }
            set
            {
                if (value.Length > 10 || value == "")
                {
                    throw new ArgumentException("Passport number can't be more than ten digits");
                }
                _passportNumber = value;
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 0 || value > 101)
                {
                    throw new ArgumentException("Age must be between 101 and 0");
                }
                _age = value;
            }
        }
    }
}