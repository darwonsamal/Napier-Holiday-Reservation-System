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
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private Customer customer;

        private Booking booking;

        private ExistingCustomer win;

        public CustomerWindow(Booking b)
        {
            this.booking = b;
            InitializeComponent();
            btnAddCustomer.IsEnabled = false;
            btnModifyCustomer.Visibility = Visibility.Hidden;
        }

        public CustomerWindow(Customer c, ExistingCustomer win)
        {
            InitializeComponent();
            this.customer = c;
            this.win = win;
            txtCustomerName.Text = customer.Name;
            txtCustomerAddress.Text = customer.Address;
            btnAddCustomer.Visibility = Visibility.Hidden;
            btnBack.Visibility = Visibility.Hidden;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NewBooking win = new NewBooking();
            win.Show();
            this.Close();
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                customer = new Customer();

                customer.Address = txtCustomerAddress.Text;
                customer.Name = txtCustomerName.Text;


                BookingDetails win = new BookingDetails(booking, customer);

                win.Show();
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Customer");
            }
         
        }

        private void btnModifyCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCustomerName.Text == String.Empty || txtCustomerAddress.Text == String.Empty)
                {
                    throw new ArgumentException("You need to fill both customer name and address!", "Customer");
                }

              
                customer.Address = txtCustomerAddress.Text;
                customer.Name = txtCustomerName.Text;
                

                win.updateCustomer(customer);

                win.dataGridCustomer.SelectedItem = null;
                win.disableButtons();

                win.dataGridBooking.Items.Refresh();

                win.Show();
                
                
                this.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void txtCustomerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.checkTextBoxesIfFilled();
        }

        private void txtCustomerAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.checkTextBoxesIfFilled();
        }

        public void checkTextBoxesIfFilled()
        {
            if(txtCustomerName.Text == String.Empty || txtCustomerAddress.Text == String.Empty)
            {
                btnAddCustomer.IsEnabled = false;
                btnModifyCustomer.IsEnabled = false;
            }
            else
            {
                btnAddCustomer.IsEnabled = true;
                btnModifyCustomer.IsEnabled = true;
            }
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {


           if(win != null)
           {
                win.Show();
           }
       
        }
    }
}
