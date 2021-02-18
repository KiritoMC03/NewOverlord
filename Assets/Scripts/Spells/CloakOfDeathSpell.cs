using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    public class CloakOfDeathSpell : LongLastingSpell
    {
        protected Sinner targetSinner = null;

#region LifeAndDestroy
        protected override void MakeStartJob()
        {
            base.MakeStartJob();
            _transform.localScale = Vector3.zero;
            _rigidbody.isKinematic = true;

            targetSinner = target.GetComponent<Sinner>();
            if(targetSinner != null)
            {
                targetSinner?.cloakOfDeath.SetActive(true);
            }
        }

        protected override IEnumerator LifeTimerRoutine()
        {
            yield return new WaitForSeconds(lifeTime);
            if (targetSinner != null)
            {
                targetSinner?.cloakOfDeath.SetActive(false);
            }
            DestroySpell();
        }
        #endregion
    }
}