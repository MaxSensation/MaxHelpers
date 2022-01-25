using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MaxHelpers
{
    public class InGameMenuState : IState
    {
        public event Action OnResumeEvent, OnOptionsClickedEvent;
        private readonly VisualTreeAsset _inGameMenu;
        public InGameMenuState(VisualTreeAsset inGameMenu) => _inGameMenu = inGameMenu;

        public void OnEnter()
        {
            UIManager.Instance.UIElement.visualTreeAsset = _inGameMenu;
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("Resume").clicked += () => OnResumeEvent?.Invoke();
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("Options").clicked += () => OnOptionsClickedEvent?.Invoke();
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("Quit").clicked += Application.Quit;
            GameManager.Instance.Inputs.UI.Enable();
        }

        public void OnExit()
        {
            GameManager.Instance.Inputs.UI.Disable();
        }
    }
}