using Godot;
using System;
using Godot.Collections;
using System.Threading.Tasks;

public partial class PlayerHurt : State
{
	[Export] private PlayerController _player;
	
	// Al entrar, rojo y repele
	public override void Enter(Dictionary payload = null)
	{
		_player.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Modulate = Colors.Red;
		GetHurt((Vector2)payload!["normal"]);
	}
	
	// Repele y vuelve al tiempo a Normal
	public async void GetHurt(Vector2 normal)
	{
		_player.Velocity = new Vector2(normal.X * _player._speed, _player._jumpVelocity) * 0.5f;
		await Task.Delay(TimeSpan.FromMilliseconds(300));
		fsm.TransitionTo("Move/Normal");
	} 

	// Al terminar vuelve a blanco
	public override void Exit()
	{
		_player.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Modulate = Colors.White;
	}
}
