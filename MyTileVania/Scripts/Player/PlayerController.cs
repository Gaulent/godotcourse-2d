using Godot;
using System;
using Godot.Collections;

public partial class PlayerController : CharacterBody2D
{
	[Export] public Area2D _areaDetector;
	
	[Export] public float _speed = 250.0f;
	[Export] public float _jumpVelocity = -650.0f;
	[Export] private StateMachine fsm;
	[Export] private Area2D hurtBox;

	public float GravityMultiplier { get; set; }

	public float GetGravity()
	{
		return ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle() * 2f * GravityMultiplier;
	}
	
	public override void _Ready()
	{
		GravityMultiplier = 1f;
		fsm = GetNode<StateMachine>("StateMachine");
		hurtBox.BodyEntered += DoDamage;
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleGravity(delta);
		MoveAndSlide();
	}

	public void DoDamage(Node2D node)
	{
		if (node is EnemyController enemy)
		{
			Vector2 velocity = Velocity;
			fsm.TransitionTo("Move/Normal");
			velocity.Y = _jumpVelocity * 0.75f;
			Velocity = velocity;
			enemy.Die();
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
