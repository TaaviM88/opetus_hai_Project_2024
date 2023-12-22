using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    private void Awake()
    {
        // Ensure there's only one GameManager instance (Singleton pattern)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep the GameManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    private void Update()
    {
      
        

        switch (currentState)
        {
            case GameState.Gameplay:
                // Handle gameplay logic
                break;
            case GameState.Pause:
                // Handle pause logic
                break;
            case GameState.Cutscene:
                // Handle cutscene logic
                break;
            case GameState.Transition:
                // Handle transition logic
                break;
        }
    }



    // Method to change the game state
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        OnStateChanged(newState);
    }

    // Handle any special logic when the state changes
    private void OnStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.Gameplay:
                // Resume gameplay activities
               // Time.timeScale = 1;
                break;
            case GameState.Pause:
                // Pause gameplay activities
              //  Time.timeScale = 0;
                break;
            case GameState.Cutscene:
                // Prepare and play cutscene
                break;
            case GameState.Transition:
                // Handle transitions between levels or scenes
                break;
        }
    }

    public bool IsGameplay()
    {
        return currentState == GameState.Gameplay;
    }

}
