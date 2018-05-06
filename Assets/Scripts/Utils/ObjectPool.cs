using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace Utils.Pool
{
    public class ObjectPool<T> where T : MonoBehaviour, IReleasable
    {
        private T itemPrefab;
        private readonly int _poolLen = 10;


        private readonly List<T> _pool = new List<T>();
        private readonly List<T> _pool_items = new List<T>();

        protected ObjectPool(T prefab)
        {
            itemPrefab = prefab;
            ExpandPool(_poolLen);
        }

        public static ObjectPool<T> Init(T prefab)
        {
            return new ObjectPool<T>(prefab);
        }

        private void ExpandPool(int poolLen)
        {
            for (var i = 0; i < poolLen; i++)
            {
                var obj = GameObject.Instantiate(itemPrefab.gameObject);
                obj.transform.SetParent(itemPrefab.transform.parent);
                obj.SetActive(false);
                _pool.Add(obj.GetComponent<T>());
            }
        }

        public T GetFromPool()
        {
            T item = null;
            foreach (var poolItem in _pool)
                if (!poolItem.gameObject.activeInHierarchy)
                {
                    item = poolItem;
                    break;
                }

            if (item == null)
            {
                ExpandPool(10);
                item = _pool.Last();
            }

            _pool_items.Add(item);
            item.gameObject.SetActive(true);
            return item;
        }

        public void FreePool()
        {
            foreach (var poolItem in _pool_items)
            {
                poolItem.Release();
            }

            _pool_items.Clear();
        }
    }
}