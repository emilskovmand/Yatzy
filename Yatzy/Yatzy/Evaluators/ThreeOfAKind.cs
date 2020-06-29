using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Evaluators
{
    class ThreeOfAKind : IDiceEvaluator
    {
        public int Evaluate(Dictionary<int, int> diceCountByValue)
        {
            int score = 0;

            foreach (KeyValuePair<int,int> d in diceCountByValue)
            {
                int faceValue = d.Key;
                int noOfDice = d.Value;

                if ((noOfDice >= 3) && ((faceValue * 3) > score))
                {
                    score = faceValue * 3;
                }
            }

            return score;
        }
    }
}
