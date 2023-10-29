using Godot;
using System;

public partial class PlayerMove : State
{
	private PlayerController _player;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Ready");
		_player = (PlayerController)Owner;
	}


	public override void PhysicsUpdate(float delta)
	{
		HandleMovement();

		_player.MoveAndSlide();
	}
	
	public void HandleMovement()
	{
		var Direction = Input.GetAxis("ui_left", "ui_right");
		
		Vector2 velocity = _player.Velocity;
		
		if (Direction != 0f)
			velocity.X = Direction * _player._speed;
		else
			velocity.X = Mathf.MoveToward(_player.Velocity.X, 0, _player._speed);
		
		_player.Velocity = velocity;		
	}
}
