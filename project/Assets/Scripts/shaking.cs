using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allow the trees to shake and produce fruits 
/// when the player enters in collision with them.
/// </summary>
public class shaking : MonoBehaviour
{
	private Vector3 originPosition;
	private Quaternion originRotation;
	public float shake_decay = 0.002f;
	public float shake_intensity = .3f;

	/// <summary>
    /// Text answering the player to press a button to make the tree shake
    /// </summary>
	public GameObject shakeText;
	/// <summary>
    /// indicates whether the shaking is allowes
    /// </summary>
	private bool shakeAllowed;

	private float temp_shake_intensity = 0;

	/// <summary>
    /// fruit produced by the tree
    /// </summary>
	public GameObject fruit;

	/// <summary>
    /// Start method, set active the shaking text.
    /// </summary>
	void Start()
    {
		shakeText.SetActive(false);
    }

	/// <summary>
    /// Update method
    /// Makes the tree shaking.
    /// </summary>
	void Update()
	{
		// if the shaking is allowed and the player presses the T key, makes the tree shake
		if (shakeAllowed && Input.GetKeyDown(KeyCode.T))
			Shake();
		// if it's still shaking time, makes the tree shake
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

	/// <summary>
    /// shaking method
    /// modify the position and rotation of the tree and produce fruit.
    /// </summary>
	void Shake()
	{
		originPosition = transform.position;
		originRotation = transform.rotation;
		temp_shake_intensity = shake_intensity;
		ProduceFruit();
	}

	/// <summary>
    /// When the player enters in collision with the tree, set the shaking text active.
    /// </summary>
    /// <param name="collisionInfo">information about the collision</param>
	void OnCollisionEnter(Collision collisionInfo)
	{
		if (collisionInfo.collider.gameObject.layer == LayerMask.NameToLayer(Constants.PLAYER_MASK))
		{
			shakeText.SetActive(true);
			shakeAllowed = true;
		}
	}

	/// <summary>
    /// When the player go out of the collision, disable the shaking text
    /// </summary>
    /// <param name="collisionInfo"></param>
	void OnCollisionExit(Collision collisionInfo)
    {
		// Hit by a player
		if (collisionInfo.collider.gameObject.layer == LayerMask.NameToLayer(Constants.PLAYER_MASK))
		{
			shakeText.SetActive(false);
			shakeAllowed = false;
		}
	}

	/// <summary>
    /// Produce fruit.
    /// </summary>
	void ProduceFruit()
    {
		// randomly determine how many fruits will be produced
		int numberOfFruit = Random.Range(0, 4);

		// for each fruit, we instanciate it in a random position around the tree.
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
