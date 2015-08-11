using UnityEngine;
using System.Collections;

public class BasePlayer : MonoBehaviour 
{
	private Rigidbody2D cachedRigidbody;

	public enum SIDE
	{
		LEFT,
		RIGHT,
	}

	public SIDE playerSide;

	void OnEnable()
	{
		if (cachedRigidbody == null)
			cachedRigidbody = GetComponent<Rigidbody2D> ();

		EasyTouch.On_TouchDown += OnTouchDown;
		EasyTouch.On_SimpleTap += OnSimpleTap;
	}

	void OnDisable()
	{
		EasyTouch.On_TouchDown -= OnTouchDown;
		EasyTouch.On_SimpleTap -= OnSimpleTap;
	}

	public void Update()
	{

	}

	void OnTouchDown(Gesture pGesture)
	{
		if(playerSide == SIDE.LEFT)
		{
			if(pGesture.position.x <= Screen.width / 2.0f)
			{
				RaycastHit2D hit2D = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (pGesture.position), Vector2.zero);

				transform.position = Vector3.MoveTowards(transform.position, 
					new Vector3 (hit2D.point.x, transform.position.y, transform.position.z), 
					Time.deltaTime * 10.0f);
			}
		}
		else if(playerSide == SIDE.RIGHT)
		{
			if(pGesture.position.x > Screen.width / 2.0f)
			{
				RaycastHit2D hit2D = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (pGesture.position), Vector2.zero);

				transform.position = Vector3.MoveTowards(transform.position, 
					new Vector3 (hit2D.point.x, transform.position.y, transform.position.z), 
					Time.deltaTime * 10.0f);
			}
		}
	}

	void OnSimpleTap(Gesture pGesture)
	{
		//cachedRigidbody.AddForce (Vector2.up * 350.0f);
	}
}
