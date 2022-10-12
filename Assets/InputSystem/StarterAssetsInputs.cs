using FishNet.Object;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

public class StarterAssetsInputs : NetworkBehaviour
{
	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;
	public bool jump;
	public bool sprint;
	public bool ready;

	[Header("Movement Settings")]
	public bool analogMovement;

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	public void OnMove(InputValue value)
	{
		if(!base.IsOwner) return;
		MoveInput(value.Get<Vector2>());
	}

	public void OnLook(InputValue value)
	{
		if(!base.IsOwner) return;
		if(cursorInputForLook)
		{
			LookInput(value.Get<Vector2>());
		}
	}

	public void OnJump(InputValue value)
	{
		if(!base.IsOwner) return;
		JumpInput(value.isPressed);
	}

	public void OnSprint(InputValue value)
	{
		if(!base.IsOwner) return;
		SprintInput(value.isPressed);
	}

	public void OnReady(InputValue value)
	{
		if(!base.IsOwner) return;
		ReadyInput(value.isPressed);
	}
#endif


	public void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;
	} 

	public void LookInput(Vector2 newLookDirection)
	{
		look = newLookDirection;
	}

	public void JumpInput(bool newJumpState)
	{
		jump = newJumpState;
	}

	public void SprintInput(bool newSprintState)
	{
		sprint = newSprintState;
	}
	
	private void OnApplicationFocus(bool hasFocus)
	{
		SetCursorState(cursorLocked);
	}

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}

	private void ReadyInput(bool newReadyState)
	{
		ready = newReadyState;
	}
}