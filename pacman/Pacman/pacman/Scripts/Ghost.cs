using Godot;
using System;

public partial class Ghost : CharacterBody2D
{
	[Export] public float Speed = 20.0f;
	[Export] public Node2D Target;
	[Export] public TileMapLayer NavigationPlayer;

	Vector2 _direction = Vector2.Zero;
	Vector2 _currentTarget = Vector2.Zero;

	NavigationAgent2D _agent;
	public override void _Ready()
	{
		_agent = GetNode<NavigationAgent2D>("NavigationAgent2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		var dir = ToLocal(_agent.GetNextPathPosition()).Normalized();
		Velocity = dir * Speed;
		MoveAndSlide();
		if ((_currentTarget - GlobalPosition).Dot(_direction)<=0)
		{
			if (_currentTarget != Vector2.Zero)
			GlobalPosition = _currentTarget;
			UpdatePath();
		}
	}

	public void UpdatePath()
	{
		_agent.TargetPosition = Target.GlobalPosition;
		var next = _agent.GetNextPathPosition();
		var dir = (next - GlobalPosition).Normalized();
		var currentCell = WorldToCell(GlobalPosition);
		_currentTarget = CellToWorld(NextCell);
		_direction = (_currentTarget - GlobalPosition).Normalized();
	}

	private Vector2I WorldToCell(Vector2 worldPosition)
	{
		worldPosition.X = Mathf.RountToInt(worldPosition.X);
		worldPosition.Y = Mathf.RountToInt(worldPosition.Y);
		return NavigationLayer.LocalToMap(NavigationLayer.ToLocal(worldPosition));
	}

	private Vector2 WorldToCell(Vector2I cellPosition)
	{
		return NavigationLayer.ToGlobal(NavigationLayer.MapToLocal(cellPosition));
	}

	private Vector2I NextCell(Vector2I currentCell, Vector2 _direction)
	{
		List<Vector2I> cells = new List<Vector2I> ();
		if (_direction.X > 0) cells.Add(new Vector2I(currentCell.X + 1, currentCell.Y));
		if (_direction.X < 0) cells.Add(new Vector2I(currentCell.X - 1, currentCell.Y));
		if (_direction.Y < 0) cells.Add(new Vector2I(currentCell.X, currentCell.Y + 1));
		if (_direction.Y < 0) cells.Add(new Vector2I(currentCell.X, currentCell.Y - 1));
		cells = cells.Where(c => NavigationLayer.GetCellSourceId(c) != -1).ToList();
		var cell = currentCell;
		float congruence = float.MinValue;
		foreach (var c in cells)
		{
			var cellPos = CellToWorld(c);
			var con = (cellPos - GlobalPosition).Dot(direction);
			if (con > congruence) 
			{
				congruence = con; cell = c;
			}
			return cell;
		}
	}
}
