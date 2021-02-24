using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    [RequireComponent(typeof(Player))]
    public class Stats : MonoBehaviour
    {
        [SerializeField] private StatsSystem statsSystem = null;
        [SerializeField] static private int playerLevel = 0;

        private string rang = null;
        private float attackMultiplier = 0f;
        private int strength = 0;
        private int soulsCount = 0;
        private int needSouls = 0;
        private string nextRang = null;

        private void Awake()
        {
            CalculateStats();
        }

        private void CalculateStats()
        {
            rang = statsSystem.GetRang(playerLevel);
            attackMultiplier = Mathf.Sqrt(playerLevel + 1f);
            strength = (playerLevel + 1) * 10;
            needSouls = statsSystem.GetNeedSoul(playerLevel + 1);

            if (playerLevel == 50)
            {
                nextRang = "";
            }
            else
            {
                nextRang = statsSystem.GetRang(playerLevel + 1);
            }
        }

        static public int GetLevel()
        {
            return playerLevel;
        }

        public string GetRang()
        {
            return rang;
        }
        public float GetAttackMultiplier()
        {
            return (float)Math.Round(Convert.ToDouble(attackMultiplier), 1);
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

        internal void AddSoul()
        {
            soulsCount++;
        }

        public void LevelUp()
        {
            if(soulsCount >= needSouls)
            {
                soulsCount -= needSouls;
                playerLevel++;
                CalculateStats();
            }
        }
    }
}