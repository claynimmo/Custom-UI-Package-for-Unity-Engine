using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//this script requires a new input system named PlayerInputs, with the controls UI.OpenMenu and UI.CloseMenu
public class InventoryMenuManager : MonoBehaviour
{
    public UIInputs playerControls;
    private InputAction openMenu;
    private InputAction cycleInventory;

    public MenuTab[] menuTabs; //access scripts that derive from the menutaboveride class
    public MenuTab[] specialTabs;

    public Image[] tabVisuals;
    public Color tabDefaultCol;
    public Color tabActiveCol;

    private int currentMenu = 0;

    private bool menuOpened = false;

    public GameObject menuFull;


    public AudioSource source;
    public float volume;
    public AudioClip submitClip;
    public AudioClip navigateClip;
    public AudioClip tabClip;

    private GameObject lastSelected;




    private void Awake(){
        playerControls = new();
    }

    private void OnEnable(){
        openMenu = playerControls.UI.OpenMenu;
        openMenu.Enable();
        openMenu.performed += OpenMenu;

        cycleInventory = playerControls.UI.CycleMenu;
        cycleInventory.Enable();
        cycleInventory.performed += CycleInventoryInput;

        playerControls.UI.Submit.Enable();
        playerControls.UI.Submit.performed += OnSubmit;

        playerControls.UI.Navigate.Enable();
        playerControls.UI.Navigate.performed += OnNavigate;
    }

    private void OnDisable(){
        openMenu.performed -= OpenMenu;
        openMenu.Disable();
        cycleInventory.performed -= CycleInventoryInput;
        cycleInventory.Disable();
        playerControls.UI.Submit.performed -= OnSubmit;
        playerControls.UI.Submit.Disable();
        playerControls.UI.Navigate.performed -= OnNavigate;
        playerControls.UI.Navigate.Disable();
    }


    private void OnSubmit(InputAction.CallbackContext context){
        if(!menuOpened){return;}
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        if (currentSelected != null && currentSelected.GetComponent<IPointerClickHandler>() != null)
        {
            source.PlayOneShot(submitClip,volume * SettingsData.Volume);
        }
    } 

    private void OnNavigate(InputAction.CallbackContext context){
        if(!menuOpened){return;}
        if (context.phase == InputActionPhase.Performed) { 
            StartCoroutine(CheckSelectedAfterDelay()); 
        }
        
    } 
    private IEnumerator CheckSelectedAfterDelay()
    {
        yield return null; // Wait for the next frame
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        if(currentSelected != lastSelected && currentSelected != null){
            source.PlayOneShot(navigateClip,volume * SettingsData.Volume);
        }
        lastSelected = currentSelected;
    }

    private void OpenMenu(InputAction.CallbackContext context){
        if(menuOpened){
            CloseMenu();
        return;}

        menuOpened = true;
        currentMenu = 0;
        menuFull.SetActive(true);
        menuTabs[currentMenu].OpenMenu(); 
        tabVisuals[currentMenu].color = tabActiveCol;

        //add code to disable movement controls
    }

    public void OpenSpecialMenu(){
        menuOpened = true;
    }


    public void CloseMenu(){

        foreach(MenuTab tab in specialTabs){
            tab.CloseMenu();
        }
        menuTabs[currentMenu].CloseMenu();
        tabVisuals[currentMenu].color = tabDefaultCol;
        EventSystem.current.SetSelectedGameObject(null);
        menuFull.SetActive(false);

        menuOpened = false;

        //add code to enable movement controls
    } 

    private void CycleInventoryInput(InputAction.CallbackContext context){
        if(!menuOpened){return;}
        int axisValue = (int)context.ReadValue<float>();

        if(axisValue == 0){return;}
        source.PlayOneShot(tabClip,volume * SettingsData.Volume);
        CycleInventory(axisValue);
    }


    private void CycleInventory(int val){
        menuTabs[currentMenu].CloseMenu();
        tabVisuals[currentMenu].color = tabDefaultCol;

        currentMenu = currentMenu + val;
        if(currentMenu >= menuTabs.Length){
            currentMenu = 0;
        }
        else if(currentMenu < 0){
            currentMenu = menuTabs.Length - 1;
        }

        menuTabs[currentMenu].OpenMenu();
        tabVisuals[currentMenu].color = tabActiveCol;
    }
}
