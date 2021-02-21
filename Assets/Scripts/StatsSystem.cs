using System;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    public class StatsSystem : MonoBehaviour
    {
        [SerializeField]
        private static Dictionary<int, int> levels = new Dictionary<int, int>();
        private static Dictionary<int, string> rang = new Dictionary<int, string>();

        private string[] rangList = new string[]
        {
            "Злобный дух",
            "Чертик",
            "Зло",
            "Ярость",
            "Гнев",
            "Демон-искуситель",
            "Грех",
            "",
            "",
            "",
            "",
            "",
            "Демон войн",
            "",
            "",
            "Тьма",
            "",
            "Чистый демон",
            "Кошмар",
            "Безжалостный",
            "Ненависть",
            "Демон вероломства",
            "Жестокость",
            "Предводитель духов",
            "Князь духов",
            "Кровопускатель",
            "Кровавый палач",
            "Адское жудовище",
            "Маркиз демонов",
            "Чума",
            "Властелин бездны",
            "Страх",
            "Ужас",
            "Предводитель демонов",
            "Предводитель злых",
            "Жаждущий крови",
            "Обезглавливатель",
            "Легион",
            "Герцог ада",
            "Повелитель крови",
            "Нечистый",
            "Бельфегор",
            "Асмодей",
            "Левиафан",
            "Мамона",
            "Сатана",
            "Вельзевул",
            "Люцифер",
            "Принц демонов",
            "Дьявол",
        };
        private int tempInt = 0;

        private void Awake()
        {
            for (int key = 1; key <= 50; key++)
            {
                tempInt = Mathf.FloorToInt(Mathf.Pow((6 * key), 2.34f));
                levels.Add(key, tempInt);
            }

            levels[1] = 66;
            levels[50] = 666666;
        }

        public string GetRang(int level)
        {
            if(level > rangList.Length)
            {
                throw new ArgumentOutOfRangeException("Значение уровня слишком высоко.");
            }
            return rangList[level + 1];
        }

        public int GetNeedSoul(int targetLevel)
        {
            return levels[targetLevel];
        }
    }
}