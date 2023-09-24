using Godot;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class Customer : Area2D
{
	public float timeToReturn = 5f;

	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{
		BodyEntered += OnPlayerEnter;
	}
	
	private void OnPlayerEnter(Node2D node)
	{
		if (node.Name == "Jugador") // TODO: Algo mas solido que el nombre?
		{
			var player = (PlayerMovement)node;

			if (player.gotPackage) // TODO: Continuar la logica
			{
				player.gotPackage = false;
				DisablePickup();
				GameManager.AddPoints();
			}
		}
	}
	
	private void EnablePickup()
	{
		GetChild<Sprite2D>(0).Visible = true;
		GetChild<CollisionShape2D>(1).SetDeferred("disabled", false);
	}
    
	private void DisablePickup()
	{
		GetChild<Sprite2D>(0).Visible = false;
		// Necesario cambiar al final del frame
		GetChild<CollisionShape2D>(1).SetDeferred("disabled", true);
		ReEnableAfterSeconds();
	}
	
	private async void ReEnableAfterSeconds()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(timeToReturn * 1000));  // TODO: Time to recover
		EnablePickup();
	}


}



