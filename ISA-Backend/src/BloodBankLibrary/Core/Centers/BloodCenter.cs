using BloodBankLibrary.Core.Materials;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBankLibrary.Core.Centers
{
    public class BloodCenter
    {
        
        [Key]
        private int id;
        private string name;
        private Address address;
        private string description;
        private double avgScore;
        private DateTime workTimeStart;
        private DateTime workTimeEnd;
        [NotMapped]
        private string addressString;
        [NotMapped]
        private string startString;
        [NotMapped]
        private string endString;
       
        public BloodCenter()
        {
        }

        public BloodCenter(int id,string name,string addressString, string description, double avgScore, string workTimeStart, string workTimeEnd)
        {
            this.id = id;
            this.name = name;
            this.address = new Address(addressString);
            this.description = description;
            this.avgScore = avgScore;
            this.startString = workTimeStart;
            this.endString=workTimeEnd;
            
        }

        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
        public Address Address { get => address; set => address = value; }
        public string Description { get => description; set => description = value; }
        public double AvgScore { get => avgScore; set => avgScore = value; }
        public DateTime WorkTimeStart { get => workTimeStart; set => workTimeStart = value; }
        public DateTime WorkTimeEnd { get => workTimeEnd; set => workTimeEnd = value; }
        [NotMapped]
        public string AddressString { get => addressString; set
            {
                address = new Address(value);
                addressString= value;
            }
        }

        [NotMapped]
        public string StartString
        {
            get => startString; set
            {
                WorkTimeStart = DateTime.ParseExact(value, "hh:mm", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                startString = value;
            }
        }

        [NotMapped]
        public string EndString
        {
            get => endString; set
            {
                WorkTimeEnd = DateTime.ParseExact(value,"hh:mm", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                endString = value;
            }
        }
    }
}
