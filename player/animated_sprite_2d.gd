extends AnimatedSprite2D


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

const SPEED = 300.0

func _physics_process(delta: float) -> void:
	var velocity = Vector2.ZERO
	var direction := Input.get_axis("left_axis","right_axis")
	if direction:
		velocity.x = direction * SPEED
	else:
		velocity.x = 0
	
	position += velocity * delta
