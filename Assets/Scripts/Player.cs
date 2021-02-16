using System;
using System.Collections;
using UnityEngine;

namespace NewOverlord
{
    public class Player : MonoBehaviour
    {
        [SerializeField] internal Spell currentSpell = null;
        [SerializeField] internal float spellsCastDelay = 1f;
        [SerializeField] internal float armSwingTime = 0.3f;
        [SerializeField] internal Vector3 offsetToArm = new Vector3(0.1f, 2.1f, 8.75f);

        private Transform _transform = null;
        private Animator _animator;
        private Camera _mainCamera = null;
        private Spell _newCharge = null;
        private Mana manaHandler = null;
        private Transform _tempTarget = null;
        private bool isWaitCastDelay = false;
        private Coroutine waitCastDelayRoutine = null;
        private Coroutine castSpellRoutine = null;

        private void Awake()
        {
            _transform = transform;
            _animator = GetComponent<Animator>();
            _mainCamera = Camera.main;
            manaHandler = GetComponent<Mana>();
            isWaitCastDelay = false;

            if (manaHandler == null)
            {
                Debug.LogWarning("Рекомендуется добавить компонент Mana!");
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (manaHandler.Get() < currentSpell.GetManaCost())
                {
                    
                }
                else
                {
                    manaHandler.Spend(currentSpell.GetManaCost()); 
                    AttackSinnerRaycast();

                    if (_tempTarget != null)
                    {
                        AttackSinner();
                        SetAnimation(true);
                    }
                }
            }
            else
            {
                SetAnimation(false);
            }
        }

#region AttackWork
        private void AttackSinner()
        {
            if (currentSpell == null)
            {
                throw new Exception("Поле Charge не установлено.");
            }
            else if (isWaitCastDelay)
            {
                return;
            }

            waitCastDelayRoutine = StartCoroutine(WaitCastDelayRoutine(spellsCastDelay));
            castSpellRoutine = StartCoroutine(CastSpellRoutine(armSwingTime));
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

        private IEnumerator WaitCastDelayRoutine(float spellCastDelay)
        {
            isWaitCastDelay = true;
            yield return new WaitForSeconds(spellCastDelay);
            isWaitCastDelay = false;
        }

        private IEnumerator CastSpellRoutine(float armSwingTime)
        {
            yield return new WaitForSeconds(armSwingTime);

            _newCharge = Instantiate(currentSpell, offsetToArm, Quaternion.identity).GetComponent<Spell>();
            _newCharge.SetTarget(_tempTarget);
            _tempTarget = null;

        }
#endregion

#region Animation
        private void SetAnimation(bool cond)
        {
            _animator.SetBool("IsAttack", cond);
        }
        #endregion

#region GettersSetters

#endregion
        private void OnDisable()
        {
            if(waitCastDelayRoutine != null)
            {
                StopCoroutine(waitCastDelayRoutine);
            }
            if(castSpellRoutine != null)
            {
                StopCoroutine(castSpellRoutine);
            }
        }
    }
}