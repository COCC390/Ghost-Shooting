using System;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "CharacterConfigSO", menuName = "Game/Gameplay/Character/CharacterConfigSO")]
    public class CharacterConfigSO : ScriptableObject
    {
        [SerializeField] private CharacterConfig _characterConfig;

        public CharacterConfig CharacterConfig => _characterConfig;
    }

    [Serializable]
    public class CharacterConfig
    {
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _runMultiplier = 1.5f;
        [SerializeField] private float _rotateSpeed = 10f;
        public float MoveSpeed => _moveSpeed;
        public float RunMultiplier => _runMultiplier;
        public float RotateSpeed => _rotateSpeed;
    }
}
