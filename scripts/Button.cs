using Godot;
using System;

public partial class Button : Godot.Button
{
	public override void _Ready()
	{
		Pressed += ButtonExitPressed;
	}

	private void ButtonExitPressed()
	{
		GD.Print("Bt pressed");
		CustomMainLoop.GetInstance()._Finalize();
	}
}
