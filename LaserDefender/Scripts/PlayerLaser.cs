using Godot;
using System;

public partial class PlayerLaser : Area2D
{
	public const float Speed = 1400.0f;

	public override void _PhysicsProcess(double delta)
	{
		Position+= Vector2.Up * Speed * (float)delta;
	}
}
