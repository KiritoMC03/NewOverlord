using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    public class SpellManager : MonoBehaviour
    {
        [SerializeField] private Spell[] list = null;

        public Spell[] GetAllSpells()
        {
            return list;
        }

        public Spell GetSpell(int id)
        {
            if (id <= list.Length)
            {
                return list[id];
            }

            else return null;
        }
    }
}