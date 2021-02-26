using System;
using UnityEngine;

namespace NewOverlord
{
    public class SpellLayout : MonoBehaviour
    {
        [SerializeField] private SpellManager spellManager = null;
        [SerializeField] private SpellLayoutItem layoutItemPrefab = null;
        [SerializeField] private Sprite lockIcon = null;
        
        private SpellLayoutItem[] spellLayoutItems = null;
        private Spell[] spells = null;
        private Transform _transform = null;
        private SpellLayoutItem tempItem = null;

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
            
        }

        private void OnEnable()
        {
            spellLayoutItems = null;

            for (int i = 0; i < spells.Length; i++)
            {
                spellLayoutItems = new SpellLayoutItem[spells.Length];
                tempItem = Instantiate(layoutItemPrefab, _transform);

                tempItem.SetSpell(spells[i]);
                SetLayoutItemIcon(tempItem.GetSpell(), tempItem);
                spellLayoutItems[i] = tempItem;
            }
        }

        private void Update()
        {
            for (int i = 0; i < spellLayoutItems.Length; i++)
            {
                Debug.Log("SPellLayItem: " + spellLayoutItems[i]);
                Debug.Log("GetSpell: " + spellLayoutItems[i].GetSpell());
                SetLayoutItemIcon(spellLayoutItems[i].GetSpell(), spellLayoutItems[i]);
            }
        }

        private void SetLayoutItemIcon(Spell spell, SpellLayoutItem layoutItem)
        {
            if (spell.GetIsLock())
            {
                if (lockIcon != null)
                {
                    layoutItem.SetSpellIcon(lockIcon);
                }
                else
                {
                    throw new Exception("Не установленно поле Lock Icon.");
                }
            }
            else
            {
                layoutItem.SetSpellIcon(spell.GetIconSprite());
            }
        }
    }
}