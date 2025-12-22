using Godot;
using System;
using System.Collections.Generic;
using OGS.Map;
namespace OGS.Map;

/*
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

 */
public partial class StateImporter : Node
{
    [Signal]
    public delegate void ReparentProvincesEventHandler(State state);

    public override void _Ready()
    {
        GenerateStates();
        AssignOwners();
    }

    private void GenerateStates()
    {
        GD.Print("STARTING TO GENERATE STATES");
        var stateFolder = DirAccess.Open("res://map/map_data/States/");
        if (stateFolder == null)
        {
            GD.PrintErr("Failed to open States directory.");
            return;
        }

        stateFolder.ListDirBegin();
        string fileName = stateFolder.GetNext();
        while (fileName != "")
        {
            if (!fileName.EndsWith(".txt"))
            {
                fileName = stateFolder.GetNext();
                continue;
            }

            var stateFile = FileAccess.Open($"res://map/map_data/States/{fileName}", FileAccess.ModeFlags.Read);
            if (stateFile == null)
            {
                GD.PrintErr($"Failed to open state file: {fileName}");
                fileName = stateFolder.GetNext();
                continue;
            }

            string fileContent = stateFile.GetAsText().Trim();
            stateFile.Close();

            // Parse id
            int from = fileContent.IndexOf("id=") + 3;
            int to = fileContent.IndexOf("name=") - from;
            int id = int.Parse(fileContent.Substring(from, to));

            // Parse name
            from = fileContent.IndexOf("name=") + 5;
            to = fileContent.IndexOf("provinces=") - from;
            string stateName = fileContent.Substring(from, to).Replace("\"", "");

            // Parse provinces
            from = fileContent.IndexOf("provinces=") + 11;
            to = fileContent.IndexOf("}") - from;
            var provinceNames = fileContent.Substring(from, to).Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Create State
            var state = new State
            {
                Name = id.ToString(),
                Id = id,
                StateName = stateName,
                provinces = new List<string>(provinceNames)
            };

            // Reparent provinces to the state
            /*var provincesNode = GetNode<ProvinceImporter>("Provinces");
            foreach (var provinceName in provinceNames)
            {
                if (provincesNode.colorToProvince.TryGetValue(provinceName, out var province))
                {
                    provincesNode.RemoveChild(province);
                    state.AddChild(province);
                }
                else
                {
                    GD.PrintErr($"Province {provinceName} not found in colorToProvince dictionary.");
                }
            }*/

            AddChild(state);
            EmitSignal(SignalName.ReparentProvinces, state);

            fileName = stateFolder.GetNext();
        }
        stateFolder.ListDirEnd();
        GD.Print("Finished generating states!");
    }

