using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

        [SerializeField] internal Image icon = null;
        [SerializeField] protected int needLevel = 0;
        [SerializeField] private bool isLocked = true;
        [SerializeField] protected float moveSpeed = 4f;
        [SerializeField] protected float damage = 1f;
        [SerializeField] protected float manaCost = 10f;
        [SerializeField] protected float lifeTimeAfterCrash = 0.5f;
        [SerializeField] protected bool destroyOnCollision = true;
        [SerializeField] protected Vector3 offsetFromGround = new Vector3(0f, 1f, 0f);

        protected Transform _transform = null;
        protected Rigidbody _rigidbody = null;
        protected Vector3 scale = Vector3.zero;
        protected Sinner tempSinner = null;
        protected HashSet<Sinner> loggedSinners = new HashSet<Sinner>();
        protected HashSet<Sinner> tempLoggedSinners = new HashSet<Sinner>();
        protected Vector3 errorTarget = new Vector3(-666, -666, -666);

        private void Awake()
        {
            _transform = transform;
            scale = _transform.localScale;
            _rigidbody = GetComponent<Rigidbody>();
            fixedTarget = errorTarget;
            checkTargetRoutine = StartCoroutine(CheckTargetRoutine());
        }

        protected virtual void Move()
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
        protected virtual Vector3 MoveToFixedTarget(Vector3 fixedTarget)
        {
            _rigidbody.velocity = (fixedTarget + offsetFromGround - _transform.position).normalized * moveSpeed * Time.fixedDeltaTime;
            return fixedTarget;
        }

        protected virtual void Set()
        {
            if (!CheckTargetAndAliveAsTrue())
            {
                return;
            }

            _transform.position = target.position + offsetFromGround;
        }

#region Utils
        internal virtual void SetTarget(Transform target)
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            this.target = target;
        }
        internal virtual void SetFixedTarget(Vector3 fixedTarget)
        {
            this.fixedTarget = fixedTarget;
        }

        protected virtual bool CheckTargetAndAliveAsTrue()
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

        protected virtual IEnumerator CheckTargetRoutine()
        {
            while (true)
            {
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
        protected virtual void OnCollisionEnter(Collision collision)
        {
            tempSinner = collision.gameObject.GetComponent<Sinner>();
            if (tempSinner != null)
            {
                if (destroyOnCollision)
                {
                    Destroy(gameObject);
                }
                tempSinner.GetDamage(damage);
            }
        }
#endregion

#region LifeAndDestroy
        protected virtual void OnDestroy()
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

        protected virtual void DestroySpell()
        {
            Destroy(gameObject);
        }
#endregion

        public float GetManaCost()
        {
            return manaCost;
        }

        public Image GetIconImage()
        {
            if(icon == null)
            {
                throw new Exception("Не установленно поле Icon.");
            }
            return icon;
        }

        public Sprite GetIconSprite()
        {
            if (icon == null)
            {
                throw new Exception("Не установленно поле Icon.");
            }
            return icon.sprite;
        }
        
        public int GetNeedLevel()
        {
            return needLevel;
        }

        public bool GetIsLock()
        {
            UpdateIsLock();
            return isLocked;
        }

        private void UpdateIsLock()
        {
            if(Stats.GetLevel() >= needLevel)
            {
                isLocked = false;
            }
        }
    }
}