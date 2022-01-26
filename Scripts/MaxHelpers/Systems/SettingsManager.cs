using UnityEngine;
using UnityEngine.UIElements;

namespace MaxHelpers
{
    public class SettingsManager : StaticInstance<SettingsManager>
    {
        public float VolumeMaster => _data.masterVolume;
        public float VolumeMusic => _data.musicVolume;
        public float VolumeSoundFX => _data.soundFXVolume;
        public bool Fullscreen => _data.fullScreen;
        public string QualitySettings => _data.qualitySettings;
        public float Sensitivity => _data.sensitivity;

        private SettingsData _data;
        private void Start()
        {
            GetData();
            LoadData();
        }

        private void GetData()
        {
            DataManager.Instance.Load<SettingsData>("Settings");
            var iSave = DataManager.Instance.GetData("Settings");
            _data = iSave != null ? (SettingsData) iSave : CreateNewDefaultData();
        }

        private void LoadData()
        {
            AudioManager.Instance.SetVolume("Master", VolumeMaster);
            AudioManager.Instance.SetVolume("Music", VolumeMusic);
            AudioManager.Instance.SetVolume("SoundFX", VolumeSoundFX);
            SetQuality(QualitySettings);
            SetFullscreen(Fullscreen);
        }

        public void SaveData() => DataManager.Instance.SaveData("Settings", _data);

        private SettingsData CreateNewDefaultData()
        {
            // TODO Make a default scriptable object instead! 
            return new SettingsData
            {
                masterVolume = 0.1f,
                musicVolume = 0.1f,
                soundFXVolume = 0.1f,
                fullScreen = true,
                qualitySettings = "High",
                sensitivity = 0.5f
            };
        }
        
        private static void SetQuality(string quality)
        {
            switch (quality)
            {
                case "Low":
                    UnityEngine.QualitySettings.SetQualityLevel (0, true);
                    break;
                case "Normal":
                    UnityEngine.QualitySettings.SetQualityLevel (3, true);
                    break;
                case "High":
                    UnityEngine.QualitySettings.SetQualityLevel (5, true);
                    break;
            }
        }
        
        private static void SetFullscreen(bool fullscreen) => Screen.fullScreenMode = fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

        public void UpdateFullscreen(ChangeEvent<bool> toggleEvent)
        {
            _data.fullScreen = toggleEvent.newValue;
            SetFullscreen(toggleEvent.newValue);
        }

        public void UpdateGraphicSetting(ChangeEvent<string> dropdownEvent)
        {
            _data.qualitySettings = dropdownEvent.newValue;
            SetQuality(dropdownEvent.newValue);
        }
        
        public void UpdateSensitivity(ChangeEvent<float> sliderEvent)
        {
            _data.sensitivity = sliderEvent.newValue;
        }
        
        public void UpdateMasterVolume(ChangeEvent<float> sliderEvent)
        {
            _data.masterVolume = sliderEvent.newValue;
            AudioManager.Instance.SetVolume("Master", sliderEvent.newValue);
        }
        
        public void UpdateMusicVolume(ChangeEvent<float> sliderEvent)
        {
            _data.musicVolume = sliderEvent.newValue;
            AudioManager.Instance.SetVolume("Music", sliderEvent.newValue);
        }

        public void UpdateSoundFxVolume(ChangeEvent<float> sliderEvent)
        {
            _data.soundFXVolume = sliderEvent.newValue;
            AudioManager.Instance.SetVolume("SoundFX", sliderEvent.newValue);
        }
    }
}