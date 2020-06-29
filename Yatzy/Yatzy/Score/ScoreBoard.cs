using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatzy.Evaluators;

namespace Yatzy.Score
{
    public class ScoreBoard
    {
        int ID { get; set; }

        private ScoreBoardEntry _ChanceScore = new ScoreBoardEntry("Chance", "Chance");
        private ScoreBoardEntry _OnePairScore = new ScoreBoardEntry("A single pair of dice", "One Pair");
        private ScoreBoardEntry _TwoPairScore = new ScoreBoardEntry("Two pair of dice", "Two Pair");
        private ScoreBoardEntry _ThreePairScore = new ScoreBoardEntry("Three pairs of dice", "Three Pair");
        private ScoreBoardEntry _ThreeOfAkindScore = new ScoreBoardEntry("Three dice with same face value", "ThreeOfkind");
        private ScoreBoardEntry _YatzyScore = new ScoreBoardEntry("Each die has the same value", "Yatzy");

        public ScoreBoard(int PlayerID)
        {
            ID = PlayerID;
        }

        public void CompleteEvaluation(DiceCup cup)
        {
            Dictionary<int, int> D = cup.DiceCountByFaceValue;

            Dictionary<ScoreBoardEntry, int> Evaluations = new Dictionary<ScoreBoardEntry, int>()
            {
                { _ChanceScore, new ChanceEvaluator().Evaluate(D) },
                { _OnePairScore, new OnePairEvaluator().Evaluate(D) },
                { _TwoPairScore, new TwoPairEvaluator().Evaluate(D) },
                { _ThreePairScore, new ThreePairEvaluator().Evaluate(D)},
                { _ThreeOfAkindScore, new ThreeOfAKind().Evaluate(D)},
                { _YatzyScore, new YatzyEvaluation().Evaluate(D)}
            };

            Dictionary<ScoreBoardEntry, int> SavedEvaluations = new Dictionary<ScoreBoardEntry, int>();

            foreach (KeyValuePair<ScoreBoardEntry, int> Eva in Evaluations)
            {
                if (Eva.Value > 0) SavedEvaluations.Add(Eva.Key, Eva.Value);
            }
            Console.WriteLine("Choose");
            int index = 1;
            foreach (KeyValuePair<ScoreBoardEntry, int> Eva in SavedEvaluations)
            {
                Console.WriteLine($"{index} : {Eva.Key.name} = {Eva.Value}");
                index++;
            }
            bool Choosing = true;
            int input;
            while (Choosing)
            {
                ConsoleKeyInfo inputKey = Console.ReadKey();
                try
                {
                    input = int.Parse(inputKey.KeyChar.ToString());
                    if ((input > 0) && (input < SavedEvaluations.Count + 1))
                    {
                        index = 1;
                        foreach (KeyValuePair<ScoreBoardEntry, int> Eva in SavedEvaluations)
                        {
                            if (input == index)
                            {
                                Eva.Key.AddScore(Eva.Value);
                                Choosing = false;
                            }
                            index++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input");
                }
            }

        }

        public int totalScore
        {
            get
            {
                int Total = _ChanceScore.score +
                    _OnePairScore.score +
                    _TwoPairScore.score +
                    _ThreePairScore.score +
                    _ThreeOfAkindScore.score +
                    _YatzyScore.score;

                return Total;
            }
        }

        public string ScoreOutput { 
            get
            {
                return $"{_ChanceScore}\n{_OnePairScore}\n{_TwoPairScore}\n{_ThreePairScore}\n{_ThreeOfAkindScore}\n{_YatzyScore}";
            }
        }


        public override string ToString()
        {
            return $"Player {ID} Score = {totalScore}";
        }
    }
}
