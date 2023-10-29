using Godot;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class EnemyController : CharacterBody2D
{
	public const float Speed = 50.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	[Export] private RayCast2D leftFoot;
	[Export] private RayCast2D rightFoot;
	[Export] private RayCast2D leftHand;
	[Export] private RayCast2D rightHand;
	private bool facingRight = true;
	[Export] private AnimatedSprite2D sprite;
	[Export] private AnimationPlayer animator;
	private bool isDyeing = false;

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
		if (IsOnFloor())
		{
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


	public void Die()
	{
		if (isDyeing) return;

		isDyeing = true;
		animator.CurrentAnimation = "Enemy_Death";
		CollisionLayer = 0; // Desactivar el layer
		animator.AnimationFinished += _ => QueueFree();
	}
}
