using Godot;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class Customer : Area2D
{
	private Respawnable _respawnable;
	
	public override void _Ready()
	{
		BodyEntered += OnPlayerEnter;
		_respawnable = GetNode<Respawnable>("Respawnable");
	}
	
	private void OnPlayerEnter(Node2D node)
	{
		if (node.Name == "Jugador") // TODO: Algo mas solido que el nombre?
		{
			var player = (PlayerMovement)node;

			if (player.gotPackage) // TODO: Continuar la logica
			{
				player.gotPackage = false;
				_respawnable.DisablePickup();
				GameManager.AddPoints();
			}
		}
	}
}



