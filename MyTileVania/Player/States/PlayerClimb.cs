using Godot;
using System;

public partial class PlayerClimb : State
{
	private PlayerController _player;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = (PlayerController)Owner;
	}

	public override void PhysicsUpdate(float delta)
	{

		
		if (!_player.IsTouchingLadder())
		{
			_player.fsm.TransitionTo("Move/Normal");
			return;
		}

		if (Input.IsActionJustPressed("ui_accept"))
		{
			//_player.HandleJump();
			_player.fsm.TransitionTo("Move/Normal");
			return;
		}
		
		HandleClimb();
		GetParent<State>().PhysicsUpdate(delta);
	}
	
	
	public void HandleClimb()
	{
		Vector2 velocity = _player.Velocity;
		var _climbDirection = Input.GetAxis("ui_up", "ui_down");
		
		if (_climbDirection != 0f)
			velocity.Y = _climbDirection * _player._speed;
		else
			velocity.Y = Mathf.MoveToward(_player.Velocity.Y, 0, _player._speed);

		_player.Velocity = velocity;
	}
}
