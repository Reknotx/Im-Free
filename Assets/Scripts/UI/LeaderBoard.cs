using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    private readonly string boardOneScore = "Leaderboard 1 Score";
    private readonly string boardTwoScore = "Leaderboard 2 Score";
    private readonly string boardThreeScore = "Leaderboard 3 Score";
    private readonly string boardFourScore = "Leaderboard 4 Score";
    private readonly string boardFiveScore = "Leaderboard 5 Score";


    private List<string> boardKeys = new List<string>();

    private void Awake()
    {
        boardKeys.Add(boardOneScore);
        boardKeys.Add(boardTwoScore);
        boardKeys.Add(boardThreeScore);
        boardKeys.Add(boardFourScore);
        boardKeys.Add(boardFiveScore);
        
        foreach (string prefKey in boardKeys)
        {
            if (!PlayerPrefs.HasKey(prefKey))
            {
                PlayerPrefs.SetInt(prefKey, 0);
            }
        }
    }

    public void TakeInitials()
    {
        
    }

    public void CheckForNewHighScore(int score)
    {
        for (int i = 0; i < boardKeys.Count; i++)
        {
            if (PlayerPrefs.GetInt(boardKeys[i]) < score)
            {

            }
        }
    }

    public void AddScoreToLeaderBoard(int boardPos, int score)
    {
        switch (boardPos)
        {
            case 1:
                break;

            case 2:
                break;

            case 3:
                break;

            case 4:
                break;

            case 5:
                break;


            default:
                break;
        }
    }

    public void ShiftScoresDown()
    {

    }
}
