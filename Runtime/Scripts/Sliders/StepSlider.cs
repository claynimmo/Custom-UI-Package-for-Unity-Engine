using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StepSlider : Slider
{

    public float keyboardStep = 0.05f;  

    public override void OnMove(AxisEventData eventData)
    {
        if (!IsActive() || !IsInteractable())
        {
            base.OnMove(eventData);
            return;
        }

        switch (eventData.moveDir)
        {
            case MoveDirection.Left:
                value = Mathf.Clamp(value - keyboardStep, minValue, maxValue);
                break;
            case MoveDirection.Right:
                value = Mathf.Clamp(value + keyboardStep, minValue, maxValue);
                break;
            default:
                base.OnMove(eventData);
                break;
        }
    }
}