using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UVImageControl : MonoBehaviour
{
	RawImage _rawImage;
	public bool _isLoop = true;
	bool _isFinish = false;
	public bool _isPause = false;
	public bool _isRestart = false;
	private float _curTime;
	public float _intervalTime = 0.015f;

	Rect _uvRect;


	void Start()
	{
		_rawImage = GetComponent<RawImage>();
		_uvRect = _rawImage.uvRect;
		_uvRect.x = 0;
		_uvRect.y = 1 - _uvRect.height;
		_rawImage.uvRect = _uvRect;
	}

	void Update()
	{
		if (_isRestart)
		{
			Restart();
		}

		if (!_isFinish)
		{
			PlayUVAnim();
		}
	}

	void Restart()
	{
		_isRestart = false;
		_isPause = false;
		_curTime = 0.0f;
		_isFinish = false;
		_uvRect.x = 0;
		_uvRect.y = 1 - _uvRect.height;
		_rawImage.uvRect = _uvRect;
	}

	void PlayUVAnim()
	{
		if (_isPause)
		{
			return;
		}

		if (_curTime <= _intervalTime)
		{
			_curTime += Time.deltaTime;
			return;
		}
		_curTime -= _intervalTime;

		if (_uvRect.x < 1)
		{
			_uvRect.x += _uvRect.width;
		}
		_rawImage.uvRect = _uvRect;

		if (_uvRect.x > 1 - _uvRect.width)
		{
			_uvRect.x = -_uvRect.width;
			_uvRect.y -= _uvRect.height;
		}

		if (_uvRect.y < 0)
		{
			_uvRect.x = 0;
			_uvRect.y = 1 - _uvRect.height;
			if (_isLoop)
			{
				_isFinish = false;
			}
			else
			{
				_isFinish = true;
			}
		}
	}
}
