using System;
using UnityEngine.SceneManagement;

namespace MaxHelpers
{
    public class LevelManager : StaticInstance<LevelManager>
    {
        public float LoadingProgress { get; private set; }
        public Action OnLoadEvent;
        
        public async void LoadScene(string sceneName)
        {
            OnLoadEvent?.Invoke();
            var scene = SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = false;
            do LoadingProgress = 1f / scene.progress;
            while (scene.progress < 0.9f);
            scene.allowSceneActivation = true;
        }
    }
}