namespace BloodBankLibrary.Core.Model
{
    public class Question
    {
        private int id;
        private string text;

        public int Id { get { return id; } set { id = value; } }
        public string Text { get { return text; } set { text = value; } }

        public Question()
        {
        }

        public Question(int id,string text)
        {
            this.id = id;
            this.text = text;
        }


    }
}
