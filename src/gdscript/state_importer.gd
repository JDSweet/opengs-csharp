extends Node

signal  reparent_provinces

func  _ready() -> void:
	generate_states()
	assign_owners()
	
func generate_states() -> void:
	print("STARTING TO GENERATE STATES")
	var state_folder = DirAccess.open("res://map/map_data/States/")
	state_folder.list_dir_begin()
	var file_name = state_folder.get_next()
	while file_name != "":
		var state_file = FileAccess.open("res://map/map_data/States/" + file_name,FileAccess.READ)
		var file_content = state_file.get_as_text().strip_edges()
		state_file.close()
		#id
		var from = file_content.find("id=")+3
		var to = file_content.find("name=")-from
		var id = int(file_content.substr(from,to))
		#name
		from = file_content.find("name=")+5
		to = file_content.find("provinces=")-from
		var state_name = str(file_content.substr(from,to)).replace('"',"")
		#provinces
		from = file_content.find("provinces=")+11
		to = file_content.find("}")-from
		var provinces = file_content.substr(from,to).strip_edges().split(" ")
		
		#Create State
		var state:State = State.new()
		state.name = str(id)
		state.id = id
		state.state_name = state_name
		state.provinces = provinces
		add_child(state)
		reparent_provinces.emit(state)
		
		#
		file_name = state_folder.get_next()
	state_folder.list_dir_end()
	print("Finished generating states!")


func assign_owners() -> void:
	get_node("925").set_state_owner("FRA")
	get_node("925").set_state_controller("FRA")
	get_node("931").set_state_owner("FRA")
	get_node("931").set_state_controller("FRA")
	get_node("936").set_state_owner("FRA")
	get_node("936").set_state_controller("FRA")
	get_node("470").set_state_controller("FRA")
	get_node("470").set_state_owner("FRA")
	get_node("926").set_state_owner("FRA")
	get_node("926").set_state_controller("FRA")
	get_node("386").set_state_controller("FRA")
	get_node("386").set_state_owner("FRA")
	get_node("322").set_state_owner("DEU")
	get_node("322").set_state_controller("DEU")
	get_node("261").set_state_owner("GBR")
	get_node("261").set_state_controller("GBR")
	get_node("318").set_state_owner("GBR")
	get_node("318").set_state_controller("GBR")
	get_node("475").set_state_controller("PRT")
	get_node("475").set_state_owner("PRT")
	get_node("483").set_state_owner("ITA")
	get_node("483").set_state_controller("ITA")
	get_node("500").set_state_controller("ITA")
	get_node("429").set_state_controller("ITA")
	get_node("500").set_state_owner("ITA")
	get_node("429").set_state_owner("ITA")
	get_node("387").set_state_controller("CZE")
	get_node("387").set_state_owner("CZE")
	get_node("404").set_state_controller("CZE")
	get_node("404").set_state_owner("CZE")
	get_node("325").set_state_controller("POL")
	get_node("409").set_state_controller("AUT")
	get_node("409").set_state_owner("AUT")
	get_node("452").set_state_controller("AUT")
	get_node("452").set_state_owner("AUT")
	get_node("133").set_state_controller("NOR")
	get_node("64").set_state_controller("NOR")
	get_node("416").set_state_controller("HUN")
	get_node("325").set_state_owner("POL")
	get_node("133").set_state_owner("NOR")
	get_node("64").set_state_owner("NOR")
	get_node("879").set_state_owner("SWE")
	get_node("879").set_state_controller("SWE")
	get_node("117").set_state_owner("SWE")
	get_node("117").set_state_controller("SWE")
	get_node("416").set_state_owner("HUN")
	get_node("465").set_state_owner("ESP")
	get_node("963").set_state_owner("ESP")
	get_node("465").set_state_controller("ESP")
	get_node("963").set_state_controller("ESP")
	get_node("960").set_state_owner("ESP")
	get_node("946").set_state_owner("ESP")
	get_node("960").set_state_controller("ESP")
	get_node("946").set_state_controller("ESP")
	get_node("464").set_state_owner("BGR")
	get_node("464").set_state_controller("BGR")
	get_node("421").set_state_owner("ROM")
	get_node("421").set_state_controller("ROM")
	get_node("499").set_state_owner("GRC")
	get_node("499").set_state_controller("GRC")
	get_node("482").set_state_owner("GRC")
	get_node("482").set_state_controller("GRC")
	
	get_node("321").set_state_controller("DNK")
	get_node("304").set_state_owner("DNK")
	get_node("286").set_state_controller("DNK")
	get_node("321").set_state_owner("DNK")
	get_node("304").set_state_controller("DNK")
	get_node("286").set_state_owner("DNK")
	get_node("310").set_state_controller("DNK")
	get_node("310").set_state_owner("DNK")
	
	get_node("425").set_state_controller("CHE")
	get_node("425").set_state_owner("CHE")
	
	get_node("379").set_state_controller("BEN")
	get_node("379").set_state_owner("BEN")
	get_node("353").set_state_controller("BEN")
	get_node("353").set_state_owner("BEN")

	get_node("436").set_state_controller("SER")
	get_node("436").set_state_owner("SER")
	get_node("473").set_state_controller("SER")
	get_node("473").set_state_owner("SER")
	get_node("474").set_state_controller("SER")
	get_node("474").set_state_owner("SER")
	get_node("439").set_state_controller("SER")
	get_node("439").set_state_owner("SER")
	get_node("467").set_state_controller("SER")
	get_node("467").set_state_owner("SER")
	get_node("459").set_state_controller("SER")
	get_node("459").set_state_owner("SER")
	
	get_node("331").set_state_controller("IRL")
	get_node("331").set_state_owner("IRL")
