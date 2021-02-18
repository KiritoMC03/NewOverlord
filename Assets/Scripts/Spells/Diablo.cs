using UnityEngine;

namespace NewOverlord
{
    public class Diablo : LongLastingSpell
    {
        private Vector3 startSpeed = Vector3.zero;

        internal override void SetTarget(Transform target)
        {
            this.target = target;
            SetFixedTarget(this.target.position);
            MoveToFixedTarget(fixedTarget);
        }

    }
}