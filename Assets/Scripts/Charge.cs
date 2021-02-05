using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Charge : MonoBehaviour
    {
        internal Transform target = null;
        internal Coroutine destroyRoutine = null;
        internal bool chargeAlive = true;

        [SerializeField] protected float moveSpeed = 4f;

        protected Vector3 errorTarget = new Vector3(-666, -666, -666);
        protected Transform _transform = null;
        protected Rigidbody _rigidbody = null;
        protected Vector3 offsetFromGround = new Vector3(0f, 1f, 0f);

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        virtual protected void MoveCharge()
        {
            if (!CheckTargetAndAlive())
            {
                return;
            }
            
            _rigidbody.velocity = (target.position + offsetFromGround - _transform.position).normalized * moveSpeed * Time.fixedDeltaTime;
        }

        virtual protected bool CheckTargetAndAlive()
        {
            if (!chargeAlive)
            {
                return false;
            }

            if (target == null)
            {
                destroyRoutine = StartCoroutine(DestroyRoutine(2));
                return false;
            }

            return true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision");
            if(collision.gameObject.GetComponent<Sinner>() != null)
            {
                Debug.Log("CollisionDestroy");
                Destroy(gameObject);
                Destroy(collision.gameObject);
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

        virtual protected void OnDestroy()
        {
            if (destroyRoutine != null)
            {
                StopCoroutine(destroyRoutine);
            }
        }
    }
}