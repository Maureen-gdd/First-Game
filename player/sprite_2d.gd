extends Sprite2D

const SPEED = 300.0

func _physics_process(delta: float) -> void:
	var velocity = Vector2.ZERO
	var direction := Input.get_axis("left_axis","right_axis")
	if direction:
		velocity.x = direction * SPEED
	else:
		velocity.x = 0
	
	position += velocity * delta
