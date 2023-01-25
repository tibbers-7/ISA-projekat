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
        private int? amountA;
        private int? amountB;
        private int? amountAB;
        private int? amountO;
      
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
      
        
        public int? AmountA { get => amountA; set => amountA = value; }
        public int? AmountB { get => amountB; set => amountB = value; }
        public int? AmountAB { get => amountAB; set => amountAB = value; }
        public int? AmountO { get => amountO; set => amountO = value; }




    }
}
