using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour 
{
	private Rigidbody2D cachedRigidbody;

	void Awake()
	{
		cachedRigidbody = GetComponent<Rigidbody2D> ();
	}

	public void OnCollisionEnter2D(Collision2D pOther)	
	{
		if(pOther.transform.CompareTag("Player"))
		{
			Vector2 direction =  transform.position - pOther.transform.position ;
			direction = direction.normalized;
			cachedRigidbody.velocity = direction * 8.0f;
		}
	}
}
