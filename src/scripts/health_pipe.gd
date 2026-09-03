extends Node2D

const water_sections = 10

@onready var water = $Water
@onready var front = $Front
@onready var water_size = water.texture.get_size()
@onready var section_width = water_size.x / water_sections
var water_left_original: float

func _on_ready():
	water.region_enabled = true
	water_left_original = float(water.position.x)
	set_water_level(10)

func set_water_level(to: int):
	print("to", to)
	var width_after: float = to*section_width
	water.region_rect = Rect2(
		0,
		0,
		width_after,
		water_size.y
	)
	# reposition
	water.position.x = 0.5 * (water_size.x - width_after)
