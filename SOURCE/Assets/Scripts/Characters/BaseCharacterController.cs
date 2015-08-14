using UnityEngine;
using System.Collections;

public class BaseCharacter : MonoBehaviour 
{
	public float movementSpeed = 10.0f;
	public float jumpForce = 7.0f;

	public float ballImpulseForce = 8.0f;
	public float ballImpulseDelta = 0.12f;			//How much force we can random...

	public bool isGrounded;
	protected PhysicsController2D physicsController;

	protected virtual void Awake()
	{
		if (physicsController == null)
			physicsController = GetComponent<PhysicsController2D> ();
	}

	protected virtual void FixedUpdate()
	{
		isGrounded = physicsController.IsGrounded ();
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

	public virtual void OnCollisionEnter2D(Collision2D pCollisionInfo)
	{
		if(pCollisionInfo.transform.CompareTag("Ball"))
		{
			BallScript ball = pCollisionInfo.transform.GetComponent<BallScript> ();
			Vector3 direction = ball.CachedTransform.position - transform.position;

			if(direction.y < 0.8f)
				direction.y = 0.8f;

			float force = ballImpulseForce + (Random.Range (-ballImpulseDelta, ballImpulseDelta) * ballImpulseForce);
			ball.ApplyForce (direction.normalized, force);
		}
	}
}
