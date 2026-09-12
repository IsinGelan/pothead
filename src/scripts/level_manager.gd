extends Node


@export var player_init_hp: int = 100
const player_init_lives = 3

@onready var level_hud = %Hud
@onready var death_timer = $DeathTimer

var player_hp: int = player_init_hp
var player_lives: int = player_init_lives
var my_player: Player

func register_player(player: Player):
	my_player = player

func player_take_damage(hp_amount: int):
	player_hp -= hp_amount
	player_show_hp()
	
	if player_hp <= 0:
		player_die()
		
func player_show_hp():
	level_hud.set_health_water_level(int(player_hp/10))
	
func player_die():
	player_lives -= 1
	level_hud.die(player_lives)
	death_timer.start()


func _on_death_timer_timeout() -> void:
#	Falscher Funktionsname
	player_respawn()

func player_respawn():
	print("YOU RESPAWNED +++")
	get_tree().reload_current_scene()
	#my_player.reload_current_scene()
	player_show_hp()
	player_hp = player_init_hp
	
	
