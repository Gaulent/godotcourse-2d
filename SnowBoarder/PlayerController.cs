using Godot;
using System;
using System.Diagnostics;

public partial class PlayerController : RigidBody2D
{
	private float _turnAmount = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		_turnAmount = Input.GetAxis("ui_left", "ui_right");
	}

	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		GD.Print(state.GetContactCount());
		
		for (int i=0; i < state.GetContactCount(); i++)
		{
			GD.Print(state.GetContactLocalNormal(i));
			state.ApplyForce(state.GetContactLocalNormal(i).Rotated(Mathf.Pi/2f)  * 5000f);
		}
		
		state.ApplyTorque(_turnAmount * 1000000f);

		state.AngularVelocity = Mathf.Clamp(state.AngularVelocity, -50f, 50f);
	}
}
