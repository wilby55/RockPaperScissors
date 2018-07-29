using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class RPSTrainer
    {
        //Definitions
        public const int ROCK = 0, PAPER = 1, SCISSORS = 2, NUM_ACTIONS = 3;
        public Random rnd = new Random();
        double[] regretSum = new double[NUM_ACTIONS],
                 strategy = new double[NUM_ACTIONS],
                 strategySum = new double[NUM_ACTIONS],
                 oppStrategy = { .4, .3, .3 };

        //Get Current mixed strategy through regret-matching

        // Get random action according to mixed strategy dist

        //train

        //get average mixed strategy across all training iterations

        // main method initializing computation
    }
}
