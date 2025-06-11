using System;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private GameObject _camera;

    private void Awake()
    {
        ResumeGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Checker();
        }
    }

    public void Checker()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    private bool isPaused = false;
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Stop time
        _pauseCanvas.gameObject.SetActive(true); // Activate pause canvas
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
    }

    // Method to resume the game
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume time
        _pauseCanvas.gameObject.SetActive(false); // Deactivate pause canvas
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
    }

    // Method to toggle pause/resume
    public void TogglePause()
    {
        if (Time.timeScale == 0f)
        {
            ResumeGame(); // If the game is paused, resume it
        }
        else
        {
            PauseGame(); // If the game is running, pause it
        }
    }
}