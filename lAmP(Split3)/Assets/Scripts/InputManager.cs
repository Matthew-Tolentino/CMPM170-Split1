// Code based off of https://www.youtube.com/watch?v=edERdn2NmNA&t=161s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public Keybindings keybindings;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public bool GetKey(string key)
    {
        if (Input.GetKey(keybindings.CheckKey(key)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool KeyDown(string key)
    {
        if (Input.GetKeyDown(keybindings.CheckKey(key)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
