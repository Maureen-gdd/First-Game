using Godot;
using System;

[GlobalClass]
public partial class CustomMainLoop : SceneTree
{
	private static CustomMainLoop instance;

	public CustomMainLoop()
	{
		instance = this;
	}
	
	public static CustomMainLoop Get()
	{
		return instance;
	}
	public LevelManager GetLevelManager(LevelManager LevelManager)
	{
		return LevelManager;
	}
	public SaveManager GetSaveManager(SaveManager SaveManager)
	{
		return SaveManager;
	}
}
