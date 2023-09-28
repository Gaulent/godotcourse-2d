using Godot;
using System;

public partial class FinishLine : Area2D
{
	private GpuParticles2D _fireworks;
	private AudioStreamPlayer _audio;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_fireworks = GetNode<GpuParticles2D>("GPUParticles2D");
		_audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		BodyEntered += OnPlayerEnter;
	}

	void OnPlayerEnter(Node2D node)
	{
		if (!node.IsInGroup("Player")) return;

		_fireworks.Emitting = true;
		_audio.Play();
	}
}
