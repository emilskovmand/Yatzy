using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Score
{
    class ScoreBoardEntry
    {
        private string _desc;
        private string _entryName;
        internal int score;

        public ScoreBoardEntry(string description, string name)
        {
            _desc = description;
            _entryName = name;
        }


        public void AddScore(int amount)
        {
            score += amount;
        }

        public override string ToString()
        {
            return $"{_entryName} : {score}";
        }

        public string name
        {
            get
            {
                return _entryName;
            }
        }

        public string Description
        {
            get
            {
                return _desc;
            }
        }
    }
}
