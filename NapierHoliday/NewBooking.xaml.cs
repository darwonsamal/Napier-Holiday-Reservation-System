using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections;
using Business;
using Data;
using System.ComponentModel;

namespace NapierHoliday
{
    /// <summary>
    /// Interaction logic for NewCustomer.xaml
    /// </summary>
    public partial class NewBooking : Window
    {

        private Booking booking;

        private Customer customer;

        private ExistingCustomer win;

        private BusinessFacade facade;



        public NewBooking(Booking b, ExistingCustomer win)
        {
            this.booking = b;

            this.win = win;  

            InitializeComponent();

            this.facade = win.accessFacade();

            this.calendarBookingDate.BlackoutDates.AddDatesInPast();

            this.btnNext.IsEnabled = false;

            this.btnNext.Visibility = Visibility.Hidden;

            this.btnBack.Visibility = Visibility.Hidden;

            this.btnUpdateBooking.IsEnabled = false;  
            
        }

        public NewBooking()
        {
            this.facade = BusinessFacade.Instance;

            InitializeComponent();
            this.calendarBookingDate.BlackoutDates.AddDatesInPast();
            this.btnNext.IsEnabled = false;
            this.btnUpdateBooking.Visibility = Visibility.Hidden;
        }

        public NewBooking(Customer c)
        {
            this.customer = c;
            this.facade = BusinessFacade.Instance;

            InitializeComponent();
          
            this.calendarBookingDate.BlackoutDates.AddDatesInPast();
            this.btnNext.IsEnabled = false;
            this.btnUpdateBooking.Visibility = Visibility.Hidden;
            this.btnBack.Visibility = Visibility.Hidden;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            this.booking = new Booking();

            try
            {
                SelectedDatesCollection Dates = calendarBookingDate.SelectedDates;

                if(Dates.Count <= 1)
                {
                    throw new ArgumentException("You need to select and drag a range of dates!");
                }
                if(Dates.Count > 1)
                {
                    booking.ArrivalDate = Dates[0];
                    booking.DepartureDate = Dates[Dates.Count - 1];
                }
                if (Dates[0].Date > Dates[Dates.Count - 1].Date)
                {
                    throw new ArgumentException("The Arrival Date Can't be after the Departure Date!");
                }
                if (customer != null)
                {
                    BookingDetails winB = new BookingDetails(booking, customer, true);
                    winB.Show();
                    this.Close();
                }
                else
                {
                    CustomerWindow win = new CustomerWindow(booking);
                    win.Show();
                    this.Close();
                }           
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Booking");
            }         
        }

        private void calendarBookingDate_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SelectedDatesCollection Dates = calendarBookingDate.SelectedDates;

                if (Dates.Count > 1)
                {
                    this.btnNext.IsEnabled = true;

                    this.btnUpdateBooking.IsEnabled = true;

                    lblDays.Content = Dates.Count.ToString();
                
                    int chaletID = facade.setChaletForBooking(Dates[0], Dates[Dates.Count - 1]);

                    if(chaletID > 10)
                    {
                        lblChalet_ID.Content = 0;
                        this.btnNext.IsEnabled = false;
                        throw new ArgumentException("Can't book for all the Chalets are taken during that time!");
                        
                    }
                    
                    int numberOfChaletsAvaliable = 11 - chaletID;

                    lblChalet_ID.Content = numberOfChaletsAvaliable;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Booking");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {


            if(win != null)
            {
                win.Show();
            }
            else
            {
                MainWindow win = new MainWindow();
                win.Show();
                this.Close();
            }
         
        }

        private void btnUpdateBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedDatesCollection Dates = calendarBookingDate.SelectedDates;

                if (Dates.Count <= 1)
                {
                    throw new ArgumentException("You need to select and drag a range of dates!");
                }

                if(Dates[0].Date > Dates[Dates.Count - 1].Date)
                {
                    throw new ArgumentException("The Arrival Date Can't be after the Departure Date!");
                }

                

                if (Dates.Count > 1)
                {   
                    booking.ArrivalDate = Dates[0];
                    booking.DepartureDate = Dates[Dates.Count - 1];
                    booking.ChaletID = facade.setChaletForBooking(Dates[0], Dates[Dates.Count - 1]);
                    
                }

                win.updateBookingDates(booking);

                win.disableButtons();
                win.dataGridBooking.SelectedItem = null;
                win.dataGridCustomer.SelectedItem = null;
                win.dataGridBooking.Items.Refresh();

                win.Show();
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Booking");
            }

        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            if (win != null)
            {
                
                win.Show();
                
            }
        }
    }
}
