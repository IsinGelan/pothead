extends Node2D

@onready var heart1 = $Heart1
@onready var heart2 = $Heart2
@onready var heart3 = $Heart3

func reset():
	heart1.reappear()
	heart2.reappear()
	heart3.reappear()
	
func die(lives_left: int):
	if lives_left == 2:
		heart3.fade()
	if lives_left == 1:
		heart2.fade()
	if lives_left == 0:
		heart1.fade()
	
