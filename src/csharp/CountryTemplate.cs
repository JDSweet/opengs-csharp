using Godot;

using System;
using System.Collections.Generic;
using OGS.Map;
namespace OGS.Map;

/*
 extends Node

	class_name Country

	var owned_provinces: Array[Province]
	var map_label: Node2D

	func add_province(province: Province) -> void:
		owned_provinces.append(province)

	func remove_province(province: Province) -> void:
		owned_provinces.erase(province)

	var tag:String
	var country_name:String
	var color:Color
	var ideology:String:
		set(value):
			ideology = value
			match ideology:
				"Democratic":
					ideology_color = Color("BLUE")
				"Communist":
					ideology_color = Color("RED")
	var ideology_color:Color
 */

public partial class Country : Node
{
    public List<Province> OwnedProvinces { get; private set; } = new();
    public Node2D MapLabel { get; set; }

    public string Tag { get; set; }
    public string CountryName { get; set; }
    public Color Color { get; set; }

    private string ideology;
    public string Ideology
    {
        get => ideology;
        set
        {
            ideology = value;
            IdeologyColor = ideology switch
            {
                "Democratic" => Colors.Blue,
                "Communist" => Colors.Red,
                _ => Colors.White
            };
        }
    }

    public Color IdeologyColor { get; private set; }

    public void AddProvince(Province province)
    {
        OwnedProvinces.Add(province);
    }

    public void RemoveProvince(Province province)
    {
        OwnedProvinces.Remove(province);
    }
}
