using Godot;

using System;
using System.Collections.Generic;

using OGS.Map;
namespace OGS.Map;

/*
 extends Node
class_name State

var id:int
var state_name:String
var	provinces:Array

func set_state_owner(tag):
	for node in get_children():
		node.set_province_owner(tag)
		
func set_state_controller(tag):
	for node in get_children():
		node.set_province_controller(tag)

 */

public partial class State : Node
{
    public int Id { get; set; }
    public string StateName { get; set; }
    public List<string> provinces { get; set; } = new();

    public void SetStateOwner(string tag)
    {
        foreach (Node node in GetChildren())
        {
            if (node is Province province)
            {
                province.SetProvinceOwner(tag);
            }
        }
    }

    public void SetStateController(string tag)
    {
        foreach (Node node in GetChildren())
        {
            if (node is Province province)
            {
                province.SetProvinceController(tag);
            }
        }
    }
}
