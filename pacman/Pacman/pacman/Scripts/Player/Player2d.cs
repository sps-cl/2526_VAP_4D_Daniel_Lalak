using Godot;
using System;

public partial class Player2d : CharacterBody2D
{
	public const float Speed = 50.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
			Rotation = direction.Angle();
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}
		var pos = Position;
		if (pos.X < -8) pos.X = 232;
		if (pos.X > 232) pos.X = -8;
		Position = pos;
		Velocity = velocity;
		MoveAndSlide();
	}
}
