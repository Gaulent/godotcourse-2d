using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public partial class GameManager : Node
{
	private List<QuestionRes> _questions = new();
	private UIManager _uiManager;
	private int _currentQuestion = 0;
	public Action<float> OnScoreChange;
	private Timer _timer;
	private int _score;
	public int Score
	{
		get => _score;
		set
		{
			_score = value;
			OnScoreChange.Invoke(_score * 100f / _questions.Count);
		}
	}
	
	// Called when the node enters the scene tree for the first time.
	
	public void LoadAllQuestionResources()
	{
		DirAccess dir = DirAccess.Open("Resources/");
		dir.ListDirBegin();
		
		string fileName = dir.GetNext();
		while (fileName != "")
		{
			QuestionRes question = GD.Load<QuestionRes>("Resources/" + fileName);
			_questions.Add(question);
			fileName = dir.GetNext();
		}
	}

	public override void _Ready()
	{
		LoadAllQuestionResources();
		_uiManager = GetNode<UIManager>("../UIManager");
		_uiManager.SelectedResponse += GuessAnswer;
		_timer = GetNode<Timer>("../Timer");
		_timer.Start();
		_timer.Timeout += OutOfTime;
		LateReadyAsync();
	}

	// Truquito para ejecutar algo justo despues del Ready.
	private async void LateReadyAsync()
	{
		await Task.Yield();
		_uiManager.LoadQuestion(_questions[_currentQuestion]);
	}
	
	public void GuessAnswer(int answer)
	{
		if (_questions[_currentQuestion].rightAnswer == answer)
			RightAnswer();
		else
			WrongAnswer();
		
		EndOfQuestion();
	}

	public void RightAnswer()
	{
		Score++;
		_uiManager.SetQuestionText("Respuesta Correcta!");
	}
	
	public void WrongAnswer()
	{
		_uiManager.SetQuestionText("Respuesta Incorrecta. La Correcta era la " + (_questions[_currentQuestion].rightAnswer +1));
	}
	
	private void OutOfTime()
	{
		_uiManager.SetQuestionText("Se acabó el tiempo! La Correcta era la " + (_questions[_currentQuestion].rightAnswer +1));
		EndOfQuestion();
	}
	
	private async void EndOfQuestion()
	{
		_uiManager.HightlightAnswer(_questions[_currentQuestion].rightAnswer);
		_timer.Paused = true;
		_uiManager.DisableButtons(true);
		_uiManager.UpdateProgress((_currentQuestion + 1f) * 100 / _questions.Count);

		//await ToSignal(GetTree().CreateTimer(1f), "timeout");
		await Task.Delay(TimeSpan.FromSeconds(2));

		if (_currentQuestion + 1 >= _questions.Count)
		{
			GameOver();
		}
		else
		{
			_currentQuestion++;
			_uiManager.LoadQuestion(_questions[_currentQuestion]);
			_timer.Paused = false;
			_timer.Start();
			_uiManager.ResetPressedStatus();
			_uiManager.DisableButtons(false);
		}
	}

	public string GetScoreString()
	{
		return (_score * 100f / _questions.Count) + "%";
	}
	
	public void GameOver()
	{
		_uiManager.Visible = false;
		GetNode<Control>("../GameOverUI").Visible = true;
	}
}
