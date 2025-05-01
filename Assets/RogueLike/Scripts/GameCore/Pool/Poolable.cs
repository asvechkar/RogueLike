using UnityEngine;
using UnityEngine.Pool;

namespace RogueLike.Scripts.GameCore.Pool
{
    public class Poolable : MonoBehaviour
    {
        private IObjectPool<GameObject> _pool;

        public IObjectPool<GameObject> Pool { set => _pool = value; }
    }
}