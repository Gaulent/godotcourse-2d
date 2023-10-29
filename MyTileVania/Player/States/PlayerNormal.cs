using Godot;
using System;

public partial class PlayerNormal : State
{
	private PlayerController _player;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = (PlayerController)Owner;
	}

	public override void Enter()
	{
		GD.Print("ENTER");
	}

	public override void PhysicsUpdate(float delta)
	{

		var _climbDirection = Input.GetAxis("ui_up", "ui_down");
		
		
		
		if (_player.IsTouchingLadder() && _climbDirection != 0)
		{
			_player.fsm.TransitionTo("Move/Climb");
			return;
		}

		_player.HandleGravity(delta);

		if (Input.IsActionJustPressed("ui_accept"))
		{
			GD.Print("DENTRO");
			if ((_player.IsOnFloor() || !_player._coyoteTimer.IsStopped()))
			{
				HandleJump();
				GD.Print("HOLA");
				_player.fsm.TransitionTo("Move/Jumping");
			}
		}
		GetParent<State>().PhysicsUpdate(delta);
	}
	
	public void HandleJump()
	{
		Vector2 velocity = _player.Velocity;
		GD.Print("In jump");
		velocity.Y = _player._jumpVelocity;
		_player.Velocity = velocity;
	} 
}
