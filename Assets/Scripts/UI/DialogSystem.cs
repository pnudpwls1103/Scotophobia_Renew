using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public string[] dialogues;

    private const float TEXTSPEED = 0.05f;

    public TMPro.TextMeshProUGUI dialogueText;
    private int dialogueIndex = 0;
    private bool isTyping = false;
    private Coroutine coroutine;

    public void Setup()
    {
        InActiveObjects();
        SetNextDialog();
    }

    public bool UpdateDialog()
    {
        ActiveObjects();
        
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping == true)
            {
                StopCoroutine(coroutine);
                isTyping = false;
                dialogueText.text = dialogues[dialogueIndex];
                
                return false;
            }

            if (dialogues.Length > dialogueIndex + 1)
            {
                SetNextDialog();
            }
            else
            {
                InActiveObjects();

                return true;
            }
        }

        return false;
    }

    private void SetNextDialog()
    {
        dialogueIndex++;
        coroutine = StartCoroutine(TypingText(dialogues[dialogueIndex], TEXTSPEED));
    }

    IEnumerator TypingText(string message, float speed)
    {
        isTyping = true;
        
        for (int i = 0; i < message.Length; i++)
        {
            dialogueText.text = message.Substring(0, i + 1);  // Display the current character of the dialogue
            yield return new WaitForSeconds(speed);  // Wait for a certain amount of time before displaying the next character
        }

        isTyping = false;
    }

    private void ActiveObjects()
    {
        this.gameObject.SetActive(true);
    }

    private void InActiveObjects()
    {
        this.gameObject.SetActive(false);
    }
}
