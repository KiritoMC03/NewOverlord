using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NewOverlord
{
	[RequireComponent(typeof(Sinner))]
	public class WanderMode : MonoBehaviour, INavMeshAgent
	{
		[SerializeField] private Sinner _sinner;
		[SerializeField] internal Transform walkableGround;
		[SerializeField] private float _walkableGroundRadius = 1f;

		private Transform _transform;
		private Vector3 _nextPosition = new Vector3();

		private float _randomX = 0;
		private float _randomZ = 0;

		private void Awake()
		{
			_sinner = GetComponent<Sinner>();
			_transform = GetComponent<Transform>();

			if(walkableGround != null)
			{
				walkableGround = walkableGround.transform;
			}
		}

		void Start()
		{
			_nextPosition = _transform.localPosition;
		}

		private void Update()
		{
			if (Mathf.Floor(_transform.localPosition.x) == Mathf.Floor(_nextPosition.x) ||
			Mathf.Floor(_transform.localPosition.z) == Mathf.Floor(_nextPosition.z))
			{
				UpdateMoveAgent();
			}
		}

		public void UpdateMoveAgent()
		{
			if(walkableGround != null)
			{
				_nextPosition = FindNextPosition() + walkableGround.position;
			}
			MoveNext(_nextPosition);
		}

		public void MoveNext(Vector3 nextPosition)
		{
			_sinner.agent.SetDestination(nextPosition);
		}

		public Vector3 FindNextPosition()
		{
			_randomX = UnityEngine.Random.Range(-_walkableGroundRadius, _walkableGroundRadius);
			_randomZ = UnityEngine.Random.Range(-_walkableGroundRadius, _walkableGroundRadius);

			return new Vector3(_randomX, 0, _randomZ);
		}
	}
}