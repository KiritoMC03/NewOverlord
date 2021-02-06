using System;
using UnityEngine;

namespace NewOverlord
{
    public class Player : MonoBehaviour
    {
        [SerializeField] internal Spell charge = null;

        private Transform _transform = null;
        private Animator _animator;
        private Camera _mainCamera = null;
        private Spell _newCharge = null;
        private Transform _tempTarget = null;

        private void Awake()
        {
            _transform = transform;
            _animator = GetComponent<Animator>();
            _mainCamera = Camera.main;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                AttackSinnerRaycast();

                if(_tempTarget != null)
                {
                    AttackSinner();
                    SetAnimation(true);
                    _tempTarget = null;
                }
            }
            else
            {
                SetAnimation(false);
            }
        }

        private void AttackSinner()
        {
            if (charge == null)
            {
                throw new Exception("Поле Charge не установлено.");
            }

            _newCharge = Instantiate(charge, _transform.position, Quaternion.identity).GetComponent<Spell>();
            _newCharge.target = _tempTarget;
        }

        private void AttackSinnerRaycast()
        {
            RaycastHit hit;
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if(hit.collider.GetComponent<Sinner>() != null)
                {
                    _tempTarget = hit.collider.transform;
                }
            }
        }

        private void SetAnimation(bool cond)
        {
            _animator.SetBool("IsAttack", cond);
        }
    }
}