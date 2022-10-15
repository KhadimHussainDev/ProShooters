using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProShooters.Classes
{
    internal class game
    {
        public Health health = new Health();
        public Counters counters = new Counters();
        public Boss boss = new Boss();
        public Enemy1 enemy1 = new Enemy1();
        public Enemy2 enemy2 = new Enemy2();
        public Enemy3 enemy3 = new Enemy3();
        public List<int> highScores = new List<int>();
        public Cursor cursor = new Cursor();
        public Move move = new Move();
        
    }
}
