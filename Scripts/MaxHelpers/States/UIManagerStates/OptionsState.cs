using UnityEngine.UIElements;

namespace MaxHelpers
{
    public class OptionsState : IState
    {
        private readonly VisualTreeAsset _optionsUI;

        public OptionsState(VisualTreeAsset optionsUI) => _optionsUI = optionsUI;

        public void OnEnter()
        {
            UIManager.Instance.UIElement.visualTreeAsset = _optionsUI;
            GameManager.Instance.Inputs.UI.Enable();
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("GoBack").clicked += UIManager.Instance.GoToPreviousState;
            // TODO Load settings and apply to elements
        }

        public void OnExit()
        {
            // TODO Apply and save settings to disk
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("GoBack").clicked -= UIManager.Instance.GoToPreviousState;
            GameManager.Instance.Inputs.UI.Disable();
        }
    }
}