using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NewOverlord
{
    public class SpellsButtonsItem : MonoBehaviour
    {
        [SerializeField] private Image spellIcon = null;

        internal void SetSpellIcon(Sprite icon)
        {
            if (icon != null)
            {
                spellIcon.sprite = icon;
            }
        }
    }
}