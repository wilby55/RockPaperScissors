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
        double[] regretSum_player1 = new double[NUM_ACTIONS],
                 regretSum_player2 = new double[NUM_ACTIONS],
                 strategy_player1 = new double[NUM_ACTIONS],
                 strategy_player2 = new double[NUM_ACTIONS],
                 strategySum_player1 = new double[NUM_ACTIONS],
                 strategySum_player2 = new double[NUM_ACTIONS],
                 oppStrategy = { .4, .3, .3 };

        // train
        public void Train(int iterations)
        {
            double[] actionUtility_player1 = new double[NUM_ACTIONS];
            double[] actionUtility_player2 = new double[NUM_ACTIONS];
            for (int i = 0; i < iterations; ++i)
            {
                // Get regret matched mixed strategy actions
                double[] strategy_player1 = GetStrategy_player1();
                int myAction = GetAction(strategy_player1);

                double[] strategy_player2 = GetStrategy_player2();
                int oppAction = GetAction(strategy_player2);

                // Compute action utilities
                actionUtility_player1[oppAction] = 0;
                actionUtility_player1[oppAction == NUM_ACTIONS - 1 ? 0 : oppAction + 1] = 1;
                actionUtility_player1[oppAction == 0 ? NUM_ACTIONS - 1 : oppAction - 1] = -1;

                // Compute action utilities
                actionUtility_player2[myAction] = 0;
                actionUtility_player2[myAction == NUM_ACTIONS - 1 ? 0 : myAction + 1] = 1;
                actionUtility_player2[myAction == 0 ? NUM_ACTIONS - 1 : myAction - 1] = -1;

                // Accumulate action regrets
                for (int j = 0; j < NUM_ACTIONS; ++j)
                {
                    regretSum_player1[j] += (actionUtility_player1[j] - actionUtility_player1[myAction]);
                }

                // Accumulate action regrets
                for (int j = 0; j < NUM_ACTIONS; ++j)
                {
                    regretSum_player2[j] += (actionUtility_player2[j] - actionUtility_player2[myAction]);
                }
            }
        }

        // Get Current mixed strategy through regret-matching
        private double[] GetStrategy_player1()
        {
            double normalizer = 0;
            for (int i = 0; i < NUM_ACTIONS; ++i)
            {
                strategy_player1[i] = regretSum_player1[i] > 0 ? regretSum_player1[i] : 0;
                normalizer += strategy_player1[i];
            }

            for (int i = 0; i < NUM_ACTIONS; ++i)
            {
                if (normalizer > 0)
                {
                    strategy_player1[i] /= normalizer;
                }
                else
                {
                    strategy_player1[i] = 1.0 / NUM_ACTIONS;
                }
                strategySum_player1[i] += strategy_player1[i];
            }

            return strategy_player1;
        }

        // Get Current mixed strategy through regret-matching
        private double[] GetStrategy_player2()
        {
            double normalizer = 0;
            for (int i = 0; i < NUM_ACTIONS; ++i)
            {
                strategy_player2[i] = regretSum_player2[i] > 0 ? regretSum_player2[i] : 0;
                normalizer += strategy_player2[i];
            }

            for (int i = 0; i < NUM_ACTIONS; ++i)
            {
                if (normalizer > 0)
                {
                    strategy_player2[i] /= normalizer;
                }
                else
                {
                    strategy_player2[i] = 1.0 / NUM_ACTIONS;
                }
                strategySum_player2[i] += strategy_player2[i];
            }

            return strategy_player2;
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
        public double[] GetAverageStrategy_player1()
        {
            double normalizer = 0;
            double[] averageStrategy_player1 = new double[NUM_ACTIONS];

            for (int i = 0; i<NUM_ACTIONS; ++i)
            {
                normalizer += strategySum_player1[i];
            }

            for (int i = 0; i < NUM_ACTIONS; ++i)
            {
                if (normalizer > 0)
                {
                    averageStrategy_player1[i] = strategySum_player1[i] / normalizer;
                }
                else
                {
                    averageStrategy_player1[i] = 1.0 / NUM_ACTIONS;
                }
            }
            return averageStrategy_player1;
        }

        // get average mixed strategy across all training iterations
        public double[] GetAverageStrategy_player2()
        {
            double normalizer = 0;
            double[] averageStrategy_player2 = new double[NUM_ACTIONS];

            for (int i = 0; i < NUM_ACTIONS; ++i)
            {
                normalizer += strategySum_player2[i];
            }

            for (int i = 0; i < NUM_ACTIONS; ++i)
            {
                if (normalizer > 0)
                {
                    averageStrategy_player2[i] = strategySum_player2[i] / normalizer;
                }
                else
                {
                    averageStrategy_player2[i] = 1.0 / NUM_ACTIONS;
                }
            }
            return averageStrategy_player2;
        }
    }
}
