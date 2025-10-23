using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultiChoiceButton : MonoBehaviour
{

    public TextMeshProUGUI displayText;
    private int currentValue;
    public string[] displayOptions;
    public void ButtonPressed(){
        currentValue = (currentValue + 1) % displayOptions.Length;
        displayText.text = displayOptions[currentValue];
    }

    public void SetValue(string value){
        for(int i = 0; i < displayOptions.Length; i++){
            if(displayOptions[i] == value){
                currentValue = i;
                displayText.text = displayOptions[i];
                return;
            }
        }
    }

    public string GetValue(){
        return displayOptions[currentValue];
    }
}
