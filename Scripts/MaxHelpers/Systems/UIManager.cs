using UnityEngine;
using UnityEngine.UIElements;

namespace MaxHelpers {
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private VisualTreeAsset mainMenuUI;
        [SerializeField] private VisualTreeAsset inGameMenuUI;
        [SerializeField] private VisualTreeAsset inGameUI;
        [SerializeField] private VisualTreeAsset optionsUI;
        [SerializeField] private VisualTreeAsset loadingUI;
        public UIDocument UIElement { get; private set; }
        private readonly StateMachine _stateMachine = new();
        protected void Start()
        {
            UIElement = GetComponent<UIDocument>();
            // Create states
            MainMenuState mainMenuState = new(mainMenuUI);
            InGameMenuState inGameMenuState = new(inGameMenuUI);
            InGameState inGameState = new(inGameUI);
            OptionsState optionsState = new(optionsUI);
            LoadingState loadingState = new(loadingUI);
            // Listen to events
            LevelManager.Instance.OnStartLoadEvent += () => _stateMachine.SetState(loadingState);
            LevelManager.Instance.OnCompletedLoadEvent += () => _stateMachine.SetState(inGameState);
            mainMenuState.OnOptionsClickedEvent += () => _stateMachine.SetState(optionsState);
            inGameMenuState.OnOptionsClickedEvent += () => _stateMachine.SetState(optionsState);
            optionsState.OnGoBackEvent += () => _stateMachine.GoToPreviousState();
            inGameMenuState.OnResumeEvent += () => _stateMachine.SetState(inGameState);
            // Add Transitions
            _stateMachine.AddTransition(inGameState, inGameMenuState, () => GameManager.Instance.Inputs.Player.OpenGameMenu.IsPressed());
            _stateMachine.AddTransition(inGameMenuState, inGameState, () => GameManager.Instance.Inputs.UI.ExitMenu.IsPressed());
            _stateMachine.SetState(mainMenuState);
        }

        private void Update() => _stateMachine.Tick();
    }
}