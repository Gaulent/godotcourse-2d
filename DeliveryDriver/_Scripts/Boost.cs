using Godot;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class Boost : Area2D
{
	[Export] public float boostTimer = 5f;
	[Export] public float boostMultiplier = 1.5f;
	private Respawnable _respawnable;
	
	public override void _Ready()
	{
		BodyEntered += OnPlayerEnter;
		_respawnable = GetNode<Respawnable>("Respawnable");
	}
	
	private void OnPlayerEnter(Node2D node)
	{
		if (node.Name == "Jugador") // TODO: Algo mas solido que el nombre?
		{
			var player = (PlayerMovement)node;

			_respawnable.DisablePickup();
			player.SpeedUp(boostTimer, boostMultiplier);
		}
	}
}
