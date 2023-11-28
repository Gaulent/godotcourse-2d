using Godot;
using System;

public partial class PlayerGuns : Node2D
{
	[Export] private Timer _recoil;
	[Export] private PackedScene _bullet;
	
	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionPressed("ui_accept") && _recoil.IsStopped())
		{
			_recoil.Start();
			Node2D myBullet = _bullet.Instantiate<Node2D>();
			myBullet.GlobalPosition = GlobalPosition;
			GetNode("/root/").AddChild(myBullet);
		}
	}
}
