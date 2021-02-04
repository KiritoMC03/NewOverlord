using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NewOverlord
{
	[RequireComponent(typeof(Sinner))]
	public class WanderMode : MonoBehaviour
	{
        [SerializeField] private Sinner _sinner;
		[SerializeField] private Transform _walkableGround;
		[SerializeField] private float _walkableGroundRadius = 1f;

		private Transform _transform;
		private Vector3 _nextPosition = new Vector3();
		//private StateController _stateController;

		private float _randomX = 0;
		private float _randomZ = 0;

		private void Awake()
		{
			_sinner = GetComponent<Sinner>();
			_transform = GetComponent<Transform>();
			_walkableGround = _walkableGround.transform;
			//_stateController = GetComponent<StateController>();
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
			_nextPosition = FindNextPosition() + _walkableGround.position;
			MoveNext(_nextPosition);
		}

		private void MoveNext(Vector3 nextPosition)
		{
			_sinner.agent.SetDestination(nextPosition);
		}

		private Vector3 FindNextPosition()
		{
			_randomX = Mathf.Clamp(UnityEngine.Random.Range(-_walkableGroundRadius, _walkableGroundRadius), -_walkableGroundRadius, _walkableGroundRadius);
			_randomZ = Mathf.Clamp(UnityEngine.Random.Range(-_walkableGroundRadius, _walkableGroundRadius), -_walkableGroundRadius, _walkableGroundRadius);

			return new Vector3(_randomX, 0, _randomZ);
		}
	}
}