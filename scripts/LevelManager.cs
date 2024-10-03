using Godot;
using System;

public partial class LevelManager : GodotObject
{
	private double _timeElapsed = 0;
	public void LoadLevel()
	{
		GD.Print("Initialized:");
		GD.Print($"  Starting Time: {_timeElapsed}");
	}
}
