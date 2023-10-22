using Godot;
using System;

public partial class Coin : Area2D
{
	private AudioStreamPlayer _player;
	private AnimatedSprite2D _sprite;
	private CollisionShape2D _shape;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_shape = GetNode<CollisionShape2D>("CollisionShape2D");
		BodyEntered += OnPlayerPickup;
		_player.Finished+=QueueFree;
	}

	private void OnPlayerPickup(Node2D node2D)
	{
		_sprite.Visible = false;
		_shape.Disabled = true;
		_player.Play();
	}
}
