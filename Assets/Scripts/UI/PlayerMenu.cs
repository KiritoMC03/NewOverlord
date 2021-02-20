using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NewOverlord
{
    public class PlayerMenu : Menu
    {
        [SerializeField] private Stats playerStats = null;
        [Header("Stats:")]
        [SerializeField] private Text rang = null;
        [SerializeField] private Text attackMultiplier = null;
        [SerializeField] private Text strength = null;
        [SerializeField] private Text soulsCount = null;
        [SerializeField] private Text nextRang = null;
        [SerializeField] private Text needSouls = null;

        private string rangText = "Ранг: ";
        private string attackMultiplierText = "Множитель атаки: ";
        private string strengthText = "Могущество: ";
        private string soulsCountText = "Число душ: ";
        private string nextRangText = "Следующий ранг: ";
        private string needSoulsText = "Необходимо душ: ";

        private void Update()
        {
            SetText();
        }

        private void SetText()
        {
            rang.text = rangText + playerStats.GetRang();
            attackMultiplier.text = attackMultiplierText + playerStats.GetAttackMultiplier();
            strength.text = strengthText + playerStats.GetStrength();
            soulsCount.text = soulsCountText + playerStats.GetSoulCount();
            nextRang.text = nextRangText + playerStats.GetNextRang();
            needSouls.text = needSoulsText + playerStats.GetNeedSoul();
        }
    }
}