using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class BallScript : MonoBehaviour 
{
	private Transform cachedTransform;
	private Rigidbody2D cachedRigidbody;
	private Collider2D cachedCollider;

	public Transform CachedTransform {
		get {
			if(cachedTransform == null)
				cachedTransform = GetComponent<Transform> ();
			
			return cachedTransform;
		}
	}

	void Awake()
	{
		
		cachedRigidbody = GetComponent<Rigidbody2D> ();
		cachedCollider = GetComponent<Collider2D> ();

//		cachedCollider.isTrigger = true;
	}

	public void ApplyForce(Vector3 pDirection, float pForce)
	{
		cachedRigidbody.velocity = pDirection * pForce;
	}


    void OnCollisionEnter2D(Collision2D pCol)
    {

        // super-pro code
        if (pCol.gameObject.name == "Beach")
            Prime31.MessageKit.MessageKit.post(StateMessages.GP_OnGameOver);
    }
}
