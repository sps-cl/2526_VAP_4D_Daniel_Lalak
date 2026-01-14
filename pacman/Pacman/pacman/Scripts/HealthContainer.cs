using Godot;
using System;

public partial class HealthContainer : HBoxContainer
{
	[Export] public TextureRect HealthPoint;
	[Export] public int Health 
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
		if (HealthPoint == null) return;//kontrola zda je bod spravne nahrany
		var newHealthPoint = HealthPoint.Duplicate() as TextureRect;//vytvoreni kopie
		newHealthPoint.Visible = true;//zobrazeni kopie
		AddChild(newHealthPoint);//pridani kopie do HBoxu
	}

	private void RemoveHealthPoint()
	{
		var oldHealthPoint = GetChild(GetChildCount() - 1);//ziskani posledniho potomka
		RemoveChild(oldHealthPoint);//vymazani zivota
		oldHealthPoint.QueueFree();//vymazani potomka
	}

}
