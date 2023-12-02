using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class ScriptedSpawner : Node
{
	[Export] private PackedScene[] enemies;
	private List<Node2D> spawnPositions;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Level1_Pattern();
	}

	private async void Level1_Pattern()
	{
		await Spawn(1,0,5f);
		await Spawn(1,1,0.5f);
		await Spawn(1,2,0.5f);
		await Spawn(1,3,0.5f);

		await Spawn(0,0,2f);
		await Spawn(0,1,0.0f);
	}

	public async Task Spawn(int enemy, int position, float afterSeconds)
	{
		await Task.Delay(TimeSpan.FromSeconds(afterSeconds));
		
		Node2D myBullet = enemies[enemy].Instantiate<Node2D>();
		GetNode("/root/").AddChild(myBullet);
		
		myBullet.GlobalPosition = ((Node2D)GetChildren()[position]).GlobalPosition;
	}
}
