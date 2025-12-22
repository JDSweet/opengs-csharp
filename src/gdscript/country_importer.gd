extends Node

func _ready() -> void:
	create_countries("NNN","No Name",Color("TRANSPARENT"),"Communist")
	create_countries("FRA","France",Color("BLUE"),"Democratic")
	create_countries("DEU","Germany",Color("BLACK"),"Democratic")
	create_countries("ITA","Italy",Color("WEB_GREEN"),"Democratic")
	create_countries("ESP","Spain",Color("GOLD"),"Democratic")
	create_countries("NOR","Norway",Color("DARK_RED"),"Democratic")
	create_countries("SWE","Sweden",Color("MEDIUM_BLUE"),"Democratic")
	create_countries("GBR","United Kingdom",Color("RED"),"Democratic")
	create_countries("GRC","Greece",Color("CYAN"),"Democratic")
	create_countries("PRT","Portugal",Color("TEAL"),"Democratic")
	create_countries("AUT","Austria",Color("MAGENTA"),"Democratic")
	create_countries("POL","Poland",Color("PINK"),"Communist")
	create_countries("CZE","Czechoslovakia",Color("AQUA"),"Communist")
	create_countries("HUN","Hungary",Color("MAROON"),"Communist")
	create_countries("ROM","Romania",Color("BROWN"),"Communist")
	create_countries("BGR","Bulgaria",Color("DARK_GREEN"),"Communist")
	create_countries("USR","Soviet Union",Color("CRIMSON"),"Communist")
	create_countries("DNK","Denmark",Color("9d655dff"),"Democratic")
	create_countries("CHE","Switzerland",Color("bf0b39ff"),"Democratic")
	create_countries("BEN","Benelux",Color("bb5711ff"),"Democratic")
	create_countries("SER","Serbia",Color("3c39a8ff"),"Democratic")
	create_countries("IRL","Ireland",Color("1e9028ff"),"Democratic")
	
func create_countries(tag, country_name, color, ideology):
	var country:Country = Country.new()
	country.tag = tag
	country.name = tag
	country.country_name = country_name
	country.color = color
	country.ideology = ideology
	add_child(country)
	Globals.tag_to_country[tag] = country
