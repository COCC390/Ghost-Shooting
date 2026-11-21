using UnityEngine;

namespace Konzit.GameSystem
{
    [CreateAssetMenu(fileName = "ScriptableContext", menuName = "Game/GameSystems/ScriptableContext")]
    public class ScriptableContext : ScriptableObject
    {
        private IContext _context;
        public IContext Context => _context;

        public void OnEnable()
        {
            _context = new BaseContext();
        }
    }
}
