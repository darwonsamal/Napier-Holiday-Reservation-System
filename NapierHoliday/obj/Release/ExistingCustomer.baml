   M S B A M L   `   `   `  ���� H  DNapierHoliday, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:NapierHolidayNapierHoliday  $    NapierHoliday.ExistingCustomer   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:NapierHoliday       � Title$  ExistingCustomer��$	��400��$	��750��.��� .+��� -    ��Name$ lblExistingCustomer��$��Existing Customer��$��Center=�$��218,10,206.2,0q�$	��Top=�$��20(�$	��171��&   System.Windows.Controls.DataGrid  -     Name$ dataGridCustomer��$
��Left=�$	��180��$��0,84,0,0q�$	��Top=�$	��321��   AutoGenerateColumns .     
IsReadOnly .    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Customer Reference��1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ CustomerReferenceNumber�  $ Customer Name�� �� $
 Name�  $ Customer Address�� �� $ Address��� $ ��Select Customer To Modify:��$
��Left=�$��0,58,0,0q�$	��Top=��� -   	 ��Name$	 btnModifyCustomer��$��Modify Customer��$
��Left=�$��
10,285,0,0q�$	��Top=�$	��112��  -   $ dataGridBooking��$
��Left=�$	��180��$��
326,84,0,0q�$	��Top=�$	��274�� .   .    $ Booking Reference�� �� $ BookingReferenceNumber�  $ Arrival Date�� �� $ ArrivalDate�
 �� StringFormat$
 
MM/dd/yyyy��  $ Departure Date�� �� $ DepartureDate�$
 
MM/dd/yyyy���� $��Select Booking To Modify:��$
��Left=�$��
326,58,0,0q�$	��Top=� J� RenderTransformOrigin$ -0.242,0.454&��� -   $	 btnModifyBooking��$��Modify Booking Dates��$
��Left=�$��127,285,0,0q�$	��Top=�$	��132���� -   $%	 btnModifyBookingExtrasAndGuests��$&�� Modify Booking Extras and Guests��$
��Left=�$��
10,310,0,0q�$	��Top=�$	��249���� $��Button��$
��Left=�$��525,285,0,0q�$	��Top=�$��75��