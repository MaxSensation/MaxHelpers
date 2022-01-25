using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace MaxHelpers
{
    public class MainMenuState : IState
    {
        private readonly VisualTreeAsset _mainMenuUI;
        private readonly UIManager _uiManager;

        public MainMenuState(VisualTreeAsset mainMenuUI) => _mainMenuUI = mainMenuUI;

        public void OnEnter()
        {
            UIManager.Instance.UIElement.visualTreeAsset = _mainMenuUI;
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("StartGame").clicked += GameManager.Instance.Continue;
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("Options").clicked += UIManager.Instance.PressedOptionMenu;
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("Quit").clicked += Application.Quit;
            GameManager.Instance.Inputs.UI.Enable();
        }
        
        public void OnExit()
        {
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("StartGame").clicked -= GameManager.Instance.Continue;
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("Options").clicked -= UIManager.Instance.PressedOptionMenu;
            UIManager.Instance.UIElement.rootVisualElement.Q<Button>("Quit").clicked -= Application.Quit;
            GameManager.Instance.Inputs.UI.Disable();
        }
    }
}