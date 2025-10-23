using UnityEngine;
using UnityEngine.UI;
using TMPro;

//script to display the current value of the slider in a textmeshpro object
public class SliderDisplay : MonoBehaviour
{

    public TextMeshProUGUI displayText;

    public float scalar = 1; //variable to multiply the value of the slider, to show larger numbers

    public int decimalPlaces = 2; //value to control how many decimal places there are

    public float maxValue;


    //set this function to play when the slider value changes
    public void OnSliderMove(float value){
        displayText.text = $"{ (value * scalar).ToString($"F{decimalPlaces}")}/{(maxValue * scalar).ToString($"F{decimalPlaces}")}";
    }
}
