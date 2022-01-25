using System;
using UnityEngine.UIElements;

namespace MaxHelpers
{
    public class OptionsState : IState
    {
        public event Action OnGoBackEvent;
        private readonly VisualTreeAsset _optionsUI;

        public OptionsState(VisualTreeAsset optionsUI) => _optionsUI = optionsUI;

        public void OnEnter()
        {
            UIManager.Instance.UIElement.visualTreeAsset = _optionsUI;
            GameManager.Instance.Inputs.UI.Enable();
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("GoBack").clicked += () => OnGoBackEvent?.Invoke();
            // TODO Load settings and apply to elements
        }

        public void OnExit()
        {
            // TODO Apply and save settings to disk
            GameManager.Instance.Inputs.UI.Disable();
        }
    }
}