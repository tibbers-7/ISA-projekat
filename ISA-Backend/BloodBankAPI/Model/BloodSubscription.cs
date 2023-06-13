using BloodBankAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.BloodSubscription
{
    public class BloodSubscription: Entity
    {
        private int amountOfA { get; set; }
        private int amountOfB { get; set; }
        private int amountOfAB { get; set; }
        private int amountOfO { get; set; }
        private int date { get; set; }

        public BloodSubscription(int amountA, int amountB, int amountAB, int amountO, int date) { 
            
            amountOfA= amountA;
            amountOfB= amountB;
            amountOfAB= amountAB;
            amountOfO= amountO;
            this.date = date; 
        }


        public int AmountOfA { get => amountOfA; set => amountOfA=value; }
        public int AmountOfB { get => amountOfB; set => amountOfB = value; }
        public int AmountOfAB { get => amountOfAB; set => amountOfAB = value; }
        public int AmountOfO { get => amountOfO; set => amountOfO = value; }
        public int Date { get => date; set => date = value;}
    }
}
