using Godot;
using System;

public partial class SoundManager : SingletonNode<SoundManager>
{
	public void PlaySound(AudioStream audio)
	{
		AudioStreamPlayer player = new AudioStreamPlayer();
		AddChild(player);
		player.Stream = audio;
		player.Play();
		player.Finished += player.QueueFree;
	}
}
