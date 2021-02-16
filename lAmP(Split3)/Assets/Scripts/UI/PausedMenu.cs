using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PausedMenu : MonoBehaviour
{
    public GameObject spriteUI;

    
    private List<string> spiritsInLevel;
    private GameObject spiritUIHolder;

    void Start()
    {
        spiritsInLevel = new List<string>();
        spiritUIHolder = GameObject.Find("SpiritUIHolder");

        // Set up UI for all the spirits inside the level
        // NOTE: Can be a problem if more than 6
        GetSpiritsInLevel();

        for (int i = 0; i < spiritsInLevel.Count; i++)
        {
            Instantiate(spriteUI, spiritUIHolder.transform);
        }

        int index = 0;
        foreach (Transform child in spiritUIHolder.transform)
        {
            child.name = spiritsInLevel[index] + "UI";
            child.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(spiritsInLevel[index++]);
        }
    }

    // Checks to see what spirits have been found and updates UI
    public void displaySpiritsOnUI()
    {
        List <GameObject> foundSpirits = FindObjectOfType<SpiritHandler>().SpiritList;
        
        foreach (string spirit in spiritsInLevel)
        {
            GameObject tempSpiritUI = GameObject.Find(spirit + "UI");
            Image img = tempSpiritUI.GetComponent<Image>();
            Color tmpColor = img.color;
            for (int i = 0; i < foundSpirits.Count; i++)
            {
                if (foundSpirits[i] != null)
                {
                    if (foundSpirits[i].name == spirit)
                    {
                        tmpColor.a = 1f;
                        break;
                    }
                }
                tmpColor.a = 0.3f;
            }
            img.color = tmpColor;
        }
    }

    private void GetSpiritsInLevel()
    {
        GameObject spiritHolder = GameObject.Find("Spirits");
        foreach (Transform child in spiritHolder.transform)
        {
            spiritsInLevel.Add(child.name);
        }
    }

    public void ReturnToMainMenu()
    {
        transform.parent.gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
