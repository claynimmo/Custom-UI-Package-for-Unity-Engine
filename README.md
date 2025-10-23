
# UI Package for Unity Instructions

## Prerequisites

The package relies on the TextMeshPro asset for the text objects in the prefabs. Some versions of Unity will automatically import the TMPro package.

The new input system must be installed and enabled in the project for the inventory management script. It also requires an InputActions named PlayerInput. A sample of the input actions is supplied in case you do not have one.

## Usage

### Health Bar

To add the health bar to the scene, simply create an instance of the prefab. The health bar contains a script HealthBarFill.cs which is used to manually control its state. The healthbar has a set of colour animations that are enabled and disabled through the PoisonEffects and FireEffects public functions, where sending a boolean true enables and fals disables.
Poison - pulses purple
Fire   - pulses orange

To update the healthbar, call the TakeDamage(currentHealth, previosHealth, maxHealth, damage) function in the place where damage calculations are done. It is filled upwards, so when current health = maxhealth, the bar is empty. To invert this, just remove the 1- in the HealthBarFill.TakeDamage function.

An example function for this is supplied:

```csharp

private float currentHealth = 0;
private float maxHealth     = 5;
public HealthBarFill healthBar;

public void TakeDamage(float damage){

    float previousHealth = currentHealth;
    currentHealth += damage;

    if(currentHealth >= maxHealth){
        //code to handle being killed
    }
    else if(currenthealth < 0){
        currentHealth = 0;
    }

    healthBar.TakeDamage(currentHealth, previousHealth, maxHealth, damage);
}
```

### Stamina Bar

Similar to the health bar, the stamina bar is included by instantiating the prefab. To control the bar, your stamina management scripts must access StaminaBarFill.cs to call its public methods.

UpdateStamina   - simply updates the fill amount of the bar, where 0 stamina has 0 fill.

InfiniteStamina - starts an animation where the stamina bar starts pulsing yellow. This sets the stamina bar to full, but it does not block future updates. Stop the effect by calling StopInfiniteStamina.

TiredEffects - starts an animation where the stamina bar pulses blue, showing exhaustion. Sending true enables the effect, while sending false disables it.


### Tooltip

Place the ToolTip.cs script on any object. Reference an animator with the appear and disappear boolean values, and the TextMeshPro object to display the message. To show the tooltip, reference it in any script and run the function ShowToolTip, passing in a string of the text you want to display, and how long it should remain up (this time is inclusive with the appear anim, and exclusive with the disappear anim). Repeated calls while a tooltip is enabled cancels the previous one.

An example tooltip is provided as a prefab, with the tooltip script and animations correctly setup.

### Ability Cooldown

The ability cooldown script updates the fill amount of a selected image, and plays an animation on the ui for when it is complete. The animation controller requires a node called "abilityflash" that is isolated from the rest of the controller. To start the cooldown, reference the script in the code that handles your abilities, and call the StartCooldown function by inputting the duration of the cooldown. The ability cooldown calls the inherited propery CooldownComplete on finish, if the field is not null. Create classes that derive from the Ability abstract class to implement custom off-cooldown functions. To reuse the same cooldown icon, the reference to the ability can be updated with the SetAbility function

A sample complete cooldown sample is provided as a prefab.


### Inventory

The inventory is driven through the MenuTab base class, where it cycles between the tabs on the player input, which calls the inherited functions OpenMenu and CloseMenu. Follow the prefab for how to setup the inventory correctly, with an example menu tab in the form of the settings.

The inventory script also plays audio clips when navigating across the ui with the keyboard.

### Dialogue

The dialogue menu is a special type of menu tab. To overide what each option does, make a new class deriving from the DialogueOptions base class. The dialogue menu is coupled to the inventory menu, so that the same key for opening the menu can close this special menu. Follow the implementation in the sample inventory menu to grasp how the setup works. For adding methods to open the dialogue menu, you need to reference a DialogueObject instance through a custom script and call the OpenMenu function. Make sure to remove the Awake method once another method to open the dialoge menu is made.

### Fade

The fade script uses the alpha channel of the canvas group component to fade in or out groups of ui elements. The fade is controlled within the Update function, driven by the two bools fadeIn and fadeOut. Use the show and hide ui functions to properly set the values of these booleans. The script also contains a setting to start by fading out after a delay, usefull for scene transitions.

## Components

### Multi-choice Button

The mutiple choice button switches between strings within a list when the button is pressed. This script is applied to the same gameobject as a button ui element, where the ButtonPressed function is added to the ui buttons on click event listener. To get the value of the string value of the button, call the GetValue function.

### Step Slider

The slider is composed of three classes:
- InitializeStepSlider
- SliderDisplay
- StepSlider

StepSlider is a custom override of the built-in slider object, where the amount incremented by the keyboard is controllable. The InitializeStepSlider must be placed on the same gameobject as the step slider, where it modifies the step amount (since the variable is not serialized in the editor). The default value is 0.05.

The SliderDisplay class can be placed anywhere, where the OnSliderMove function is put on the slider event listener. The value input should automatically be input by the slider, following the prefab example. The display writes the value of the slider in the form current/max, with a self defined amount of decimal places, and a scalar multiplyer to display larger numbers.




