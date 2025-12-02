using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarFill : MonoBehaviour
{
    public Image staminaBar;
    public Animator staminaAnim;

    public void UpdateStamina(float currentStamina, float maxStamina){
        staminaBar.fillAmount = currentStamina / maxStamina;
    }

    public void InfiniteStamina(){
        staminaBar.fillAmount = 1;
        staminaAnim.SetBool("Infinite",true);
    }

    public void StopInfiniteStamina(){
        staminaAnim.SetBool("Infinite",false);
    }

    public void TiredEffects(bool enableEffects){
        staminaAnim.SetBool("Tired",enableEffects);
    }
}
