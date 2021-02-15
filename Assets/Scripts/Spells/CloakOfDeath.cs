using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    [RequireComponent(typeof(Sinner))]
    public class CloakOfDeath : MonoBehaviour
    {
        [SerializeField] private float damagePerTick = 5f;

        private Sinner sinnerCompotent = null;
        
        private void OnEnable()
        {
            sinnerCompotent = GetComponent<Sinner>();
        }

        private IEnumerator DealLastLassingDamageRoutine(float damage, int iterations, float delay)
        {
            for (int i = 0; i < iterations - 1; i++)
            {
                sinnerCompotent.GetDamage(damage);
                yield return new WaitForSeconds(delay);
            }
        }
    }
}