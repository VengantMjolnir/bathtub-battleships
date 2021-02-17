using UnityEngine;
using System.Collections;

public class ShakeTransform : MonoBehaviour
{
	public Transform target;
	public float Duration = 0.5f;
	public float Speed = 3.0f;
	public float Magnitude = 0.1f;
	public float Damping = 0.1f;
	public bool PlayOnStart = true;
	public bool Looping = false;

#if UNITY_EDITOR
	public bool _Test = false;
#endif

	private Transform _CachedTransform;
	private Vector3 _OriginalPosition;

	// -------------------------------------------------------------------------
	public void Start()
	{
		_CachedTransform = target;
		if (target == null)
		{
			_CachedTransform = transform;
		}

		if (PlayOnStart)
		{
			PlayShake();
		}
	}

	// -------------------------------------------------------------------------
	void OnDisable()
	{
		StopAllCoroutines();
		//StartCoroutine("Reset");
		_CachedTransform.localPosition = _OriginalPosition;
	}

	// -------------------------------------------------------------------------
	public void PlayShake()
	{
		_OriginalPosition = _CachedTransform.localPosition;

		StopAllCoroutines();
		StartCoroutine("Shake");
	}

	// -------------------------------------------------------------------------
	void Update()
	{
#if UNITY_EDITOR
		if (_Test)
		{
			_Test = false;
			PlayShake();
		}
#endif
	}

	// -------------------------------------------------------------------------
	IEnumerator Shake()
	{
		do
		{
			float randomStart = Random.Range(-1000.0f, 1000.0f);

			float x = Util.Noise.GetNoise(randomStart, 0.0f, 0.0f) * 2.0f - 1.0f;
			float y = Util.Noise.GetNoise(0.0f, randomStart, 0.0f) * 2.0f - 1.0f;
			Vector3 startPosition = new Vector3(x * Magnitude, y * Magnitude, _OriginalPosition.z);
			Vector3 position = _OriginalPosition;
			float distance = Vector3.Distance(startPosition, position);
			while (distance > 0.125f)
			{
				position = Vector2.Lerp(position, startPosition, Damping);
				_CachedTransform.localPosition = position;
				distance = Vector3.Distance(startPosition, position);

				yield return null;
			}

			float elapsed = 0.0f;
			while (elapsed < Duration)
			{

				elapsed += Time.deltaTime;

				float percentComplete = elapsed / Duration;
				float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

				// Calculate the noise parameter starting randomly and going as fast as speed allows
				float alpha = randomStart + Speed * percentComplete;

				// map noise to [-1, 1]
				x = Util.Noise.GetNoise(alpha, 0.0f, 0.0f) * 2.0f - 1.0f;
				y = Util.Noise.GetNoise(0.0f, alpha, 0.0f) * 2.0f - 1.0f;
				x *= Magnitude * damper;
				y *= Magnitude * damper;

				position = new Vector3(x, y, _OriginalPosition.z);
				_CachedTransform.localPosition = position;

				yield return null;
			}

			distance = Vector3.Distance(_OriginalPosition, position);
			while (distance > 0.125f)
			{
				position = Vector2.Lerp(position, _OriginalPosition, Damping);
				_CachedTransform.localPosition = position;
				distance = Vector3.Distance(_OriginalPosition, position);

				yield return null;
			}
		} while (Looping);

		_CachedTransform.localPosition = _OriginalPosition;
		Debug.Log("Done shaking");
	}

	IEnumerator Reset()
	{
		Vector3 position = _CachedTransform.localPosition;
		float distance = Vector3.Distance(_OriginalPosition, position);
		while (distance > 0.125f)
		{
			position = Vector2.Lerp(position, _OriginalPosition, Damping);
			_CachedTransform.localPosition = position;
			distance = Vector3.Distance(_OriginalPosition, position);

			yield return null;
		}

		_CachedTransform.localPosition = _OriginalPosition;
	}
}
