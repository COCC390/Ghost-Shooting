using UnityEngine;

namespace Gameplay
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private CharacterConfigSO _characterConfigSO;

        [SerializeField] private CharacterMovement _characterMovement;
        [SerializeField] private CharacterShooting _characterShooting;
        void Start()
        {
            _characterMovement.InitializeMovement(_characterConfigSO.CharacterConfig);
            _characterShooting.InitializeShooting();
        }
    }
}
