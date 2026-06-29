using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Blocks")]
    public GridBlock[] blocks;

    [Header("UI")]
    public TMP_Text streakText;
    public TMP_Text highScoreText;
    public TMP_Text messageText;

    List<int> sequence = new List<int>();

    int currentInputIndex = 0;
    int streak = 0;
    int highScore = 0;

    bool playerTurn = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
        StartGame();
    }

    public void StartGame()
{
    StopAllCoroutines();

    sequence.Clear();

    streak = 0;

    UpdateUI();

    AddNewStep();

    StartCoroutine(PlaySequence());
}
    void AddNewStep()
    {
        sequence.Add(Random.Range(0, 9));
    }
    IEnumerator PlaySequence()
    {
        playerTurn = false;

        SetBlocksInteractable(false);

        messageText.text = "Watch";

        yield return new WaitForSeconds(1f);

        foreach (int index in sequence)
        {
            yield return blocks[index].StartCoroutine(blocks[index].Flash());
            yield return new WaitForSeconds(0.2f);
        }

        currentInputIndex = 0;

        playerTurn = true;

        SetBlocksInteractable(true);

        messageText.text = "Repeat";
    }
    public void BlockPressed(int blockIndex){
    if (!playerTurn)
        return;

    if (blockIndex == sequence[currentInputIndex])
    {
        currentInputIndex++;

        // Sequence completed
        if (currentInputIndex >= sequence.Count)
        {
            streak++;

            difficultyProg(streak);

            UpdateUI();

            AddNewStep();

            StartCoroutine(PlaySequence());
        }
    }
    else
    {
        GameOver();
    }
}
    void GameOver(){
    playerTurn = false;

    SetBlocksInteractable(false);

    if (streak > highScore)
    {
        highScore = streak;
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    UpdateUI();

    messageText.text =
        $"Game Over\nStreak: {streak}";

    Invoke(nameof(RestartGame), 2f);
    }

    void RestartGame()
    {
        StartGame();
    }

    void UpdateUI()
    {
        streakText.text = "Streak: " + streak;

        highScoreText.text = "Best: " + highScore;
    }

    void SetBlocksInteractable(bool value)
    {
        foreach (GridBlock block in blocks)
        {
            block.SetInteractable(value);
        }
    }
    void difficultyProg(int streak)
    {
        foreach (GridBlock block in blocks)
        {
            block.t = Mathf.Clamp(0.5f - (streak * 0.02f),0.15f,0.5f);
        }
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}