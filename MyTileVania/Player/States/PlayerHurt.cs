using Godot;
using System;

public partial class PlayerHurt : State
{
	private PlayerController _player;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = (PlayerController)Owner;
	}
	
	public override void PhysicsUpdate(float delta)
	{
		_player.HandleGravity(delta);
	}
}
