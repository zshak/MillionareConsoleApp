using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionareApp
{
    public static class Constants
    {
        public const int NUM_QUESTIONS = 3;
        public const int NUM_ANSWERS = 4;
        public static readonly int[] PRIZES = { 50, 100, 500, 1000, 5000, 20000, 100000, 200000, 500000, 1000000, 0};
        public const string FILE_LOC = @"C:\Users\zshakulashvili\source\repos\BankAccount\MillionareApp\leaderboard.txt.txt";
    }
}
