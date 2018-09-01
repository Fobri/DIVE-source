
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Canvas dialogueCanvas;
	public Text dialogueText;
    public Text dialogueTitle;
    public Dialogue[] dialoguesToRunInThisScene;

    [Tooltip("Get settigns from GameManager")]
    public bool useGlobalSettings;
    public bool surfaceScene;
    public int specialCharacterOpen;
    public string pieceOfStringToChange;

    public DialogueSystemSettings systemSettings;

    private void Awake()
    {
        if (useGlobalSettings)
            systemSettings = GameManager.instance.globalDialogueSystemSettings;
    }

    void Start () {
        if (surfaceScene)
        {
            GameManager.instance.surfaceSceneDialogueIndex++;
            try
            {
                if (GameManager.instance.surfaceSceneDialogueIndex <= dialoguesToRunInThisScene.Length)
                {
                    StartCoroutine(DialogueStart(dialoguesToRunInThisScene[GameManager.instance.surfaceSceneDialogueIndex]));
                }
            }
            catch (System.Exception e)
            {
                Debug.Log("No more dialogues to run");
            }
        }
        else
        {
            GameManager.instance.underWaterSceneDialogueIndex++;
            try
            {
                if (GameManager.instance.underWaterSceneDialogueIndex <= dialoguesToRunInThisScene.Length)
                    StartCoroutine(DialogueStart(dialoguesToRunInThisScene[GameManager.instance.underWaterSceneDialogueIndex]));
            }
            catch(System.Exception e)
            {
                Debug.Log("No more dialogues to run");
            }
        }
	}
    public void StartDialogue(Dialogue dialogue)
    {
        StartCoroutine(DialogueStart(dialogue));
    }


    public IEnumerator DialogueStart(Dialogue dialogue)
    {
        dialogueTitle.text = dialogue.dialogueName;
        yield return new WaitForSeconds(systemSettings.showDialogueCanvasAfterSeconds);
        dialogueCanvas.enabled = true;
        yield return new WaitForSeconds(systemSettings.startDialogueAfterSeconds);
        int thisChar = 0;
        specialCharacterOpen = 0;
        pieceOfStringToChange = "";
        while(dialogue.dialogue.Length > thisChar)
        {
            //If thisChar is equals to the special character
            if (dialogue.dialogue[thisChar] == '{'){

                //The { opens something that should be closed by a }
                specialCharacterOpen++;
                //This is to make it faster to look for characters into the {}; It loops for each character into the string
                for (int i = thisChar; i < dialogue.dialogue.Length; i++){

                    //If it's not a }
                    if (dialogue.dialogue[i] != '}'){

                        pieceOfStringToChange += dialogue.dialogue[thisChar];
                        thisChar++;
                    }else{

                        //Else if it is a } then break the loop because we found all the characters into the {}
                        break;
                    }
                }
            }
            if (dialogue.dialogue[thisChar] == '}'){

                //The } closes the {}
                specialCharacterOpen--;
            }

            //If the {} is closed or it hasn't been never opened
            if (specialCharacterOpen == 0){

                //Add the char to the string/char to add
                pieceOfStringToChange += dialogue.dialogue[thisChar];
                thisChar++;
                //If the characters to add are more than 1 (So it was a {})
                if (pieceOfStringToChange.Length > 1){

                    //Remove the { and the } at the start and the end of the string
                    splitSpecialChars();
                    dialogueText.text += pieceOfStringToChange;
                    
                }else{

                    dialogueText.text += pieceOfStringToChange;
                }

                //Reset the string
                pieceOfStringToChange = "";
                yield return new WaitForSeconds(systemSettings.letterAddTimeInterval + Random.Range(0, systemSettings.randomSpeedVariationAmount));
            }
            //
            //dialogueText.text += dialogue.dialogue[thisChar];
            //thisChar++;
            //yield return new WaitForSeconds(systemSettings.letterAddTimeInterval + Random.Range(0, systemSettings.randomSpeedVariationAmount));
            yield return null;
        }
        if (systemSettings.eraseTextAfterFinished)
        {
            yield return new WaitForSeconds(systemSettings.startErasingAfterSeconds);
            thisChar = dialogue.dialogue.Length - 1;
            while(dialogueText.text.Length > 0)
            {
                if (dialogue.dialogue[thisChar] == '{'){

                    specialCharacterOpen--;
                }
                if (dialogue.dialogue[thisChar] == '}'){

                    specialCharacterOpen++;
                    for (int i = thisChar; i < dialogue.dialogue.Length; i--){

                        if (dialogue.dialogue[i] != '{'){

                            pieceOfStringToChange += dialogue.dialogue[thisChar];
                            thisChar--;
                        }else{

                            break;
                        }
                    }
                }
                if (specialCharacterOpen == 0){

                    pieceOfStringToChange += dialogue.dialogue[thisChar];
                    thisChar--;
                    if (pieceOfStringToChange.Length > 1){

                        dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - pieceOfStringToChange.Length + 1);    //+1 because when it is a { we don't add it into the string to change and so we add 1
                        
                    }else{

                        dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - pieceOfStringToChange.Length);
                    }
                
                    pieceOfStringToChange = "";
                    yield return new WaitForSeconds(systemSettings.letterAddTimeInterval + Random.Range(0, systemSettings.randomSpeedVariationAmount));
                }
                //
                //dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - 1);
                //yield return new WaitForSeconds(systemSettings.letterRemoveTimeInterval);
                yield return null;
            }
        }
        yield return new WaitForSeconds(systemSettings.stayVisibleAfterFinishedDuration);
        dialogueCanvas.enabled = false;
    }

    void splitSpecialChars (){

        string[] stringsFirstSplit = pieceOfStringToChange.Split('{');
        string[] stringsSecondSplit = stringsFirstSplit[1].Split('}');
        pieceOfStringToChange = stringsSecondSplit[0];
    }
}

[System.Serializable]
public class DialogueSystemSettings
{
    public float letterAddTimeInterval;
    public bool eraseTextAfterFinished;
    public float letterRemoveTimeInterval;
    public float randomSpeedVariationAmount;
    public float showDialogueCanvasAfterSeconds;
    public float startDialogueAfterSeconds;
    public float startErasingAfterSeconds;
    public float stayVisibleAfterFinishedDuration;
}
