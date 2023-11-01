using Godot;
using System;
using Godot.Collections;

public partial class PlayerController : CharacterBody2D
{
	[Export] public Area2D _areaDetector;
	
	[Export] public float _speed = 250.0f;
	[Export] public float _jumpVelocity = -550.0f;
	[Export] private StateMachine fsm;

	public float GravityMultiplier { get; set; }

	public float GetGravity()
	{
		return ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle() * 2f * GravityMultiplier;
	}
	
	public override void _Ready()
	{
		GravityMultiplier = 1f;
		fsm = GetNode<StateMachine>("StateMachine");
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleGravity(delta);
		
		MoveAndSlide();

		HandleEnemyCollisions();
	}

	public void HandleEnemyCollisions()
	{	
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			KinematicCollision2D collision = GetSlideCollision(i);
			Node target = (Node)collision.GetCollider();

			if (target is EnemyController)
			{
				EnemyController enemy = (EnemyController)target;
				if (collision.GetNormal() == Vector2.Up)
				{
					Vector2 velocity = Velocity;
					velocity.Y = _jumpVelocity * 0.75f;
					Velocity = velocity;
					enemy.Die();
				}
				else
				{
					GetHurt(collision.GetNormal());
				}
			}
		}
	}

	public void GetHurt(Vector2 normal)
	{
		var payload = new Dictionary{{"normal", normal}};
		fsm.TransitionTo("Hurt", payload);
	} 
	
	public void HandleGravity(double delta)
	{
		Vector2 velocity = Velocity;
		// Add the gravity.
		if (!IsOnFloor())
		{
			if (GetRealVelocity().Y > 0)
				velocity.Y += GetGravity() * (float)delta * 1.5f;
			else
				velocity.Y += GetGravity() * (float)delta;				
		}
		Velocity = velocity;
	}

	public bool IsTouchingLadder()
	{
		foreach(Area2D area in _areaDetector.GetOverlappingAreas())
		{
			if (area.GetCollisionLayerValue(5)) //Ladder
				return true;
		}
		return false;
	}
}
