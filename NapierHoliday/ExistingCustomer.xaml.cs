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
using Data;
using System.Collections.ObjectModel;
using Business;
using System.Data;

namespace NapierHoliday
{
    /// <summary>
    /// Interaction logic for ExistingCustomer.xaml
    /// </summary>
    public partial class ExistingCustomer : Window
    {

        private Customer c;

        private Booking b;

        private BusinessFacade facade = BusinessFacade.Instance;

        public ExistingCustomer()
        {
            InitializeComponent();

            

            this.dataGridCustomer.ItemsSource = facade.getCustomersFromDataBase;
            this.disableButtons();         
        }

        public ExistingCustomer(Customer c)
        {
            InitializeComponent();
            

            this.dataGridCustomer.ItemsSource = facade.getCustomersFromDataBase;
            this.disableButtons();

        }

        public void disableButtons()
        {
            this.btnModifyBooking.IsEnabled = false;
            this.btnModifyCustomer.IsEnabled = false;
            this.btnModifyBookingExtrasAndGuests.IsEnabled = false;
            this.btnDeleteBooking.IsEnabled = false;
            this.btnDeleteCustomer.IsEnabled = false;
            this.btnAddBooking.IsEnabled = false;
            this.btnInvoice.IsEnabled = false;
        }

        public BusinessFacade accessFacade()
        {
            return this.facade;
        }

        private void dataGridCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dataGridCustomer.CurrentCell.Item.GetType() != typeof(Customer))
            {
                return;
            }

            Customer test = (Customer)dataGridCustomer.SelectedItem;

            if(test == null)
            {
                this.disableButtons();
                return;
            }

            this.btnAddBooking.IsEnabled = true;



       //     facade.getBookingsFromDataBase(test.CustomerReferenceNumber);

            this.dataGridBooking.ItemsSource = facade.getBookingsFromDataBase(test.CustomerReferenceNumber);
            this.dataGridBooking.Items.Refresh();
            this.c = test;
            
