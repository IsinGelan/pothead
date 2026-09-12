extends Node

var projectile_scene = preload("res://src/scenes/projectile.tscn")

@onready var projectiles = $"."

func shoot(as_player: Player):
	var projectile = projectile_scene.instantiate()
	var rel_pos = as_player.get_global_mouse_position() - as_player.global_position
	var direction = rel_pos.normalized()
	print(direction)
	
	projectile.global_position = as_player.position
	projectile.shoot(direction)

	#get_tree().current_scene.add_child(projectile)
	projectiles.add_child(projectile)
