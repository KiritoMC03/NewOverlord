using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    public class LongLastingSpell : FadingSpell
    {
        [SerializeField] protected float lifeTime = 5f;
        protected Coroutine lifeTimerRoutine = null;

        private void Start()
        {
            Set();
            lifeTimerRoutine = StartCoroutine(LifeTimerRoutine());
        }

        protected IEnumerator LifeTimerRoutine()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy();
        }

        private void OnDestroy()
        {
            if(lifeTimerRoutine != null)
            {
                StopCoroutine(lifeTimerRoutine);
            }
        }

        override protected void OnCollisionEnter(Collision collision)
        {
            return;
        }

        virtual protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Sinner>() != null)
            {
                Destroy(other.gameObject);
            }
        }
    }
}