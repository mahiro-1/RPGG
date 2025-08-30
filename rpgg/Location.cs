using Godot;
using System;
using System.Numerics;

public partial class Location : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	public Godot.Vector2 get_pos(){
		return new Godot.Vector2(1,1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
