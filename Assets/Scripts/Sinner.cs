using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NewOverlord
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Sinner : MonoBehaviour, IDamageable, IMortal
    {
        [SerializeField] internal float soulCapacity = 100f;
        [SerializeField] protected GameObject soulEffect = null;
        [SerializeField] protected Soul soul = null;
        [SerializeField] protected Vector3 offsetFromGround = new Vector3(0f, 1f, 0f);

        internal NavMeshAgent agent = null;
        private Transform _transform;
        private Coroutine disableSoulEffectRoutine = null;


        private void Awake()
        {
            _transform = transform;
            agent = GetComponent<NavMeshAgent>();
            gameObject.layer = 10; // Sinners
        }

#region MoveWork
        public void FindNextPosition()
        {

        }

        public void GoNext()
        {

        }
        #endregion

#region DamageWork
        public void GetDamage(float damage)
        {
            CheckSoulCapacity();
            soulCapacity -= damage;
            EnableSoulEffectForTime();
        }

        private void CheckSoulCapacity()
        {
            if (soulCapacity <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            if(disableSoulEffectRoutine != null)
            {
                StopCoroutine(disableSoulEffectRoutine);
            }
            SpawnSoul();
            Destroy(gameObject);
        }

        private void SpawnSoul()
        {
            Instantiate(soul, _transform.position + offsetFromGround, Quaternion.identity);
        }

        private void EnableSoulEffectForTime()
        {
            soulEffect.SetActive(true);
            disableSoulEffectRoutine = StartCoroutine(DisableSoulEffectRoutine());
        }
        private void DisableSoulEffectNow()
        {
            soulEffect.SetActive(false);
        }

        private IEnumerator DisableSoulEffectRoutine()
        {
            yield return new WaitForSeconds(2f);
            soulEffect.SetActive(false);
        }
        #endregion
    }
}