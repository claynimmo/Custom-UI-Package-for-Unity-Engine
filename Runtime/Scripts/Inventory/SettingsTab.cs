using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.IO;

public class SettingsTab : MenuTab
{
    public GameObject defaultSelect;
    public GameObject entireUI;

    public Slider sensitivity;
    public Slider cameraDistance;

    public Slider musicVolume;
    public Slider otherVolume;

    public MultiChoiceButton resolution;
    public MultiChoiceButton framerate;
    
    public Slider bloomIntensity;
    public Slider exposure;
    public Slider saturation;
    public Slider contrast;

    public Toggle showUIControls;
    public Toggle pauseGameOnInventory;


    public override void CloseMenu(){
        entireUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
    
    public override void OpenMenu(){
        entireUI.SetActive(true);
        SetValues();
        EventSystem.current.SetSelectedGameObject(defaultSelect);
    }

    private void SetValues(){
        sensitivity.value = 0;
        cameraDistance.value = 0;
        musicVolume.value = 0;
        otherVolume.value = 0;
        resolution.SetValue($"{0}x{0}");
        framerate.SetValue(0.ToString());
        bloomIntensity.value = 0;
        exposure.value = 0;
        saturation.value = 0;
        contrast.value = 0;

        showUIControls.isOn = false;
        pauseGameOnInventory.isOn = false;
    }

    //called via a UI button
    public void SetDefaults(){
        return; //exit early, since the settings file is not included in the package

        TextAsset jsonFile = Resources.Load<TextAsset>("settings");
        string json = jsonFile.text;
        DefaultSetting settings = JsonUtility.FromJson<DefaultSetting>(json);
        SetValuesFromFile(json);
        Resources.UnloadAsset(jsonFile);
    }

    private void SetValuesFromFile(string json){
        DefaultSetting settings = JsonUtility.FromJson<DefaultSetting>(json);
        sensitivity.value = settings.cameraSensitivity;
        cameraDistance.value = settings.cameraDistance;
        musicVolume.value = settings.musicVolume;
        otherVolume.value = settings.otherVolume;
        resolution.SetValue($"{settings.resolution[0]}x{settings.resolution[1]}");
        framerate.SetValue(settings.framerate.ToString());
        bloomIntensity.value = settings.bloomIntensity;
        exposure.value = settings.exposure;
        saturation.value = settings.saturation;
        contrast.value = settings.contrast;
        showUIControls.isOn = settings.showUIControls;
        pauseGameOnInventory.isOn = settings.pauseGameOnInventory;
    }

    public void SetPreset(){
        string filePath = Path.Combine(Application.persistentDataPath, "settings_preset");
        if(File.Exists(filePath)){
            string json = File.ReadAllText(filePath);
            SetValuesFromFile(json);
        }
        else{
            SetDefaults();
        }
    }

    public void SavePreset(){
        DefaultSetting preset = new DefaultSetting();
        preset.cameraSensitivity = Mathf.Round(sensitivity.value*100)/100;
        preset.cameraDistance = Mathf.Round(cameraDistance.value*100)/100;
        preset.musicVolume = Mathf.Round(musicVolume.value*100)/100;
        preset.otherVolume = Mathf.Round(otherVolume.value*100)/100;
        string resolutionString = resolution.GetValue();
        int[] resolutionValues = ParseResolutionString(resolutionString);
        preset.resolution = new int[2];
        preset.resolution[0] = resolutionValues[0];
        preset.resolution[1] = resolutionValues[1];
        preset.framerate = int.Parse(framerate.GetValue());
        preset.bloomIntensity = bloomIntensity.value;
        preset.exposure = Mathf.Round(exposure.value*100)/100;
        preset.saturation = Mathf.Round(saturation.value*100)/100;
        preset.contrast = Mathf.Round(contrast.value*100)/100;
        preset.showUIControls = showUIControls.isOn;
        preset.pauseGameOnInventory = pauseGameOnInventory.isOn;
        
        string filePath = Path.Combine(Application.persistentDataPath,"settings_preset");
        string json = JsonUtility.ToJson(preset,true);
        File.WriteAllText(filePath,json);
    }

    public static int[] ParseResolutionString(string input){
        string[] strings = input.Split('x');
        int[] outputs = new int[strings.Length];
        for(int i = 0; i < strings.Length; i++){
            outputs[i] = int.Parse(strings[i]);
        }
        return outputs;
    }
    //function called through the on change and press of UI elements
    public void UpdateSettings(){
       
        string resolutionString = resolution.GetValue();
        int[] resolutionValues = ParseResolutionString(resolutionString);
        //add code to update the settings
    }
}
