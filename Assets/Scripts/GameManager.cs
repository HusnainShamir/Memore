using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Block[] blocks; // Assign 9 blocks in Inspector
    public TMP_Text streakText;

    private List<int> sequence = new();
    private int playerIndex = 0;

    private bool playerTurn = false;

    public int streak = 0;

    private void Start()
    {
        StartCoroutine(StartRound());
    }

    IEnumerator StartRound()
    {
        playerTurn = false;
        playerIndex = 0;

        yield return new WaitForSeconds(1f);

        sequence.Add(Random.Range(0, blocks.Length));

        yield return StartCoroutine(ShowSequence());

        playerTurn = true;
    }

    IEnumerator ShowSequence()
    {
        foreach (int index in sequence)
        {
            blocks[index].LightOn();

            yield return new WaitForSeconds(0.5f);

            blocks[index].LightOff();

            yield return new WaitForSeconds(0.2f);
        }
    }

    public void BlockClicked(int blockIndex)
    {
        if (!playerTurn)
            return;

        if (blockIndex == sequence[playerIndex])
        {
            playerIndex++;

            if (playerIndex >= sequence.Count)
            {
                streak++;
                streakText.text = "Streak: " + streak;

                StartCoroutine(StartRound());
            }
        }
        else
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");

        sequence.Clear();
        streak = 0;

        streakText.text = "Streak: 0";

        StartCoroutine(StartRound());
    }
}