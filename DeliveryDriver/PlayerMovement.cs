using Godot;
using System;
using System.Diagnostics;

public partial class PlayerMovement : CharacterBody2D
{
	[Export] private float carSpeed = 1500f;
	
	[Export] private float turnSpeed = 180f;
	[Export] public bool gotPackage = false;
	//private float? _boostTimer = null;
	[Export] private float boostMultiplier = 1f;

	private float _xAxis = 0f;
	private float _yAxis = 0f;
	private float _timeToMaxAxis = 0.2f;
	
	
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public override void _Process(double delta)
	{   
		_xAxis = Mathf.Lerp(_xAxis,Input.GetAxis("ui_left","ui_right"),(float)delta / _timeToMaxAxis);
		_yAxis = Mathf.Lerp(_yAxis, Input.GetAxis("ui_down", "ui_up"), (float)delta / _timeToMaxAxis);

		//_xAxis = Input.GetAxis("ui_left", "ui_right");
		//_yAxis = Input.GetAxis("ui_down", "ui_up");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{	 
		float turnAmount = _xAxis * turnSpeed * (float)delta;
		float moveAmount = _yAxis * carSpeed * boostMultiplier * (float)delta;
		
		RotationDegrees += turnAmount * _yAxis;
		Transform = Transform.TranslatedLocal(new Vector2(0,-moveAmount));

		MoveAndSlide(); // Gestionar las colisiones

		KinematicCollision2D collision = GetLastSlideCollision();

		/*if (collision != null)
			Debug.Print("YOU IT A WALL");*/
	}

	public void PackagePickUp()
	{
		gotPackage = true;
	}


	

}

