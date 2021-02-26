using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace NewOverlord
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Sinner : MonoBehaviour, IDamageable, IMortal, IPooledObject
    {
        public ObjectPooler.ObjectInfo.ObjectType Type => type;

        [SerializeField] private ObjectPooler.ObjectInfo.ObjectType type = ObjectPooler.ObjectInfo.ObjectType.Sinner;
        [SerializeField] private ObjectPooler.ObjectInfo.ObjectType soulType;
        [SerializeField] internal GameObject soulEffect = null;
        [SerializeField] internal GameObject cloakOfDeath = null;
        [SerializeField] private Soul soul = null;
        [SerializeField] private Vector3 offsetFromGround = new Vector3(0f, 1f, 0f);
        [SerializeField] internal float soulCapacity = 100f;
        [SerializeField] private float startNonDamageDelay = 3f;

        internal NavMeshAgent agent = null;

        private Transform _transform;
        private Coroutine disableSoulEffectRoutine = null;
        private Coroutine getLastLassingDamageRoutine = null;
        private Coroutine nonDamageableRoutine = null;
        private bool mayGetDamage = true;
        
        private static uint count = 0;


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
            if (!mayGetDamage) return;
            soulCapacity -= damage;
            CheckSoulCapacity();
            EnableSoulEffectForTime();
        }
        public void GetLastLassingDamage(float damage)
        {
            CheckSoulCapacity();
            soulCapacity -= damage;
            EnableSoulEffectForTime();
        }

        private void CheckSoulCapacity()
        {
            if (soulCapacity <= 0)
            {
                soulCapacity = 100f;
                mayGetDamage = false;
                Die();
            }
        }

        public void Die()
        {
            if (disableSoulEffectRoutine != null)
            {
                StopCoroutine(disableSoulEffectRoutine);
            }

            SpawnSoul();
            ObjectPooler.Instance.DestroyObject(gameObject);
        }

        private void SpawnSoul()
        {
            var tempSoul = ObjectPooler.Instance.GetObject(soulType).transform;
            tempSoul.position = _transform.position + offsetFromGround;
        }

        private void EnableSoulEffectForTime()
        {
            if (gameObject.activeInHierarchy)
            {
                soulEffect.SetActive(true);
                disableSoulEffectRoutine = StartCoroutine(DisableSoulEffectRoutine());
            }
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

        private IEnumerator NonDamageableRoutine(float delay)
        {
            mayGetDamage = false;
            yield return new WaitForSeconds(delay);
            mayGetDamage = true;
        }
        #endregion

        #region GettersSetters
        internal GameObject GetCloakOfDeath()
        {
            if (cloakOfDeath != null)
            {
                return cloakOfDeath;
            }

            throw new ArgumentNullException("CloakOfDeath не установлен.");
        }

        internal GameObject GetSoulEffect()
        {
            if (soulEffect != null)
            {
                return soulEffect;
            }

            throw new ArgumentNullException("SoulEffect не установлен.");
        }
        #endregion

        private void OnDisable()
        {
            StopAllCoroutines();
            /*
            if(disableSoulEffectRoutine != null)
            {
                StopCoroutine(disableSoulEffectRoutine);
            }

            if(getLastLassingDamageRoutine != null)
            {
                StopCoroutine(getLastLassingDamageRoutine);
            }

            if (getLastLassingDamageRoutine != null)
            {
                StopCoroutine(getLastLassingDamageRoutine);
            }
            */
            soulEffect.SetActive(false);
            DecreaseCount(1);
        }

        private void OnEnable()
        {
            mayGetDamage = true;
            IncreaseCount(1);
            nonDamageableRoutine = StartCoroutine(NonDamageableRoutine(startNonDamageDelay));
        }

        private void IncreaseCount(uint value)
        {
            count += value;
        }
        private void DecreaseCount(uint value)
        {
            count -= value;
        }

        public static uint GetSinnersCount()
        {
            if(count < 0)
            {
                return (count = 0);
            }
            else
            {
                return count;
            }
        }
    }
}