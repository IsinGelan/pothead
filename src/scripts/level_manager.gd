extends Node

## We are doing this, to have a reference from anywhere in the level to
## attributes of e.g. the player

## Called when the node enters the scene tree for the first time.
#func _ready() -> void:
	#pass # Replace with function body.
#
#
## Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta: float) -> void:
	#pass
	
@export var player_init_hp: int = 100

var player_hp: int = player_init_hp

func player_take_damage(hp_amount: int):
	player_hp -= hp_amount
	print("Your HP are:", player_hp)
	if player_hp <= 0:
		player_die()
		
func player_die():
	print("YOU DIED :(")
	get_tree().reload_current_scene()
	player_respawn()

func player_respawn():
	print("YOU RESPAWNED +++")
	player_hp = player_init_hp
	
