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
using System.ComponentModel;

namespace NapierHoliday
{
    /// <summary>
    /// Interaction logic for AddCarHire.xaml
    /// </summary>
    public partial class AddCarHire : Window
    {
        DateTime arrival;
        DateTime departure;
        BookingDetails win;
          

        public AddCarHire(DateTime arrival, DateTime departure, BookingDetails win)
        {
            InitializeComponent();

            this.win = win;
            this.arrival = arrival;
            this.departure = departure;
            this.datePickerStart.DisplayDateStart = arrival;
            this.datePickerEnd.DisplayDateStart = arrival;
            this.datePickerStart.DisplayDateEnd = departure;
            this.datePickerEnd.DisplayDateEnd = departure;

            this.btnSaveCarHire.IsEnabled = false;

            lblArrivalDate.Content = arrival.Date;
            lblDepartureDate.Content = departure.Date;

        }

        public AddCarHire(DateTime arrival, DateTime departure, BookingDetails win, CarHire car)
        {
            InitializeComponent();

            this.win = win;
            this.arrival = arrival;
            this.departure = departure;
            this.datePickerStart.DisplayDateStart = arrival;
            this.datePickerEnd.DisplayDateStart = arrival;
            this.datePickerStart.DisplayDateEnd = departure;
            this.datePickerEnd.DisplayDateEnd = departure;

            lblArrivalDate.Content = arrival.Date;
            lblDepartureDate.Content = departure.Date;
            txtBoxDriverName.Text = car.DriverName;

            this.datePickerStart.SelectedDate = car.StartDate;
            this.datePickerEnd.SelectedDate = car.EndDate;
           
        }

        private void btnSaveCarHire_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datePickerStart.SelectedDate == null || datePickerEnd.SelectedDate == null || txtBoxDriverName.Text == String.Empty)
                {
                    throw new ArgumentException("You must select Both dates and driver's name!", "Car Hire");
                }

                if(datePickerStart.SelectedDate.Value >= datePickerEnd.SelectedDate.Value)
                {
                    throw new ArgumentException("Nice try! But I caught you! You can't do that, but A+ for effort!");
                }
                                                     

                        win.getCar().StartDate = datePickerStart.SelectedDate.Value;
                        win.getCar().EndDate = datePickerEnd.SelectedDate.Value;
                        win.getCar().DriverName = txtBoxDriverName.Text;


                        MessageBox.Show("Car Hire succesfully added!", "Car Hire");

                        win.Show();
                        this.Close();
                
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Car Hire");
            }
            
        }

        private void txtBoxDriverName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtBoxDriverName.Text == String.Empty)
            {
                this.btnSaveCarHire.IsEnabled = false;
            }
            else
            {
                this.btnSaveCarHire.IsEnabled = true;
            }

        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {

            win.Show();
        }
    }
}
