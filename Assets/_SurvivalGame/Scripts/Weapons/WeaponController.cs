using Konzit.GameSystem;
using UnityEngine;

namespace Gameplay
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Transform _firingPoint;
        [SerializeField] private PoolManager<Bullet> _poolManager;

        private void Start()
        {
            _poolManager.Initialize();
        }

        public void Shoot()
        {
            var bullet = _poolManager.GetItem(_firingPoint.position, _firingPoint.rotation);

        }

        private void OnDestroy()
        {
            _poolManager?.Dispose();
        }
    }
}
