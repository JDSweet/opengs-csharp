extends Node3D
@export var province_map:Texture2D

func _on_player_province_selected(coordinates) -> void:
	var province_color = province_map.get_image().get_pixel(coordinates.x*10,coordinates.y*10)
	var selected_province = $Provinces.color_to_province[province_color]
	print(selected_province)
	$ProvinceSelected.update_labels(selected_province)
	$Map.highlight_province(selected_province)
	$ProvinceSelected.set_position(selected_province, coordinates)
	
func _on_states_reparent_provinces(state) -> void:
	for province in state.provinces:
		var node_to_move = $Provinces.get_node(province)
		node_to_move.reparent(state)
