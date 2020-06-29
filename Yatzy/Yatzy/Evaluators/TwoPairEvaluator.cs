using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Evaluators
{
    class TwoPairEvaluator : IDiceEvaluator
    {


        public int Evaluate(Dictionary<int, int> diceCountByValue)
        {
            // Første postion = FaceValue, Anden postion = Antal
            int score = 0;
 
            foreach (KeyValuePair<int, int> dieCount in diceCountByValue)
            {
                int faceValue = dieCount.Key;
                int noOfDice = dieCount.Value;

                if (noOfDice >= 2)
                {
                    foreach(KeyValuePair<int, int> d in diceCountByValue)
                    {
                        // Hvis det er samme ansigt, så fortsæt
                        // Hvis "noOfDice" er større end to
                        // Hvis scoren med den nye terning er større end den tidligere score, så sæt den igen med den nye udregning
                        if ((d.Key != faceValue) && (d.Value >= 2) && (d.Key * 2) + (faceValue * 2) > score)
                        {
                            score = (d.Key * 2) + (faceValue * 2);
                        }
                    }
                }
            }


            return score;
        }
    }
}
