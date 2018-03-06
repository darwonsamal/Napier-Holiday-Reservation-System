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
using Business;

namespace NapierHoliday
{
    /// <summary>
    /// Interaction logic for InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : Window
    {

        private BookingDetails win;

        private ExistingCustomer existingCustomer;

        public InvoiceWindow(BookingDetails win)
        {

            this.win = win;
            InitializeComponent();

            int breakfast = 0;
            int carHire = 0;
            int eveningMeal = 0;
            int numberOfGuests = this.win.lstBoxViewGuests.Items.Count;
            int totalDays = (this.win.getBooking().DepartureDate - this.win.getBooking().ArrivalDate).Days;
            int carHireRentalDays = (this.win.getCar().EndDate - this.win.getCar().StartDate).Days;
            int chaletPrice = (60 + numberOfGuests * 25) * totalDays;

            lblCustomerName.Content = this.win.lblDIsplayCustomerName.Content;
            lblNumberOfDays.Content = totalDays;
            lblNumberOfGuests.Content = numberOfGuests + " Guests";


            if(this.win.checkBoxBreakfast.IsChecked == true)
            {
                
                breakfast = (numberOfGuests * 5) * totalDays;
                lblBreakfast.Content = breakfast + " $";
            }              
            else if(this.win.checkBoxBreakfast.IsChecked == false)
            {
                lblBreakfast.Content = "Not Included";
            }

            if (this.win.checkBoxEvening.IsChecked == true)
            {
                eveningMeal = (numberOfGuests * 10) * (totalDays - 1);
                lblEveningMeal.Content =  eveningMeal + " $";
            }
            else if (this.win.checkBoxEvening.IsChecked == false)
            {
                lblEveningMeal.Content = "Not Included";
            }

            if (this.win.checkBoxCar.IsChecked == true)
            {
                carHire = carHireRentalDays * 50;
                lblCarHire.Content =  carHire + " $";
            }
            else if (this.win.checkBoxCar.IsChecked == false)
            {
                lblCarHire.Content = "Not Included";
            }


            int totalPrice = chaletPrice + breakfast + eveningMeal + carHire;

            lblTotal.Content = totalPrice + " $";



        }

        public InvoiceWindow(ExistingCustomer win, CarHire car, int guests)
        {
            this.existingCustomer = win;
            InitializeComponent();


            int breakfast = 0;
            int carHire = 0;
            int eveningMeal = 0;
            int numberOfGuests = guests;
            int totalDays = (this.existingCustomer.getBooking().DepartureDate - this.existingCustomer.getBooking().ArrivalDate).Days;
            
            int chaletPrice = (60 + numberOfGuests * 25) * totalDays;

            lblCustomerName.Content = this.existingCustomer.getCustomer().Name;
            lblNumberOfDays.Content = totalDays;
            lblNumberOfGuests.Content = numberOfGuests + " Guests";


            if (car != null)
            {
                int carHireRentalDays = (car.EndDate - car.StartDate).Days;
                carHire = carHireRentalDays * 50;
                lblCarHire.Content = carHire + " $";
            }
            else if (car == null)
            {
                lblCarHire.Content = "Not Included";
            }
            if (existingCustomer.getBooking().Breakfast == true)
            {

                breakfast = (numberOfGuests * 5) * totalDays;
                lblBreakfast.Content = breakfast + " $";
            }
            else if (this.existingCustomer.getBooking().Breakfast == false)
            {
                lblBreakfast.Content = "Not Included";
            }

            if (this.existingCustomer.getBooking().EveningMeal == true)
            {
                eveningMeal = (numberOfGuests * 10) * (totalDays - 1);
                lblEveningMeal.Content = eveningMeal + " $";
            }
            else if (this.existingCustomer.getBooking().EveningMeal == false)
            {
                lblEveningMeal.Content = "Not Included";
            }



            int totalPrice = chaletPrice + breakfast + eveningMeal + carHire;

            lblTotal.Content = totalPrice + " $";




        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            if(win != null)
            {
                win.Show();
                this.Close();
            }
            else if(existingCustomer != null)
            {
                existingCustomer.Show();
                this.Close();
            }
        }
    }
}
