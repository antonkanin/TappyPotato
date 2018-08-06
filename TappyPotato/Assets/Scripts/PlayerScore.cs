using System;
using System.Collections.Generic;

namespace PlayerClasses
{
    [Serializable]
    public class Player
    {
        public string player_name = "";
        public string score = "";
        public string death_position = "";
    }

    [Serializable]
    public class ScoreBoard
    {
        public List<Player> Items = null;
    }
}