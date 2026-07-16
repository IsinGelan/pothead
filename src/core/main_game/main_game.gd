class_name MainGame
extends Node
## Main entry point for the game
## Responsible for setting up the World layers and coordinating high-level systems

const PLAYER        : String = "uid://bkv0mgw1spm67"
const TEST_LEVEL_02 : String = "uid://dpksge3nv1ae2"

var player = null


# Game World
@onready var level_root  : Node2D = $World/LevelRoot
@onready var entity_root : Node2D = $World/EntityRoot
@onready var effect_root : Node2D = $World/EffectRoot

# UI Root Nodes
@onready var hud_root: Control = $HudLayer/HudRoot
@onready var pause_root: Control = $PauseLayer/PauseRoot
@onready var transition_layer: CanvasLayer = $TransitionLayer
