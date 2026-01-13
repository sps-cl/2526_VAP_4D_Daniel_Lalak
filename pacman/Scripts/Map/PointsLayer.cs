using Godot;
using System;

public partial class PointsLayer : TileMapLayer
{
	[Export] public Player2d Player;
	[Export] public ScoreDisplay ScoreDisplay;
	
	public override void _Ready()
	{
	}
	public override void _Process(double delta)
	{
		if (Player == null) return;
		var pos = Player.GlobalPosition;
		var localPos = ToLocal(pos);
		var cell = LocalToMap(localPos);
		var id = GetCellSourceId(cell);
		if (id != -1)
		{
			if(ScoreDisplay != null) 
				ScoreDisplay.Score+=10;
			SetCell(cell, -1);
		}
	}
}
