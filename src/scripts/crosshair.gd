extends Node


# Load the custom images for the mouse cursor.
var crosshair = load("res://assets/textures/crosshair.svg")
#var beam = load("res://beam.png")


func _ready():
	# Changes only the arrow shape of the cursor.
	# This is similar to changing it in the project settings.
	Input.set_custom_mouse_cursor(crosshair)

	# Changes a specific shape of the cursor (here, the I-beam shape).
	#Input.set_custom_mouse_cursor(beam, Input.CURSOR_IBEAM)
