using Godot;
using System;

public partial class Enemy : Area2D
{
	public override void _Ready()
	{
		AreaEntered += OnContact;
	}
	
	public void OnContact(Node2D node)
	{
		node.QueueFree();
		QueueFree();
		GameManager.Instance.AddPoint();
	}
}
