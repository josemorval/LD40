using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{

	[SerializeField]
	private GameManager _gameManager;
	[SerializeField]
	private float _checkpointTime;

	void OnTriggerEnter2D (Collider2D other)
	{
		_gameManager.UpdateCheckPoint (_checkpointTime);
		gameObject.SetActive (false);
	}
}
