using Godot;
using System;

public partial class character_body_2d : Godot.CharacterBody2D
{
	private const float SPEED = 100.0f;

	public override void _PhysicsProcess(double delta)
	{
		var animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var velocity = Vector2.Zero;
		
		float directionX = Input.GetAxis("left_axis", "right_axis");
		float directionY = Input.GetAxis("up_axis", "down_axis");

		velocity.X = directionX * SPEED;
		velocity.Y = directionY * SPEED;
		
		if (directionX != 0)
		{
			animation.Play("running");
			animation.FlipH = directionX < 0;
		}
		else if (directionY != 0)
		{
			animation.Play("running");
		}
		else
		{
			animation.Play("idle");
		}
		
		MoveAndCollide(velocity * (float)delta);
	}
	public Godot.Collections.Dictionary<string, Variant> Save()
	{
		return new Godot.Collections.Dictionary<string, Variant>()
		{
			{ "Filename", SceneFilePath },  // Scene file path
			{ "Parent", GetParent().GetPath() },  // Parent node path to add the character to on load
			{ "PosX", Position.X },
			{ "PosY", Position.Y }
		};
	}
}
