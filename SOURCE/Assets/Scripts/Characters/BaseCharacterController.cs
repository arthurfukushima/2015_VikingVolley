using UnityEngine;
using System.Collections;

public class BaseCharacter : MonoBehaviour 
{
	public float movementSpeed = 10.0f;
	public float jumpForce = 7.0f;

	protected PhysicsController2D physicsController;


	protected virtual void Awake()
	{
		if (physicsController == null)
			physicsController = GetComponent<PhysicsController2D> ();
	}

	protected virtual void Move(Vector3 pDirection, float pSpeed)
	{
		transform.position = Vector3.MoveTowards (transform.position, pDirection, Time.deltaTime * pSpeed);
//		physicsController.SetPosition(pDirection, pSpeed);
	}

	protected virtual void Jump(Vector2 pDirection, float pForce)
	{
		Vector2 velocity = physicsController.GetVelocity ();
		velocity.y = pDirection.y * pForce;
		physicsController.SetVelocity(velocity);
	}
}
