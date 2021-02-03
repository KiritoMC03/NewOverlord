using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    public class SinnerSpawner : MonoBehaviour
    {
        internal HashSet<GameObject> spawnedCreatures = new HashSet<GameObject>();

        [SerializeField] internal uint count = 3;
        [SerializeField] internal GameObject sinner;
        [SerializeField] private float _spread = 5f;
        [Range(0.1f, 5f)]
        [SerializeField] internal float speed = 0.5f;

        private Transform _transform;
        private Coroutine _spawnerRoutine;

        private void Awake()
        {
            _transform = transform;

            if (sinner == null)
            {
                throw new Exception("Поле Creature не установлено!");
            }
        }

        void Start()
        {
            _spawnerRoutine = StartCoroutine(Spawner());
        }

        IEnumerator Spawner()
        {
            if (sinner == null)
            {
                throw new Exception("Поле Creature или Area не установлено!");
            }

            for (int i = 0; i < count; i++)
            {
                Instantiate(sinner);
                yield return new WaitForSeconds(speed);
            }
        }
    }
}