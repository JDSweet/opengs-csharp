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
