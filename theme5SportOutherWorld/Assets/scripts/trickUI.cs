using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class trickUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text comboText;
    public TMP_Text landingText;

    private int score = 0;
    private int combo = 0;

    public void UpdateScore(int amount)
    {
        score = amount;
        scoreText.text = "Score: " + score;
    }

    public void UpdateCombo(int amount)
    {
        combo = amount;
        comboText.text = "Combo: x" + combo;
    }

    public void ShowLanding(bool success)
    {
        landingText.text = success ? "Perfect Landing!" : "Crash!";
        landingText.color = success ? Color.green : Color.red;
    }
}
