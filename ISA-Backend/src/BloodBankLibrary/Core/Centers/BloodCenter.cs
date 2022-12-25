using BloodBankLibrary.Core.Materials;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BloodBankLibrary.Core.Centers
{
    public class BloodCenter
    {
        
        [Key]
        private int id;
        private string name;
        [NotMapped]
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
            this.Address = new Address(addressString);
            this.AddressJson=JsonSerializer.Serialize(Address);
            this.description = description;
            this.avgScore = avgScore;

           /* Regex checkTime = new Regex(@"^(?i)(0?[1-9]|1[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?( AM| PM)?$");
            if (checkTime.IsMatch(workTimeStart) && checkTime.IsMatch(workTimeEnd))
            {*/
                this.StartString = workTimeStart;
                this.EndString = workTimeEnd;
           /* }
            else new ArgumentException();*/

            
            
        }

        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
        [NotMapped]
        public Address Address
        {
            get => address; set
            {
                address = value;
                AddressJson = JsonSerializer.Serialize(address);
            }
        }
        public string Description { get => description; set => description = value; }
        public double AvgScore { get => avgScore; set => avgScore = value; }
        public DateTime WorkTimeStart { get => workTimeStart; set => workTimeStart = value; }
        public DateTime WorkTimeEnd { get => workTimeEnd; set => workTimeEnd = value; }
        [Column(TypeName = "jsonb")]
        public string AddressJson { get; set; }
        [NotMapped]
        public string AddressString { get => addressString; set
            {
                Address = new Address(value);
                addressString= value;
            }
        }

        [NotMapped]
        public string StartString
        {
            get => startString; set
            {
                value = "01/01/0001 " + value;
                WorkTimeStart = DateTime.ParseExact(value, "dd/MM/yyyy HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                startString = value;
            }
        }

        [NotMapped]
        public string EndString
        {
            get => endString; set
            {
                value = "01/01/0001 " + value;
                WorkTimeEnd = DateTime.ParseExact(value, "dd/MM/yyyy HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                
                endString = value;
            }
        }
    }
}
