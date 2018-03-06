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
using Business;
using Data;
using System.ComponentModel;

namespace NapierHoliday
{
    /// <summary>
    /// Interaction logic for NewCustomer.xaml
    /// </summary>
    public partial class BookingDetails : Window
    {       

        private BusinessFacade facade = BusinessFacade.Instance;

        private Booking booking;

        private Customer customer;

        private ExistingCustomer win;

        private bool addBooking = false;

        private int countGuest;

        private CarHire car;

        private Guest guest;


        public BookingDetails(Booking booking, Customer customer)
        {
            InitializeComponent();
            this.booking = booking;
            this.customer = customer;

            this.lblDIsplayCustomerName.Content = customer.Name;
            this.lblDIsplayCustomerAddress.Content = customer.Address;

            this.lblArrivalDate.Content = booking.ArrivalDate.Date;
            this.lblDepartureDate.Content = booking.DepartureDate.Date;


            this.disableButtons();
          

            this.btnUpdateBooking.Visibility = Visibility.Hidden;

        }

        public BookingDetails(Booking booking, Customer customer, bool addBooking)
        {
            InitializeComponent();
            this.booking = booking;
   
        
            this.customer = customer;
            this.addBooking = addBooking;

            this.lblDIsplayCustomerName.Content = customer.Name;
            this.lblDIsplayCustomerAddress.Content = customer.Address;


            this.lblArrivalDate.Content = booking.ArrivalDate.Date;
            this.lblDepartureDate.Content = booking.DepartureDate.Date;

            this.disableButtons();
         

            this.btnSaveCustomerAndBooking.Visibility = Visibility.Visible;
            this.btnUpdateBooking.Visibility = Visibility.Hidden;
        }
            
        public BookingDetails(Booking booking, List<Guest> guests, Customer customer, ExistingCustomer win, CarHire car)
        {
            InitializeComponent();

            this.booking = booking;
            this.customer = customer;
            this.win = win;
          
            this.lblDIsplayCustomerName.Content = customer.Name;
            this.lblDIsplayCustomerAddress.Content = customer.Address;


            this.lblArrivalDate.Content = booking.ArrivalDate.Date;
            this.lblDepartureDate.Content = booking.DepartureDate.Date;

            this.checkBoxBreakfast.IsChecked = booking.Breakfast;
            this.checkBoxEvening.IsChecked = booking.EveningMeal;

            this.disableButtons();

            if(car != null)
            {
                this.car = car;
                this.checkBoxCar.IsChecked = true;
                this.btnAddCar.IsEnabled = true;
            }

            this.btnUpdateBooking.Visibility = Visibility.Visible;
            this.btnSaveCustomerAndBooking.Visibility = Visibility.Hidden;


            facade.Guests.Clear();

            
            foreach(Guest x in guests)
            {

                if (lstBoxViewGuests.Items.Contains(x.PassportNo) == false)
                {
                    this.facade.Guests.Add(x);
                    lstBoxViewGuests.Items.Add(x.PassportNo);
                    countGuest++;
                }
               
            }

        }

        public void disableButtons()
        {
            this.btnAddGuests.IsEnabled = false;
            this.btnDeleteGuest.IsEnabled = false;
            this.btnAddCar.IsEnabled = false;
            this.btnModifyGuest.IsEnabled = false;
        }

        private void btnAddGuests_Click(object sender, RoutedEventArgs e)
        {
          
            try
            {
              
                if(this.btnAddGuests.Content.ToString() == "Update Guest")
                {
                    lstBoxViewGuests.Items.Remove(guest.PassportNo);

                    guest.Name = txtBoxGuestName.Text;
                    guest.Age = Convert.ToInt32(txtBoxGuestAge.Text);
                    guest.PassportNo = txtBoxPassportNumber.Text;

                    facade.addGuest(guest);

                    lstBoxViewGuests.Items.Add(guest.PassportNo);

                    this.btnAddGuests.IsEnabled = false;
                    this.clearGuestTxtBoxes();

                    this.btnAddGuests.Content = "Add Guest";
                }
                else
                {

                    if(countGuest > 6)
                    {
                        throw new ArgumentException("There are more than 6 guests!");
                    }

                    guest = new Guest();

                    guest.Name = txtBoxGuestName.Text;
                    guest.Age = Convert.ToInt32(txtBoxGuestAge.Text);
                    guest.PassportNo = txtBoxPassportNumber.Text;


                    if(lstBoxViewGuests.Items.Contains(guest.PassportNo))
                    {
                        throw new ArgumentException("Can't have two guests with the same passport number!");
                    }

                    countGuest++;

                    facade.addGuest(guest);

                   
                    lstBoxViewGuests.Items.Add(guest.PassportNo);

                    this.btnAddGuests.IsEnabled = false;
                    this.clearGuestTxtBoxes();
                    
                }
                
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Add Guest");
            }
            
            
        }

