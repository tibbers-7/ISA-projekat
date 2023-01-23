using BloodBankLibrary.Core.Addresses;
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
        private string description;
        private double avgScore;
        private DateTime workTimeStart;
        private DateTime workTimeEnd;
        [NotMapped]
        private string startString;
        [NotMapped]
        private string endString;
       
        public BloodCenter(){}

        public BloodCenter(int id,string name,string description, double avgScore, string workTimeStart, string workTimeEnd)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.avgScore = avgScore;
        }
           
            
      
        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
        public string Description { get => description; set => description = value; }
        public double AvgScore { get => avgScore; set => avgScore = value; }
        public DateTime WorkTimeStart { get => workTimeStart; set => workTimeStart = value; }
        public DateTime WorkTimeEnd { get => workTimeEnd; set => workTimeEnd = value; }
        
      
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
