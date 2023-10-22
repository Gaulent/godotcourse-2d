using Godot;
using System;
using System.Text;

public partial class PlayerController : CharacterBody2D
{
	public enum PlayerStatus {Normal, Climb}

	public PlayerStatus _playerStatus = PlayerStatus.Normal;
	[Export] private float Speed = 250.0f;
	[Export] private float JumpVelocity = -550.0f;
	private Timer coyoteTime;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle() * 2f;
	public float direction { get; private set; }
	public float climbDirection { get; private set; }
	private bool tryToJump;
	private Area2D _areaDetector;
	private Area2D _hurtArea;


	public override void _Ready()
	{
		coyoteTime = GetNode<Timer>("CoyoteTime");
		_areaDetector = GetNode<Area2D>("Area2DDetector");
		_hurtArea = GetNode<Area2D>("Feet");
		_hurtArea.BodyEntered += GetHurt;
	}


	public override void _Process(double delta)
	{
		direction = Input.GetAxis("ui_left", "ui_right");
		climbDirection = Input.GetAxis("ui_up", "ui_down");
		
		
		if (Input.IsActionJustPressed("ui_accept"))
			tryToJump = true;
	}


	public bool IsTouchingLadder()
	{
		foreach(Area2D area in _areaDetector.GetOverlappingAreas())
		{
			if (area.GetCollisionLayerValue(5))
				return true;
		}

		return false;
	}
	
	

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;


		if (IsTouchingLadder() && climbDirection != 0)
		{
			_playerStatus = PlayerStatus.Climb;
		}
		if (!IsTouchingLadder())
		{
			_playerStatus = PlayerStatus.Normal;
		}
		
		
		
		// Add the gravity.
		if (!IsOnFloor() && _playerStatus == PlayerStatus.Normal)
		{
			if (GetRealVelocity().Y > 0)
			{
				velocity.Y += gravity * (float)delta * 1.5f;
			}
			else
			{
				velocity.Y += gravity * (float)delta;				
			}
			
		}
		
		if (_playerStatus == PlayerStatus.Climb)
		{
			if (climbDirection != 0f)
			{
				velocity.Y = climbDirection * Speed;
			}
			else
			{
				velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
			}
			
		}
			

		// Handle Jump.
		if (tryToJump)
		{
			tryToJump = false;
			if (IsOnFloor() || !coyoteTime.IsStopped() || _playerStatus == PlayerStatus.Climb)
			{
				_playerStatus = PlayerStatus.Normal;
				velocity.Y = JumpVelocity;
			}
		}

		if (direction != 0f)
		{
			velocity.X = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		if (IsOnFloor())
			coyoteTime.Start();
		   
		

		
		
		Velocity = velocity;
		MoveAndSlide();
	}
	
	void GetHurt(Node2D node)
	{
		Vector2 velocity = Velocity;
		velocity.Y = JumpVelocity;
		Velocity = velocity;
		node.QueueFree();
	}
}
