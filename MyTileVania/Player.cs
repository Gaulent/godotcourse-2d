using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float Speed = 300.0f;
	[Export] public float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private float direction;
	private bool hasToJump;
	private AnimatedSprite2D _sprite;
	

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}


	public override void _Process(double delta)
	{
		direction = Input.GetAxis("ui_left", "ui_right");

		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			hasToJump = true;

		_sprite.FlipH = direction switch
		{
			< 0 => true,
			> 0 => false,
			_ => _sprite.FlipH
		};

		if(direction!=0)
			_sprite.Animation = "moving";
		else
			_sprite.Animation = "idle";
	}


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (hasToJump)
		{
			velocity.Y = JumpVelocity;
			hasToJump = false;
		}

		if (direction != 0f)
		{
			velocity.X = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
