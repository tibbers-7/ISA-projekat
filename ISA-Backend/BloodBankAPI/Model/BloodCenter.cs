
namespace BloodBankAPI.Model
{
    public class BloodCenter : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double AvgScore { get; set; }
        public DateTime WorkTimeStart { get; set; }
        public DateTime WorkTimeEnd { get; set; }

        public int? AmountA { get; set; }
        public int? AmountB { get; set; }
        public int? AmountAB { get; set; }
        public int? AmountO { get; set; }

        public BloodCenter(){}

        public BloodCenter(int id, string name,string description, double avgScore, string workTimeStart, string workTimeEnd)
        {
            Id = id;
            Name = name;
            Description = description;
            AvgScore = avgScore;
        }
  
    }
}
