using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObject : MonoBehaviour
{
    [System.Serializable]
    public class DialogueOptions{
        public string option;
        public string response;
        public bool closeOnResponse = false;
    }

    public string defaultText;

    public DialogueOptions[] options;

    public DialogueUITabInitializer dialogueTab;

    public InventoryMenuManager uiMan;

    public int functionIndex;

    public BaseDialogueOptions optionLogic;


    //awake is included just for display purposes. Remove this, and add your own method to open the menu
    public void Awake(){
        OpenMenu();
    }

    
    public void RunFunction(int selectedOption){
        if(optionLogic == null){return;}
    
        if(selectedOption == 0){
            optionLogic.Option1();
        }
        else if(selectedOption == 1){
            optionLogic.Option2();
        }
        else if(selectedOption == 2){
            optionLogic.Option3();
        }
        else if(selectedOption == 3){
            optionLogic.Option4();
        }
    }


    public void OpenMenu(){
        uiMan.OpenSpecialMenu();
        dialogueTab.PopulateOptions(this);
        dialogueTab.OpenMenu();
    }


}
