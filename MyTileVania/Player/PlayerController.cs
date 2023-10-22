using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
	[Export] private float Speed = 250.0f;
	[Export] private float JumpVelocity = -550.0f;
	private Timer coyoteTime;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle() * 2f;
	public float direction { get; private set; }
	private bool tryToJump;


	public override void _Ready()
	{
		coyoteTime = GetNode<Timer>("CoyoteTime");
	}


	public override void _Process(double delta)
	{
		direction = Input.GetAxis("ui_left", "ui_right");

		if (Input.IsActionJustPressed("ui_accept"))
			tryToJump = true;
	}

	
	

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			if (GetRealVelocity().Y > 0)
			{
				velocity.Y += gravity * (float)delta * 1.5f;
			}
			else
			{
				velocity.Y += gravity * (float)delta;				
			}
			
		}
			

		// Handle Jump.
		if (tryToJump && (IsOnFloor() || !coyoteTime.IsStopped()))
		{
			velocity.Y = JumpVelocity;
			tryToJump = false;
		}

		if (direction != 0f)
		{
			velocity.X = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		if (IsOnFloor())
			coyoteTime.Start();
		   
		

		
		
		Velocity = velocity;
		MoveAndSlide();
	}
}
