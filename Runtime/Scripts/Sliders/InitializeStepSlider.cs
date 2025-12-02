using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeStepSlider : MonoBehaviour
{

    public float step = 0.05f;

    void Awake(){
        GetComponent<StepSlider>().keyboardStep = step; 
    }
}
