using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefs 
{
    public static int BESTSCORE
    {
        set
        {
            if(PlayerPrefs.GetInt(Const.BEST_SCORE_KEY,0)< value)
            {
                PlayerPrefs.SetInt(Const.BEST_SCORE_KEY, value);
            }
        }
        get => PlayerPrefs.GetInt(Const.BEST_SCORE_KEY);
    }
}
