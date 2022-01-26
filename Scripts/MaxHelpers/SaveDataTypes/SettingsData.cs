using System;

namespace MaxHelpers
{
    [Serializable]
    public struct SettingsData : ISave
    {
        public float masterVolume;
        public float musicVolume;
        public float soundFXVolume;
        public bool fullScreen;
        public string qualitySettings;
        public float sensitivity;
    }
}