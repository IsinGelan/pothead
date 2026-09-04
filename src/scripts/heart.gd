extends Node2D

func fade():
	var tween = create_tween()
	tween.tween_property($".", "modulate:a", 0.0, 0.5)
	tween.tween_callback(hide)
	disappear()

func disappear():
	print("I disappear!")
	visible = false
func reappear():
	visible = true
