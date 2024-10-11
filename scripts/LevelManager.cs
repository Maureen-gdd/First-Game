using Godot;
using System;

public partial class LevelManager : Node
{
	public SceneTree GameManager = CustomMainLoop.GetInstance();
	
	public void LoadLevel(string path)
	{
		// On vérifie si le chemin d'accès renvoie bien une scène
		var levelPacked = GD.Load<PackedScene>("res://scenes/" + path);
		if (levelPacked == null)
		{
			GD.PrintErr($"Scene not found: {path}");
			return;
		}
		
		GameManager.ChangeSceneToPacked(levelPacked);
		
		GD.Print($"Loaded scene: {path}");
	}
}
