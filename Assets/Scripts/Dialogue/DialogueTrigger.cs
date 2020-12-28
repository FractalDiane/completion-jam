using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] TextAsset story;

    [SerializeField] UnityEvent endEvent;
    // Start is called before the first frame update

    void OnTriggerStay(Collider other)
    {
        
    }

    public void StartDialogue()
    {
        if(!DialogueManager.dialogueManagerInstance.IsPlayerConversing())
        {
            DialogueManager.dialogueManagerInstance.StartStory(story, endEvent);
        }
    }
}
