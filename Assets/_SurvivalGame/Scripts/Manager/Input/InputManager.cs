using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private List<BaseInput> _allInputs;
        private Dictionary<InputControlState, BaseInput> _inputDict;
        private GameInputAction _gameInputAction;
        private InputControlState _currentState = InputControlState.None;

        public void Initialize()
        {
            _gameInputAction = new GameInputAction();
            _inputDict = new();

            _gameInputAction.Enable();

            foreach (var input in _allInputs)
            {
                input.InitializeInput(_gameInputAction);
                _inputDict.TryAdd(input.State, input);
            }
        }

        public void EnableSession(InputControlState state)
        {
            if (_inputDict.ContainsKey(state))
            {
                if(_currentState != InputControlState.None)
                {
                    _inputDict[_currentState].DisableInput();
                }

                _inputDict[state].EnableInput();
                _currentState = state;
            }
        }    
    }

    public enum InputControlState
    {
        None = 0,
        Menu = 1,
        Gameplay = 2,
    }

    public abstract class BaseInput : ScriptableObject
    {
        [SerializeField] private InputControlState _state;
        protected GameInputAction gameInputAction;
        public InputControlState State => _state;

        public void InitializeInput(GameInputAction gameInputAction)
        {
            this.gameInputAction = gameInputAction;
        }    

        public abstract void EnableInput();

        public abstract void DisableInput();
    }
}