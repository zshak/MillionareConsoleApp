
//leaderboard class
//leaderboard populated by user class
//questionare class populated by question class

using System.IO.Pipes;
using System.Text.Json;
using System.Xml.Linq;
using static MillionareApp.Constants;

namespace MillionareApp
{
    internal class App
    {

        private static Question[] GetQuestions()
        {
            Question[] questions = new Question[NUM_QUESTIONS];
            for(int question = 0; question < NUM_QUESTIONS; question++)
            {
                Console.WriteLine("Enter a question: ");
                string? userQuestion = Console.ReadLine();
                questions[question] = new Question { QuestionContent = userQuestion };

                for(int answer = 0; answer < NUM_ANSWERS; answer++)
                {
                    Console.WriteLine($"Enter answer #{answer + 1}");
                    string? userAnswer = Console.ReadLine();
                    questions[question][answer] = userAnswer;
                }

                Console.WriteLine("Enter correct answers \"1 - 4\" ");
                int correctAns = int.Parse(Console.ReadLine());
                questions[question].CorrectAnswer = correctAns - 1;
            }
            return questions;
        }

        private static Questionnaire GetQuestionnaire()
        {
            Question[] questions = GetQuestions();

            return new Questionnaire { Questions = questions };
        }

        private static User GetUser()
        {
            Console.WriteLine("Enter Username: ");
            string? userName = Console.ReadLine();
            return new User { Username = userName };
        }

        private static T getInput<T>(Predicate<string> pred)
        {
            string ans = Console.ReadLine();
            while (pred(ans))
            {
                Console.WriteLine("Illegal input, try again: ");
                ans = Console.ReadLine();
            }
            return (T)Convert.ChangeType(ans, typeof(T));
        }

        private static string[] DisplayQuestion(Question q)
        {
            string[] questions = q.ShuffleQuestions();
            foreach(string  question in questions)
            {
                Console.Write(question + " ");
            }
            Console.WriteLine();
            return questions;
        }

        private static void SimulateGame(User user, Questionnaire questionnaire, LeaderBoard lb)
        {
            Console.WriteLine("Who Wants To Be A Millionare?");
            for (int question = 0; question < NUM_QUESTIONS; question++)
            {
                Question q = questionnaire[question];
                Console.WriteLine(q.ToString());
                string[] shuffledQuestions = DisplayQuestion(q);
                Console.WriteLine("Enter your Answer \"1 - 4\"");
                int ans = getInput<int>(str => int.Parse(str) < 1 || int.Parse(str) > 4);
                if (!q.isCorrectAns(shuffledQuestions[ans - 1]))
                {
                    Console.WriteLine("Wrong Answer, Good Game!");
                    lb.updateLeadearboard(user);
                    return;
                }
                Console.WriteLine("Your Answer Is Correct!");
                if (question == NUM_QUESTIONS - 1) break;
                Console.WriteLine("Do You Wish To Continue? [Y/N]");
                string choice = getInput<string>(str => str != "Y" && str != "N");
                if(choice == "N")
                {
                    // update leaderboard
                    lb.updateLeadearboard(user);
                    user.PrizeWon = questionnaire.getQuestionPrize(question);
                    Console.WriteLine("Good Game");
                    return;
                }
                Console.WriteLine("Next Question: ");
                
            }

            Console.WriteLine("Congratulations! You Won");
            lb.updateLeadearboard(user);

        }

        private static void showLeaderBoard(LeaderBoard lb)
        {
            Console.WriteLine("LeaderBoard: ");
            List<User> users = lb.Leaders;
            for(int i = 0; i < users.Count; i++)
            {

                Console.WriteLine($"#{i + 1}: {users.ElementAt(i)}");
            }
        }

        private static LeaderBoard GetLeaderBoard()
        {

            string lbString = File.ReadAllText(FILE_LOC);
            if (string.IsNullOrEmpty(lbString)) return new LeaderBoard();
            LeaderBoard lb = JsonSerializer.Deserialize<LeaderBoard>(lbString);
            return lb;
        }

        private static void save(LeaderBoard lb)
        {
            string serializedString = JsonSerializer.Serialize(lb, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(FILE_LOC, serializedString);
        }

        private static void StartGame()
        {
            LeaderBoard lb = GetLeaderBoard();
            while(true)
            {
                Console.WriteLine("Would You Like To Play? [Y/N]");
                string shouldPlay = getInput<string>(str => str != "Y" && str != "N");
                if (shouldPlay == "N")
                {
                    showLeaderBoard(lb);
                    break;
                }
                User user = GetUser();
                Questionnaire questionnaire = GetQuestionnaire();
                user.Questionnaire = questionnaire;
                SimulateGame(user, questionnaire, lb);
                showLeaderBoard(lb);
            }

            save(lb);
            
        }
        public static void Main(string[] args)
        {
            StartGame();
        }
    }    
}
