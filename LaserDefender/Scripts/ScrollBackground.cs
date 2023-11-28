using Godot;
using System;

public partial class ScrollBackground : ParallaxBackground
{
	[Export] private float _baseSpeed = 250f;
	
	//Para que funcione correctamente debe estar el sprite centrado y con transformada en 0,0
	//Controlamos la posicion moviendo el Layer.
	//Mirroring al tamaño del sprite
	//El offset se pisa si existe camara. Por eso tocamos el baseOffset
	
	public override void _Process(double delta)
	{
		ScrollBaseOffset += Vector2.Down * _baseSpeed * (float)delta;
	}
}
