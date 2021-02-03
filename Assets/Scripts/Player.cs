using System;
using UnityEngine;

namespace NewOverlord
{
    public class Player : MonoBehaviour
    {
        [SerializeField] internal Charge charge = null;

        private Transform _transform = null;
        private Charge _newCharge = null;
        private Vector3 _tempTarget = new Vector3(-666, -666, -666);

        private void Awake()
        {
            _transform = transform;
        }

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                AttackSinner();
            }
        }

        private void AttackSinner()
        {
            if(charge == null)
            {
                throw new Exception("Поле Charge не установлено.");
            }

            _newCharge = Instantiate(charge, _transform.position, Quaternion.identity).GetComponent<Charge>();
            _tempTarget.Set(0, 0.5f, 0);
            _newCharge.target = _tempTarget;
        }
    }
}