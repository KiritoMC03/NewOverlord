using UnityEngine;

namespace NewOverlord
{
    interface INavMeshAgent
    {
        Vector3 FindNextPosition();
        void MoveNext(Vector3 nextPosition);
    }
}