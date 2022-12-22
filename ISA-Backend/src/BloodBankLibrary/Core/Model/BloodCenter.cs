namespace BloodBankLibrary.Core.Model
{
    public class BloodCenter
    {
        private string name;
        private int id;
        private Address address;
        private string description;
        private float avgScore;
        private int workTimeStart;
        private int workTimeEnd;
       
        public BloodCenter()
        {
        }

        public BloodCenter(string name, int id, string addressString, string description, float avgScore, int workTimeStart, int workTimeEnd)
        {
            this.name = name;
            this.id = id;
            this.address = new Address(addressString);
            this.description = description;
            this.avgScore = avgScore;
            this.workTimeStart = workTimeStart;
            this.workTimeEnd = workTimeEnd;
            
        }

        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
        public string Address { get => address.ToString(); set => address = new Address(value); }
        public string Description { get => description; set => description = value; }
        public float AvgScore { get => avgScore; set => avgScore = value; }
        public int WorkTimeStart { get => workTimeStart; set => workTimeStart = value; }
        public int WorkTimeEnd { get => workTimeEnd; set => workTimeEnd = value; }
    }
}
