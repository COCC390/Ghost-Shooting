using UnityEngine;

namespace Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;
        private void Awake()
        {
            _inputManager.Initialize();
            _inputManager.EnableSession(InputControlState.Gameplay);
        }

        private void Start()
        {
        }
    }
}
