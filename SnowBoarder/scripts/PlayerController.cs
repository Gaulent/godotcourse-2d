using Godot;
using System;
using System.Diagnostics;

public partial class PlayerController : RigidBody2D
{
	private float _turnAmount = 0;
	private Vector2 _bodyCenter;

	public override void _Ready()
	{
		_bodyCenter = GetNode<Node2D>("Center").Position;
	}

	public override void _Process(double delta)
	{
		_turnAmount = Input.GetAxis("ui_left", "ui_right");
	}
	
	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		// Si toca el suelo
		if (state.GetContactCount() >= 1)
		{
			CenterOfMass = Vector2.Zero;			
			for (int i=0; i < state.GetContactCount(); i++)
			{
				Vector2 forceDirection = state.GetContactLocalNormal(i).Rotated(Mathf.Pi / 2f);
				state.ApplyForce(forceDirection  * 2000f);
			}
		}

		// Solo puede rotar con 1 contacto o ninguno
		if (state.GetContactCount() <= 1)
		{
			CenterOfMass = _bodyCenter;
			state.ApplyTorque(_turnAmount * 500000f);
			state.AngularVelocity = Mathf.Clamp(state.AngularVelocity, -5f, 5f);			
		}
	}
}
