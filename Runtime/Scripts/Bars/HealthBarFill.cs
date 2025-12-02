using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarFill : MonoBehaviour
{
    public Image healthBar;
    public Image healthBarDelay;

    public Animator healthAnim;

    private float previousHealth;
    private float healthPercentile = 1;

    private float previousHealthFillAmount;


    void Update(){
        if(previousHealthFillAmount > 0){
            previousHealth += 1*Time.deltaTime;
            previousHealthFillAmount = 1 - (previousHealth*healthPercentile);
            healthBarDelay.fillAmount = previousHealthFillAmount;
        }
    }

    public void TakeDamage(float currentHealth, float previousHealth, float maxHealth, float damage){
        CalculateHealthPercentile(maxHealth);
        previousHealth     = previousHealth;
        
        previousHealthFillAmount  = 1 - (previousHealth*healthPercentile);
        healthBarDelay.fillAmount = previousHealthFillAmount;
        healthBar.fillAmount      = 1 - (currentHealth*healthPercentile);
    }

    public void CalculateHealthPercentile(float maximumHealth){
        healthPercentile = 1 / maximumHealth;
    }
    
    public void PoisonEffects(bool isOn){
        healthAnim.SetBool("Poisoned",isOn);
    }
    

    public void FireEffects(bool isOn){
        healthAnim.SetBool("onFire",isOn);
    }

}
