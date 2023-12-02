using Godot;
using System;
using System.Linq;

public partial class EnemyLaser : Area2D
{
	public Vector2 direction;
	public float speed = 140f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// SetLoops y FromCurrent hace que la animacion sea infinita
		// Omitir GetTree hace que se asocie al nodo y se destruyan juntos
		Tween tween = CreateTween().SetLoops();
		tween.TweenProperty(this, "rotation_degrees", 360, 1f).FromCurrent();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Position+= direction * speed * (float)delta;
	}
}
