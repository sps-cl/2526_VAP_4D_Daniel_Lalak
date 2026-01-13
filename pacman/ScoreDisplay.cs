using Godot;
using System;

public partial class ScoreDisplay : HBoxContainer
{
	[Export] public Label ScoreLabel;
	
	public int Score 
	{
		get => int.Parse(ScoreLabel.Text);
		set => ScoreLabel.Text = value.ToString();
	}
}
