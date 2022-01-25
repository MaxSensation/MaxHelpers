using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MaxHelpers
{
    public class InGameMenuState : IState
    {
        private readonly VisualTreeAsset _inGameMenu;
        public InGameMenuState(VisualTreeAsset inGameMenu) => _inGameMenu = inGameMenu;

        public void OnEnter()
        {
            UIManager.Instance.UIElement.visualTreeAsset = _inGameMenu;
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("Resume").clicked += UIManager.Instance.ResumeGame;
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("Options").clicked += UIManager.Instance.PressedOptionMenu;
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("Quit").clicked += Application.Quit;
            GameManager.Instance.Inputs.UI.Enable();
        }

        public void OnExit()
        {
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("Resume").clicked -= UIManager.Instance.ResumeGame;
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("Options").clicked -= UIManager.Instance.PressedOptionMenu;
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("Quit").clicked -= Application.Quit;
            GameManager.Instance.Inputs.UI.Disable();
        }
    }
}