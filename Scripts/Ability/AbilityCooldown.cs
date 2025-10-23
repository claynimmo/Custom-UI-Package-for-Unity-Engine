using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//this only does the display for the cooldown. Any sounds or functionality when the cooldown is complete needs to be done separately.
public class AbilityCooldown : MonoBehaviour
{

    public Image cooldownImage;

    public Animator anim;

    private Coroutine currentRoutine;

    public Ability ability; //optional ability reference, where the CooldownComplete function is called, which can be used for playing sounds or enabling the ability

    public void StartCooldown(float duration){

        if(currentRoutine != null){
            StopCoroutine(currentRoutine);
        }

        currentRoutine = StartCoroutine(Cooldown(duration));
    }
    
    IEnumerator Cooldown(float duration){

        float waitDuration = 0.1f;
        float currentFill = duration;

        cooldownImage.fillAmount = currentFill / duration;
        WaitForSeconds wait = new WaitForSeconds(waitDuration);

        while(currentFill > 0){
            currentFill -= waitDuration;
            cooldownImage.fillAmount = currentFill / duration;
            yield return wait;
        }
        
        cooldownImage.fillAmount = 0;
        anim.Play("abilityflash",-1,0f);

        if(ability != null){
            ability.CooldownComplete();
        }
    }

    public void SetAbility(Ability ability){
        this.ability = ability;
    }
}
