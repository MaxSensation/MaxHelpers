namespace MaxHelpers
{
    public class GameManager : StaticInstance<GameManager>
    {
        public PlayerInputs Inputs { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            Inputs = new();
        }

        public void Continue() => LevelManager.Instance.LoadLastLevel();
    }
}