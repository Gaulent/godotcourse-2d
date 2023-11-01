using Godot;
using System;

public partial class PlayerMove : State
{
	[Export] private PlayerController _player;
	[Export] private AnimatedSprite2D sprite;
	
	public override void PhysicsUpdate(double delta)
	{
		var Direction = Input.GetAxis("ui_left", "ui_right");
		
		// Cambia la direccion del sprite
		sprite.FlipH = Direction switch
		{
			< 0 => true,
			> 0 => false,
			_ => sprite.FlipH
		};
		
		// Permite moverse al personaje
		Vector2 velocity = _player.Velocity;
		
		if (Direction != 0f)
			velocity.X = Direction * _player._speed;
		else
			velocity.X = Mathf.MoveToward(_player.Velocity.X, 0, _player._speed);
		
		_player.Velocity = velocity;	
	}
}
