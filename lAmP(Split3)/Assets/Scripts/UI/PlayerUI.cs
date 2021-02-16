using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI selectedSpiritText;

    void Start()
    {
        selectedSpiritText = GameObject.Find("SpiritSelected").GetComponent<TextMeshProUGUI>();
    }

    // Call this function to change spirit selected in UI
    public void SelectSpirit(string name)
    {
        selectedSpiritText.SetText("Spirit Selected: " + name);
    }
}
