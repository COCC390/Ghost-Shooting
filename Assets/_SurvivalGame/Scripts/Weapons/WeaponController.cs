using Konzit.GameSystem;
using UnityEngine;

namespace Gameplay
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Transform _firingPoint;
        [SerializeField] private PoolManager<Bullet> _poolManager;

        [SerializeField] private float _bulletSpeed;

        private void Start()
        {
            _poolManager.Initialize();
        }

        public void Shoot(Vector3 target)
        {
            var bullet = _poolManager.GetItem(_firingPoint.position, Quaternion.identity);

            Vector3 direction = (target - _firingPoint.position).normalized;

            var meshOffset = Quaternion.FromToRotation(Vector3.up, Vector3.forward);
            bullet.transform.rotation = Quaternion.LookRotation(direction) * meshOffset;

            Rigidbody rb = bullet.Rb;
            rb.useGravity = false; 
            rb.linearVelocity = direction * _bulletSpeed;
        }

        private void OnDestroy()
        {
            _poolManager?.Dispose();
        }
    }
}
