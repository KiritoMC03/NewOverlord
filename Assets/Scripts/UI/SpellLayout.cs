using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    public class SpellLayout : MonoBehaviour
    {
        [SerializeField] private SpellManager spellManager = null;
        [SerializeField] private LayoutItem layoutItemPrefab = null;

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
                LayoutItem tempItem = Instantiate(layoutItemPrefab, _transform);
                tempItem.SetSpellIcon(spells[i].GetIconSprite());
            }
        }
    }
}