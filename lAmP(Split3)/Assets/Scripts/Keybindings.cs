using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keybindings", menuName = "Keybindings")]
public class Keybindings : ScriptableObject
{
    public KeyCode jump, spiritAbility, pause;

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

            default:
                return KeyCode.None;
        }
    }
}
