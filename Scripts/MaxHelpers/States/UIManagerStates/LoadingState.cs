using UnityEngine;
using UnityEngine.UIElements;

namespace MaxHelpers
{
    public class LoadingState : IState
    {
        private readonly VisualTreeAsset _loadingUI;
        private ProgressBar _progressBar;
        private float _transitionSpeed = 3;
        private float _targetPercentage;

        public LoadingState(VisualTreeAsset loadingUI) => _loadingUI = loadingUI;

        public void OnEnter()
        {
            UIManager.Instance.UIElement.visualTreeAsset = _loadingUI;
            _progressBar = UIManager.Instance.UIElement.rootVisualElement.Q<ProgressBar>();
            _progressBar.value = 0f;
            _targetPercentage = 0f;
        }

        public void Tick()
        {
            _targetPercentage = LevelManager.Instance.LoadingProgress;
            _progressBar.value = Mathf.MoveTowards(_progressBar.value, _targetPercentage, _transitionSpeed * Time.deltaTime);
        }
    }
}