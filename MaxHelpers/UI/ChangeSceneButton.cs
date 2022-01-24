using UnityEngine;

namespace MaxHelpers
{
    public class ChangeSceneButton : MonoBehaviour
    {
        public void ChangeScene(string sceneName) => LevelManager.Instance.LoadScene(sceneName);
    }
}