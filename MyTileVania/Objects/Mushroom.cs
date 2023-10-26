using Godot;
using System;

public partial class Mushroom : StaticBody2D
{
	[Export] private Area2D _area2D;
	[Export] private float _rejectForce = 1000f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_area2D.BodyEntered+=OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public void OnBodyEntered(Node2D node)
	{
		if (node is CharacterBody2D)
		{
			((CharacterBody2D)node).Velocity = Vector2.Up * _rejectForce;
		}
	}
	
}
