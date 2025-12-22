using Godot;

using System;

using OGS.Map;

namespace OGS.Map;

/*
 extends Node
class_name Province

var id:int
var color:Color
var type:String
var province_owner:Country 
var province_controller:Country
var position: Vector2 = Vector2(0,0)

func set_province_owner(tag):
	if province_owner:
		province_owner.remove_province(self)
	province_owner = Globals.tag_to_country[tag]
	province_owner.add_province(self)
	
func set_province_controller(tag):
	province_controller = Globals.tag_to_country[tag]
 */

public partial class Province : Node
{
    public int Id { get; set; }
    public Color Color { get; set; }
    public string Type { get; set; }
    public Country ProvinceOwner { get; private set; }
    public Country ProvinceController { get; private set; }
    public Vector2 Position { get; set; } = Vector2.Zero;

    public void SetProvinceOwner(string tag)
    {
        if (ProvinceOwner != null)
        {
            ProvinceOwner.RemoveProvince(this);
        }
        ProvinceOwner = Globals.TagToCountry[tag];
        ProvinceOwner.AddProvince(this);
    }

    public void SetProvinceController(string tag)
    {
        ProvinceController = Globals.TagToCountry[tag];
    }
}
