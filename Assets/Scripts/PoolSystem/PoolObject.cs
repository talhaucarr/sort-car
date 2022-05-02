using UnityEngine;
using Utilities;

namespace PoolSystem
{
    public class PoolObject : ModifiedMonoBehaviour
    {
        #region Variables
        [SerializeField] private bool poolAfterDelay;
        [SerializeField] private float delay = 0;

        protected Transform parent;

        public Transform Parent
        {
            get => parent;
            set => parent = value;
        }
        
        #endregion

        #region Public Methods
        
        public virtual void GoToPool(float delay)
        {
            Invoke(nameof(GoToPool), delay);
        }

        public virtual void GoToPool()
        {
            transform.SetParent(parent);
            gameObject.SetActive(false);

            IPoolable[] components = GetComponents<IPoolable>();
            foreach(IPoolable poolable in components)
            {
                poolable.OnGoToPool();
            }
        }

        public virtual void PoolSpawn()
        {
            IPoolable[] components = GetComponents<IPoolable>();
            foreach (IPoolable poolable in components)
            {
                poolable.OnPoolSpawn();
            }

            if (poolAfterDelay) { Invoke(nameof(GoToPool), delay); }
        }

        public void ManuelPoolAfterDelay(float delay)
        {
            CancelInvoke();
            Invoke(nameof(GoToPool), delay);
        }
        
        #endregion
    }
}