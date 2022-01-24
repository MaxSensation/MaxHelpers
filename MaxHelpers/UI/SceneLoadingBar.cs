using UnityEngine;
using UnityEngine.UI;

namespace MaxHelpers
{
    public class SceneLoadingBar : MonoBehaviour
    {
        [SerializeField] private GameObject loaderCanvas;
        [SerializeField] private Image progressBarFiller;
        [SerializeField] private float transitionSpeed = 3;

        private bool _active;
        private float _targetPercentage;

        private void Start()
        {
            LevelManager.Instance.OnLoadEvent += EnableLoadScreen;
        }

        private void EnableLoadScreen()
        {
            _active = true;
            loaderCanvas.SetActive(true);
        }
        
        private void DisableLoadScreen()
        {
            _active = false;
            progressBarFiller.fillAmount = 0f;
            _targetPercentage = 0f;
            loaderCanvas.SetActive(false);
        }

        private void Update()
        {
            if (!_active) return;
            _targetPercentage = LevelManager.Instance.LoadingProgress;
            progressBarFiller.fillAmount = Mathf.MoveTowards(progressBarFiller.fillAmount, _targetPercentage, transitionSpeed * Time.deltaTime);
            if (progressBarFiller.fillAmount >= 1f) DisableLoadScreen();
        }
    }
}