using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Makes GameManager into a singleton
    public static GameManager instance;

    public static bool gameIsPaused = false;

    public enum MouseState { canvas, game };
    public static MouseState mouseState = MouseState.game;

    public GameObject pauseMenuCanvasUI;
    public GameObject pauseMenuUI;

    public CameraSettings camSettings;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        // All player key press logic goes here
        // Pause game
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

        // Next Dialogue
        if (InputManager.instance.KeyDown("NextDialogue"))
        {
            DialogueManager.instance.DisplayNextSentence();
        }
    }

    void Resume()
    {
        // Close pause menu, unfreeze time in game, and mark game as unpaused
        pauseMenuCanvasUI.SetActive(false);
        //Time.timeScale = 1f;
        gameIsPaused = false;

        // Enable game control of camera (A and D)
        setMouseLock(false);

        // Set mouse state back to game
        mouseState = MouseState.game;
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

        // Set mouse state to canvas
        mouseState = MouseState.canvas;
    }

    void UpdateUI()
    {
        pauseMenuUI.GetComponent<PausedMenu>().displaySpiritsOnUI();
    }

    public void setMouseLock(bool isLocked)
    {
        if (isLocked)
        {
            // Hide mouse and lock it to center
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Turn on camera control
            setCameraControl(true);
        }
        else
        {
            // Show mouse and unlock it
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            setCameraControl(false);
        }
    }

    public void setCameraControl(bool isEnabled)
    {
        if (isEnabled)
        {
            // let mouse control camera
            camSettings.inputXAxis = "Mouse X";
            camSettings.inputYAxis = "Mouse Y";
        }
        else if (!isEnabled && gameIsPaused)
        {
            camSettings.inputXAxis = "";
            camSettings.inputYAxis = "";
        }
        else
        {
            // let A and D control camera
            camSettings.inputXAxis = "Horizontal";
            camSettings.inputYAxis = "";
        }
    }
}
