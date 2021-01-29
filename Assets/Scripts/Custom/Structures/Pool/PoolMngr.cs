using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    [System.Serializable]
    public class PoolMngr : MonoBehaviour
    {
        [Header("Editor")]
        public GameObject prefab;

        public Transform container;
        public bool autoHide = true;
        public int poolSize = 10;

        [SerializeField] protected List<GameObject> pool = new List<GameObject>();
        [SerializeField] protected readonly HashSet<GameObject> totalPool = new HashSet<GameObject>();
        [SerializeField] protected readonly Queue<GameObject> availablePool = new Queue<GameObject>();

        protected virtual void Awake()
        {
            availablePool.Clear();
            totalPool.Clear();
            pool.ForEach(el =>
            {
                if (autoHide) el.SetActive(false);
                availablePool.Enqueue(el);
                totalPool.Add(el);
            });
            //pool.Clear();
        }

#if UNITY_EDITOR
        public bool setup = false;

        private void OnDrawGizmos()
        {
            if (setup)
            {
                Setup();
                setup = false;
            }
        }

#endif

        public void Setup()
        {
            try { prefab.transform.SetParent(null); } catch { }
            Helper.DestroyAllChildren(container);
            pool.Clear();
            for (int i = 0; i < poolSize; i++)
            {
                var child = Helper.CreateChild(container, "pool_element_" + i, prefab);
                pool.Add(child);
            }
        }

        public virtual GameObject GetElement()
        {
            if (availablePool.Count == 0) return null;
            if (autoHide) availablePool.Peek().SetActive(true);
            return availablePool.Dequeue();
        }

        public virtual void ReleaseElement(GameObject element)
        {
            if (totalPool.Contains(element))
            {
                if (autoHide) element.SetActive(false);
                availablePool.Enqueue(element);
            }
        }
    }
}