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
            Console.WriteLine("Player 1: ");
            trainer.GetAverageStrategy_player1().ToList().ForEach(Console.WriteLine);

            Console.WriteLine("\nPlayer 2: ");
            trainer.GetAverageStrategy_player2().ToList().ForEach(Console.WriteLine);
        }
    }
}
