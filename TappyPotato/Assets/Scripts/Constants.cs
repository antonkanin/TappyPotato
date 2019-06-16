﻿using System.Collections;
using System.Collections.Generic;

namespace Constants
{
    public static class Const
    {
        public const string SCORE_FIELD = "score";
        public const string POSITIONX_FIELD = "position_x";
        public const string VERSION_FIELD = "version";
        public const string ACCESS_TOKEN = "access_token";
        public const string AES_KEY = "key";
        public const string AES_IV = "iv";

        //public const string POST_URL = "http://localhost/tappyservice/score_post.php";
        //public const string GET_URL = "http://localhost/tappyservice/score_get.php";

        public const string POST_URL = "http://antonkanin.com/tappyservice/score_post.php";
        public const string GET_URL = "http://antonkanin.com/tappyservice/score_get.php";

        //public const string PLAYER_NAME_PREF = "PlayerName";
        public const string PLAYER_HIGH_SCORE_PREF = "TappyPotato_HighScore";

        public const string FBAppID = "219403772103872";
    }

    public static class TappyTag
    {
        public const string DeadZone = "DeadZone";
        public const string ScoreZone = "ScoreZone";
        public const string DeadZoneSlide = "DeadZoneSlide";
        public const string DeadZoneGround = "DeadZoneGround";
        public const string DeadZoneEye = "DeadZoneEye";
    }
}