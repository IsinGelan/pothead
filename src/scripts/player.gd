extends CharacterBody2D
class_name Player  # This makes "Player" a recognized type

@export var horizontal_speed: float = 300.0
@export var crouch_speed: float = 150.0
@export var jump_speed: float = -600.0
@export var gravity: float = 980.0
@export var onground_slowdown_steps: int = 2
@export var max_jumps: int = 4;

@onready var projectile_scene = get_node_or_null("%Projectile") 
@onready var level_manager = get_node_or_null("%LevelManager")



# ================================
var jumps_remaining: int = max_jumps;

#func _on_collision_shape_2d_ready() -> void:
	#level_manager.register_player(self)

func _init() -> void:
	assert(onground_slowdown_steps > 0, "Onground Slowdown Steps must be > 0!")
	Input.action_press("crouch")
	Input.action_release("crouch")

func _physics_process(delta: float) -> void:
	if is_on_floor():
		onground_movement(delta)
	else:
		in_air_movement(delta)
	if Input.is_action_pressed("crouch"):
		crouch()
	elif Input.is_action_just_released("crouch"):
		uncrouch()
	move_and_slide()

# ================================
# Helpers
func reset_jumps() -> void:
	jumps_remaining = max_jumps
	
func jump() -> void:
	if jumps_remaining == 0:
		return
	velocity.y = jump_speed
	jumps_remaining -= 1

func horizontal_move() -> bool:
	# returns whether player got movement input
	var key_pressed_direction = Input.get_axis("walk left", "walk right")
	if key_pressed_direction:
		if Input.is_action_pressed("crouch"):
			velocity.x = key_pressed_direction * crouch_speed
		else:
			velocity.x = key_pressed_direction * horizontal_speed
		return true
	return false

func slow_down() -> void:
	velocity.x = move_toward(velocity.x, 0, horizontal_speed / onground_slowdown_steps)

# ================================
# Movement functions
func onground_movement(delta: float) -> void:
	reset_jumps()
	var jumping = Input.is_action_just_pressed("jump")
	if jumping:
		jump()
	if not horizontal_move():
		slow_down()
	
func in_air_movement(delta: float) -> void:
	velocity.y += gravity * delta
	var jumping = Input.is_action_just_pressed("jump")
	if jumping:
		jump()
	horizontal_move()
	
# ================================
# Shooting
func _input(event):
	if event.is_action_pressed("shoot"):
		shoot()

func shoot():
	projectile_scene.shoot(self)

#==================================
#Crouching visuals and hitbox

func crouch():
	$Sprite2D.visible = false
	$cSprite2D.visible = true
	$CollisionShape2D.set_deferred("disabled", true)
	$cCollision.set_deferred("disabled", false)

func uncrouch():
	velocity.y += -200
	$Sprite2D.visible = true
	$cSprite2D.visible = false
	$CollisionShape2D.set_deferred("disabled", false)
	$cCollision.set_deferred("disabled", true)
	
