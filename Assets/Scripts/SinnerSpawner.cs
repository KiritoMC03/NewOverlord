using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    public class SinnerSpawner : MonoBehaviour
    {
        internal HashSet<GameObject> spawnedCreatures = new HashSet<GameObject>();

        [SerializeField] ObjectPooler.ObjectInfo.ObjectType objectType;
        [SerializeField] internal uint count = 3;
        [SerializeField] internal Sinner sinner;
        [SerializeField] private float _spread = 5f;
        [Range(0.1f, 5f)]
        [SerializeField] internal float speed = 0.5f;
        [SerializeField] private Transform walkableGround;

        private Transform _transform;
        private Coroutine _spawnerRoutine;

        private WanderMode tempSinner = null;

        private void Awake()
        {
            _transform = transform;

            if (sinner == null)
            {
                throw new Exception("Поле Sinner не установлено!");
            }
            if(walkableGround == null)
            {
                throw new Exception("Поле Walkable Ground не установлено!");
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
                tempSinner = ObjectPooler.Instance.GetObject(objectType).GetComponent<WanderMode>();

                if (tempSinner != null)
                {
                    tempSinner.walkableGround = walkableGround;
                }
                yield return new WaitForSeconds(speed);
            }
        }

        /*
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
                tempSinner = Instantiate(sinner).GetComponent<WanderMode>();

                if (tempSinner != null)
                {
                    tempSinner.walkableGround = walkableGround;
                }
                yield return new WaitForSeconds(speed);
            }
        }
        */
    }
}