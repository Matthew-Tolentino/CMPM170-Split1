using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuCanvasUI;
    public GameObject pauseMenuUI;

    void Update()
    {
        // All player key press logic goes here
        if (InputManager.instance.KeyDown("Pause"))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        // Close pause menu, unfreeze time in game, and mark game as unpaused
        pauseMenuCanvasUI.SetActive(false);
        //Time.timeScale = 1f;
        gameIsPaused = false;

        // Lock mouse
        setMouseLock(true);
    }

    void Pause()
    {
        // Bring up pause menu, freeze time in game, and mark game as paused
        pauseMenuCanvasUI.SetActive(true);
        //Time.timeScale = 0f;
        gameIsPaused = true;

        // Update UI for spirits found
        Invoke(nameof(UpdateUI), 1f);

        // Unlock mouse
        setMouseLock(false);
    }

    void UpdateUI()
    {
        pauseMenuUI.GetComponent<PausedMenu>().displaySpiritsOnUI();
    }

    public static void setMouseLock(bool isLocked)
    {
        if (isLocked)
        {
            // Hide mouse and lock it to center
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Turn on camera control
            CameraSettings.instance.setCameraControl(true);
        }
        else
        {
            // Show mouse and unlock it
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            CameraSettings.instance.setCameraControl(false);
        }
    }
}
