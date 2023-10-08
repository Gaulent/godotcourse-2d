using Godot;
using System;
using System.Collections.Generic;

public partial class UIManager : Control
{
	private List<Button> _buttons = new();
	private List<Label> _buttonsLabels = new();
	private Label _questionText;
	private Label _scoreText;
	private TextureProgressBar _timeRemaining;
	private ProgressBar _gameProgress;
	private Timer _timer;
	
	public Action<int> SelectedResponse;
	private GameManager _myGM;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_questionText = GetNode<Label>("Question/Label");
		for (int i = 1; i <= 4; i++)
		{
			_buttons.Add(GetNode<Button>("Button" + i));
			_buttonsLabels.Add(GetNode<Label>("Button" + i + "/Label"));
		}
		_scoreText = GetNode<Label>("Score");
		_timeRemaining = GetNode<TextureProgressBar>("TextureProgressBar");
		_gameProgress = GetNode<ProgressBar>("ProgressBar");
		_timer = GetNode<Timer>("../Timer");

		_buttons[0].Pressed += Button1Pressed;
		_buttons[1].Pressed += Button2Pressed;
		_buttons[2].Pressed += Button3Pressed;
		_buttons[3].Pressed += Button4Pressed;
		
		_myGM = GetNode<GameManager>("../GameManager");
		_myGM.OnScoreChange += UpdateScore;
	}

	public void LoadQuestion(QuestionRes currentQuestion)
	{
		_questionText.Text = currentQuestion.question;
		
		for (int i = 0; i<4; i++)
			_buttonsLabels[i].Text = currentQuestion.answers[i];
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_timeRemaining.Value = _timer.TimeLeft * 100 / _timer.WaitTime;
	}

	public void Button1Pressed() => SelectedResponse.Invoke(0);
	public void Button2Pressed() => SelectedResponse.Invoke(1);
	public void Button3Pressed() => SelectedResponse.Invoke(2);
	public void Button4Pressed() => SelectedResponse.Invoke(3);

	public void SetQuestionText(string respuestaCorrecta)
	{
		_questionText.Text = respuestaCorrecta;
	}

	public void UpdateScore(float score)
	{
		_scoreText.Text = "Score: " + score + "%";
	}

	public void UpdateProgress(float progress)
	{
		_gameProgress.Value = progress;
	}

	public void DisableButtons(bool flag)
	{
		var mode = flag ? MouseFilterEnum.Ignore : MouseFilterEnum.Stop;

		for (int i = 0; i < 4; i++)
		{
			if (flag)
				_buttons[i].MouseFilter = mode;
			else
				_buttons[i].MouseFilter = mode;
		}
	}

	public void ResetPressedStatus()
	{
		for (int i = 0; i < 4; i++)
		{
			_buttons[i].SetPressedNoSignal(false);
		}				
	}
	
	public void HightlightAnswer(int rightAnswer)
	{
		ResetPressedStatus();
		_buttons[rightAnswer].SetPressedNoSignal(true);
	}
}
