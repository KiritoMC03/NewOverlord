using System;
using UnityEngine;
using UnityEngine.UI;

namespace NewOverlord
{
    [RequireComponent(typeof(Player))]
    public class Spells : MonoBehaviour
    {
        [SerializeField] private SpellManager spellManager = null;
        
        private Player player = null;
        private Spell[] spells = null;

        private void Awake()
        {
            player = GetComponent<Player>();
            spells = spellManager.GetAllSpells();
        }

        /// <summary>
        /// Minimum = 0.
        /// </summary>
        /// <param name="id">Номер заклинания начиная с 0</param>
        public void Choose(int id)
        {
            if(id < 0)
            {
                throw new ArgumentOutOfRangeException("id Должен быть равен 0 и более.");
            }

            SetToPlayer(id);
        }

        private void SetToPlayer(int id)
        {
            if(player != null && spells.Length >= (id - 1))
            {
                player.currentSpell = spells[id];
            }
        }
    }
}