        private void clearGuestTxtBoxes()
        {
            this.txtBoxGuestName.Clear();
            this.txtBoxGuestAge.Clear();
            this.txtBoxPassportNumber.Clear();
        }

        private void clearGuestDetails()
        {
            this.lblViewGuestAge.Content = "";
            this.lblViewGuestName.Content = "";
            this.lblViewGuestPassportNumber.Content = "";
        }
   
        private void lstBoxViewGuests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if(lstBoxViewGuests.SelectedItem == null)
                {
                    throw new ArgumentException("List Box is empty!");
                }

                string passportNumber = (string)lstBoxViewGuests.SelectedItem;

                Guest guest = facade.findGuest(passportNumber);

                lblViewGuestName.Content = guest.Name;
                lblViewGuestPassportNumber.Content = guest.PassportNo;
                lblViewGuestAge.Content = guest.Age;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "List Box");
            }
        }

        private void btnSaveCustomerAndBooking_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Are you sure of the details?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            else 
            {
               
                try
                {
                  
                    if (facade.Guests.Count < 1 && lstBoxViewGuests.Items.Count < 1)
                    {
                        throw new ArgumentException("There are no guests! Add guests!");
                    }

                    if(checkBoxCar.IsChecked != true && car != null)
                    {
                        car = null;
                    }
                    
                    int breakfast = checkBreakfast();
                    int eveningMeal = checkEveningMeal();

                    if (addBooking == true)
                    {

                        facade.SaveCustomerAndBooking(customer, facade.Guests, booking, car, breakfast, eveningMeal, addBooking);


                        MessageBox.Show("Succesfully Saved!", "Confirmation");

                        facade.Guests.Clear();

                        ExistingCustomer win = new ExistingCustomer();
                        win.Show();
                        this.Close();
                    }
                    else
                    {                       

                        facade.SaveCustomerAndBooking(customer, facade.Guests, booking, car, breakfast, eveningMeal, false);


                        MessageBox.Show("Succesfully Saved!", "Confirmation");

                        facade.Guests.Clear();

                        MainWindow win = new MainWindow();
                        win.Show();
                        this.Close();

                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message, "Save");
                }
            }

              
        }

        private void DeleteGuest_Click(object sender, RoutedEventArgs e)
        {
            string find = txtBoxAmendGuest.Text;

            if (lstBoxViewGuests.Items.Contains(find))
            {
                lstBoxViewGuests.Items.Remove(find);
                facade.removeGuest(find);
                txtBoxAmendGuest.Clear();
                this.btnDeleteGuest.IsEnabled = false;
                this.clearGuestDetails();

            }
            else
            {
                MessageBox.Show("Could not find guest!");
            }
       
        }

        public CarHire getCar()
        {
            if(this.car == null)
            {
                this.car = new CarHire();
            }
            else
            {
                return this.car;
            }

            return this.car;
        }

        public Booking getBooking()
        {
            return booking;
        }

        public int checkBreakfast()
        {
            if(checkBoxBreakfast.IsChecked == true)
            {
                return 1;
            }
            else if(checkBoxBreakfast.IsChecked == false)
            {
                return 0;
            }

            return 0;
        }

        public int checkEveningMeal()
        {
            if(checkBoxEvening.IsChecked == true)
            {
                return 1;

            }
            else if(checkBoxEvening.IsChecked == false)
            {
                return 0;
            }

            return 0;
        }

        private void txtBoxAmendGuest_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtBoxAmendGuest.Text == String.Empty)
            {
                this.btnDeleteGuest.IsEnabled = false;
                this.btnModifyGuest.IsEnabled = false;
            }
            else
            {
                this.btnDeleteGuest.IsEnabled = true;
                this.btnModifyGuest.IsEnabled = true;
            }
        }

        private void btnAddCarHire_Click(object sender, RoutedEventArgs e)
        {
            if(car == null)
            {
                AddCarHire win = new AddCarHire(booking.ArrivalDate, booking.DepartureDate, this);
                this.Hide();
                win.Show();
            }
            else
            {
                AddCarHire win = new AddCarHire(booking.ArrivalDate, booking.DepartureDate, this, car);
                this.Hide();
                win.Show();
            }
         
        }

        private void checkBoxCar_Click(object sender, RoutedEventArgs e)
        {
            if(checkBoxCar.IsChecked.Value == true)
            {
                this.btnAddCar.IsEnabled = true;
            }
            else if(checkBoxCar.IsChecked.Value == false)
            {
                this.btnAddCar.IsEnabled = false;
            }
        }

        private void btnUpdateBooking_Click(object sender, RoutedEventArgs e)
        {


            if (MessageBox.Show("Are you sure of the details?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            else
            {

                Guest g = new Guest();


                try
                {


                    if (facade.Guests.Count < 1 || lstBoxViewGuests.Items.IsEmpty)
                    {
                        throw new ArgumentException("There are no guests! Add guests!");
                    }

                    int breakfast = checkBreakfast();
                    int eveningMeal = checkEveningMeal();

                    facade.UpdateBooking(customer, facade.Guests, booking, car, breakfast, eveningMeal, checkBoxCar.IsChecked.Value);

                    facade.Guests.Clear();
                    
                    MessageBox.Show("Succesfully Saved!", "Confirmation");

                    win.dataGridBooking.SelectedItem = null;
                    win.dataGridCustomer.SelectedItem = null;
                    win.disableButtons();
                    win.dataGridBooking.ItemsSource = null;
                    win.dataGridBooking.Items.Refresh();

                    win.Show();
                    this.Close();

                } 

                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message, "Update");
                }
            }
        }

        private void btnModifyGuest_Click(object sender, RoutedEventArgs e)
        {
            string find = txtBoxAmendGuest.Text;

            if (lstBoxViewGuests.Items.Contains(find))
            {
              
                Guest g = facade.findGuest(find);

                facade.removeGuest(find);

                txtBoxAmendGuest.Clear();

                this.btnDeleteGuest.IsEnabled = false;
                this.btnModifyGuest.IsEnabled = false;

                this.txtBoxGuestName.Text = g.Name;
                this.txtBoxPassportNumber.Text = g.PassportNo;
                this.txtBoxGuestAge.Text = Convert.ToString(g.Age);

                this.btnAddGuests.Content = "Update Guest";

                this.guest = g;

                this.clearGuestDetails();
            }
            else
            {
                MessageBox.Show("Could not find guest!");
            }
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            if (win != null)
            {
                win.Show();
            }
        }

        private void txtBoxGuestName_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkTxtGuestBoxes();
        }

        private void txtBoxGuestAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkTxtGuestBoxes();
        }

        private void txtBoxPassportNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkTxtGuestBoxes();
        }

        private void checkTxtGuestBoxes()
        {
            if(txtBoxGuestAge.Text == String.Empty || txtBoxGuestName.Text == String.Empty || txtBoxPassportNumber.Text == String.Empty)
            {
                this.btnAddGuests.IsEnabled = false;
            }
            else
            {
                this.btnAddGuests.IsEnabled = true;
            }
        }

        private void btnTotalInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(lstBoxViewGuests.Items.Count < 1)
                {
                    throw new ArgumentException("You must add at least one guest");
                }

                InvoiceWindow invoice = new InvoiceWindow(this);

                invoice.Show();
                this.Hide();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Invoice");
            }

        }
    }

}
