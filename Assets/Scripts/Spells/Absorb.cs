using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    public class Absorb : FadingSpell
    {   
        internal override void SetTarget(Transform target)
        {
            this.target = target;
            SetFixedTarget(this.target.position);
            MoveToFixedTarget(fixedTarget);
        }
    }
}