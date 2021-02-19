using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NewOverlord
{
    public class PlayerMenu : Menu
    {
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
            rang.text = rangText + "Lord";
            attackMultiplier.text = attackMultiplierText + 1.3f;
            strength.text = strengthText + 120;
            soulsCount.text = soulsCountText + 100;
            nextRang.text = nextRangText + "Diablo";
            needSouls.text = needSoulsText + 1000;
        }
    }
}