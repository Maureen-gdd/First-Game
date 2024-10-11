using Godot;
using System;

public partial class SaveManager : Node
{
	private static SaveManager instance;
	private bool caca = false;

	public static SaveManager GetInstance() 
	{
		if (instance == null)
		{
			instance = new SaveManager();
		}
		return instance;
	}

public override void _Ready()
{
	LoadScene("user://savegame.save");
	
}

public override void _Process(double delta)
{
	if(caca == false) 
	{
		LoadGame("user://savegame.save");
		GD.Print("Character Loaded !");
		caca = true;
	}
}

public void LoadScene(string cheminFichierSaveLoad)
	{
		if (!FileAccess.FileExists(cheminFichierSaveLoad))
		{
			GD.Print($"No Save !");
			return; // Error! We don't have a save to load.
		}

		// Load the file line by line and process that dictionary to restore the object
		// it represents.
		using var saveFile = FileAccess.Open(cheminFichierSaveLoad, FileAccess.ModeFlags.Read);
		while (saveFile.GetPosition() < saveFile.GetLength())
		{
			GD.Print($"LoadScene in file");
			var jsonString = saveFile.GetLine();

			// Creates the helper class to interact with JSON
			var json = new Json();
			var parseResult = json.Parse(jsonString);
			if (parseResult != Error.Ok)
			{
				GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
				continue;
			}

			// Get the data from the JSON object
			var nodeData = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);
			
			string sceneName = null;
			if (nodeData.ContainsKey("Scene"))
			{
				sceneName = nodeData["Scene"].ToString();
				GD.Print($"La scène est : {sceneName}");
				if (sceneName == "Game1")
				{
					CustomMainLoop.GetInstance().GetLevelManager().LoadLevel("scene1.tscn");
				}
				else
				{
					CustomMainLoop.GetInstance().GetLevelManager().LoadLevel("scene2.tscn");
				}
			}
		}
	}

	public void LoadGame(string cheminFichierSaveLoad)
	{
		if (!FileAccess.FileExists(cheminFichierSaveLoad))
		{
			GD.Print($"No Save !");
			return; // Error! We don't have a save to load.
		}

		// We need to revert the game state so we're not cloning objects during loading.
		// This will vary wildly depending on the needs of a project, so take care with
		// this step.
		// For our example, we will accomplish this by deleting saveable objects.
		
		var saveNodes = GetTree().GetNodesInGroup("Persist");
		foreach (Node saveNode in saveNodes)
		{
			saveNode.QueueFree();
		}

		// Load the file line by line and process that dictionary to restore the object
		// it represents.
		using var saveFile = FileAccess.Open(cheminFichierSaveLoad, FileAccess.ModeFlags.Read);
		while (saveFile.GetPosition() < saveFile.GetLength())
		{
			GD.Print($"SaveGame in File");
			var jsonString = saveFile.GetLine();

			// Creates the helper class to interact with JSON
			var json = new Json();
			var parseResult = json.Parse(jsonString);
			if (parseResult != Error.Ok)
			{
				GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
				continue;
			}

			// Get the data from the JSON object
			var nodeData = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);

			// Firstly, we need to create the object and add it to the tree and set its position.
			var newObjectScene = GD.Load<PackedScene>(nodeData["Filename"].ToString());
			var newObject = newObjectScene.Instantiate<Node>();
			GetNode(nodeData["Parent"].ToString()).AddChild(newObject);
			newObject.Set(Node2D.PropertyName.Position, new Vector2((float)nodeData["PosX"], (float)nodeData["PosY"]));
			
			newObject.AddToGroup("Persist");
		
			// Now we set the remaining variables.
			foreach (var (key, value) in nodeData)
			{
				if (key == "Filename" || key == "Parent" || key == "PosX" || key == "PosY")
				{
					continue;
				}
				newObject.Set(key, value);
			}
		}
	}
	
	public void SaveGame(string cheminFichierSaveWrite)
	{
		using var saveFile = FileAccess.Open(cheminFichierSaveWrite, FileAccess.ModeFlags.Write);

		var saveNodes = GetTree().GetNodesInGroup("Persist");
		foreach (Node saveNode in saveNodes)
		{
			// Check the node is an instanced scene so it can be instanced again during load.
			if (string.IsNullOrEmpty(saveNode.SceneFilePath))
			{
				GD.Print($"persistent node '{saveNode.Name}' is not an instanced scene, skipped");
				continue;
			}

			// Check the node has a save function.
			if (!saveNode.HasMethod("Save"))
			{
				GD.Print($"persistent node '{saveNode.Name}' is missing a Save() function, skipped");
				continue;
			}

			// Call the node's save function.
			var nodeData = saveNode.Call("Save");
			
			//Debug
			GD.Print($"NodeData : {nodeData}");

			// Json provides a static method to serialized JSON string.
			var jsonString = Json.Stringify(nodeData);

			// Store the save dictionary as a new line in the save file.
			saveFile.StoreLine(jsonString);
		}
	}
}
