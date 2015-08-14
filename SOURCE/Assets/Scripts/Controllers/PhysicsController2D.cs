using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsController2D : MonoBehaviour 
{
	private Rigidbody2D cachedRigidbody;

	public LayerMask groundLayers;

	public class GroundInfo
	{
		public Transform ground;
		public bool isGrounded;
	}
	private GroundInfo groundInfos;

	private Rigidbody2D CachedRigidbody {
		get {
			if (cachedRigidbody == null)
				cachedRigidbody = GetComponent<Rigidbody2D> ();
			
			return cachedRigidbody;
		}
	}

	public void SetVelocity(Vector2 pVelocity)
	{
		CachedRigidbody.velocity = pVelocity;
	}

	public Vector2 GetVelocity()
	{
		return CachedRigidbody.velocity;
	}

	public void SetPosition(Vector3 pPosition, float pSpeed)
	{
		CachedRigidbody.position = Vector3.MoveTowards (CachedRigidbody.position, 
			pPosition, pSpeed * Time.deltaTime);
	}

	public void AddForce(Vector2 pDirection, float pForce)
	{
		CachedRigidbody.AddForce(pDirection * pForce, ForceMode2D.Force);
	}

	public bool IsGrounded()
	{
		if (groundInfos == null)
			groundInfos = new GroundInfo ();

		groundInfos.isGrounded = false;
		groundInfos.ground = null;

		RaycastHit2D hit = Physics2D.Raycast (CachedRigidbody.position, Vector3.down, 0.8f, groundLayers.value);

		if(hit.transform != null)
		{
			groundInfos.ground = hit.transform;
			groundInfos.isGrounded = true;
		}

		return groundInfos.isGrounded;
	}
}
