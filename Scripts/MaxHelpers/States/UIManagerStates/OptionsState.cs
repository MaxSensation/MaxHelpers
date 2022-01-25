using UnityEngine;
using UnityEngine.UIElements;

namespace MaxHelpers
{
    public class OptionsState : IState
    {
        private readonly VisualTreeAsset _optionsUI;

        public OptionsState(VisualTreeAsset optionsUI) => _optionsUI = optionsUI;
        private Slider _masterSlider, _musicSlider, _soundFxSlider, _sensitivitySlider;
        private DropdownField _dropdown;
        private Toggle _fullscreenToggle;

        public void OnEnter()
        {
            UIManager.Instance.UIElement.visualTreeAsset = _optionsUI;
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("GoBack").clicked += UIManager.Instance.GoToPreviousState;
            // Sound
            _masterSlider = UIManager.Instance.UIElement.rootVisualElement.Q<Slider>("MasterSlider");
            _musicSlider = UIManager.Instance.UIElement.rootVisualElement.Q<Slider>("MusicSlider");
            _soundFxSlider = UIManager.Instance.UIElement.rootVisualElement.Q<Slider>("SoundFXSlider");
            _masterSlider.RegisterValueChangedCallback(UpdateMasterVolume);
            _musicSlider.RegisterValueChangedCallback(UpdateMusicVolume);
            _soundFxSlider.RegisterValueChangedCallback(UpdateSoundFxVolume);
            // Graphics Settings
            _dropdown = UIManager.Instance.UIElement.rootVisualElement.Q<DropdownField>("GraphicSetting");
            _dropdown.RegisterValueChangedCallback(UpdateGraphicSetting);
            _fullscreenToggle = UIManager.Instance.UIElement.rootVisualElement.Q<Toggle>("FullscreenToggle");
            _fullscreenToggle.RegisterValueChangedCallback(UpdateFullscreen);
            // Controls
            _sensitivitySlider = _soundFxSlider = UIManager.Instance.UIElement.rootVisualElement.Q<Slider>("SensitivitySlider");
            _sensitivitySlider.RegisterValueChangedCallback(UpdateSensitivity);
            GameManager.Instance.Inputs.UI.Enable();
            // TODO Load settings and apply to elements
        }

        private void UpdateFullscreen(ChangeEvent<bool> toggleEvent) => Screen.fullScreenMode = toggleEvent.newValue ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

        private void UpdateGraphicSetting(ChangeEvent<string> dropdownEvent)
        {
            switch (dropdownEvent.newValue)
            {
                case "Low":
                    QualitySettings.SetQualityLevel (0, true);
                    break;
                case "Normal":
                    QualitySettings.SetQualityLevel (3, true);
                    break;
                case "High":
                    QualitySettings.SetQualityLevel (5, true);
                    break;
            }
        }

        // TODO Refactor this!!!
        private void UpdateSensitivity(ChangeEvent<float> sliderEvent) => GameManager.Instance.UpdateSensitivity(sliderEvent.newValue);
        private void UpdateMasterVolume(ChangeEvent<float> sliderEvent) => AudioSystem.Instance.SetVolume("Master", sliderEvent.newValue);
        private void UpdateMusicVolume(ChangeEvent<float> sliderEvent) => AudioSystem.Instance.SetVolume("Music", sliderEvent.newValue);
        private void UpdateSoundFxVolume(ChangeEvent<float> sliderEvent) => AudioSystem.Instance.SetVolume("SoundFX", sliderEvent.newValue);

        public void OnExit()
        {
            // TODO Apply and save settings to disk
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("GoBack").clicked -= UIManager.Instance.GoToPreviousState;
            _masterSlider.UnregisterValueChangedCallback(UpdateMasterVolume);
            _musicSlider.UnregisterValueChangedCallback(UpdateMusicVolume);
            _soundFxSlider.UnregisterValueChangedCallback(UpdateSoundFxVolume);
            _sensitivitySlider.UnregisterValueChangedCallback(UpdateSensitivity);
            _dropdown.UnregisterValueChangedCallback(UpdateGraphicSetting);
            _fullscreenToggle.UnregisterValueChangedCallback(UpdateFullscreen);
            GameManager.Instance.Inputs.UI.Disable();
        }
    }
}