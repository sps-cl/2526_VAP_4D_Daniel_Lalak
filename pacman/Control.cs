using Godot;
using System;

public partial class Control : Godot.Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (Player == null) return;
		var pos = Player.GlobalPosition;
		var localPos = ToLocal(pos);
		var cell = LocalMap(LocalPos);
		var id = GetCellSourceId(cell);
		if (id != -1)
		{
			if(ScoreDisplay != null)
				ScoreDisplay.Score+=10;
			SetCell(cell, -1);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
