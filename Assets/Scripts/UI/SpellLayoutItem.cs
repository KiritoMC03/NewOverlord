using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NewOverlord
{
    public class SpellLayoutItem : MonoBehaviour
    {
        [SerializeField] private Image spellIcon = null;
        private Spell spell = null;

        internal void SetSpellIcon(Sprite icon)
        {
            if (icon != null)
            {
                spellIcon.sprite = icon;
            }
        }

        internal void SetSpell(Spell spell)
        {
            this.spell = spell;
        }

        internal Spell GetSpell()
        {
            return spell;
        }
    }
}