using Godot;
using System;

public partial class Spikes : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered+=OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public void OnBodyEntered(Node2D node)
	{
		if (node is PlayerController)
		{
			((PlayerController)node).GetHurt(Vector2.Up);
		}
	}
}
