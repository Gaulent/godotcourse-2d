using Godot;
using System;

public partial class GameManager : SingletonNode<GameManager>
{
	private int points = 0;
	public Action<int> OnPointsChange;
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void AddPoint()
	{
		points++;
		GD.Print(points);
		OnPointsChange.Invoke(points);
	}
}
