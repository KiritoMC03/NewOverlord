using System;
using UnityEngine;

namespace NewOverlord
{
    public class SpellLayout : MonoBehaviour
    {
        [SerializeField] private SpellManager spellManager = null;
        [SerializeField] private SpellLayoutItem layoutItemPrefab = null;
        [SerializeField] private Sprite lockIcon = null;

        private Spell[] spells = null;
        private Transform _transform = null;

        private void Awake()
        {
            _transform = transform;

            if(spellManager != null)
            {
                spells = spellManager.GetAllSpells();
            }
        }

        private void Start()
        {
            for (int i = 0; i < spells.Length; i++)
            {
                SpellLayoutItem tempItem = Instantiate(layoutItemPrefab, _transform);

                if (spells[i].GetNeedLevel() > Stats.GetLevel())
                {
                    if (lockIcon != null)
                    {
                        tempItem.SetSpellIcon(lockIcon);
                    }
                    else
                    {
                        throw new Exception("Не установленно поле Lock Icon.");
                    }
                }
                else
                {
                    tempItem.SetSpellIcon(spells[i].GetIconSprite());
                }
            }
        }
    }
}