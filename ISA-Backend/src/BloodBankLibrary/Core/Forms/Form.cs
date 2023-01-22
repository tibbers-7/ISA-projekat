﻿using System.ComponentModel.DataAnnotations;

namespace BloodBankLibrary.Core.Forms
{
    public class Form
    {
        [Key]
        private int id;
        private int donorId;
        private int[] questionIds;
        private bool[] answers;

        public int Id { get { return id; } set { id = value; } }
        public int DonorId { get { return donorId; } set { donorId = value; } }
        public bool[] Answers { get { return answers; } set { answers = value; } }
        public int[] QuestionIds { get { return questionIds; } set { questionIds = value; } }

        public Form()
        {

        }

        public Form(int id,int donorId,int[] questionIds,bool[] answers)
        {
            this.id = id;
            this.donorId = donorId;
            this.questionIds = questionIds;
            this.answers = answers;
        }
    }
}