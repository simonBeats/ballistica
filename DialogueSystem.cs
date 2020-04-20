using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {

    [TextArea]
    public List<string> dialogue = new List<string>();

    public GameObject panel;
    public Text dialogueText;

    public AudioSource aSource;
    public AudioClip incomming;
    public AudioClip radioOut;

    public AudioClip type;

    public GameObject buildingSystem;

    private bool skip;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();

        StartDialogue(dialogue[0]);
    }


    public void StartDialogue(string text)
    {
        panel.SetActive(true);
        StartCoroutine(TypeSentence(text));
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            skip = true;
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        aSource.PlayOneShot(incomming);
        yield return new WaitForSeconds(1.0f);

        

        foreach (char letter in sentence.ToCharArray())
        {
            aSource.pitch = Random.Range(1.0f, 1.5f);
            dialogueText.text += letter;

            if (skip) dialogueText.text = sentence;

            if (dialogueText.text == sentence)
            {
                yield return new WaitForSeconds(1.5f);
                aSource.PlayOneShot(radioOut);

                panel.SetActive(false);
                buildingSystem.SetActive(true);
                skip = false;
                break;
            }

            if (letter == '.') yield return new WaitForSeconds(1.5f);

            aSource.PlayOneShot(type);
            yield return new WaitForSeconds(0.05f);
        }

    }
}
