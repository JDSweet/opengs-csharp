extends CanvasLayer
signal map_mode_selected(mode)
enum MapMode {POLITICAL, IDEOLOGY}

func _on_button_political_button_up() -> void:
	map_mode_selected.emit(MapMode.POLITICAL)

func _on_button_ideology_button_up() -> void:
	map_mode_selected.emit(MapMode.IDEOLOGY)
