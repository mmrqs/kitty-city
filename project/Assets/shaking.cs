using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaking : MonoBehaviour
{
	private Vector3 originPosition;
	private Quaternion originRotation;
	public float shake_decay = 0.002f;
	public float shake_intensity = .3f;

	public GameObject shakeText;
	private bool shakeAllowed;

	private float temp_shake_intensity = 0;

	public GameObject fruit;
	public Vector3 spawnValues;

	void Start()
    {
		shakeText.SetActive(false);
    }

	void Update()
	{
		if (shakeAllowed && Input.GetKeyDown(KeyCode.T))
			Shake();
		if (temp_shake_intensity > 0)
		{
			transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
			transform.rotation = new Quaternion(
				originRotation.x + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
				originRotation.y + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
				originRotation.z + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
				originRotation.w + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f);
			temp_shake_intensity -= shake_decay;
		}

	}

	void Shake()
	{
		originPosition = transform.position;
		originRotation = transform.rotation;
		temp_shake_intensity = shake_intensity;
		ProduceFruit();
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		if (collisionInfo.gameObject.name == "Rabbit")
		{
			shakeText.SetActive(true);
			shakeAllowed = true;
		}
	}

	void OnCollisionExit(Collision collisionInfo)
    {
		// Hit by a player
		if (collisionInfo.gameObject.name == "Rabbit")
		{
			shakeText.SetActive(false);
			shakeAllowed = false;
		}
	}

	void ProduceFruit()
    {
		int numberOfFruit = Random.Range(0, 4);
		for (int i = 0; i < numberOfFruit; i++) 
		{
			float x = Random.Range(-1.37f, 1.63f);
			float y = Random.Range(-1.66f, 1.63f);
			Vector3 spawnPosition = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y + 2, gameObject.transform.position.z + 1);
			Instantiate(
				fruit,
				spawnPosition,
				gameObject.transform.rotation);
		}
	}
}
