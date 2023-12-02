using Godot;
using System;
using System.Linq;

public partial class EnemyGuns : Node2D
{
	[Export] private Timer _recoil;
	[Export] private PackedScene _bullet;
	
	public enum ShootMode
	{
		Directed,
		Front
	}

	[Export] private ShootMode _shootMode;
	[Export] private float speed = 140f;
	
	public override void _PhysicsProcess(double delta)
	{
		if (_recoil.IsStopped())
		{
			_recoil.Start();
			EnemyLaser myBullet = _bullet.Instantiate<EnemyLaser>();
			GetNode("/root/").AddChild(myBullet);

			myBullet.GlobalPosition = GlobalPosition;
			myBullet.speed = speed;
			
			if (GetTree().GetNodesInGroup("Player").Count == 0) _shootMode = ShootMode.Front;
			
			if (_shootMode == ShootMode.Directed)
			{
				Node2D player = (Node2D)GetTree().GetNodesInGroup("Player").First();
				myBullet.direction = (player.GlobalPosition - GlobalPosition).Normalized();
			}
			else
			{
				myBullet.direction = Vector2.Down;

			}
			
		}
	}
}
