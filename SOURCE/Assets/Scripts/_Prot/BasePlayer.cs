using UnityEngine;
using System.Collections;

public class BasePlayer : BaseCharacter 
{
	private bool isSwiping = false;

	private Vector3 targetPosition;

	public enum SIDE
	{
		LEFT,
		RIGHT,
	}

	public SIDE playerSide;

	void OnEnable()
	{
		EasyTouch.On_TouchDown += OnTouchDown;
		EasyTouch.On_TouchUp += OnTouchUp;
		EasyTouch.On_SimpleTap += OnSimpleTap;
		EasyTouch.On_SwipeStart += OnSwipeStart;
		EasyTouch.On_SwipeEnd += OnSwipeEnd;
	}

	void OnDisable()
	{
		EasyTouch.On_TouchDown -= OnTouchDown;
		EasyTouch.On_SimpleTap -= OnSimpleTap;
		EasyTouch.On_SwipeStart -= OnSwipeStart;
		EasyTouch.On_SwipeEnd -= OnSwipeEnd;
	}

	protected virtual void OnSwipeStart(Gesture pGesture)
	{
		if (IsValidInput(pGesture.position))
		{
			isSwiping = true;
		}
	}

	protected virtual void OnSwipeEnd(Gesture pGesture)
	{
		if (IsValidInput(pGesture.position))
		{
			isSwiping = false;
		}
	}

	void Update()
	{
		physicsController.SetPosition (transform.position + targetPosition, movementSpeed);
	}

	protected virtual void OnTouchDown(Gesture pGesture)
	{
		if(IsValidInput(pGesture.position))
		{
			RaycastHit2D hit2D = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (pGesture.position), Vector2.zero);

			transform.position = Vector3.MoveTowards(transform.position, 
				new Vector3 (hit2D.point.x, transform.position.y, transform.position.z), 
				Time.deltaTime * 10.0f);

			Move(new Vector3 (hit2D.point.x, transform.position.y, transform.position.z), movementSpeed);
		}
	}

	protected virtual void OnTouchUp(Gesture pGesture)
	{
		if(IsValidInput(pGesture.position))
			targetPosition = Vector3.zero;
	}

	protected virtual void OnSimpleTap(Gesture pGesture)
	{
		if (IsValidInput(pGesture.position))
		{
			Jump (Vector2.up, jumpForce);
		}
	}

	public bool IsValidInput(Vector3 pInputPosition)
	{
		return (playerSide == SIDE.LEFT && pInputPosition.x <= Screen.width / 2.0f ||
		(playerSide == SIDE.RIGHT && pInputPosition.x > Screen.width / 2.0f));
	}
}
