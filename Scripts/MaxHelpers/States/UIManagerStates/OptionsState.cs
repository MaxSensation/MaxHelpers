using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace MaxHelpers
{
    public class OptionsState : IState
    {
        private readonly VisualTreeAsset _optionsUI;
        private Slider _masterSlider, _musicSlider, _soundFxSlider, _sensitivitySlider;
        private DropdownField _qualityDropdown, _resolutionDropDown;
        private Toggle _fullscreenToggle;
        
        public OptionsState(VisualTreeAsset optionsUI) => _optionsUI = optionsUI;

        private void LoadData()
        {
            _masterSlider.SetValueWithoutNotify(SettingsManager.Instance.VolumeMaster);
            _musicSlider.SetValueWithoutNotify(SettingsManager.Instance.VolumeMusic);
            _soundFxSlider.SetValueWithoutNotify(SettingsManager.Instance.VolumeSoundFX);
            _fullscreenToggle.SetValueWithoutNotify(SettingsManager.Instance.Fullscreen);
            _qualityDropdown.SetValueWithoutNotify(SettingsManager.Instance.QualitySettings);
            _resolutionDropDown.choices = Screen.resolutions.Select(i => i.width + "x" + i.height).ToList();
            _resolutionDropDown.SetValueWithoutNotify(Screen.currentResolution.width + "x" + Screen.currentResolution.height);
            _sensitivitySlider.SetValueWithoutNotify(SettingsManager.Instance.Sensitivity);
        }

        public void OnEnter()
        {
            UIManager.Instance.UIElement.visualTreeAsset = _optionsUI;
            // Get all UI elements
            _masterSlider = UIManager.Instance.UIElement.rootVisualElement.Q<Slider>("MasterSlider");
            _musicSlider = UIManager.Instance.UIElement.rootVisualElement.Q<Slider>("MusicSlider");
            _soundFxSlider = UIManager.Instance.UIElement.rootVisualElement.Q<Slider>("SoundFXSlider");
            _qualityDropdown = UIManager.Instance.UIElement.rootVisualElement.Q<DropdownField>("GraphicSetting");
            _resolutionDropDown = UIManager.Instance.UIElement.rootVisualElement.Q<DropdownField>("ResolutionSetting");
            _fullscreenToggle = UIManager.Instance.UIElement.rootVisualElement.Q<Toggle>("FullscreenToggle");
            _sensitivitySlider = UIManager.Instance.UIElement.rootVisualElement.Q<Slider>("SensitivitySlider");
            // Load Data to UI
            LoadData();
            // Register for UI events
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("GoBack").clicked += UIManager.Instance.GoToPreviousState;
            _masterSlider.RegisterValueChangedCallback(SettingsManager.Instance.UpdateMasterVolume);
            _musicSlider.RegisterValueChangedCallback(SettingsManager.Instance.UpdateMusicVolume);
            _soundFxSlider.RegisterValueChangedCallback(SettingsManager.Instance.UpdateSoundFxVolume);
            _qualityDropdown.RegisterValueChangedCallback(SettingsManager.Instance.UpdateGraphicSetting);
            _resolutionDropDown.RegisterValueChangedCallback(SettingsManager.Instance.UpdateResolution);
            _fullscreenToggle.RegisterValueChangedCallback(SettingsManager.Instance.UpdateFullscreen);
            _sensitivitySlider.RegisterValueChangedCallback(SettingsManager.Instance.UpdateSensitivity);
            // Enable UI Controls
            GameManager.Instance.Inputs.UI.Enable();
        }

        public void OnExit()
        {
            // Unregister for UI events
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("GoBack").clicked -= UIManager.Instance.GoToPreviousState;
            _masterSlider.UnregisterValueChangedCallback(SettingsManager.Instance.UpdateMasterVolume);
            _musicSlider.UnregisterValueChangedCallback(SettingsManager.Instance.UpdateMusicVolume);
            _soundFxSlider.UnregisterValueChangedCallback(SettingsManager.Instance.UpdateSoundFxVolume);
            _sensitivitySlider.UnregisterValueChangedCallback(SettingsManager.Instance.UpdateSensitivity);
            _qualityDropdown.UnregisterValueChangedCallback(SettingsManager.Instance.UpdateGraphicSetting);
            _fullscreenToggle.UnregisterValueChangedCallback(SettingsManager.Instance.UpdateFullscreen);
            // Disable UI Controls
            GameManager.Instance.Inputs.UI.Disable();
            SettingsManager.Instance.SaveData();
        }
    }
}