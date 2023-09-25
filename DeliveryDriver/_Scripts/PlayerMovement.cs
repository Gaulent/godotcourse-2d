using Godot;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class PlayerMovement : CharacterBody2D
{
	[Export] private float carSpeed = 1500f;
	
	[Export] private float turnSpeed = 180f;
	[Export] public bool gotPackage = false;
	//private float? _boostTimer = null;
	[Export] private float _boostMultiplier = 1f;
	
	public float BoostMultiplier
	{
		get => _boostMultiplier;
		set
		{
			_boostMultiplier = value;
			onSpeedChange.Invoke(value);
		}
	}

	private float _xAxis = 0f;
	private float _yAxis = 0f;
	private float _timeToMaxAxis = 0.2f;

	private Timer boostTimer;

	public Action<float> onSpeedChange;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		boostTimer = GetNode<Timer>("BoostTimer");
		boostTimer.Timeout += RestoreSpeed;
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
		float moveAmount = _yAxis * carSpeed * BoostMultiplier * (float)delta;
		
		RotationDegrees += turnAmount * _yAxis;
		Transform = Transform.TranslatedLocal(new Vector2(0,-moveAmount));

		MoveAndSlide(); // Gestionar las colisiones

		KinematicCollision2D collision = GetLastSlideCollision();

		if (collision != null)
		{
			Node collideNode = (Node)collision.GetCollider();
			if (collideNode.IsInGroup("Obstacle"))
				SpeedUp(5f, 0.5f);
		}
	}
	
	public void SpeedUp(float boostTime, float boostMultiplier)
	{
		BoostMultiplier = boostMultiplier;
		boostTimer.WaitTime = boostTime;
		boostTimer.Start();
	}
	
	private void RestoreSpeed()
	{
		BoostMultiplier = 1f;
	}

}

