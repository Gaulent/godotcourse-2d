using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
	[Export] private Area2D hurtbox;
	
	public const float Speed = 300.0f;

	public override void _Ready()
	{
		hurtbox.AreaEntered += OnContact;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
		if (direction != Vector2.Zero)
			velocity = direction * Speed;
		else
			velocity = Velocity.MoveToward(Vector2.Zero, Speed);

		Velocity = velocity;
		MoveAndSlide();
	}

	public void OnContact(Node2D node)
	{
		node.QueueFree();
		QueueFree();
	}
}
