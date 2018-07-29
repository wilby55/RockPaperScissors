using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            RPSTrainer trainer = new RPSTrainer();
            //trainer.train(100);
            Console.WriteLine("---------------------------------");
            //Console.WriteLine(getAverageStrategy().toSting());

            Console.WriteLine(trainer.rnd.NextDouble());
        }
    }
}
