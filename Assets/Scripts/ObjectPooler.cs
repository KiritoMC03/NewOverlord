using System;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    public class ObjectPooler : MonoBehaviour
    {
        [SerializeField] internal static ObjectPooler Instance = null;
        [SerializeField] private List<ObjectInfo> objectInfo;

        private Dictionary<ObjectInfo.ObjectType, Pool> pools;

        [Serializable]
        public struct ObjectInfo
        {
            public enum ObjectType
            {
                Sinner,
                Soul
            }
            public ObjectType Type;
            public GameObject Prefab;
            public int StartCount;
        }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            InitPool();
        }

        private void InitPool()
        {
            pools = new Dictionary<ObjectInfo.ObjectType, Pool>();

            var emptyGameObject = new GameObject();

            foreach (var obj in objectInfo)
            {
                var container = Instantiate(emptyGameObject, transform, false);
                container.name = obj.Type.ToString();

                pools[obj.Type] = new Pool(container.transform);

                for (int i = 0; i < obj.StartCount; i++)
                {
                    var tempGameObject = InstantiateObject(obj.Type, container.transform);
                    pools[obj.Type].objects.Enqueue(tempGameObject);
                }
            }
            Debug.Log("objectInfo: " + objectInfo.Count);
            Debug.Log("pools: " + pools.Count);
            Destroy(emptyGameObject);
        }

        private GameObject InstantiateObject(ObjectInfo.ObjectType type, Transform parent)
        {
            var tempGameObject = Instantiate(objectInfo.Find(x => x.Type == type).Prefab, parent);
            tempGameObject.SetActive(false);
            return tempGameObject;
        }

        public GameObject GetObject(ObjectInfo.ObjectType type)
        {
            var obj = pools[type].objects.Count > 0 ?
                pools[type].objects.Dequeue() : InstantiateObject(type, pools[type].container);

            obj.SetActive(true);
            return obj;
        }

        public void DestroyObject(GameObject obj)
        {
            pools[obj.GetComponent<IPooledObject>().Type].objects.Enqueue(obj);
            obj.SetActive(false);
        }
    }
}