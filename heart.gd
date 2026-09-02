extends Node2D


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if Levelmanager.player_hp >= position.x / 130:
		rotation_degrees == 0
	else:
		rotation_degrees == 90
		
	
