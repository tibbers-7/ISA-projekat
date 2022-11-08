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
        string adress;
        string description;
        float avgScore;

        public BloodCenter(string name, int id, string adress, string description, float avgScore)
        {
            this.name = name;
            this.id = id;
            this.adress = adress;
            this.description = description;
            this.avgScore = avgScore;
        }

        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
        public string Adress { get => adress; set => adress = value; }
        public string Description { get => description; set => description = value; }
        public float AvgScore { get => avgScore; set => avgScore = value; }
    }
}
