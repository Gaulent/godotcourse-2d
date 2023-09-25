using Godot;
using System;

public partial class UIManager : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	private RichTextLabel scoreText;
	
	public override void _Ready()
	{
		GameManager.onScoreModify += UpdateUI;
		scoreText = GetNode<RichTextLabel>("ScoreText");
	}

	void UpdateUI(int score)
	{
		scoreText.Text = score.ToString();
	}
}
