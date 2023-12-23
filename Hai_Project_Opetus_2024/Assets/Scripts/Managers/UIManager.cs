using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI scoreText; // Assign in the inspector
    public float comboDuration = 2f; // Time in seconds to get the next hit to continue the combo
    public Slider slider;
    private int totalScore = 0;

    private int comboMultiplier = 1;
    private float comboTimer;

    private void Awake()
    {
        // Ensure there's only one UIManager instance (Singleton pattern)
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        slider.maxValue = comboDuration;
    }

    public void AddScore(int score)
    {
        if (comboTimer <= 0)
        {
            comboMultiplier = 1;
        }

        totalScore += score * comboMultiplier;
        scoreText.text = "Score: " + totalScore + " x" + comboMultiplier; // Show the multiplier

        // Reset the combo timer
        
        comboTimer = comboDuration;
    }

    private void Update()
    {
        // Decrease the combo timer
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
            slider.value = comboTimer;
        }
        else if (comboMultiplier > 1)
        {
            comboMultiplier = 1; // Reset multiplier if the timer has expired
            UpdateScoreDisplay(); // Update the score display to reflect the reset
        }
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + totalScore + " x" + comboMultiplier;
    }

    public void IncreaseComboMultiplier(int amount)
    {
        comboMultiplier += amount;
        comboTimer = comboDuration;
        UpdateScoreDisplay();
    }
    public int GetCurrentComboMultiplier()
    {
        return comboMultiplier;
    }
}
