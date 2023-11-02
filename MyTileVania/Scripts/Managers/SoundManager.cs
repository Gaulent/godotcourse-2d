using Godot;
using System;

public partial class SoundManager : SingletonNode<SoundManager>
{
	public void PlaySound(AudioStream audio)
	{
		// Limit audio to 1 of the same type.
		
		foreach (Node child in GetChildren())
		{
			if (child is not AudioStreamPlayer audioStreamPlayer) continue;
			
			if (audioStreamPlayer.Stream == audio)
				audioStreamPlayer.QueueFree();
		}
		
		AudioStreamPlayer player = new AudioStreamPlayer();
		AddChild(player);
		player.Stream = audio;
		player.Play();
		player.Finished += player.QueueFree;
	}
}
