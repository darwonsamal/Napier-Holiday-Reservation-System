using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business
{
    public class CarHire
    {

        private DateTime _startDate;
        private DateTime _endDate;
        private string _driverName;
        

        public CarHire()
        {

        }

        public CarHire(DateTime _startDate, DateTime _endDate)
        {
            this._endDate = _endDate;
            this._startDate = _startDate;
        }

        public string DriverName
        {
            get
            {
                return _driverName;
            }
            set
            {
                if(value == String.Empty)
                {
                    throw new ArgumentException("Driver Name can't be empty");
                }
                _driverName = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if(value == null)
                {
                    throw new ArgumentException("Start Date can't be null!");
                }
                _startDate = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("End Date can't be null!");
                }
                _endDate = value;
            }
        }
    }
}
