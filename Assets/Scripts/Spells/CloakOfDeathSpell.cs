using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    [RequireComponent(typeof(Collider))]
    public class CloakOfDeathSpell : FixedLongLastingSpell
    {
        private Collider triggerCollider = null;

#region LifeAndDestroy
        protected override void MakeStartJob()
        {
            base.MakeStartJob();
            triggerCollider = GetComponent<Collider>();
            
            if(triggerCollider != null)
            {
                triggerCollider.isTrigger = true;
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            tempSinner = other.GetComponent<Sinner>();
            if (tempSinner != null)
            {
                tempSinner.GetCloakOfDeath().SetActive(true);
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            DestroySpell();
        }
        #endregion
    }
}