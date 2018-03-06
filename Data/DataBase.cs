using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using Business;
using System.Collections.ObjectModel;


namespace Data
{
    public class DataBase
    {

        private SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\dataBase.mdf;Integrated Security = True; Connect Timeout = 30");

        private static DataBase instance;

        private DataBase()
        {
        }

        public static DataBase Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new DataBase();
                }
                return instance;
            }
        }

        //Insert methods for inserting data into the database

        public void insertCustomer(int customer_ref, string customer_name, string customer_address)
        {
            con.Open();

            string sqla = String.Format(@"insert into Customer(customer_ref, customer_name, customer_address) values(@customer_ref, @customer_name, @customer_address)");


            SqlCommand com = new SqlCommand(sqla, con);

            using (com)
            {
                com.Parameters.Add("@customer_ref", SqlDbType.Int, 100).Value = customer_ref;
                com.Parameters.Add("@customer_name", SqlDbType.VarChar, 50).Value = customer_name;
                com.Parameters.Add("@customer_address", SqlDbType.VarChar, 100).Value = customer_address;

            }

            com.ExecuteNonQuery();

            con.Close();
        }

        public void insertBooking(int booking_ref, DateTime arrival_date, DateTime departure_date, int chalet_ID, int customer_ref, int breakfast, int evening_meal)
        {
            con.Open();

            try
            {

                if (chalet_ID > 10)
                {
                    throw new ArgumentException("Can't Book for all Chalets are taken!");
                }

                string sqla = String.Format(@"insert into Booking(booking_ref, arrival_date, departure_date, chalet_ID, customer_ref, breakfast, evening_meal) values(@booking_ref, @arrival_date, @departure_date, @chalet_ID, @customer_ref, @breakfast, @evening_meal)");

                SqlCommand com = new SqlCommand(sqla, con);

                using (com)
                {
                    com.Parameters.AddWithValue("@booking_ref", booking_ref);
                    com.Parameters.AddWithValue("@arrival_date", arrival_date);
                    com.Parameters.AddWithValue("@departure_date", departure_date);
                    com.Parameters.AddWithValue("@chalet_ID", chalet_ID);
                    com.Parameters.AddWithValue("@customer_ref", customer_ref);
                    com.Parameters.AddWithValue("@breakfast", breakfast);
                    com.Parameters.AddWithValue("@evening_meal", evening_meal);

                }

                com.ExecuteNonQuery();

            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }



            con.Close();
        }

        public void insertGuest(int guest_id, int guest_age, string guest_name, int booking_ref, string passport_number)
        {
            con.Open();

            string sql = String.Format(@"insert into Guest(guest_id, guest_age, guest_name, booking_ref, passport_number) values(@guest_id, @guest_age, @guest_name, @booking_ref, @passport_number)");

            SqlCommand com = new SqlCommand(sql, con);

            using (com)
            {
                com.Parameters.Add("@guest_id", SqlDbType.VarChar, 10).Value = guest_id;
                com.Parameters.Add("@guest_age", SqlDbType.Int, 100).Value = guest_age;
                com.Parameters.Add("@guest_name", SqlDbType.VarChar, 50).Value = guest_name;
                com.Parameters.Add("@booking_ref", SqlDbType.Int, 100).Value = booking_ref;
                com.Parameters.Add("@passport_number", SqlDbType.VarChar, 10).Value = passport_number;
            }

            com.ExecuteNonQuery();

            con.Close();

        }

        public void insertCarHire(int booking_ref, DateTime start_date, DateTime end_date, string driver_name)
        {
            con.Open();


            string sqla = String.Format(@"insert into Car(booking_ref, start_date, end_date, driver_name) values(@booking_ref, @start_date, @end_date, @driver_name)");


            SqlCommand com = new SqlCommand(sqla, con);

            using (com)
            {
                com.Parameters.Add("@booking_ref", SqlDbType.Int, 100).Value = booking_ref;
                com.Parameters.Add("@start_date", SqlDbType.Date, 100).Value = start_date;
                com.Parameters.Add("@end_date", SqlDbType.Date, 100).Value = end_date;
                com.Parameters.Add("@driver_name", SqlDbType.VarChar, 50).Value = driver_name;

            }

            com.ExecuteNonQuery();

            con.Close();
        }

        // Update methods that update data in the database

        public void updateCarHire(int booking_ref, DateTime start_date, DateTime end_date, string driver_name)
        {
            

            bool check = checkCarHireForBooking(booking_ref);


            if(check == false)
            {
                
                this.insertCarHire(booking_ref, start_date, end_date, driver_name);
                
            }
            else
            {
                con.Open();
                string sqla = String.Format(@"update Car set start_date = @start_date, end_date = @end_date, driver_name = @driver_name where Car.booking_ref = @booking_ref");


                SqlCommand com = new SqlCommand(sqla, con);

                using (com)
                {
                    com.Parameters.AddWithValue("@booking_ref", booking_ref);
                    com.Parameters.AddWithValue("@start_date", start_date);
                    com.Parameters.AddWithValue("@end_date", end_date);
                    com.Parameters.AddWithValue("@driver_name", driver_name);
                }

                com.ExecuteNonQuery();
                con.Close();
            }

        
        }

        public void updateCustomer(int customer_ref, string customer_name, string customer_address)
        {
            con.Open();

            string sqla = String.Format(@"update Customer set customer_name = @customer_name, customer_address = @customer_address where customer_ref = @customer_ref");


            SqlCommand com = new SqlCommand(sqla, con);

            using (com)
            {
                com.Parameters.AddWithValue("@customer_ref", customer_ref);
                com.Parameters.AddWithValue("@customer_name", customer_name);
                com.Parameters.AddWithValue("@customer_address", customer_address);

            }

            com.ExecuteNonQuery();

            con.Close();
        }





        public void updateBookingDates(int booking_ref, DateTime arrival_date, DateTime departure_date, int chalet_ID)
        {
            con.Open();

            string sqla = String.Format(@"update Booking set arrival_date = @arrival_date, departure_date = @departure_date, chalet_ID = @chalet_ID where booking_ref = @booking_ref");


            SqlCommand com = new SqlCommand(sqla, con);

            using (com)
            {
                com.Parameters.AddWithValue("@arrival_date", arrival_date);
                com.Parameters.AddWithValue("@departure_date", departure_date);
                com.Parameters.AddWithValue("@booking_ref", booking_ref);
                com.Parameters.AddWithValue("@chalet_ID", chalet_ID);

            }

            com.ExecuteNonQuery();

            con.Close();
        }

        public void updateBookingExtras(int booking_ref, int customer_ref, int breakfast, int evening_meal)
        {
            con.Open();

            string sqla = String.Format(@"update Booking set breakfast = @breakfast, evening_meal = @evening_meal where customer_ref = @customer_ref and booking_ref = @booking_ref");


            SqlCommand com = new SqlCommand(sqla, con);

            using (com)
            {

                com.Parameters.AddWithValue("@customer_ref", customer_ref);
                com.Parameters.AddWithValue("@booking_ref", booking_ref);
                com.Parameters.Add("@breakfast", SqlDbType.Bit, 1).Value = breakfast;
                com.Parameters.Add("@evening_meal", SqlDbType.Bit, 1).Value = evening_meal;


            }

            com.ExecuteNonQuery();

            con.Close();
        }

        //Delete methods that delete data in the database

        public void deleteCarHire(int booking_ref)
        {
            bool check = checkCarHireForBooking(booking_ref);


            if (check == false)
            {

                return;

            }
            else
            {
                con.Open();
                string sqla = String.Format(@"delete from Car where Car.booking_ref = @booking_ref");


                SqlCommand com = new SqlCommand(sqla, con);

                using (com)
                {
                    com.Parameters.AddWithValue("@booking_ref", booking_ref);
                }

                com.ExecuteNonQuery();
                con.Close();
            }

        }

        public void deleteBooking(int booking_ref)
        {

            con.Open();

            string sql = String.Format(@"delete from Booking where Booking.booking_ref = @booking_ref");

            SqlCommand com = new SqlCommand(sql, con);

            using (com)
            {
                com.Parameters.AddWithValue("@booking_ref", booking_ref);
            }

            com.ExecuteNonQuery();
            con.Close();
        }

        public void deleteCustomer(int customer_ref)
        {
            con.Open();

            string sql = String.Format(@"delete from Customer where Customer.customer_ref = @customer_ref");

            SqlCommand com = new SqlCommand(sql, con);

            using (com)
            {
             
                com.Parameters.AddWithValue("@customer_ref", customer_ref);
            }

            com.ExecuteNonQuery();
            con.Close();
        }

        public void deleteGuests(int booking_ref)
        {
            con.Open();

            string sql = String.Format(@"delete from Guest where Guest.booking_ref = @booking_ref");

            SqlCommand com = new SqlCommand(sql, con);

            using (com)
            {
                com.Parameters.AddWithValue("@booking_ref", booking_ref);


            }

            com.ExecuteNonQuery();

            con.Close();
        }





        // Methods that get data from the database

        public List<Customer> getAllCustomers()
        {
            con.Open();           

            string sql = String.Format(@"select * from Customer");

            SqlCommand com = new SqlCommand(sql, con);

            
            List<Customer> customers = new List<Customer>();

            SqlDataReader sdr = com.ExecuteReader();

            while(sdr.Read())
            {
                try
                {
                    Customer c = new Customer();

                    c.Name = (string)sdr["customer_name"];
                    c.Address = (string)sdr["customer_address"];
                    c.CustomerReferenceNumber = (int)sdr["customer_ref"];


                    customers.Add(c);
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                }
            }

          
            con.Close();
            return customers;
        
        }

        public Customer getCertainCustomer(int customer_ref)
        {
            con.Open();

            string sql = String.Format(@"select * from Customer where customer_ref = '{0}'", customer_ref);

            SqlCommand com = new SqlCommand(sql, con);

            SqlDataReader sdr = com.ExecuteReader();

            Customer c = new Customer();

            while (sdr.Read())
            {
                try
                {
                    
                    c.Name = (string)sdr["customer_name"];
                    c.Address = (string)sdr["customer_address"];
                    c.CustomerReferenceNumber = (int)sdr["customer_ref"];


                    
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                }
            }


            con.Close();
            return c;
        }


        public List<Booking> getBooking(int customer_ref)
        {
            con.Open();

            string sql = String.Format(@"select * from Booking 
                                        where Booking.customer_ref = @customer_ref");

            SqlCommand com = new SqlCommand(sql, con);

            using (com)
            {
                com.Parameters.AddWithValue("@customer_ref", customer_ref);
            }

            List<Booking> bookings = new List<Booking>();

            SqlDataReader sdr = com.ExecuteReader();

            while (sdr.Read())
            {
                try
                {
                    Booking booking = new Booking();

                    booking.BookingReferenceNumber = (int)sdr["booking_ref"];
                    booking.ArrivalDate = (DateTime)sdr["arrival_date"];
                    booking.DepartureDate = (DateTime)sdr["departure_date"];
                    booking.ChaletID = (int)sdr["chalet_ID"];
                    booking.Breakfast = (bool)sdr["breakfast"];
                    booking.EveningMeal = (bool)sdr["evening_meal"];
                    bookings.Add(booking);
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                }
            }
            con.Close();
            return bookings;
        }

        public List<Guest> getGuests(int booking_ref)
        {
            con.Open();

            string sql = String.Format(@"select * from Guest 
                                        where Guest.booking_ref = '{0}'", booking_ref);

            SqlCommand com = new SqlCommand(sql, con);

            List<Guest> guests = new List<Guest>();

            SqlDataReader sdr = com.ExecuteReader();

            while (sdr.Read())
            {
                try
                {
                    Guest guest = new Guest();
                    guest.Name = (string)sdr["guest_name"];
                    guest.Age = (int)sdr["guest_age"];
                    guest.PassportNo = (string)sdr["passport_number"];
                    guests.Add(guest);
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                }
            }
            con.Close();
            return guests;
        }

        public CarHire getCarHire(int booking_ref)
        {
            bool check = checkCarHireForBooking(booking_ref);

            if (check == false)
            {
                return null;
            }
            else
            {
                con.Open();
                string sql = String.Format(@"select * from Car
                                            where Car.booking_ref = '{0}'", booking_ref);

                SqlCommand com = new SqlCommand(sql, con);

                CarHire car = new CarHire();

                SqlDataReader sdr = com.ExecuteReader();

                while (sdr.Read())
                {
                    try
                    {
                        car.DriverName = (string)sdr["driver_name"];
                        car.StartDate = (DateTime)sdr["start_date"];
                        car.EndDate = (DateTime)sdr["end_date"];
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine(ee.Message);
                    }
                }
                con.Close();
                return car;
            }
        }

        // Checks if a certain booking has a car hire or not
        public bool checkCarHireForBooking(int booking_ref)
        {
            con.Open();

            string sql = String.Format(@"select count(*) from Car where Car.booking_ref = @booking_ref");

            SqlCommand comCheck = new SqlCommand(sql, con);

            using (comCheck)
            {
                comCheck.Parameters.AddWithValue("@booking_ref", booking_ref);

            }

            int ifCarHireExists = (int)comCheck.ExecuteScalar();

            con.Close();

            if (ifCarHireExists < 1)
            {
                con.Close();
                return false;
            }
            else
            {
                con.Close();
                return true;
            }           
        }

        public int customerReferenceNumber()
        {
            con.Open();

            string sqla = String.Format(@"select customer_ref from Customer order by customer_ref desc");

            SqlCommand com = new SqlCommand(sqla, con);

            int customerReferenceNumber = Convert.ToInt32(com.ExecuteScalar());

            Console.WriteLine(customerReferenceNumber + "data");

            con.Close();
          
            return customerReferenceNumber;
        }

        public int bookingReferenceNumber()
        {
            con.Open();

            string sqla = String.Format(@"select booking_ref from Booking order by booking_ref desc");

            SqlCommand com = new SqlCommand(sqla, con);

            int bookingReferenceNumber = Convert.ToInt32(com.ExecuteScalar());

            con.Close();
           
            return bookingReferenceNumber;
        }

        public int guestID()
        {
            con.Open();

            string sqla = String.Format(@"select guest_id from Guest order by guest_id desc");

            SqlCommand com = new SqlCommand(sqla, con);

            int guestID = Convert.ToInt32(com.ExecuteScalar());

            con.Close();

            return guestID;
        }

        public int setChaletForBooking(DateTime arrival, DateTime departure)
        {

            List<int> chalets = new List<int>();

            con.Open();

            string sql = String.Format(@"select * from Booking");

            SqlCommand com = new SqlCommand(sql, con);

            SqlDataReader sdr = com.ExecuteReader();

            while (sdr.Read())
            {

                try
                {

                    DateTime BookingArrivalDate = (DateTime)sdr["arrival_date"];

                    DateTime BookingDepartureDate = (DateTime)sdr["departure_date"];

                    int ChaletID = (int)sdr["chalet_ID"];

                    if ((arrival.Date >= BookingArrivalDate.Date && arrival.Date <= BookingDepartureDate.Date) || (departure.Date >= BookingArrivalDate.Date && departure.Date <= BookingDepartureDate.Date))
                    {
                        chalets.Add(ChaletID);
                    }

                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                }


            }

            int newChaletID = Enumerable.Range(1, int.MaxValue).Except(chalets).FirstOrDefault();

            con.Close();
            return newChaletID;        
        }
    }
}
