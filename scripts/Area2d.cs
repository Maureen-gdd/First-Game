using Godot;
using System;

public partial class Area2d : Area2D
{
	 public override void _Ready()
	{
		// Connecte le signal body_entered pour détecter les collisions
		this.BodyEntered += OnBodyEntered;
	}

	// Fonction appelée lors de l'entrée d'un corps dans la zone de collision
	private void OnBodyEntered(Node body)
	{
		
		if(GetTree().CurrentScene.Name == "Game1")
			CustomMainLoop.GetInstance().GetLevelManager().LoadLevel("scene2.tscn");
		else
			CustomMainLoop.GetInstance().GetLevelManager().LoadLevel("scene1.tscn");
	}
}
