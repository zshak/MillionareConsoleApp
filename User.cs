using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

using static MillionareApp.Constants;
namespace MillionareApp
{
    internal class User: IComparable<User>
    {
        private string _username;
        private int _prizeWon = 0;
        public Questionnaire Questionnaire { get; set; }
        public string Username
        {
            get
            {
                return (String)_username.Clone();
            }
            set
            {
                if(value == null) throw new ArgumentNullException();
                _username = value;
            }
        }

        public int PrizeWon
        {
            get { return _prizeWon; }
            set
            {
                if (!PRIZES.Contains(value)) throw new ArgumentException();
                _prizeWon = value;
            }
        }

        public int CompareTo(User? other)
        {
            return this.PrizeWon - other.PrizeWon;
        }

        public override string ToString()
        {
            return Username;
        }

    }
}
