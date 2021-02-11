using UnityEngine;

namespace NewOverlord
{
    public class Absorb : LongLastingSpell
    {   
        internal override void SetTarget(Transform target)
        {
            this.target = target;
            SetFixedTarget(this.target.position);
            MoveToFixedTarget(fixedTarget);
        }
    }
}