using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	void Start ()
	{
		ActivateLevels (false, false, false, false);
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
		ActivateLevels (true, false, false, false);
		_gameState = STATE_INTRO;
		_cameraTransform.position = _introContentTransform.position + new Vector3 (0f, 0f, -10f);
	}

	public void SetupGameplayLevel ()
	{
		ActivateLevels (false, true, false, true);
		_gameState = STATE_GAMEPLAY;
		_globalTime = 0f;
		_dreamLevel = 1f;
		_timeCheckpoint = _initTimeCheckPoint;
		_currentRatioDreamLevel = _ratioDreamLevelLap [_numberLaps];
		SetupCheckPoint ();
	}

	public void SetupGameOverLevel ()
	{
		ActivateLevels (true, false, true, false);
		_gameState = STATE_GAMEOVER;
		_cameraTransform.position = _gameoverContentTransform.position + new Vector3 (0f, 0f, -10f);

	}

	public void UpdateIntroLevel ()
	{
		if (Input.GetKeyDown (KeyCode.A)) {
			SetupGameplayLevel ();
		}
	}

	public void UpdateGameplayLevel ()
	{
		_cameraTransform.transform.position = _carTransform.position + new Vector3 (0f, 0f, -10f);

		_dreamLevel -= _currentRatioDreamLevel * Time.deltaTime;
		_globalTime += Time.deltaTime;
		_timeCheckpoint -= Time.deltaTime;

		if (_dreamLevel < 0f || _timeCheckpoint < 0f) {
			SetupGameOverLevel ();
		} else {
			_dreamLevelTransform.localScale = new Vector3 (_dreamLevel, 1f, 1f);
			_timeCheckPointText.text = _timeCheckpoint.ToString ("#0") + "s";
			_globalTimeText.text = _globalTime.ToString ("#0") + "s";
		}

	
	}

	public void UpdateGameoverLevel ()
	{
		if (Input.GetKeyDown (KeyCode.A)) {
			SetupIntroLevel ();
		}	
	}

	public void ActivateLevels (bool activate)
	{
		ActivateLevels (activate, activate, activate, activate);
	}

	public void ActivateLevels (bool a, bool b, bool c, bool d)
	{
		_introContentTransform.gameObject.SetActive (a);
		_gameplayContentTransform.gameObject.SetActive (b);
		_gameoverContentTransform.gameObject.SetActive (c);
		_uiTransform.gameObject.SetActive (d);
	}

	public void SetupCheckPoint ()
	{
		for (int i = 0; i < _checkPointList.Length; i++) {
			_checkPointList [i].gameObject.SetActive (false);
		}

		_currentCheckPoint = 0;
		_checkPointList [_currentCheckPoint].gameObject.SetActive (true);
	}

	public void UpdateCheckPoint (float time)
	{
		_checkPointList [_currentCheckPoint].gameObject.SetActive (false);

		if (_currentCheckPoint != _checkPointList.Length - 1) {
			_timeCheckpoint += time;
			_timeCheckPointText.text = _timeCheckpoint.ToString ("#0") + "s";
		} else {
			_numberLaps++;
			_currentRatioDreamLevel = _ratioDreamLevelLap [_numberLaps];
		}

		_currentCheckPoint++;
		_currentCheckPoint %= _checkPointList.Length;
		_checkPointList [_currentCheckPoint].gameObject.SetActive (true);
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
	private Transform _uiTransform;

	private float _timeCheckpoint;
	[SerializeField]
	private TextMesh _timeCheckPointText;

	private float _globalTime;
	[SerializeField]
	private TextMesh _globalTimeText;

	private float _dreamLevel;
	[SerializeField]
	private Transform _dreamLevelTransform;
	[SerializeField]
	private float _currentRatioDreamLevel;
	[SerializeField]
	private float[] _ratioDreamLevelLap;

	[SerializeField]
	private int _numberLaps;

	[SerializeField]
	private int _gameState;

	[SerializeField]
	private float _initTimeCheckPoint;

	[SerializeField]
	private Transform _carTransform;

	[SerializeField]
	private int _currentCheckPoint;
	[SerializeField]
	private Transform[] _checkPointList;


	private int STATE_INTRO = 0;
	private int STATE_GAMEPLAY = 10;
	private int STATE_GAMEOVER = 100;

}
