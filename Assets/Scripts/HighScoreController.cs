using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreController : MonoBehaviour
{
    public long[] highscores = new long[10];

    public bool IsHighScore(long score)
    {
        return (score > highscores[0]);
    }

    public bool AddHighScore(long score)
    {
        if (!IsHighScore(score))
        {
            return false;
        }
        else
        {
            for (int i = 9; i > 0; i--)
            {
                highscores[i] = highscores[i - 1];
            }
            highscores[0] = score;
            return true;
        }
    }
}
