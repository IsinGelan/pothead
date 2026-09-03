extends Node

@onready var health_pipe = $HealthPipe

func set_health_water_level(to: int):
	health_pipe.set_water_level(to)
