using Godot;
using System;

public partial class Coin : Area2D
{
	private SoundManager _soundManager;
	[Export] private AudioStream _sound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_soundManager = GetNode<SoundManager>("/root/SoundManager");
		BodyEntered += OnPlayerPickup;
	}

	private void OnPlayerPickup(Node2D node2D)
	{
		_soundManager.PlaySound(_sound);
		QueueFree();
	}
}
