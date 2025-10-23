using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTip : MonoBehaviour
{
    public Animator toolTipAnim;
    public TextMeshProUGUI toolTipText;

    private Coroutine currentCo;
    
    public void ShowToolTip(string text, float duration){
        if(toolTipAnim == null){return;}
        if(currentCo != null){
            StopAllCoroutines();
            toolTipAnim.SetBool("appear",false);
            toolTipAnim.SetBool("disappear",false);
        }
        currentCo = StartCoroutine(tooltip(text,duration));
    }
    IEnumerator tooltip(string text, float duration){
        toolTipText.text = text;
        toolTipAnim.Play("appear", 0, 0.0f);
        toolTipAnim.SetBool("appear",true);
        toolTipAnim.SetBool("disappear",false);
        yield return new WaitForSeconds(duration);
        toolTipAnim.SetBool("disappear",true);
        toolTipAnim.SetBool("appear",false);
        currentCo = null;
    }
}
