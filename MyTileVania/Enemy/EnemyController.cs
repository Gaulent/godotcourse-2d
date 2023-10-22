using Godot;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class EnemyController : CharacterBody2D
{
	public const float Speed = 50.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private RayCast2D leftFoot;
	private RayCast2D rightFoot;
	private RayCast2D leftHand;
	private RayCast2D rightHand;
	private bool facingRight = true;
	private AnimatedSprite2D sprite;
	
	public override void _Ready()
	{
		leftFoot = GetNode<RayCast2D>("LeftFoot");
		rightFoot = GetNode<RayCast2D>("RightFoot");
		leftHand = GetNode<RayCast2D>("LeftHand");
		rightHand = GetNode<RayCast2D>("RightHand");
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		if (facingRight)
			velocity.X = Speed;
		else
			velocity.X = -Speed;
		
		Velocity = velocity;
		MoveAndSlide();
		
		// Si no se hace despues del MoveAndSlide se puede quedar pillado en muros.
		
		if ((!rightFoot.IsColliding() && facingRight) ||
		    (!leftFoot.IsColliding() && !facingRight) ||
		    (leftHand.IsColliding() && !facingRight) ||
		    (rightHand.IsColliding() && facingRight))
		{
			facingRight = !facingRight;
			sprite.FlipH = !facingRight;
		}		
	}
}
