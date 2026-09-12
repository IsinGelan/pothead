extends Sprite2D

func fade_out():
	var tween = create_tween()
	tween.tween_property($".", "modulate:a", 0.0, 0.5)
	tween.tween_callback(queue_free)


func _on_ready() -> void:
	print("Hi! @ ", position)
	fade_out()
