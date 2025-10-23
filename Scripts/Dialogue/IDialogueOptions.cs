using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueOptions{
    public void Option1();
    public void Option2();
    public void Option3();
    public void Option4();
}

public abstract class BaseDialogueOptions : MonoBehaviour{
    public abstract void Option1();
    public abstract void Option2();
    public abstract void Option3();
    public abstract void Option4();
}
