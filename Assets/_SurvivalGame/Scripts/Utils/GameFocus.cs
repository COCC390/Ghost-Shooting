using UnityEngine;

namespace Gameplay
{
    public class GameFocus : MonoBehaviour
    {
        [SerializeField] private bool _isHideMouse = true;

        private void Awake()
        {
            if(_isHideMouse)
                Cursor.visible = false;
        }

        //private void Update()
        //{
        //    if(Input.GetKeyUp(KeyCode.Escape))
        //    {
        //        Cursor.visible = true;
        //    }
        //}
    }
}
