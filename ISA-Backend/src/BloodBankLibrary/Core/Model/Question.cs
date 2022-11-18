using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
