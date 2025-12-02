using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Text;

public class DialogueUITabInitializer : MenuTab
{
    [System.Serializable]
    public class DialogueOptionsUI{
        public UnityEngine.UI.Button dialogueButton;
        public TextMeshProUGUI dialogueText;
        public GameObject overall;
    }
    
    public TextMeshProUGUI responseText;

    public DialogueOptionsUI[] choices;

    public GameObject defaultSelect;
    public GameObject entireUI;

    public InventoryMenuManager uiMan;

    private Coroutine currentCo;
    private Coroutine typingCo;
    public Animator dialogueAnim;

    private string defaultResponse = "";
    private int functionIndex = -1;
    private DialogueObject currentDialogue;


    private bool responseSelectable = false;

    public float selectableDelay = 1.5f;

    public void PopulateOptions(DialogueObject dialogue){
        for(int i = 0; i < choices.Length; i++){
            if(i >= dialogue.options.Length){
                choices[i].overall.SetActive(false);
            }
            else{
                choices[i].dialogueText.text = dialogue.options[i].option;
                choices[i].overall.SetActive(true);
            }
        }
        EventSystem.current.SetSelectedGameObject(choices[0].dialogueButton.gameObject);
        currentDialogue = dialogue;
        defaultResponse = dialogue.defaultText;
    }
    public override void CloseMenu(){
        StopAllCoroutines();
        responseSelectable = false;
        entireUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
    
    public override void OpenMenu(){
        entireUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);

        ShowDialogueBox(defaultResponse);
    }

    private void ShowDialogueBox(string defaultText){
        if(currentCo != null){
            StopAllCoroutines();
            dialogueAnim.SetBool("appear",false);
            dialogueAnim.SetBool("disappear",false);
        }
        currentCo = StartCoroutine(OpenDialogue());
        typingCo = StartCoroutine(TypeText(defaultText,false,0));
        Invoke("ResetSelectable",selectableDelay);
    }

    private IEnumerator OpenDialogue(){
        dialogueAnim.Play("appear", 0, 0.0f);
        dialogueAnim.SetBool("appear",true);
        dialogueAnim.SetBool("disappear",false);
        yield return null;
    }

    private IEnumerator TypeText(string text, bool closeOnComplete,int optionIndex){
        responseText.text = "";
        WaitForSeconds characterDelay = new WaitForSeconds(0.02f);
        StringBuilder sb = new StringBuilder();
        foreach(char c in text){
            sb.Append(c);
            responseText.text = sb.ToString();
            yield return characterDelay;
        }
        if(closeOnComplete){
            yield return new WaitForSeconds(1f);
            uiMan.CloseMenu();
            currentDialogue.RunFunction(optionIndex);
        }
        yield return null;
    }

    public void ResetSelectable(){
        responseSelectable = true;
        EventSystem.current.SetSelectedGameObject(choices[0].dialogueButton.gameObject);
    }

    public void OnButtonClick(int optionIndex){
        if(!responseSelectable){return;}

        responseSelectable = false;
        Invoke("ResetSelectable", selectableDelay);

        EventSystem.current.SetSelectedGameObject(null);
        
        if(typingCo != null){
            StopCoroutine(typingCo);
        }
        if(currentDialogue.options[optionIndex].closeOnResponse){
            typingCo = StartCoroutine(TypeText(currentDialogue.options[optionIndex].response,true,optionIndex));
        }
        else{
            typingCo = StartCoroutine(TypeText(currentDialogue.options[optionIndex].response,false,optionIndex));
            currentDialogue.RunFunction(optionIndex);
        }
    }
}
