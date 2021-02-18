using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    public class FixedLongLastingSpell : LongLastingSpell
    {

#region LifeAndDestroy

        protected override void MakeStartJob()
        {
            base.MakeStartJob();
            Set();
        }
        #endregion
    }
}