using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NewOverlord
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Sinner : MonoBehaviour, INavMeshAgent
    {
        internal NavMeshAgent agent = null;
        private Transform _transform;
        
        private void Awake()
        {
            _transform = transform;
            agent = GetComponent<NavMeshAgent>();
        }

        public void FindNextPosition()
        {

        }

        public void GoNext()
        {

        }
    }
}