            if(dataGridBooking.Items.Count < 1)
            {
                this.btnDeleteCustomer.IsEnabled = true;

            }
            else
            {
                this.btnDeleteCustomer.IsEnabled = false;
                this.btnModifyCustomer.IsEnabled = true;
            }
            this.btnModifyCustomer.IsEnabled = true;
            

            
        }

        private void btnModifyCustomer_Click(object sender, RoutedEventArgs e)
        {

           

            CustomerWindow win = new CustomerWindow(c, this);

            win.Show();

          
           

            this.Hide();
        }

        public void updateCustomer(Customer c)
        {
            facade.updateCustomer(c);

            this.dataGridCustomer.ItemsSource = facade.getCustomersFromDataBase;

            this.dataGridCustomer.Items.Refresh();

            /*
            facade.Customers.Remove(oldC);

            if (!this.facade.Customers.Contains(c))
            {
                this.facade.Customers.Add(c);
            }

            this.dataGridCustomer.ItemsSource = facade.Customers;

            this.dataGridCustomer.Items.Refresh();
            this.btnModifyCustomer.IsEnabled = false;

            data.updateCustomer(c.CustomerReferenceNumber, c.Name, c.Address);
            */
        }

        public void updateBookingDates(Booking b)
        {
            

            CarHire car = facade.getCarHireForBooking(b);

            if (car != null)
            {
                if (car.StartDate < b.ArrivalDate || car.StartDate > b.DepartureDate || car.EndDate > b.DepartureDate || car.EndDate < b.ArrivalDate)
                {
                    if (MessageBox.Show("Your car hire start and end dates aren't between your newly selected booking dates. If you want to continue, the car hire wil be cancelled and you will have to enter a new one.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {
                        facade.deleteCarHire(b);
                    }
                }
            }

            facade.updateBookingDates(b);

            /*
            bookings.Remove(oldB);

            if(!this.bookings.Contains(b))
            {
                this.bookings.Add(b);
            }

            this.dataGridBooking.ItemsSource = bookings;
            this.dataGridBooking.Items.Refresh();
            this.btnModifyBooking.IsEnabled = false;

            CarHire car = facade.getCarHire(b.BookingReferenceNumber);

            if(car != null)
            {
                if (car.StartDate < b.ArrivalDate || car.StartDate > b.DepartureDate || car.EndDate > b.DepartureDate || car.EndDate < b.ArrivalDate)
                {
                    if (MessageBox.Show("Your car hire start and end dates aren't between your newly selected booking dates. If you want to continue, the car hire wil be cancelled and you will have to enter a new one.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        return;
                    }
                    else
                    {
                        data.deleteCarHire(b.BookingReferenceNumber);
                    }
                }
            }

    
    data.updateBookingDates(b.BookingReferenceNumber, c.CustomerReferenceNumber, b.ArrivalDate, b.DepartureDate);

    */
        }
    

        private void btnModifyBooking_Click(object sender, RoutedEventArgs e)
        {
            NewBooking win = new NewBooking(b, this);

            win.Show();
            this.Hide();
        }

        private void dataGridBooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (dataGridBooking.CurrentCell.Item.GetType() != typeof(Booking))
            {
                return;
            }

            Booking test = (Booking)dataGridBooking.SelectedItem;

            if (test == null)
            {
                return;
            }


            this.b = test;
     
            this.btnModifyBookingExtrasAndGuests.IsEnabled = true;
            this.btnModifyBooking.IsEnabled = true;
            this.btnDeleteBooking.IsEnabled = true;
            this.btnInvoice.IsEnabled = true;
        }

        private void btnModifyBookingExtrasAndGuests_Click(object sender, RoutedEventArgs e)
        {

            BookingDetails win = new BookingDetails(b, facade.getGuestsForBooking(b), c, this, facade.getCarHireForBooking(b));

            win.Show();
            this.Hide();
            /*
            List<Guest> guests = data.getGuests(b.BookingReferenceNumber);

            bool carFlag = data.checkCarHireForBooking(b.BookingReferenceNumber);

            CarHire car = data.getCarHire(b.BookingReferenceNumber);


            NewCustomer win = new NewCustomer(b, guests, c, this, carFlag ,car);

            win.Show();
            this.Hide();
            */
        }

        private void btnDeleteBooking_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                facade.deleteBooking(b);

                dataGridBooking.ItemsSource = facade.getBookingsFromDataBase(c.CustomerReferenceNumber);
                dataGridBooking.Items.Refresh();

                dataGridBooking.SelectedItem = null;
            

                this.disableButtons();

                if(dataGridBooking.Items.Count < 1)
                {
                    this.btnDeleteCustomer.IsEnabled = true;
                }
                else
                {
                    dataGridCustomer.SelectedItem = null;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Delete Booking");
            }
        
            /*
            List<Guest> guests = data.getGuests(b.BookingReferenceNumber);

            CarHire car = data.getCarHire(b.BookingReferenceNumber);

            if(car != null)
            {
                data.deleteCarHire(b.BookingReferenceNumber);
            }

            data.deleteGuests(b.BookingReferenceNumber);

            data.deleteBooking(b.BookingReferenceNumber, c.CustomerReferenceNumber);

            bookings.Remove(b);

            dataGridBooking.ItemsSource = bookings;
            dataGridBooking.Items.Refresh();

            dataGridBooking.SelectedItem = null;
            dataGridCustomer.SelectedItem = null;

            this.disableButtons();

            if(bookings.Count < 1)
            {
                this.btnDeleteCustomer.IsEnabled = true;
            }
            */
        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {

            facade.deleteCustomer(c);

            this.dataGridCustomer.ItemsSource = facade.getCustomersFromDataBase;
            this.dataGridCustomer.Items.Refresh();

            this.dataGridBooking.SelectedItem = null;
            this.dataGridCustomer.SelectedItem = null;

            this.disableButtons();
            
            /*
            data.deleteCustomer(c.CustomerReferenceNumber);

            customers.Remove(c);

            this.dataGridCustomer.ItemsSource = customers;
            this.dataGridCustomer.Items.Refresh();

            this.dataGridCustomer.SelectedItem = null;
            this.dataGridBooking.SelectedItem = null;

            this.disableButtons();
            */
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            NewBooking winB = new NewBooking(c);

            winB.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }

        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            InvoiceWindow win = new InvoiceWindow(this, facade.getCarHireForBooking(b), facade.getGuestsForBooking(b).Count);
            win.Show();
            this.Hide();


        }

        public Booking getBooking()
        {
            if(b != null)
            {
                return this.b;
            }
            return null;
           
        }

        public Customer getCustomer()
        {
            if(c != null)
            {
                return this.c;
            }
            return null;
        }

        private void txtBoxCustomer_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {
                int i = 0;
                if (txtBoxCustomer.Text == String.Empty)
                {
                    dataGridCustomer.SelectedItem = null;
                    return;
                }

                Customer c = facade.getCertainCustomerFromDataBase(Convert.ToInt32(txtBoxCustomer.Text));

                if (c == null)
                {
                    dataGridCustomer.SelectedItem = null;
                    return;
                }

                /*
                while(i < dataGridCustomer.Items.Count)
                {
                    DataGridRow row = (DataGridRow)dataGridCustomer.ItemContainerGenerator.ContainerFromIndex(i);

                    TextBlock cellContent = dataGridCustomer.Columns[0].GetCellContent(row) as TextBlock;

                    if(cellContent != null && cellContent.Text.Equals(txtBoxCustomer.Text))
                    {
                        object item = dataGridCustomer.Items[i];
                        dataGridCustomer.SelectedItem = item;
                        dataGridCustomer.ScrollIntoView(item);
                        
                       // row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                        break;
                    }
                    i++;
                }
                */
                           

                foreach (Customer x in dataGridCustomer.Items)
                {
                    if (c.CustomerReferenceNumber == x.CustomerReferenceNumber)
                    {
                        dataGridCustomer.SelectedItem = x;
                        dataGridCustomer.UpdateLayout();
                        dataGridCustomer.ScrollIntoView(x);

                        this.dataGridBooking.ItemsSource = facade.getBookingsFromDataBase(c.CustomerReferenceNumber);
                        this.dataGridBooking.Items.Refresh();

                        this.btnAddBooking.IsEnabled = true;
                        this.btnModifyCustomer.IsEnabled = true;
                       

                        this.c = c;

                        if(dataGridBooking.Items.Count < 1)
                        {
                            this.btnDeleteCustomer.IsEnabled = true;
                        }
                        else
                        {
                            this.btnDeleteCustomer.IsEnabled = false;
                        }

                    }
                }
                
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Data Grid Customer");
            }
            
            


        }
    }
}
