using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Spell : MonoBehaviour
    {
        internal Transform target = null;
        internal Coroutine destroyRoutine = null;
        internal bool spellAlive = true;

        [SerializeField] protected float moveSpeed = 4f;
        [SerializeField] protected float damage = 1f;
        [SerializeField] protected Vector3 offsetFromGround = new Vector3(0f, 1f, 0f);

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
        }

        virtual protected void Move()
        {
            if (!CheckTargetAndAliveAsTrue())
            {
                return;
            }
            
            _rigidbody.velocity = (target.position + offsetFromGround - _transform.position).normalized * moveSpeed * Time.fixedDeltaTime;
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
        virtual protected bool CheckTargetAndAliveAsTrue()
        {
            if (!spellAlive)
            {
                return false;
            }

            if (target == null)
            {
                destroyRoutine = StartCoroutine(DestroyRoutine(0.5f));
                return false;
            }

            return true;
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
        }
        IEnumerator DestroyRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy();
        }

        virtual protected void Destroy()
        {
            Destroy(gameObject);
        }
#endregion
    }
}