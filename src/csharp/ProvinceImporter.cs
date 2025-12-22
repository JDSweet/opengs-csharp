using Godot;
using System;
using System.Collections.Generic;


using OGS.Map;
namespace OGS.Map;
/*
 extends Node

@onready var color_to_province :Dictionary

func  _ready() -> void:
	generate_provinces()
	
func generate_provinces() -> void:
	print("STARTING TO GENERATE PROVINCES")
	
	var province_file:String = FileAccess.open("res://map/map_data/Provinces.txt", FileAccess.READ).get_as_text()
	var rows:Array = province_file.split("\n")
	for row in rows:
		if row.strip_edges() != "":
			var columns:Array = row.split(",")
			var province_id:int = int(columns[0])
			var province_color:Color = Color(float(columns[1])/255,float(columns[2])/255,float(columns[3])/255)
			var province_type:String = columns[4]
			var province_position: Vector2 = Vector2(float(columns[5]),float(columns[6]))
			
			var province:Province = Province.new()
			province.name = str(province_id)
			province.id = province_id
			province.color = province_color
			province.type = province_type
			province.position = province_position
			if province_type == "land":
				province.set_province_owner("NNN")
				province.set_province_controller("NNN")
			
			add_child(province)
			
			color_to_province[province_color] = province
			
			
func save_provinces_to_file() -> void:
	var province_file = FileAccess.open("res://map/map_data/Provinces.txt", FileAccess.WRITE)
	for province in color_to_province.values():
		var color = province.color
		var line = "%d,%d,%d,%d,%s,%.2f,%.2f" % [
			province.id,
			int(color.r * 255),
			int(color.g * 255),
			int(color.b * 255),
			province.type,
			province.position.x,
			province.position.y
		]
		province_file.store_line(line)
	print("Provinces saved successfully!")
	


func _on_province_selected_save_provinces() -> void:
	save_provinces_to_file()

 */

public partial class ProvinceImporter : Node
{
    public Dictionary<Color, Province> colorToProvince = new();

    public override void _Ready()
    {
        GenerateProvinces();
    }

    private void GenerateProvinces()
    {
        GD.Print("STARTING TO GENERATE PROVINCES");

        var file = FileAccess.Open("res://map/map_data/Provinces.txt", FileAccess.ModeFlags.Read);
        if (file == null)
        {
            GD.PrintErr("Failed to open Provinces.txt");
            return;
        }

        string provinceFile = file.GetAsText();
        file.Close();

        var rows = provinceFile.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        foreach (var row in rows)
        {
            var trimmedRow = row.Trim();
            if (trimmedRow != "")
            {
                var columns = trimmedRow.Split(',');
                int provinceId = int.Parse(columns[0]);
                Color provinceColor = new(
                    float.Parse(columns[1]) / 255f,
                    float.Parse(columns[2]) / 255f,
                    float.Parse(columns[3]) / 255f
                );
                string provinceType = columns[4];
                Vector2 provincePosition = new(
                    float.Parse(columns[5]),
                    float.Parse(columns[6])
                );

                var province = new Province(); //GD.Load<PackedScene>("res://path_to_province_scene.tscn")?.Instantiate<Province>() ?? new Province();
                province.Name = provinceId.ToString();
                province.Id = provinceId;
                province.Color = provinceColor;
                province.Type = provinceType;
                province.Position = provincePosition;

                if (provinceType == "land")
                {
                    province.SetProvinceOwner("NNN");
                    province.SetProvinceController("NNN");
                }

                AddChild(province);
                colorToProvince[provinceColor] = province;
            }
        }
    }

    private void SaveProvincesToFile()
    {
        var file = FileAccess.Open("res://map/map_data/Provinces.txt", FileAccess.ModeFlags.Write);
        if (file == null)
        {
            GD.PrintErr("Failed to open Provinces.txt for writing");
            return;
        }

        foreach (var province in colorToProvince.Values)
        {
            var color = province.Color;
            string line = string.Format(
                "{0},{1},{2},{3},{4},{5:F2},{6:F2}",
                province.Id,
                (int)(color.R * 255),
                (int)(color.G * 255),
                (int)(color.B * 255),
                province.Type,
                province.Position.X,
                province.Position.Y
            );
            file.StoreLine(line);
        }
        file.Close();
        GD.Print("Provinces saved successfully!");
    }

    private void _OnProvinceSelectedSaveProvinces()
    {
        SaveProvincesToFile();
    }
}
