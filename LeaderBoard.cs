using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionareApp
{
    internal class LeaderBoard
    {
        public LeaderBoard() {
            this.Leaders = new List<User>();
        }
        public List<User> Leaders
        {
            set; get;
        }

        public void updateLeadearboard(User user)
        {
            Leaders.Add(user);
        }

        public User getBestUser()
        {
            Leaders.Sort();
            return Leaders.First();
        }
    }
}
