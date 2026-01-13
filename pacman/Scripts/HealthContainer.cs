using Godot;
using System;

public partial class HealthContainer : HBoxContainer
{
	[Export] public TextureRect HealthPoint;
	[Export]public int Health
	{
		get => _health;
		set
		{
			_health = value;
			while (_health + 1 > GetChildCount())
				AddHealthPoint();
			while (_health + 1 < GetChildCount())
				RemoveHealthPoint();
		}
	}

	private int _health = 0;
	public override void _Ready()
	{
		HealthPoint.Visible = false;
		Health = _health;
	}

	private void AddHealthPoint()
	{
		if(HealthPoint == null) return;
		var newHealthPoint = HealthPoint.Duplicate() as TextureRect;
		newHealthPoint.Visible = true;
		AddChild(newHealthPoint);
	}

	private void RemoveHealthPoint()
	{
		var oldHealthPoint = GetChild(GetChildCount() - 1);
		oldHealthPoint.QueueFree();
	}
}
