using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    [RequireComponent(typeof(Collider))]
    public class CloakOfDeath : MonoBehaviour
    {
        [SerializeField] private float damagePerTick = 1.5f;
        [SerializeField] private int damageTicksCount = 30;
        [SerializeField] private float damageTicksDelay = 0.1f;
        [SerializeField] private Sinner selfSinner = null;

        private HashSet<Sinner> sinners = new HashSet<Sinner>();
        private Coroutine dealLastLassingDamageRoutine = null;
        private Sinner tempSinner = null;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnEnable()
        {
            dealLastLassingDamageRoutine = StartCoroutine(DealLastLassingDamageRoutine(damagePerTick, damageTicksCount, damageTicksDelay));
        }

        private void OnDisable()
        {
            if(dealLastLassingDamageRoutine != null)
            {
                StopCoroutine(dealLastLassingDamageRoutine);
            }
        }

        private IEnumerator DealLastLassingDamageRoutine(float damage, int iterations, float delay)
        {
            for (int i = 0; i < iterations - 1; i++)
            {
                yield return new WaitForSeconds(delay);
                foreach (var sinner in sinners)
                {
                    if (selfSinner != null && sinner == selfSinner) continue;
                    if (sinner != null)
                    {
                        sinner.GetDamage(damage);
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            tempSinner = other.GetComponent<Sinner>();
            if (tempSinner != null)
            {
                sinners.Add(tempSinner);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            tempSinner = other.GetComponent<Sinner>();
            if (tempSinner != null)
            {
                sinners.Remove(tempSinner);
            }
        }
    }
}