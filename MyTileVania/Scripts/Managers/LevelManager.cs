using Godot;
using System;

public partial class LevelManager : SingletonNode<LevelManager>
{
	[Export] private PackedScene[] levels;
	private int currentLevel = 0;
	
	
	public void LoadNextLevel()
	{
		currentLevel = (currentLevel+1) % levels.Length;
		GetTree().ChangeSceneToPacked(levels[currentLevel]);
	}
}
