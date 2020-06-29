using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Evaluators
{
    class ThreePairEvaluator : IDiceEvaluator
    {
        public int Evaluate(Dictionary<int, int> diceCountByValue)
        {
            int score = 0;

            int[] pair = new int[3] { 0, 0, 0 };

            foreach (KeyValuePair<int, int> d in diceCountByValue)
            {
                int faceValue = d.Key;
                int noOfDice = d.Value;

                if (noOfDice == 2)
                {
                    for (int i = 0; i < pair.Length; i++)
                    {
                        if (pair[i] == 0)
                        {
                            pair[i] = faceValue;
                            break;
                        } 
                    }
                }
            }

            foreach (int faceValue in pair)
            {
                if (faceValue == 0) return 0;
                else
                {
                    score += faceValue * 2;
                }
            }

            return score;
        }
    }
}
