using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Customer
    {

        private string _name;
        private string _address;
        private int _customerReferenceNumber;



        public Customer()
        {

        }

        public Customer(string _name, string _address)
        {
            this._name = _name;
            this._address = _address;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == "")
                {
                    throw new ArgumentException("Name can't be empty!");
                }
                _name = value;
            }
        }


        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                if (value == "")
                {
                    throw new ArgumentException("Name can't be empty!");
                }
                _address = value;
            }
        }

        public int CustomerReferenceNumber
        {
            get
            {
                return _customerReferenceNumber;
            }
            set
            {
                _customerReferenceNumber = value;
            }

        }

     



    }
}
