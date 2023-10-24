using Godot;
using System;
using System.Text;
using System.Threading.Tasks;

public partial class PlayerController : CharacterBody2D
{
	public enum PlayerStatus {Normal, Climb, Hurt}
	public PlayerStatus State = PlayerStatus.Normal;
	
	[Export] private Timer _coyoteTimer;
	[Export] private Area2D _areaDetector;
	
	[Export] private float _speed = 250.0f;
	[Export] private float _jumpVelocity = -550.0f;
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle() * 2f;
	public float Direction { get; private set; }
	private float _climbDirection;
	private bool _tryToJump;


	public override void _Process(double delta)
	{
		Direction = Input.GetAxis("ui_left", "ui_right");
		_climbDirection = Input.GetAxis("ui_up", "ui_down");
		
		if (Input.IsActionJustPressed("ui_accept"))
			_tryToJump = true;
	}

	public override void _PhysicsProcess(double delta)
	{
		switch (State)
		{
			case PlayerStatus.Normal:
				_PhysicsProcessNormal(delta);
				break;
			case PlayerStatus.Climb:
				_PhysicsProcessClimb(delta);
				break;
			case PlayerStatus.Hurt:
				_PhysicsProcessHurt(delta);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		if (IsOnFloor())
			_coyoteTimer.Start();

		MoveAndSlide();

		HandleEnemyCollisions();
	}

	private void HandleEnemyCollisions()
	{	
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			KinematicCollision2D collision = GetSlideCollision(i);
			Node target = (Node)collision.GetCollider();

			if (target.IsInGroup("Enemy"))
			{
				if (collision.GetNormal() == Vector2.Up)
				{
					Vector2 velocity = Velocity;
					velocity.Y = _jumpVelocity * 0.75f;
					Velocity = velocity;
					target.QueueFree();
				}
				else
				{
					GetHurt(collision.GetNormal());
				}
			}
		}
	}

	// TODO: Mejorar. Referencia a Sprite. Maquina de estados?
	private async void GetHurt(Vector2 normal)
	{
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Modulate = Colors.Red;
		State = PlayerStatus.Hurt;
		Velocity = new Vector2(normal.X * _speed, _jumpVelocity) * 0.5f;
		
		await Task.Delay(TimeSpan.FromMilliseconds(300));
		State = PlayerStatus.Normal;
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Modulate = Colors.White;
	} 
	
	private void HandleMovement()
	{
		Vector2 velocity = Velocity;
		
		if (Direction != 0f)
			velocity.X = Direction * _speed;
		else
			velocity.X = Mathf.MoveToward(Velocity.X, 0, _speed);
		
		Velocity = velocity;		
	}

	void _PhysicsProcessNormal(double delta)
	{
		if (IsTouchingLadder() && _climbDirection != 0)
		{
			State = PlayerStatus.Climb;
			return;
		}

		HandleGravity(delta);

		if (_tryToJump)
		{
			_tryToJump = false;
			if ((IsOnFloor() || !_coyoteTimer.IsStopped()))
				HandleJump();
		}
		
		HandleMovement();
	}
	void _PhysicsProcessHurt(double delta)
	{
		HandleGravity(delta);
	}
	
	void _PhysicsProcessClimb(double delta)
	{
		if (!IsTouchingLadder())
		{
			State = PlayerStatus.Normal;
			return;
		}

		if (_tryToJump)
		{
			_tryToJump = false;
			HandleJump();
			State = PlayerStatus.Normal;
			return;
		}
		
		HandleClimb();
		
		HandleMovement();
	}

	private void HandleGravity(double delta)
	{
		Vector2 velocity = Velocity;
		// Add the gravity.
		if (!IsOnFloor())
		{
			if (GetRealVelocity().Y > 0)
				velocity.Y += _gravity * (float)delta * 1.5f;
			else
				velocity.Y += _gravity * (float)delta;				
			
		}
		Velocity = velocity;
	}

	private void HandleJump()
	{
		Vector2 velocity = Velocity;
		velocity.Y = _jumpVelocity;
		Velocity = velocity;
	} 

	private void HandleClimb()
	{
		Vector2 velocity = Velocity;
		
		if (_climbDirection != 0f)
			velocity.Y = _climbDirection * _speed;
		else
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, _speed);

		Velocity = velocity;
	}
	
	public bool IsTouchingLadder()
	{
		foreach(Area2D area in _areaDetector.GetOverlappingAreas())
		{
			if (area.GetCollisionLayerValue(5)) //Ladder
				return true;
		}
		return false;
	}
}
