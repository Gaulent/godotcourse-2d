using Godot;
using System;

public partial class GameOver : Control
{
	private Label _questionText;
	private GameManager _myGM;
	private Button _button;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_questionText = GetNode<Label>("Question/Label");
		_button = GetNode<Button>("Button1");
		_myGM = GetNode<GameManager>("../GameManager");
		_questionText.Text = "Congratulations!\nYou scored " + _myGM.GetScoreString();
		_button.Pressed += ResetGame;
	}
	
	void ResetGame()
	{
		GetTree().ReloadCurrentScene();
	}

}
