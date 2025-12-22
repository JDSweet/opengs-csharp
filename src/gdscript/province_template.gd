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
