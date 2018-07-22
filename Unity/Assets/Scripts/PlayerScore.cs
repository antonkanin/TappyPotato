using System;
using System.Collections.Generic;

namespace PlayerClasses
{
    [Serializable]
    public class Player
    {
        public string player = "";
        public string score = "";
    }

    [Serializable]
    public class ScoreBoard
    {
        public List<Player> Items = null;
    }
}