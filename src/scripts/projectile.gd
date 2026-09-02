extends Area2D

@export var projectile_speed = 1000.0

@onready var projectiles = get_parent()
var bang_scene = preload("res://src/scenes/bang.tscn")

var direction = Vector2.RIGHT
var velocity = Vector2.ZERO

func shoot(dir: Vector2):
	direction = dir
	velocity = direction * projectile_speed

func _physics_process(delta):
	gravity(delta)
	position += velocity * delta

func gravity(delta):
	velocity.y += gravity * delta

func _on_body_entered(body: Node2D) -> void:
	#TODO: do something when impacting e.g. a boss
	var bang = bang_scene.instantiate()
	bang.global_position = position
	projectiles.add_child(bang)
	print("Hit", body.name)
	
	queue_free()
	
