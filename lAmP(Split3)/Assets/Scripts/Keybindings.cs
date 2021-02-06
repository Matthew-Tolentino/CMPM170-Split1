// Code based off of https://www.youtube.com/watch?v=edERdn2NmNA&t=161s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keybindings", menuName = "Keybindings")]
public class Keybindings : ScriptableObject
{
    public KeyCode jump, spiritAbility, pause, nextDialogue;

    public KeyCode CheckKey(string key)
    {
        switch (key)
        {
            case "Jump":
                return jump;

            case "SpiritAbility":
                return spiritAbility;

            case "Pause":
                return pause;

            case "NextDialogue":
                return nextDialogue;

            default:
                return KeyCode.None;
        }
    }
}
