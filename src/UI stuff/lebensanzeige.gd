

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
var n = 0
var child = TextureRect

func update_Hearts(Am: int):
	child = $node/Heart.duplicate()
	for x in range(Am):
		pass
		
	
