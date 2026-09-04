extends Node

@onready var health_pipe = $HealthPipe
@onready var hearts_bar = $HeartsBar

func set_health_water_level(to: int):
	health_pipe.set_water_level(to)
func die(lives_left: int):
	hearts_bar.die(lives_left)
