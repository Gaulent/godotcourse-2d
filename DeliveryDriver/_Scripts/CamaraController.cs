using Godot;
using System;

public partial class CamaraController : Camera2D
{
	// Called when the node enters the scene tree for the first time.
	private Vector2 originalZoom;
	private Vector2 targetZoom;
	
	public override void _Ready()
	{
		GetParent<PlayerMovement>().onSpeedChange += ChangeCameraZoom;
		originalZoom = Zoom;
		targetZoom = Zoom;
	}

	public override void _Process(double delta)
	{
		Zoom = Zoom.Lerp(targetZoom, (float)delta);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	private void ChangeCameraZoom(float zoom)
	{
		targetZoom = originalZoom / zoom;
	}
}
