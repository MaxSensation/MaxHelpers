using UnityEngine;

namespace MaxHelpers
{
    public class GameManager : StaticInstance<GameManager>
    {
        public PlayerInputs Inputs { get; private set; }
        
        [SerializeField] private string currentLevel;
        
        protected override void Awake()
        {
            base.Awake();
            Inputs = new();
        }

        public void Continue() => LevelManager.Instance.LoadScene(currentLevel);
    }
}