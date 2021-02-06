﻿// Code based on Brackeys: https://www.youtube.com/watch?v=_nRzoTzeyxU&t=783s
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // Makes DialogueManager into a singleton
    public static DialogueManager instance;

    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI dialogueTxt;

    public Animator animator;

    private Queue<string> sentences;

    // Make sure there is only 1 DialogueManager
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

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    // TODO: Implement overall input manager
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        DisplayNextSentence();
    //    }
    //}

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        nameTxt.SetText(dialogue.name);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        // Problem can happen if other coroutines are running
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        string txt = "";
        foreach (char letter in sentence.ToCharArray())
        {
            txt += letter;
            dialogueTxt.SetText(txt);
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
}
