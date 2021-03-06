﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewOverlord
{
    public class SinnerSpawner : MonoBehaviour
    {
        internal HashSet<GameObject> spawnedCreatures = new HashSet<GameObject>();

        [SerializeField] private ObjectPooler.ObjectInfo.ObjectType objectPoolerType = ObjectPooler.ObjectInfo.ObjectType.Sinner;
        [SerializeField] internal uint count = 3;
        [SerializeField] internal uint maxCount = 0;
        [SerializeField] internal Sinner sinner;
        [SerializeField] private float _spread = 5f;
        [Range(0.02f, 500f)]
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

        IEnumerator Spawner()
        {
            if (sinner == null)
            {
                throw new Exception("Поле Sinner не установлено!");
            }

            for (int i = 0; i < count; i++)
            {
                if(Sinner.GetSinnersCount() >= maxCount)
                {
                    Debug.Log("SinnersCount: " + Sinner.GetSinnersCount());
                    yield return new WaitForSeconds(speed);
                    continue;
                }
                if (ObjectPooler.Instance == null)
                {
                    yield return new WaitForSeconds(speed);
                }

                tempSinner = ObjectPooler.Instance.GetObject(objectPoolerType).GetComponent<WanderMode>();
                if (tempSinner != null)
                {
                    tempSinner.SetWalkableGround(walkableGround);
                }
                yield return new WaitForSeconds(speed);
            }
        }

        private void OnEnable()
        {
            if (_spawnerRoutine == null)
            {
                _spawnerRoutine = StartCoroutine(Spawner());
            }

        }

        private void OnDisable()
        {
            if (_spawnerRoutine != null)
            {
                StopCoroutine(_spawnerRoutine);
            }
        }
    }
}