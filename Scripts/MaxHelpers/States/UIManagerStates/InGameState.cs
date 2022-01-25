using UnityEngine;
using UnityEngine.UIElements;

namespace MaxHelpers
{
    public class InGameState : IState
    {
        private VisualTreeAsset _inGameUI;

        public InGameState(VisualTreeAsset inGameUI) => _inGameUI = inGameUI;

        public void OnEnter()
        {
            UIManager.Instance.UIElement.visualTreeAsset = _inGameUI;
            GameManager.Instance.Inputs.Player.Enable();
            Time.timeScale = 1f;
        }

        public void OnExit()
        {
            GameManager.Instance.Inputs.Player.Disable();
            Time.timeScale = 0f;
        }
    }
}