extends CharacterBody2D


const SPEED = 200.0
const JUMP_VELOCITY = -300.0
var djump = true
var facing = 0

func _physics_process(delta: float) -> void:
	# Add the gravity.
	if not is_on_floor():
		velocity += get_gravity() * delta

	# Handle jump.
	if Input.is_action_just_pressed("jump") and (is_on_floor() or djump):
		velocity.y = JUMP_VELOCITY
		if not is_on_floor():
			djump = false

	if is_on_floor():
		djump = true

	# Get the input direction and handle the movement/deceleration.
	# As good practice, you should replace UI actions with custom gameplay actions.
	var direction := Input.get_axis("Walk left", "Walk right")
	if direction:
		velocity.x = direction * SPEED
		facing = direction
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)

	
	if Input.is_action_just_pressed("dash"):
		velocity.x = facing * SPEED * 5

	move_and_slide()
