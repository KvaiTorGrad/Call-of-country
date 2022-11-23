using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SimpleTouchController : MonoBehaviour {

	public delegate void TouchDelegate(Vector2 value);
	public event TouchDelegate TouchEvent;

	public delegate void TouchStateDelegate(bool isActiveDrag);
	public static event TouchStateDelegate TouchStateEvent;

	[SerializeField]
	private RectTransform joystickArea;
	private Vector2 _movementVector;

	public Vector2 GetTouchPosition { get => _movementVector; }

	public void Drag()
	{
		if(TouchStateEvent != null)
			TouchStateEvent(true);
	}

	public void EndDrag()
	{
		_movementVector = joystickArea.anchoredPosition = Vector2.zero;
		if(TouchStateEvent != null)
			TouchStateEvent(false);
	}

	public void OnValueChanged(Vector2 value)
	{
			_movementVector.x = ((1 - value.x) - 0.5f) * 2f;
			_movementVector.y = ((1 - value.y) - 0.5f) * 2f;

			if(TouchEvent != null)
				TouchEvent(_movementVector);
	}

}
