using UnityEngine;
interface ITab
{
    void OpenMenu();
    void CloseMenu();
}

//overide for monobehaviour so that the interface can be publically referenced
public abstract class MenuTab : MonoBehaviour, ITab
{
    public abstract void OpenMenu();
    public abstract void CloseMenu();
}