    private void AssignOwners()
    {
        GetNode<State>("925").SetStateOwner("FRA");
        GetNode<State>("925").SetStateController("FRA");
        GetNode<State>("931").SetStateOwner("FRA");
        GetNode<State>("931").SetStateController("FRA");
        GetNode<State>("936").SetStateOwner("FRA");
        GetNode<State>("936").SetStateController("FRA");
        GetNode<State>("470").SetStateController("FRA");
        GetNode<State>("470").SetStateOwner("FRA");
        GetNode<State>("926").SetStateOwner("FRA");
        GetNode<State>("926").SetStateController("FRA");
        GetNode<State>("386").SetStateController("FRA");
        GetNode<State>("386").SetStateOwner("FRA");
        GetNode<State>("322").SetStateOwner("DEU");
        GetNode<State>("322").SetStateController("DEU");
        GetNode<State>("261").SetStateOwner("GBR");
        GetNode<State>("261").SetStateController("GBR");
        GetNode<State>("318").SetStateOwner("GBR");
        GetNode<State>("318").SetStateController("GBR");
        GetNode<State>("475").SetStateController("PRT");
        GetNode<State>("475").SetStateOwner("PRT");
        GetNode<State>("483").SetStateOwner("ITA");
        GetNode<State>("483").SetStateController("ITA");
        GetNode<State>("500").SetStateController("ITA");
        GetNode<State>("429").SetStateController("ITA");
        GetNode<State>("500").SetStateOwner("ITA");
        GetNode<State>("429").SetStateOwner("ITA");
        GetNode<State>("387").SetStateController("CZE");
        GetNode<State>("387").SetStateOwner("CZE");
        GetNode<State>("404").SetStateController("CZE");
        GetNode<State>("404").SetStateOwner("CZE");
        GetNode<State>("325").SetStateController("POL");
        GetNode<State>("409").SetStateController("AUT");
        GetNode<State>("409").SetStateOwner("AUT");
        GetNode<State>("452").SetStateController("AUT");
        GetNode<State>("452").SetStateOwner("AUT");
        GetNode<State>("133").SetStateController("NOR");
        GetNode<State>("64").SetStateController("NOR");
        GetNode<State>("416").SetStateController("HUN");
        GetNode<State>("325").SetStateOwner("POL");
        GetNode<State>("133").SetStateOwner("NOR");
        GetNode<State>("64").SetStateOwner("NOR");
        GetNode<State>("879").SetStateOwner("SWE");
        GetNode<State>("879").SetStateController("SWE");
        GetNode<State>("117").SetStateOwner("SWE");
        GetNode<State>("117").SetStateController("SWE");
        GetNode<State>("416").SetStateOwner("HUN");
        GetNode<State>("465").SetStateOwner("ESP");
        GetNode<State>("963").SetStateOwner("ESP");
        GetNode<State>("465").SetStateController("ESP");
        GetNode<State>("963").SetStateController("ESP");
        GetNode<State>("960").SetStateOwner("ESP");
        GetNode<State>("946").SetStateOwner("ESP");
        GetNode<State>("960").SetStateController("ESP");
        GetNode<State>("946").SetStateController("ESP");
        GetNode<State>("464").SetStateOwner("BGR");
        GetNode<State>("464").SetStateController("BGR");
        GetNode<State>("421").SetStateOwner("ROM");
        GetNode<State>("421").SetStateController("ROM");
        GetNode<State>("499").SetStateOwner("GRC");
        GetNode<State>("499").SetStateController("GRC");
        GetNode<State>("482").SetStateOwner("GRC");
        GetNode<State>("482").SetStateController("GRC");
        GetNode<State>("321").SetStateController("DNK");
        GetNode<State>("304").SetStateOwner("DNK");
        GetNode<State>("286").SetStateController("DNK");
        GetNode<State>("321").SetStateOwner("DNK");
        GetNode<State>("304").SetStateController("DNK");
        GetNode<State>("286").SetStateOwner("DNK");
        GetNode<State>("310").SetStateController("DNK");
        GetNode<State>("310").SetStateOwner("DNK");
        GetNode<State>("425").SetStateController("CHE");
        GetNode<State>("425").SetStateOwner("CHE");
        GetNode<State>("379").SetStateController("BEN");
        GetNode<State>("379").SetStateOwner("BEN");
        GetNode<State>("353").SetStateController("BEN");
        GetNode<State>("353").SetStateOwner("BEN");
        GetNode<State>("436").SetStateController("SER");
        GetNode<State>("436").SetStateOwner("SER");
        GetNode<State>("473").SetStateController("SER");
        GetNode<State>("473").SetStateOwner("SER");
        GetNode<State>("474").SetStateController("SER");
        GetNode<State>("474").SetStateOwner("SER");
        GetNode<State>("439").SetStateController("SER");
        GetNode<State>("439").SetStateOwner("SER");
        GetNode<State>("467").SetStateController("SER");
        GetNode<State>("467").SetStateOwner("SER");
        GetNode<State>("459").SetStateController("SER");
        GetNode<State>("459").SetStateOwner("SER");
        GetNode<State>("331").SetStateController("IRL");
        GetNode<State>("331").SetStateOwner("IRL");
    }
}
