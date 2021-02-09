using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NewOverlord
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Spell : MonoBehaviour
    {
        public UnityEvent TargetIsSet;

        internal Transform target = null;
        internal Vector3 fixedTarget;
        internal Coroutine destroyRoutine = null;
        internal Coroutine checkTargetRoutine = null;
        internal bool spellAlive = true;

        [SerializeField] protected float moveSpeed = 4f;
        [SerializeField] protected float damage = 1f;
        [SerializeField] protected Vector3 offsetFromGround = new Vector3(0f, 1f, 0f);
        [SerializeField] protected float lifeTimeAfterCrash = 0.5f;

        protected Transform _transform = null;
        protected Rigidbody _rigidbody = null;
        protected Vector3 scale = Vector3.zero;
        protected Sinner tempSinner = null;
        protected Vector3 errorTarget = new Vector3(-666, -666, -666);

        private void Awake()
        {
            _transform = transform;
            scale = _transform.localScale;
            _rigidbody = GetComponent<Rigidbody>();
            fixedTarget = errorTarget;
            checkTargetRoutine = StartCoroutine(CheckTargetRoutine());
        }

        virtual protected void Move()
        {
            if (!CheckTargetAndAliveAsTrue())
            {
                return;
            }
            
            _rigidbody.velocity = (target.position + offsetFromGround - _transform.position).normalized * moveSpeed * Time.fixedDeltaTime;
        }

        /// <summary>
        /// Движение к статичной позиции.
        /// </summary>
        /// <param name="fixedTarget">Цель.</param>
        /// <returns>Фиксированная цель.</returns>
        virtual protected Vector3 MoveToFixedTarget(Vector3 fixedTarget)
        {
            Debug.Log("_rigidbody.velocity: " + _rigidbody.velocity);

            _rigidbody.velocity = (new Vector3(0,0,0) + offsetFromGround - _transform.position).normalized * moveSpeed * Time.fixedDeltaTime;
            return new Vector3(0, 0, 0);
        }

        virtual protected void Set()
        {
            if (!CheckTargetAndAliveAsTrue())
            {
                return;
            }

            _transform.position = target.position + offsetFromGround;
        }

        #region Utils
        virtual internal void SetTarget(Transform target)
        {
            this.target = target;
        }
        virtual internal void SetFixedTarget(Vector3 fixedTarget)
        {
            this.fixedTarget = fixedTarget;
        }

        virtual protected bool CheckTargetAndAliveAsTrue()
        {
            if (!spellAlive)
            {
                return false;
            }
            if (target == null)
            {
                destroyRoutine = StartCoroutine(DestroyRoutine(lifeTimeAfterCrash));
                return false;
            }
            return true;
        }
        
        virtual protected IEnumerator CheckTargetRoutine()
        {
            while (true)
            {
                //Debug.Log(target);
                if(target != null)
                {
                    TargetIsSet?.Invoke();
                    break;
                }
                else
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }

#endregion

#region CollisionAndTrigger
        virtual protected void OnCollisionEnter(Collision collision)
        {
            tempSinner = collision.gameObject.GetComponent<Sinner>();
            if (tempSinner != null)
            {
                Destroy(gameObject);
                tempSinner.GetDamage(damage);
            }
        }
#endregion

#region LifeAndDestroy
        private void OnDestroy()
        {
            if (destroyRoutine != null)
            {
                StopCoroutine(destroyRoutine);
            }
            if(checkTargetRoutine != null)
            {
                StopCoroutine(checkTargetRoutine);
            }
        }
        IEnumerator DestroyRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            DestroySpell();
        }

        virtual protected void DestroySpell()
        {
            Destroy(gameObject);
        }
#endregion
    }
}