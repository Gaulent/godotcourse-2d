using Godot;
using System;

public partial class Enemy : Area2D
{
	public override void _Ready()
	{
		AreaEntered += OnContact;
		
		Tween tween = CreateTween().SetLoops();
		tween.TweenProperty(this, "position:x", 200, 1f).SetTrans(Tween.TransitionType.Sine);
		tween.TweenProperty(this, "position:x", 0, 1f).SetTrans(Tween.TransitionType.Sine);
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += Vector2.Down * 100 * (float)delta;
	}
	
	public void OnContact(Node2D node)
	{
		node.QueueFree();
		QueueFree();
	}
}
