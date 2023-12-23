using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI scoreText; // Assign in the inspector
    private int totalScore = 0;

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
    }

    public void AddScore(int score)
    {
        totalScore += score;
        scoreText.text = "Score: " + totalScore;
    }

   
}
