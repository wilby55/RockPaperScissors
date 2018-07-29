using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class RPSTrainer
    {
        // Definitions
        public const int ROCK = 0, PAPER = 1, SCISSORS = 2, NUM_ACTIONS = 3;
        public Random rnd = new Random();
        double[] regretSum = new double[NUM_ACTIONS],
                 strategy = new double[NUM_ACTIONS],
                 strategySum = new double[NUM_ACTIONS],
                 oppStrategy = { .4, .3, .3 };

        // train
        public void Train(int iterations)
        {
            double[] actionUtility = new double[NUM_ACTIONS];
            for (int i = 0; i < iterations; ++i)
            {
                // Get regret matched mixed strategy actions
                double[] strategy = GetStrategy();
                int myAction = GetAction(strategy);
                int oppAction = GetAction(oppStrategy);

                // Compute action utilities
                actionUtility[oppAction] = 0;
                actionUtility[oppAction == NUM_ACTIONS - 1 ? 0 : oppAction + 1] = 1;
                actionUtility[oppAction == 0 ? NUM_ACTIONS - 1 : oppAction - 1] = -1;

                // Accumulate action regrets
                for (int j = 0; j < NUM_ACTIONS; ++j)
                {
                    regretSum[j] += (actionUtility[j] - actionUtility[myAction]);
                }
            }
        }

        // Get Current mixed strategy through regret-matching
        private double[] GetStrategy()
        {
            double normalizer = 0;
            for (int i = 0; i < NUM_ACTIONS; ++i)
            {
                strategy[i] = regretSum[i] > 0 ? regretSum[i] : 0;
                normalizer += strategy[i];
            }

            for (int i = 0; i < NUM_ACTIONS; ++i)
            {
                if (normalizer > 0)
                {
                    strategy[i] /= normalizer;
                }
                else
                {
                    strategy[i] = 1.0 / NUM_ACTIONS;
                }
                strategySum[i] += strategy[i];
            }

            return strategy;
        }

        // Get random action according to mixed strategy dist
        public int GetAction(double[] strategy)
        {
            double rand = rnd.NextDouble();
            int i = 0;
            double cumProb = 0;

            while (i < NUM_ACTIONS - 1)
            {
                cumProb += strategy[i];
                if (rand < cumProb) { break; }
                ++i;
            }
            return i;
        }
        
        // get average mixed strategy across all training iterations
        public double[] GetAverageStrategy()
        {
            double normalizer = 0;
            double[] averageStrategy = new double[NUM_ACTIONS];

            for (int i = 0; i<NUM_ACTIONS; ++i)
            {
                normalizer += strategySum[i];
            }

            for (int i = 0; i < NUM_ACTIONS; ++i)
            {
                if (normalizer > 0)
                {
                    averageStrategy[i] = strategySum[i] / normalizer;
                }
                else
                {
                    averageStrategy[i] = 1.0 / NUM_ACTIONS;
                }
            }
            return averageStrategy;
        }
    }
}
