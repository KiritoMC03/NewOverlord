using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace NewOverlord
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Sinner : MonoBehaviour, IDamageable, IMortal, IPooledObject
    {
        [SerializeField] ObjectPooler.ObjectInfo.ObjectType soulType;
        [SerializeField] internal float soulCapacity = 100f;
        [SerializeField] internal GameObject soulEffect = null;
        [SerializeField] internal GameObject cloakOfDeath = null;
        [SerializeField] protected Soul soul = null;
        [SerializeField] protected Vector3 offsetFromGround = new Vector3(0f, 1f, 0f);

        internal NavMeshAgent agent = null;
        private Transform _transform;
        private Coroutine disableSoulEffectRoutine = null;
        private Coroutine getLastLassingDamageRoutine = null;
        private bool mayGetDamage = true;

        public ObjectPooler.ObjectInfo.ObjectType Type => type;
        [SerializeField] ObjectPooler.ObjectInfo.ObjectType type;

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
                Debug.Log("SoulCap: " + soulCapacity + " - " + gameObject);
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

            Debug.Log("Die! PreSpawn!");
            SpawnSoul();
            ObjectPooler.Instance.DestroyObject(gameObject);
        }

        private void SpawnSoul()
        {
            var tempSoul = ObjectPooler.Instance.GetObject(soulType).transform;
            tempSoul.position = _transform.position + offsetFromGround;

            // Instantiate(soul, _transform.position + offsetFromGround, Quaternion.identity);
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
            if(disableSoulEffectRoutine != null)
            {
                StopCoroutine(disableSoulEffectRoutine);
            }

            if(getLastLassingDamageRoutine != null)
            {
                StopCoroutine(getLastLassingDamageRoutine);
            }
        }

        private void OnEnable()
        {
            mayGetDamage = true;
        }
    }
}