using Godot;
using System;
using System.Threading.Tasks;

public partial class Respawnable : Node
{
	[Export] public float timeToReturn = 5f;
	private Sprite2D sprite;
	private CollisionShape2D collisionShape2D;

	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("../Sprite");
		collisionShape2D = GetNode<CollisionShape2D>("../CollisionShape2D");
	}

	private void EnablePickup()
	{
		sprite.Visible = true;
		collisionShape2D.SetDeferred("disabled", false);
	}
	
	public void DisablePickup()
	{
		sprite.Visible = false;
		// Necesario cambiar al final del frame
		collisionShape2D.SetDeferred("disabled", true);
		ReEnableAfterSeconds();
	}
	
	private async void ReEnableAfterSeconds()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(timeToReturn * 1000));  // TODO: Time to recover
		EnablePickup();
	}
}
