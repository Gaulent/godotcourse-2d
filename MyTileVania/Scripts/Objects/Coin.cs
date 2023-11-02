using Godot;
using System;

public partial class Coin : Area2D
{
	[Export] private AudioStream _sound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnPlayerPickup;
	}

	private void OnPlayerPickup(Node2D node2D)
	{
		SoundManager.Instance.PlaySound(_sound);
		QueueFree();
	}
}
