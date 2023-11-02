using Godot;
using System;

public partial class PlayerNormal : State
{
	[Export] public Timer _coyoteTimer;
	[Export] private PlayerController _player;
	[Export] private AnimatedSprite2D sprite;

	// Actualiza la animacion
	public override void Update(double delta)
	{
		sprite.Animation = _player.GetRealVelocity().X!=0 ? "moving" : "idle";	
		
		if (!_player.IsOnFloor())
		{
			sprite.Animation = _player.GetRealVelocity().Y > 0 ? "jump_down" : "jump_up";
		}
	}

	public override void PhysicsUpdate(double delta)
	{
		var _climbDirection = Input.GetAxis("ui_up", "ui_down");

		// Si puede y quiere escalar, escala
		if (_player.IsTouchingLadder() && _climbDirection < 0)
		{
			fsm.TransitionTo("Move/Climb");
			return;
		}
		
		// Reinicia el coyote time si toca el suelo
		if (_player.IsOnFloor())
			_coyoteTimer.Start();
		
		// Si quiere saltar, y puede, salta.
		if (Input.IsActionJustPressed("ui_accept") && (_player.IsOnFloor() || !_coyoteTimer.IsStopped()))
		{
			fsm.TransitionTo("Move/Jumping");
		}

		// Ejecuta estado mover
		GetParent<State>().PhysicsUpdate(delta);
	}
}
