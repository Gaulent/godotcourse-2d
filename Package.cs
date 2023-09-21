using Godot;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class Package : Area2D
{
	// Called when the node enters the scene tree for the first time.

	public override void _Ready()
	{
		BodyEntered += OnPlayerEnter;
	}
	
	private void OnPlayerEnter(Node2D node)
	{
		if (node.Name == "Jugador")
		{
			var player = (PlayerMovement)node;

			if (!player.gotPackage) // TODO: Continuar la logica
			{
				player.PackagePickUp();
				GetChild<Sprite2D>(0).Visible = false;
				// Necesario cambiar al final del frame
				GetChild<CollisionShape2D>(1).SetDeferred("disabled", true);
				ReEnableAfterSeconds();
			}
		}
	}
	
	private async void ReEnableAfterSeconds()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(5000));  // TODO: Time to recover
		GetChild<Sprite2D>(0).Visible = true;
		GetChild<CollisionShape2D>(1).SetDeferred("disabled", false);
	}


}



