using System;
using UnityEngine;

namespace NewOverlord
{
    [RequireComponent(typeof(Player))]
    public class Spells : MonoBehaviour
    {
        [SerializeField] private Spell[] spells;

        private Player player = null;

        private void Awake()
        {
            player = GetComponent<Player>();
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