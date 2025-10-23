using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private CanvasGroup ui;
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;

    [SerializeField] private bool  startWithFadeOut = false;
    [SerializeField] private float startDelay = 3;

    public float fadeSpeed = 0.5f;

    [SerializeField] private bool Controllable;

    // Start is called before the first frame update
    void Start()
    {
        if(startWithFadeOut){
            StartCoroutine(StartFadeOut());
        }
    }
    public void ShowUI(){
        fadeIn= true;
        fadeOut = false;
    }
    public void HideUI(){
        fadeOut = true;
        fadeIn=false;
    }

    public void FadeInAndOut(float stopTime){
        StartCoroutine(FadeInAndOutCo(stopTime));
    }

    IEnumerator FadeInAndOutCo(float stopTime){
        ShowUI();
        yield return new WaitForSeconds(stopTime);
        HideUI();
    }

    void Update(){
        
        if(fadeIn){
            if(ui.alpha < 1){
                ui.alpha += Time.deltaTime*fadeSpeed;
                if(ui.alpha>=1){
                    fadeIn = false;
                }
            }
            else if(ui.alpha==1){
                fadeIn=false;
            }
        }
        if(fadeOut){
            if(ui.alpha > 0){
                ui.alpha -= Time.deltaTime*fadeSpeed;
                if(ui.alpha==0){
                    fadeOut = false;
                }
            }
            else if(ui.alpha==0){
                fadeOut=false;
            }
        }
    }

    IEnumerator StartFadeOut(){
        yield return new WaitForSeconds(startDelay);
        HideUI();
    }
}
