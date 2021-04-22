using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
///Notes
///1. Check for a new high score.
///2. If a new highscore has been reached then we can put it on the
///leaderboard.
///3. Shift the lower scores down one position in the leaderboard,
///while removing the score at position 5.
///4. Put in the new score at that position and then ask for the player's
///initials to be displayed on the board. 

[System.Serializable]
public struct LeaderBoardInfo
{
    public string posName;
    public Text rankText;
    public Text initials;
    public Text scoreText;
}

public class LeaderBoard : SingletonPattern<LeaderBoard>
{

    public List<LeaderBoardInfo> leaderBoardInfo = new List<LeaderBoardInfo>();

    #region Score tags
    private readonly string boardOneScore = "Leaderboard 1 Score";
    private readonly string boardTwoScore = "Leaderboard 2 Score";
    private readonly string boardThreeScore = "Leaderboard 3 Score";
    private readonly string boardFourScore = "Leaderboard 4 Score";
    private readonly string boardFiveScore = "Leaderboard 5 Score";
    #endregion

    #region Initial tags
    private readonly string boardOneInitial = "Leaderboard 1 Initial";
    private readonly string boardTwoInitial = "Leaderboard 2 Initial";
    private readonly string boardThreeInitial = "Leaderboard 3 Initial";
    private readonly string boardFourInitial = "Leaderboard 4 Initial";
    private readonly string boardFiveInitial = "Leaderboard 5 Initial";
    #endregion

    private List<string> boardScoreKeys = new List<string>();
    private List<string> boardInitialKeys = new List<string>();

    private bool gettingInitials { get; set; } = false;

    public InputField initialField;
    public Text FinalScoreText;

    public int initialsGoto = 0;

    protected override void Awake()
    {
        base.Awake();

        #region Adding in score keys
        boardScoreKeys.Add(boardOneScore);
        boardScoreKeys.Add(boardTwoScore);
        boardScoreKeys.Add(boardThreeScore);
        boardScoreKeys.Add(boardFourScore);
        boardScoreKeys.Add(boardFiveScore);
        #endregion

        #region Adding in initial keys
        boardInitialKeys.Add(boardOneInitial);
        boardInitialKeys.Add(boardTwoInitial);
        boardInitialKeys.Add(boardThreeInitial);
        boardInitialKeys.Add(boardFourInitial);
        boardInitialKeys.Add(boardFiveInitial);
        #endregion

        foreach (string prefKey in boardScoreKeys)
        {
            if (!PlayerPrefs.HasKey(prefKey))
            {
                PlayerPrefs.SetInt(prefKey, 0);
            }
        }

        foreach (string initKey in boardInitialKeys)
        {
            if (!PlayerPrefs.HasKey(initKey))
            {
                PlayerPrefs.SetString(initKey, "");
            }
        }
        initialField.text = "Enter your initials";
    }

    private void Update()
    {
        if (!gettingInitials) return;
        initialField.characterLimit = 3;
        initialField.Select();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerPrefs.SetString(boardInitialKeys[initialsGoto], initialField.text);
            UpdateBoard();
        }

    }

    public void GameOver(int score)
    {
        if (CheckForNewHighScore(score))
        {
            AddScoreToPrefs(initialsGoto, score);
            gettingInitials = true;
        }
    }

    private bool CheckForNewHighScore(int score)
    {
        FinalScoreText.text = score.ToString();

        for (int i = 0; i < boardScoreKeys.Count; i++)
        {
            if (PlayerPrefs.GetInt(boardScoreKeys[i]) < score)
            {
                initialsGoto = i;
                return true;
            }
        }
        return false;
    }

    void AddScoreToPrefs(int boardPos, int score)
    {
        int index = 4;
        if (boardPos == 4)
        {
            PlayerPrefs.SetInt(boardFiveScore, score);
            return;
        }

        while (true)
        {
            PlayerPrefs.SetInt(boardScoreKeys[index], PlayerPrefs.GetInt(boardScoreKeys[index - 1]));
            PlayerPrefs.SetString(boardInitialKeys[index], PlayerPrefs.GetString(boardInitialKeys[index - 1]));

            index--;

            if (index == boardPos)
                break;
        }

        PlayerPrefs.SetInt(boardScoreKeys[boardPos], score);
        PlayerPrefs.SetString(boardInitialKeys[boardPos], "");

    }

    private void UpdateBoard()
    {
        int index = 0;

        foreach (LeaderBoardInfo info in leaderBoardInfo)
        {
            info.initials.text = PlayerPrefs.GetString(boardInitialKeys[index]);
            info.scoreText.text = PlayerPrefs.GetInt(boardScoreKeys[index]).ToString();
            index++;
        }
    }
}
