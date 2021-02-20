using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    [RequireComponent(typeof(Player))]
    public class Stats : MonoBehaviour
    {
        [SerializeField] private StatsSystem statsSystem = null;
        [SerializeField] private int level = 0;

        private string rang = null;
        private float attackMultiplier = 0f;
        private int strength = 0;
        private int soulsCount = 0;
        private int needSouls = 0;
        private string nextRang = null;

        private void Awake()
        {
            rang = statsSystem.GetRang(level);
            attackMultiplier = Mathf.Sqrt(level + 1f);
            strength = (level + 1) * 10;

            if(level == 50)
            {
                nextRang = "";
            }
            else
            {
                nextRang = statsSystem.GetRang(level + 1);
            }
        }

        public string GetRang()
        {
            return rang;
        }
        public float GetAttackMultiplier()
        {
            return attackMultiplier;
        }
        public int GetStrength()
        {
            return strength;
        }
        public int GetSoulCount()
        {
            return soulsCount;
        }
        public int GetNeedSoul()
        {
            return needSouls;
        }
        public string GetNextRang()
        {
            return nextRang;
        }

        private void AddSoul()
        {
            soulsCount++;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Soul>() != null)
            {
                AddSoul();
                Destroy(other.gameObject);
            }
        }
    }
}