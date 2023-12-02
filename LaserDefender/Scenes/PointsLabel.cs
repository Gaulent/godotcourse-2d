using Godot;
using System;

public partial class PointsLabel : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameManager.Instance.OnPointsChange += ChangeScore;
	}

	private void ChangeScore(int points)
	{
		Text = points.ToString();
	}
}
