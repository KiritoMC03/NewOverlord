using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Soul : MonoBehaviour
    {
        [SerializeField] private Transform player = null;
        [SerializeField] private float startSpeed = 0.3f;
        [SerializeField] private float flySpeed = 1f;
        [SerializeField] private Vector3 offsetFromGround = new Vector3(0f, 1f, 0f);

        private Transform _transform = null;
        private Rigidbody _rigidbody = null;
        private Vector3 tempVector3 = Vector3.zero;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            player = GameObject.FindGameObjectWithTag("Player").transform;

            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = false;
        }

        private void Start()
        {
            StartCoroutine(Fly());
        }

        private IEnumerator Fly()
        {
            tempVector3.Set(0, startSpeed, 0);
            SetSpeed(tempVector3);
            yield return new WaitForSeconds(1f);

            SetSpeed((player.position + offsetFromGround - _transform.position) * flySpeed);
        }

        private void SetSpeed(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }
    }
}