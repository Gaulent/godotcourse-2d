using Godot;
using System;

public partial class PlayerJumping : State
{
	private PlayerController _player;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = (PlayerController)Owner;
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

		if (Input.IsActionJustReleased("ui_accept"))
		{
			GD.Print("True");
			Vector2 velocity = _player.Velocity;
			velocity.Y = _player.GetRealVelocity().Y * 0.5f;
			_player.Velocity = velocity;
		}
		
		
		if (_player.GetRealVelocity().Y > 0)
			_player.fsm.TransitionTo("Move/Normal");
		
		GetParent<State>().PhysicsUpdate(delta);
	}
}
