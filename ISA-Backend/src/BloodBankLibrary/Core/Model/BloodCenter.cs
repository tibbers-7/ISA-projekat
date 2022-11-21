using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Model
{
    public class BloodCenter
    {
        string name;
        int id;
        Address adress;
        string description;
        float avgScore;
        string openHours;
       
        public BloodCenter()
        {
        }

        public BloodCenter(string name, int id, string addressString, string description, float avgScore, List<Appointment> appointments)
        {
            this.name = name;
            this.id = id;
            this.adress = new Address(addressString);
            this.description = description;
            this.avgScore = avgScore;
            
        }

        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
        public string Adress { get => adress.ToString(); set => adress = new Address(value); }
        public string Description { get => description; set => description = value; }
        public float AvgScore { get => avgScore; set => avgScore = value; }
        public string OpenHours { get => openHours; set => openHours = value; }
    }
}
