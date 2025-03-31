using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class win : MonoBehaviour
{
    public GameObject winScreenUI; // UI panel for the win screen
    public TMP_Text finalTimeText; // UI text to display final time
    public TMP_Text trickScoreText; // UI text to display trick score
    public TMP_Text finalScoreText; // UI text to display final score

    public timerScript timerScript; // Reference to the timer
    public TrickScript trickScript; // Reference to the trick script

    private bool hasFinished = false;

    void Start()
    {
        winScreenUI.SetActive(false); // Hide win screen at start
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasFinished)
        {
            hasFinished = true;
            ShowWinScreen();
        }
    }

    void ShowWinScreen()
    {
        winScreenUI.SetActive(true); // Show win screen

        float finalTime = timerScript.timeElapsed;
        int trickScore = trickScript.trickint;

        int finalScore = Mathf.FloorToInt(finalTime) - (trickScore/1000); // Subtract trick score from time

        finalTimeText.text = "Time: " + Mathf.FloorToInt(finalTime) + "s";
        trickScoreText.text = "Trick Score: " + trickScore;
        finalScoreText.text = "Final Score: " + finalScore;
    }

}
