extends CharacterBody2D


const SPEED = 100.0
const JUMP_VELOCITY = -400.0



func _physics_process(delta: float) -> void:
	var velocity = Vector2.ZERO
	var direction_x := Input.get_axis("left_axis", "right_axis")
	var direction_y := Input.get_axis("up_axis", "down_axis")
	
	velocity.x = direction_x * SPEED
	velocity.y = direction_y * SPEED
	
	velocity = move_and_collide(delta * velocity)
