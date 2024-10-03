using Godot;
using System;

[GlobalClass]
public partial class CustomMainLoop : SceneTree
{
	private double _timeElapsed = 0;

	public override void _Initialize()
	{
		GD.Print("Initialized:");
		GD.Print($"  Starting Time: {_timeElapsed}");
		CustomMainLoop._Get();
	}

	public override bool _Process(double delta)
	{
		_timeElapsed += delta;
		// Return true to end the main loop.
		return Input.GetMouseButtonMask() != 0 || Input.IsKeyPressed(Key.Escape);
	}

	private void _Finalize()
	{
		GD.Print("Finalized:");
		GD.Print($"  End Time: {_timeElapsed}");
	}
	
	public static _Get()
	{
		
	}
	public _GetLevelManager()
	{
		return LevelManager;
	}
	public _GetSaveManager()
	{
		return SaveManager;
	}
}
