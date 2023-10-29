using Godot;
using System;
using System.Text;
using System.Threading.Tasks;

public partial class PlayerController : CharacterBody2D
{
	[Export] public Timer _coyoteTimer;
	[Export] public Area2D _areaDetector;
	
	[Export] public float _speed = 250.0f;
	[Export] public float _jumpVelocity = -550.0f;
	[Export] public StateMachine fsm;
	public float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle() * 2f;
	public float Direction { get; private set; }


	public override void _Ready()
	{
		fsm = GetNode<StateMachine>("StateMachine");
	}

	public override void _PhysicsProcess(double delta)
	{/*
		if (IsOnFloor())
			_coyoteTimer.Start();*/

		MoveAndSlide();

		HandleEnemyCollisions();
	}

	public void HandleEnemyCollisions()
	{	
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			KinematicCollision2D collision = GetSlideCollision(i);
			Node target = (Node)collision.GetCollider();

			if (target is EnemyController)
			{
				EnemyController enemy = (EnemyController)target;
				if (collision.GetNormal() == Vector2.Up)
				{
					Vector2 velocity = Velocity;
					velocity.Y = _jumpVelocity * 0.75f;
					Velocity = velocity;
					enemy.Die();
					GD.Print("MUERTE");
				}
				else
				{
					GetHurt(collision.GetNormal());
				}
			}
		}
	}

	// TODO: Mejorar. Referencia a Sprite. Maquina de estados?
	public async void GetHurt(Vector2 normal)
	{
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Modulate = Colors.Red;
		fsm.TransitionTo("Hurt");
		Velocity = new Vector2(normal.X * _speed, _jumpVelocity) * 0.5f;
		
		await Task.Delay(TimeSpan.FromMilliseconds(300));
		fsm.TransitionTo("Move/Normal");
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Modulate = Colors.White;
	} 
	
	public void HandleGravity(double delta)
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
