using Godot;
using System;

public partial class EnemySideMovement : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Tween tween = CreateTween().SetLoops();

		tween.TweenProperty(GetParent(), "position:x", 300, 1f).SetTrans(Tween.TransitionType.Sine).AsRelative();
		tween.TweenProperty(GetParent(), "position:x", -300, 1f).SetTrans(Tween.TransitionType.Sine).AsRelative();
	}

	public override void _PhysicsProcess(double delta)
	{
		GetParent<Enemy>().Position += Vector2.Down * 100 * (float)delta;
	}
}
