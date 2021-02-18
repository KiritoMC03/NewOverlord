using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace NewOverlord
{
    [RequireComponent(typeof(Player))]
    public class Mana : MonoBehaviour
    {
        [SerializeField] private Slider bar = null;
        [SerializeField] private float value = 100f;
        [SerializeField] private float max = 100f;
        [SerializeField] private float regenSpeed = 1f;
        [SerializeField] private float regenValue = 5f;

        private Player player = null;
        private Coroutine manaRegenRoutine = null;

        private void Awake()
        {
            player = GetComponent<Player>();
            bar = bar.GetComponent<Slider>();
            bar.minValue = 0;
            bar.maxValue = max;
            StartCoroutine(ManaRegenRoutine());
        }

        private IEnumerator ManaRegenRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(10 / regenSpeed);
                value += regenValue;
                value = Mathf.Clamp(value, 0, max);
                bar.value = value;
            }
        }

        private void OnDestroy()
        {
            CheckRegenRoutine();
        }

        private void OnEnable()
        {
            StartCoroutine(ManaRegenRoutine());
        }

        private void OnDisable()
        {
            CheckRegenRoutine();
        }

        private void CheckRegenRoutine()
        {
            if(manaRegenRoutine != null)
            {
                StopCoroutine(manaRegenRoutine);
            }
        }

        public float Get()
        {
            return value;
        }

        internal void Spend(float value)
        {
            if(this.value >= value)
            {
                this.value -= value;
                bar.value = this.value;
            }
        }
    }
}