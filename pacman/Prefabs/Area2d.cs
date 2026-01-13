using Godot;
using System;

public partial class Area2d : Area2D
{

	[Export] public Player2d Player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		var pos = Player.GlobalPosition;
		var localPos = ToLocal(pos);
		var cell = LocaltoMap(localPos);
		if (GetCellSourceID(cell) != -1){

		}
	}
}
