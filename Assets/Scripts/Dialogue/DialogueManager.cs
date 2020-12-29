using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    
    public static DialogueManager dialogueManagerInstance;
    static Story story;

    List<string> tags;

    #region Dialogue Box Constants
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;
    #endregion

    IEnumerator coroutine;

    //For Dialogue Box Animations
    [SerializeField] Animator animator;

    #region Checks
    [SerializeField] bool playerCanProceed;
    [SerializeField] bool playerInConversation;
    #endregion

    [SerializeField] UnityEvent dialogueEndEvent;

    void Start()
    {
        tags = new List<string>();
        dialogueManagerInstance = this;
    }

    void Update()
    {
        if(playerCanProceed && playerInConversation && Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
        {
            if(story!= null)
            {
                if(story.canContinue)
                {
                    AdvanceDialogue();
                }
                else
                {
                    FinishDialogue();    
                }
            }
        }
    }

    public void StartStory(TextAsset inkFile, UnityEvent endEvent = null)
    {
        dialogueEndEvent = endEvent;
        story = new Story(inkFile.text);
        animator.SetBool("isOpen", true);
        playerInConversation = true;
        AdvanceDialogue();
    }

    void AdvanceDialogue()
    {
        string currentSentence = story.Continue();
        ParseTags();
        if(coroutine!=null)
        {
            StopCoroutine(coroutine);
            
        }
        coroutine = TypeSentence(currentSentence);
        StartCoroutine(coroutine);

    }

    IEnumerator TypeSentence(string sentence)
    {
        playerCanProceed = false;
        dialogueText.text = "";
        for(int i = 1; i < sentence.Length; ++i)
        {
            dialogueText.text = sentence.Substring(0,i) + "<color=#ffffff00>" + sentence.Substring(i)+ "</color>";
            yield return null;
            if(Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
            {
                dialogueText.text = sentence;
                playerCanProceed = true;
                yield break;
            }
        }
        playerCanProceed = true;
    }

    void FinishDialogue()
    {
        animator.SetBool("isOpen", false);
        playerInConversation = false;
        playerCanProceed = false;
        TriggerEndBevhavior();
    }

    void TriggerEndBevhavior()
    {
        if(dialogueEndEvent != null)
        {
            dialogueEndEvent.Invoke();
        }
    }
    void SkipDialogue()
    {
        if(playerInConversation)
        {
            story = null;
            FinishDialogue();
        }
    }
    
    void ParseTags()
    {
        tags = story.currentTags;
        foreach (string t in tags)
        {
            string prefix = t.Split(' ')[0];
            string param = t.Split(' ')[1];

            switch(prefix.ToLower())
            {
                case "char":
                    SetNameText(param);
                    break;
            }
        }
    }

    void SetNameText(string _name)
    {
        nameText.text = _name;
    }

    public bool IsPlayerConversing()
    {
        return playerInConversation;
    }
}
