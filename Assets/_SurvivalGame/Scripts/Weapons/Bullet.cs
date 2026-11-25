using Konzit.GameSystem;
using UnityEngine;

namespace Gameplay
{
    public class Bullet : ItemPooling 
    {
        [SerializeField] private Rigidbody _rb;

        public Rigidbody Rb => _rb;
    }
}
