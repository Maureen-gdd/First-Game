extends CharacterBody2D


const SPEED = 100.0


func _physics_process(delta: float) -> void:
	var animation = $AnimatedSprite2D
	var velocity = Vector2.ZERO
	var direction_x := Input.get_axis("left_axis", "right_axis")
	var direction_y := Input.get_axis("up_axis", "down_axis")
	
	velocity.x = direction_x * SPEED
	velocity.y = direction_y * SPEED
	if direction_x:
		animation.play("running")
		animation.flip_h = direction_x < 0
	elif direction_y:
		animation.play("running")
	else:
		animation.play("idle")
	velocity = move_and_collide(delta * velocity)
