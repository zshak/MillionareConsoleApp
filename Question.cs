using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static MillionareApp.Constants;

namespace MillionareApp
{
    internal class Question
    {
        private string[] answers = new string[NUM_ANSWERS];
        
        private string questionContent;
        private int correctAnswer;
        public string QuestionContent
        {
            get { return (String)questionContent.Clone(); }
            set 
            {
                if (value == null || value.Length == 0) throw new ArgumentNullException();
                questionContent = value;
            }
        }


        public int CorrectAnswer
        {
            get { return correctAnswer; }
            set 
            {
                if(value < 0 || value >= NUM_ANSWERS) throw new ArgumentOutOfRangeException();
                correctAnswer = value;
            }
        }

        private static void swap(string[] a, int ind1, int ind2)
        {
            string temp = a[ind1];
            a[ind1] = a[ind2];
            a[ind2] = temp;
        }

        public string[] ShuffleQuestions()
        {
            string[] res = (string[])answers.Clone();
            Random r = new Random();
            for(int i = 0; i < NUM_ANSWERS; i++)
            {
                int index = r.Next(0,4);
                swap(res, i, index);
            }
            return res;
        }

        public string this[int index]
        {

            get { return (String)answers[index].Clone(); }
            set
            {
                if(index < 0 || index >= NUM_ANSWERS) throw new ArgumentOutOfRangeException();
                if (value == null || value.Length == 0) throw new ArgumentException();
                answers[index] = value;
            }
        }

        public bool isCorrectAns(string ans)
        {

            return answers[CorrectAnswer] == ans;
        }

        public override string ToString()
        {
            return (String)questionContent.Clone();
        }

    }
}
