using System;
using System.Collections.Generic;
using Yatzy.Evaluators;
using Yatzy.Score;

namespace Yatzy
{
    /// <summary>
    /// This class manages - i.e. sets up and runs - a
    /// game of Yatzy. The class should be extended
    /// considerably to implement a full game of Yatzy.
    /// </summary>
    public class GameManager
    {
        #region Instance fields
        private DiceCup _cup;
        private Dictionary<string, IDiceEvaluator> _diceEvaluators;

        private int _numberOfDice;
        private int _noOfFaces;
        #endregion

        #region Constructor
        public GameManager(int numberOfDice, int noOfFaces)
        {
            _cup = new DiceCup(numberOfDice, noOfFaces);
            _numberOfDice = numberOfDice;
            _noOfFaces = noOfFaces;

            _diceEvaluators = new Dictionary<string, IDiceEvaluator>();
            _diceEvaluators.Add("Chance", new ChanceEvaluator());
            _diceEvaluators.Add("One Pair", new OnePairEvaluator());
            _diceEvaluators.Add("Two Pair", new TwoPairEvaluator());
            _diceEvaluators.Add("Three pair", new ThreePairEvaluator());
            _diceEvaluators.Add("Three of a kind", new ThreeOfAKind());
            _diceEvaluators.Add("Yatzy", new YatzyEvaluation());
        }
        #endregion

        #region Methods
        public void Run()
        {
            this.Yatzy();
        }

        private void Yatzy()
        {
            int turn = 0;
            int turnCount = 3;

            ScoreBoard Player1 = new ScoreBoard(1);
            ScoreBoard Player2 = new ScoreBoard(2);

            ScoreBoard CurrentPlayer = Player1;
            while (turn <= turnCount)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{CurrentPlayer}");
                if (turn == 0)
                {
                    _cup.Shake();
                    turn++;
                }

                bool[] choice = Behaviour(turn, turnCount, CurrentPlayer);

                if (choice[0]) turn++;
                if (turn > turnCount) choice[1] = true;

                //End of turn
                if (choice[1] && CurrentPlayer == Player1)
                {
                    CurrentPlayer = Player2;
                    turn = 0;
                    Console.Clear(); 
                    _cup.UnSaveDice();
                }
                else if (choice[1] && CurrentPlayer == Player2)
                {
                    CurrentPlayer = Player1;
                    turn = 0;
                    Console.Clear();
                    _cup.UnSaveDice();
                }
            }
        }

        private bool[] Behaviour(int Turn, int tCount, ScoreBoard Player)
        {
            // First index = Turn used, Second index = End of turn
            bool[] choiceArray = new bool[2] { false, false };
            Console.WriteLine(_cup);
            Console.Write(
                $"\n{tCount - Turn} rolls left...\n" +
                $"== 1 : To Roll \t\t\t\t==\n" +
                $"== 2 : Select dive to save\t\t==\n" +
                $"== 3 : See Scoreboard\t\t\t==\n" +
                $"== 4 : Input score to scoreboard \t==\n" +
                $""
                );
            ConsoleKeyInfo Choice = Console.ReadKey();
            Console.WriteLine();
            switch (Choice.Key)
            {
                case ConsoleKey.D1:
                    _cup.Shake();
                    choiceArray[0] = true;
                    break;

                case ConsoleKey.D2:
                    Console.WriteLine("Select dice by index number, and put space between each choice.");
                    string[] indexNumbers = Console.ReadLine().Split();
                    _cup.SaveDice(indexNumbers);
                    break;

                case ConsoleKey.D3:
                    Console.WriteLine(Player.ScoreOutput);
                    break;

                case ConsoleKey.D4:
                    Player.CompleteEvaluation(_cup);
                    choiceArray[1] = true;
                    break;

                default:
                    Console.WriteLine("Invalid input");
                    break;
            }

            return choiceArray;
        }
        #endregion
    }
}