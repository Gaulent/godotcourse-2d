using Godot;
using System;
using System.Diagnostics;

public partial class PlayerMovement : CharacterBody2D
{
	[Export] private float carSpeed = 1500f;
	
	[Export] private float turnSpeed = 180f;
	[Export] private bool gotPackage = false;
	//private float? _boostTimer = null;
	[Export] private float boostMultiplier = 1f;

	private float _xAxis = 0;
	private float _yAxis = 0;
	private float _timeToMaxAxis = 0.2f;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{   
		_xAxis = Mathf.MoveToward(_xAxis,Input.GetAxis("ui_left","ui_right"),(float)delta / _timeToMaxAxis);
		_yAxis = Mathf.MoveToward(_yAxis, Input.GetAxis("ui_down", "ui_up"), (float)delta / _timeToMaxAxis);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{	 
		float turnAmount = _xAxis * turnSpeed * (float)delta;
		float moveAmount = _yAxis * carSpeed * boostMultiplier * (float)delta;

			
		
		
		RotationDegrees += turnAmount * Input.GetAxis("ui_down","ui_up");
		//Position.Rotated()
		Transform = Transform.TranslatedLocal(new Vector2(0,-moveAmount));

		MoveAndSlide(); // Gestionar las colisiones

		KinematicCollision2D collision = GetLastSlideCollision();
		
		if(collision!=null)
			Debug.Print("TAS CHOCAO");
		//Mathf.MoveToward
		//Rotation
	}
	
}


/*




public partial class PlayerMovement : CharacterBody2D
{
	[Export] private float carSpeed = 150f;
	
	[Export] private float turnSpeed = 180f;
	[Export] private bool gotPackage = false;
	//private float? _boostTimer = null;
	[Export] private float boostMultiplier = 1f;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var turnAmount = Input.GetAxis("ui_left","ui_right") * turnSpeed * (float)delta;
		var moveAmount = Input.GetAxis("ui_down", "ui_up") * carSpeed * boostMultiplier * (float)delta;

		Transform = Transform.RotatedLocal(turnAmount * Input.GetAxis("ui_down","ui_up") * Mathf.Pi / 180f);
		Transform = Transform.TranslatedLocal(new Vector2(0,-moveAmount));

		MoveAndSlide(); // Gestionar las colisiones

		KinematicCollision2D collision = GetLastSlideCollision();
		
		if(collision!=null)
			Debug.Print("TAS CHOCAO");
		//Mathf.MoveToward
		//Rotation
	}
	
}



*/










/*


public partial class PlayerMovement : CharacterBody2D
{
	[Export]
	public float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	[Export]
	private int Number = 5;

	private Action onGameOver1;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		
		
		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
*/
