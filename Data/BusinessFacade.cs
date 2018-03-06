using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;

namespace Data
{
    public class BusinessFacade
    {

        private List<Guest> guests = new List<Guest>();

        private static BusinessFacade instance;

        private static DataBase data;

        private BusinessFacade()
        {
        }

        public static BusinessFacade Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new BusinessFacade();
                    data = DataBase.Instance;
                }
                return instance;
            }
        }

        // Methods That add relevant objects into each specific list.

        public void addGuest(Guest guest)
        {
            guests.Add(guest);
        }

        // Methods that find relevant objects in each specific list.

        public Guest findGuest(string passportNumber)
        {
            foreach (Guest c in guests)
            {
                if (passportNumber == c.PassportNo)
                {
                    return c;
                }
            }

            return null;
        }

        // Methods that return each list.

        public List<Guest> Guests
        {
            get
            {
                return guests;
            }
            set
            {
                guests = value;
            }
        }

        // Methods that set reference and ID numbers to relevant objects

        public int setCustomerReferenceNumber()
        {
            int customerRef = data.customerReferenceNumber();

            customerRef++;

            int refNumber = customerRef;

            return refNumber;
        }


        public int setBookingReferenceNumber()
        {
            int current = data.bookingReferenceNumber();

            current++;

            int refNumber = current;

            return refNumber;
        }


        public int setGuestID()
        {
            int current = data.guestID();

            current++;

            int guestID = current;

            return guestID;
        }


        // Methods that delete relevant objects in each specific list.


        public void removeGuest(string passportNumber)
        {
            Guest g = findGuest(passportNumber);

            if (g != null)
            {
                guests.Remove(g);
            }

        }

        public void SaveCustomerAndBooking(Customer customer, List<Guest> guests, Booking booking, CarHire car, int breakfast, int eveningMeal, bool addBooking)
        {
            
            booking.BookingReferenceNumber = this.setBookingReferenceNumber();

            booking.ChaletID = data.setChaletForBooking(booking.ArrivalDate, booking.DepartureDate);


            if (addBooking == false)
            {
                customer.CustomerReferenceNumber = this.setCustomerReferenceNumber();
                data.insertCustomer(customer.CustomerReferenceNumber, customer.Name, customer.Address);
            }          

            data.insertBooking(booking.BookingReferenceNumber, booking.ArrivalDate, booking.DepartureDate, booking.ChaletID, customer.CustomerReferenceNumber, breakfast, eveningMeal);


            if (car != null)
            {
                data.insertCarHire(booking.BookingReferenceNumber, car.StartDate, car.EndDate, car.DriverName);
            }

            for (int i = 0; i < guests.Count; i++)
            {
                int guest_id = this.setGuestID();

                data.insertGuest(guest_id, guests[i].Age, guests[i].Name, booking.BookingReferenceNumber, guests[i].PassportNo);
            }

        }

        public void UpdateBooking(Customer customer, List<Guest> guests, Booking booking, CarHire car, int breakfast, int eveningMeal, bool checkCarHire)
        {
            data.updateBookingExtras(booking.BookingReferenceNumber, customer.CustomerReferenceNumber, breakfast, eveningMeal);

            if(checkCarHire == false)
            {
                data.deleteCarHire(booking.BookingReferenceNumber);
            }
            
            if(car != null && checkCarHire == true)
            {
                data.updateCarHire(booking.BookingReferenceNumber, car.StartDate, car.EndDate, car.DriverName);
            }

            data.deleteGuests(booking.BookingReferenceNumber);

            for(int i = 0; i < guests.Count; i++)
            {
                int guest_id = this.setGuestID();

                data.insertGuest(guest_id, guests[i].Age, guests[i].Name, booking.BookingReferenceNumber, guests[i].PassportNo);

            }
        }

        public void deleteBooking(Booking b)
        {
            CarHire car = data.getCarHire(b.BookingReferenceNumber);

            if(car != null)
            {
                data.deleteCarHire(b.BookingReferenceNumber);
            }

            data.deleteGuests(b.BookingReferenceNumber);

            data.deleteBooking(b.BookingReferenceNumber);

        }

        public void deleteCustomer(Customer c)
        {
            data.deleteCustomer(c.CustomerReferenceNumber);
        }

        public void updateCustomer(Customer c)
        {
            data.updateCustomer(c.CustomerReferenceNumber, c.Name, c.Address);
        }

        public List<Customer> getCustomersFromDataBase
        {
            get
            {
                return data.getAllCustomers();
            }
                
        }

        public Customer getCertainCustomerFromDataBase(int customer_ref)
        {
            return data.getCertainCustomer(customer_ref);
        }

        public List<Booking> getBookingsFromDataBase(int customer_ref)
        {

            return data.getBooking(customer_ref);
        }

        public CarHire getCarHireForBooking(Booking b)
        {
            return data.getCarHire(b.BookingReferenceNumber);
        }

        public void updateBookingDates(Booking b)
        {
            data.updateBookingDates(b.BookingReferenceNumber, b.ArrivalDate, b.DepartureDate, b.ChaletID);
        }

        public void deleteCarHire(Booking b)
        {
            data.deleteCarHire(b.BookingReferenceNumber);
        }

        public int setChaletForBooking(DateTime arrival, DateTime departure)
        {
            return data.setChaletForBooking(arrival, departure);
        }

        public List<Guest> getGuestsForBooking(Booking b)
        {
            return data.getGuests(b.BookingReferenceNumber);
        }

    }
}
