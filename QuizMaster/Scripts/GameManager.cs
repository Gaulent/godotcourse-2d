using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class GameManager : Control
{
	[Export] private Button[] _buttons;
	[Export] private Label _questionText;
	private List<QuestionRes> _questions = new();
	[Export] private float maxAnswerTime = 10f;
	
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
		LoadQuestion();
	}
	
	private void LoadQuestion()
	{
		_questionText.Text = _questions[0].question;
		
        for (int i = 0; i<4; i++)
			_buttons[i].GetChild<Label>(0).Text = _questions[0].answers[i];
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
