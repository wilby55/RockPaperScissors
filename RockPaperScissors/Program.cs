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
            trainer.Train(1000000);
            Console.WriteLine("---------------------------------");
            trainer.GetAverageStrategy().ToList().ForEach(Console.WriteLine);
        }
    }
}
