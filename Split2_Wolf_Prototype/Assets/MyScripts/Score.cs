using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public int score;
    public Text ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        this.score = -2; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter()
    {
       this.score++;
       ScoreText.text = score.ToString();

    }
}
