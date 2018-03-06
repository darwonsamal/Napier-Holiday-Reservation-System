using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Booking
    {

        private DateTime _arrivalDate;
        private DateTime _departureDate;
        private int _bookingReferenceNumber;
        private int _chaletID;
        private bool _breakfast;
        private bool _eveningMeal;


        public Booking()
        {

        }

        
        public bool Breakfast
        {
            get
            {
                return _breakfast;
            }
            set
            {
                _breakfast = value;
            }
        }

        public bool EveningMeal
        {
            get
            {
                return _eveningMeal;
            }
            set
            {
                _eveningMeal = value;
            }
        }

        public DateTime ArrivalDate
        {
            get
            {
                return _arrivalDate;
            }
            set
            {
                _arrivalDate = value;
            }
        }

        public DateTime DepartureDate
        {
            get
            {
                return _departureDate;
            }
            set
            {
                _departureDate = value;
            }
        }

        public int ChaletID
        {
            get
            {
                return _chaletID;
            }
            set
            {
                if (value > 10 || value < 1)
                {
                    throw new ArgumentException("Chalet ID must be between 1 and 10!");
                }
                _chaletID = value;
            }
        }

  

        public int BookingReferenceNumber
        {
            get
            {
                return _bookingReferenceNumber;
            }
            set
            {
                _bookingReferenceNumber = value;
            }
        }


    }
}
