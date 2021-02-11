using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    public class LongLastingSpell : FadingSpell
    {
        [SerializeField] protected float lifeTime = 5f;

        protected Coroutine lifeTimerRoutine = null;
        protected Coroutine dealDamageRoutine = null;
        
        private void Start()
        {
            MakeStartJob();
        }

#region CollisionAndTrigger

        override protected void OnCollisionEnter(Collision collision)
        {
            return;
        }

        virtual protected void OnTriggerEnter(Collider other)
        {
            tempSinner = other.gameObject.GetComponent<Sinner>();
            if (tempSinner != null)
            {
                loggedSinners.Add(tempSinner);
            }
        }

        virtual protected void OnTriggerExit(Collider other)
        {
            tempSinner = other.gameObject.GetComponent<Sinner>();
            loggedSinners.Remove(tempSinner);
        }
#endregion

#region LifeAndDestroy
        protected virtual void MakeStartJob()
        {
            Debug.Log("MakeStartJob");
            lifeTimerRoutine = StartCoroutine(LifeTimerRoutine());
            dealDamageRoutine = StartCoroutine(DealDamageRoutine());
        }

        protected override void OnDestroy()
        {
            if (lifeTimerRoutine != null)
            {
                StopCoroutine(lifeTimerRoutine);
            }
            if (dealDamageRoutine != null)
            {
                StopCoroutine(dealDamageRoutine);
            }
        }

        virtual protected IEnumerator LifeTimerRoutine()
        {
            yield return new WaitForSeconds(lifeTime);
            DestroySpell();
        }

        virtual protected IEnumerator DealDamageRoutine()
        {
            while (true)
            {
                foreach (var sinner in loggedSinners)
                {
                    if (sinner != null)
                    {
                        sinner.GetDamage(damage / 10);
                    }
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
#endregion
    }
}