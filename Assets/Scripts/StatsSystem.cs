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
            "Ноль",
            "Полудемон",
            "Бес",
            "Полтергейст",
            "Люцифуг",
            "Чернота",
            "Низость",
            "Бездушный",
            "Злой дух",
            "Низший демон",
			
            "Черт",
            "Искуситель",
            "Порча",
            "Страх",
            "Вероломство",
			
            "Истинный демон",
            "Палач душ",
            "Предводитель злых духов",
            "Каратель",
            "Порождение бездны",
            "Бездушный демон",
            "Серый демон",
            "Дете Легиона",
            "",
            "",
            "",
            "",
			"",
            "Предвестник смерти",
            "Чума",
			
            "Голос бездны",
            "Пустота",
            "Голод",
            "Чума",
            "Скверна",
			
            "Демон смерти",
            "Легион",
            "Покоритель",
            "Высший демон",
            "Герцог ада",
			
            "Темнейший",
            "Асмодей",
            "Бельфегор",
            "Вельзевул",
            "Левиафан",
            "Мамон",
            "Сатана",
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