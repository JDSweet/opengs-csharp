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
