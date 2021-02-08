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
        protected HashSet<Sinner> loggedSinners = new HashSet<Sinner>();
        
        private void Start()
        {
            Set();
            lifeTimerRoutine = StartCoroutine(LifeTimerRoutine());
            dealDamageRoutine = StartCoroutine(DealDamageRoutine());
            Debug.Log("dealDamageRoutine: " + dealDamageRoutine);
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
        private void OnDestroy()
        {
            if (lifeTimerRoutine != null)
            {
                StopCoroutine(lifeTimerRoutine);
            }
        }

        virtual protected IEnumerator LifeTimerRoutine()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy();
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