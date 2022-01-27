using UnityEngine;
using UnityEngine.UIElements;

namespace MaxHelpers {
    public class UIManager : StaticInstance<UIManager>
    {
        [SerializeField] private VisualTreeAsset mainMenuUI;
        [SerializeField] private VisualTreeAsset inGameMenuUI;
        [SerializeField] private VisualTreeAsset inGameUI;
        [SerializeField] private VisualTreeAsset optionsUI;
        [SerializeField] private VisualTreeAsset loadingUI;
        public UIDocument UIElement { get; private set; }
        private readonly StateMachine _stateMachine = new();
        private OptionsState _optionsState;
        private InGameState _inGameState;
        protected void Start()
        {
            UIElement = GetComponent<UIDocument>();
            // Create states
            MainMenuState mainMenuState = new(mainMenuUI);
            InGameMenuState inGameMenuState = new(inGameMenuUI);
            _inGameState = new InGameState(inGameUI);
            _optionsState = new OptionsState(optionsUI);
            LoadingState loadingState = new(loadingUI);
            // Listen to events
            LevelManager.Instance.OnStartLoadEvent += () => _stateMachine.SetState(loadingState);
            LevelManager.Instance.OnCompletedLoadEvent += () => _stateMachine.SetState(_inGameState);
            // Add Transitions
            _stateMachine.AddTransition(_inGameState, inGameMenuState, GameManager.Instance.Inputs.Player.OpenGameMenu.IsPressed);
            _stateMachine.AddTransition(inGameMenuState, _inGameState, GameManager.Instance.Inputs.UI.ExitMenu.IsPressed);
            _stateMachine.AddTransition(_optionsState, inGameMenuState, () => GoToPrevIfPressedExit(inGameMenuState));
            _stateMachine.AddTransition(_optionsState, mainMenuState, () => GoToPrevIfPressedExit(mainMenuState));
            _stateMachine.SetState(mainMenuState);
        }

        private bool GoToPrevIfPressedExit(IState state) => GameManager.Instance.Inputs.UI.ExitMenu.IsPressed() && _stateMachine.GetPreviousState() == state;
        public void PressedOptionMenu() => _stateMachine.SetState(_optionsState);
        public void GoToPreviousState() => _stateMachine.GoToPreviousState();
        public void ResumeGame() => _stateMachine.SetState(_inGameState);
        private void Update() => _stateMachine.Tick();
        
    }
}