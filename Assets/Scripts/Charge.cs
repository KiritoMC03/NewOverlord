using UnityEngine;

namespace NewOverlord
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Charge : MonoBehaviour
    {
        internal Vector3 target = new Vector3(-666, -666, -666);

        [SerializeField] private float moveSpeed = 4f;

        private Vector3 _errorTarget = new Vector3(-666, -666, -666);
        private Transform _transform;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            MoveCharge();
        }

        private void MoveCharge()
        {
            if(target == _errorTarget)
            {
                return;
            }

            var force = (target - _transform.position) * Time.fixedDeltaTime * moveSpeed;

            _rigidbody.AddForce(force);

            //_transform.position = Vector3.MoveTowards(_transform.position, target, moveSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision!");
            if(collision.gameObject.GetComponent<Sinner>() != null)
            {
                Debug.Log("Collision! IFFF");
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}