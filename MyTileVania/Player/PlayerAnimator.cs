using Godot;
using System;

public partial class PlayerAnimator : AnimatedSprite2D
{/*
	private PlayerController player;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetParent<PlayerController>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		FlipH = player.Direction switch
		{
			< 0 => true,
			> 0 => false,
			_ => FlipH
		};

		if (player.fsm.GetState().Name == "Climb")
		{
			Animation = "climbing";
			return;
		}
		
		Animation = player.Direction!=0 ? "moving" : "idle";

		if (!player.IsOnFloor())
		{
			Animation = player.GetRealVelocity().Y > 0 ? "jump_down" : "jump_up";
		}
	}*/
}
