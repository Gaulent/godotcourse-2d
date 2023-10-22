using Godot;
using System;

public partial class SoundManager : Node
{
	// Singleton Pattern
	public static SoundManager Instance { get; private set; }

	public override void _Ready()
	{
		Instance = this;
	}

	public void PlaySound(AudioStream audio)
	{
		AudioStreamPlayer player = new AudioStreamPlayer();
		AddChild(player);
		player.Stream = audio;
		player.Play();
		player.Finished += player.QueueFree;
	}
}
