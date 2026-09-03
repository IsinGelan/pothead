extends Node


@export var player_init_hp: int = 100

@onready var level_hud = %Hud
@onready var death_timer = $DeathTimer

var player_hp: int = player_init_hp

func player_take_damage(hp_amount: int):
	player_hp -= hp_amount
	player_show_hp()
	if player_hp <= 0:
		player_die()
		
func player_show_hp():
	level_hud.set_health_water_level(int(player_hp/10))
	
func player_die():
	print("YOU DIED :(")
	death_timer.start()


func _on_death_timer_timeout() -> void:
#	Falscher Funktionsname
	print("Banana")
	player_respawn()

func player_respawn():
	print("YOU RESPAWNED +++")
	get_tree().reload_current_scene()
	player_hp = player_init_hp
	
	
