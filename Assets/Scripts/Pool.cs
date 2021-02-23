using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    public class Pool : MonoBehaviour
    {
        [SerializeField] internal Transform container { get; set; }
        [SerializeField] internal Queue<GameObject> objects = null;

        public Pool(Transform container)
        {
            this.container = container;
            objects = new Queue<GameObject>();
        }
    }
}