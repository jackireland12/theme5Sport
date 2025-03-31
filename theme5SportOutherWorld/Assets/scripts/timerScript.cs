using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timerScript : MonoBehaviour
{
    public float timeElapsed = 0f;   // Time in seconds
    public TMP_Text timerText;        // Reference to the TextMeshPro UI Text component

    void Update()
    {
        // Increment the timer
        timeElapsed += Time.deltaTime;

        // Update the UI text to show the timer value
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        // Display the time in minutes:seconds format
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);

        // Update the timerText with the formatted string
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
