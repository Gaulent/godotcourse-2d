using Godot;
using System;
using System.Dynamic;

public partial class GameManager : Node2D
{
	private static int Score = 0;
	public static Action<int> onScoreModify;
	
	
	public static void AddPoints()
	{
		Score++;
		GD.Print(Score);
		onScoreModify.Invoke(Score);
	}
}
