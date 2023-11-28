using Godot;
using System;

public partial class Shredder : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += Destroy;
		AreaEntered += Destroy;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Destroy(Node2D node)
	{
		node.QueueFree();
	}
}
