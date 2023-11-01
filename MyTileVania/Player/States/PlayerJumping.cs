using Godot;
using System;
using Godot.Collections;

public partial class PlayerJumping : State
{
	[Export] private PlayerController _player;
	
	// Al entrar, salta
	public override void Enter(Dictionary payload = null)
	{
		Vector2 velocity = _player.Velocity;
		velocity.Y = _player._jumpVelocity;
		_player.Velocity = velocity;
	}

	public override void PhysicsUpdate(double delta)
	{
		float _climbDirection = Input.GetAxis("ui_up", "ui_down");

		// Si quiere escalar, escala
		if (_player.IsTouchingLadder() && _climbDirection != 0)
		{
			fsm.TransitionTo("Move/Climb");
			return;
		}

		// Si se libera el boton, baja
		if (Input.IsActionJustReleased("ui_accept"))
		{
			Vector2 velocity = _player.Velocity;
			velocity.Y = _player.GetRealVelocity().Y * 0.5f;
			_player.Velocity = velocity;
		}
		
		// Si esta callendo, vuelve a normal
		if (_player.GetRealVelocity().Y > 0)
			fsm.TransitionTo("Move/Normal");
		
		// Ejecuta el estado 'Move'
		GetParent<State>().PhysicsUpdate(delta);
	}
}
