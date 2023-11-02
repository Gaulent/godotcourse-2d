using Godot;
using System;
using Godot.Collections;

public partial class PlayerClimb : State
{
	[Export] private PlayerController _player;
	[Export] private AnimatedSprite2D sprite;
	
	// Al entrar anula la gravedad
	public override void Enter(Dictionary payload = null)
	{
		_player.GravityMultiplier = 0;
		sprite.Animation = "climbing";
	}

	// Al salir se restablece
	public override void Exit()
	{
		_player.GravityMultiplier = 1;
	}

	public override void PhysicsUpdate(double delta)
	{
		// Si no toca escalera, vuelve a normal
		if (!_player.IsTouchingLadder())
		{
			fsm.TransitionTo("Move/Normal");
			return;
		}

		// Si quiere saltar, salta
		if (Input.IsActionJustPressed("ui_accept"))
		{
			fsm.TransitionTo("Move/Jumping");
			return;
		}
		
		// Movimiento de escalada
		HandleClimb();
		
		// Ejecuta el estado 'Move'
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
