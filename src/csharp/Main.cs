using Godot;
using System;

using OGS.Map;
namespace OGS.Map;

/*
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

 */

public partial class Main : Node3D
{
    [Export]
    public Texture2D ProvinceMap { get; set; }

    public void OnPlayerProvinceSelected(Vector2 coordinates)
    {
        var provinceColor = ProvinceMap.GetImage().GetPixel((int)(coordinates.X * 10), (int)(coordinates.Y * 10));
        var selectedProvince = GetNode<ProvinceImporter>("Provinces").colorToProvince[provinceColor];
        GD.Print(selectedProvince);
        GetNode<ProvinceSelected>("ProvinceSelected").UpdateLabels(selectedProvince);
        GetNode<Map>("Map").HighlightProvince(selectedProvince);
        GetNode<ProvinceSelected>("ProvinceSelected").SetPosition(selectedProvince, coordinates);
    }

    public void OnStatesReparentProvinces(State state)
    {
        //GD.Print("Reparenting provinces for state: " + state.Name);
        foreach (var provinceName in state.provinces)
        {
            var nodeToMove = GetNode<ProvinceImporter>("Provinces").GetNode<Province>(provinceName);
            //nodeToMove.GetParent().RemoveChild(nodeToMove);
            nodeToMove.Reparent(state);
            //state.AddChild(nodeToMove);
        }
    }
}
