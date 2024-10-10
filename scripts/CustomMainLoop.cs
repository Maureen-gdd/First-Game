using Godot;
using System;

[GlobalClass]
public partial class CustomMainLoop : SceneTree
{
	private static CustomMainLoop _instance;
	private LevelManager _levelManager;
	private SaveManager _saveManager;
	
	public CustomMainLoop()
	{
		_instance = this;
		GD.Print("CustomMainLoop instantiated");
	}
	
	public override void _Initialize()
{
	GD.Print("Initialized begins.......");
	_levelManager = new LevelManager();
	Root.AddChild(_levelManager);
	_levelManager.LoadLevel("scene2.tscn");
	_saveManager = new SaveManager();
	Root.AddChild(_saveManager);
	
	//OS.Connect("about_to_quit", this, nameof(OnAboutToQuit));
	GD.Print("Initialization finish !");
}


	
	public static CustomMainLoop GetInstance()
	{
		if (_instance == null)
		{
			_instance = new CustomMainLoop();
		}
		return _instance;
	}
	
	public LevelManager GetLevelManager()
	{
		return _levelManager;
	}

	public SaveManager GetSaveManager()
	{
		return _saveManager;
	}

	public override bool _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.Escape))
		{
			_Finalize();
			Quit();
			_saveManager.SaveGame("user://savegame.save");
			return true;
		}
		return false;
	}
	private void _Finalize()
	{
		GD.Print("Finalized finished !");
	}
}
