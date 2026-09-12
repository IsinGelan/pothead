extends Area2D

@onready var level_manager = %LevelManager

@export var hp_dealt: int = 17

func _on_body_entered(body: Node2D) -> void:
	level_manager.player_take_damage(hp_dealt)
	
