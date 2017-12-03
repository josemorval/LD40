using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	void Start ()
	{
		SetupIntroLevel ();
	}

	void Update ()
	{
		if (_gameState == STATE_INTRO) {
			UpdateIntroLevel ();
		} else if (_gameState == STATE_GAMEPLAY) {
			UpdateGameplayLevel ();
		} else if (_gameState == STATE_GAMEOVER) {
			UpdateGameoverLevel ();
		}

		//Do nothing
	}


	public void SetupIntroLevel ()
	{
		ActivateLevels (false);
		_gameState = STATE_INTRO;
	}

	public void SetupGameplayLevel ()
	{
		ActivateLevels (false);
		_gameState = STATE_GAMEPLAY;
		_globalTime = 0f;

	}

	public void SetupGameOverLevel ()
	{
		ActivateLevels (false);
		_gameState = STATE_GAMEOVER;
	}

	public void UpdateIntroLevel ()
	{

	}

	public void UpdateGameplayLevel ()
	{
		
	}

	public void UpdateGameoverLevel ()
	{
		
	}

	public void ActivateLevels (bool activate)
	{
		ActivateLevels (activate, activate, activate);
	}

	public void ActivateLevels (bool a, bool b, bool c)
	{
		_introContentTransform.gameObject.SetActive (a);
		_gameplayContentTransform.gameObject.SetActive (b);
		_gameoverContentTransform.gameObject.SetActive (c);
	}


	[SerializeField]
	private Transform _cameraTransform;

	[SerializeField]
	private Transform _introContentTransform;
	[SerializeField]
	private Transform _gameplayContentTransform;
	[SerializeField]
	private Transform _gameoverContentTransform;

	[SerializeField]
	private float _dreamLevel;
	[SerializeField]
	private float _timeCheckpoint;
	[SerializeField]
	private float _globalTime;

	[SerializeField]
	private int _gameState;

	private int STATE_INTRO = 0;
	private int STATE_GAMEPLAY = 10;
	private int STATE_GAMEOVER = 100;

}
