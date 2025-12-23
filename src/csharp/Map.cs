using Godot;
using System;
using System.Collections.Generic;

using OGS.Map;
namespace OGS.Map;

/*
 extends StaticBody3D

@onready var province_color_to_lookup : Dictionary
@onready var map_material_2d = load("res://map/shaders/map2D.tres")
@onready var color_map_political:Image = Image.create(256,256,false,Image.FORMAT_RGB8)
@onready var color_map_ideology:Image = Image.create(256,256,false,Image.FORMAT_RGB8)

var current_map_mode:Image
var color_map_texture:ImageTexture

var previously_selected_provinces :PackedColorArray

enum MapMode {POLITICAL, IDEOLOGY}

func _ready() -> void:
	create_lookup_texture()
	create_color_map()
	set_map_mode_political()
	create_country_labels()
	
func create_lookup_texture() -> void:
	var province_image : Image = get_parent().province_map.get_image()
	var lookup_image: Image = province_image.duplicate()
	var color_map_r : int = 0
	var color_map_g : int = 0
	
	for x in range(lookup_image.get_width()):
		for y in range(lookup_image.get_height()):
			var province_color : Color = province_image.get_pixel(x,y)
			if not province_color_to_lookup.has(province_color):
				province_color_to_lookup[province_color] = Color(color_map_r/255.0, color_map_g/255.0, 0.0)
				color_map_r += 1
				if color_map_r == 256:
					color_map_r = 0
					color_map_g += 1
			lookup_image.set_pixel(x,y,province_color_to_lookup[province_color])
	var lookup_texture = ImageTexture.create_from_image(lookup_image)
	map_material_2d.set_shader_parameter("lookup_image", lookup_texture)
	
func create_color_map() -> void:
	for province_color :Color in province_color_to_lookup:
		var lookup = province_color_to_lookup[province_color]
		var x = lookup.r * 255
		var y = lookup.g * 255
		var province:Province = get_parent().get_node("Provinces").color_to_province.get(province_color)
		if province.type == "land":
			var owner_color :Color = province.province_owner.color
			var controller_color :Color = province.province_controller.color
			color_map_political.set_pixel(x,y,owner_color)
			color_map_political.set_pixel(x,y+100,controller_color)
			var owner_ideology_color :Color = province.province_owner.ideology_color
			var controller_ideology_color :Color = province.province_controller.ideology_color
			color_map_ideology.set_pixel(x,y,owner_ideology_color)
			color_map_ideology.set_pixel(x,y+100,controller_ideology_color)

func update_color_map(input_color:Color, output_color:Color, offset:int) -> void:
	var lookup = province_color_to_lookup.get(input_color,null)
	if lookup:
		var x = lookup.r * 255
		var y = lookup.g * 255
		color_map_political.set_pixel(x,y+offset,output_color)
		color_map_ideology.set_pixel(x,y+offset,output_color)
		current_map_mode.set_pixel(x,y+offset,output_color)
	
	
func update_map_shader() -> void:
	color_map_texture = ImageTexture.create_from_image(current_map_mode)
	map_material_2d.set_shader_parameter("color_map_image",color_map_texture)

func set_map_mode_political() -> void:
	current_map_mode = color_map_political
	update_map_shader()
	
func set_map_mode_ideology() -> void:
	current_map_mode = color_map_ideology
	update_map_shader()
	
func highlight_province(selected_province) -> void:
	deselect_provinces()
	if selected_province.type == "land":
		for province in selected_province.get_parent().get_children():
			update_color_map(province.color, Color("WHITE"), 200)
			previously_selected_provinces.append(province.color)
			
	update_color_map(selected_province.color, Color("Green"), 200)
	update_map_shader()
	previously_selected_provinces.append(selected_province.color)
	
func deselect_provinces() -> void:
	for color in previously_selected_provinces:
		update_color_map(color, Color("BLACK"), 200)
	previously_selected_provinces.clear()


func _on_map_modes_map_mode_selected(mode: Variant) -> void:
	match mode:
		MapMode.POLITICAL:
			set_map_mode_political()
		MapMode.IDEOLOGY:
			set_map_mode_ideology()

func create_country_labels() -> void:
	var country_label_template: PackedScene = load("res://scenes/country_label_template.tscn")
	for country: Country in Globals.tag_to_country.values():
		var country_label = country_label_template.instantiate()
		country_label.initial_data(country)
		$MeshInstance3D/SubViewport2/CountryLabels.add_child(country_label)
		country_label.update_data(country)

 */

public partial class Map : StaticBody3D
{
    private Dictionary<Color, Color> provinceColorToLookup = new();
    private ShaderMaterial mapMaterial2D;
    private Image colorMapPolitical;
    private Image colorMapIdeology;

    private Image currentMapMode;
    private ImageTexture colorMapTexture;

    private List<Color> previouslySelectedProvinces = new();

    private Color oceanColor = new Color(0f, 0f, 100f / 255f);

    private MeshInstance3D mainMapMeshInstance;

    public static float MapWidth = 1f;
    public static float MapHeight = 0.5f;

    private enum MapMode { POLITICAL, IDEOLOGY }

    public override void _Ready()
    {
        mapMaterial2D = GD.Load<ShaderMaterial>("res://map/shaders/map2D.tres");
        colorMapPolitical = Image.CreateEmpty(256, 256, false, Image.Format.Rgb8);
        colorMapIdeology = Image.CreateEmpty(256, 256, false, Image.Format.Rgb8);

        this.mainMapMeshInstance = (MeshInstance3D)FindChild("MeshInstance3D");
        if(mainMapMeshInstance.Mesh is PlaneMesh planeMesh)
        {
            MapWidth = planeMesh.Size.X;
            MapHeight = planeMesh.Size.Y;
            GD.Print(MapWidth);
            GD.Print(MapHeight);
        }

        CreateLookupTexture();
        CreateColorMap();
        SetMapModePolitical();
        CreateCountryLabels();
    }

    private void CreateLookupTexture()
    {
        var provinceImage = ((Main)GetParent()).ProvinceMap;
        
        if (provinceImage == null)
            return;
        var provinceImageData = provinceImage.GetImage();
        var lookupImage = provinceImageData.Duplicate() as Image;

        int colorMapR = 0;
        int colorMapG = 0;

        for (int x = 0; x < lookupImage.GetWidth(); x++)
        {
            for (int y = 0; y < lookupImage.GetHeight(); y++)
            {
                var provinceColor = provinceImageData.GetPixel(x, y);
                if (!provinceColorToLookup.ContainsKey(provinceColor))
                {
                    provinceColorToLookup[provinceColor] = new Color(colorMapR / 255.0f, colorMapG / 255.0f, 0.0f);
                    colorMapR++;
                    if (colorMapR == 256)
                    {
                        colorMapR = 0;
                        colorMapG++;
                    }
                }
                lookupImage.SetPixel(x, y, provinceColorToLookup[provinceColor]);
            }
        }
        var lookupTexture = ImageTexture.CreateFromImage(lookupImage);
        mapMaterial2D.SetShaderParameter("lookup_image", lookupTexture);
    }

    private void CreateColorMap()
    {
        var provincesNode = GetParent().GetNode<ProvinceImporter>("Provinces");
        if (provincesNode == null)
        {
            GD.PrintErr("Provinces node not found.");
            return;
        }

        foreach (var provinceColor in provinceColorToLookup.Keys)
        {
            var lookup = provinceColorToLookup[provinceColor];
            int x = (int)(lookup.R * 255);
            int y = (int)(lookup.G * 255);

            if (!provincesNode.colorToProvince.TryGetValue(provinceColor, out var province) || province == null)
                continue;

            if (province.Type == "land")
            {
                var ownerColor = province.ProvinceOwner?.Color ?? Colors.Black;
                var controllerColor = province.ProvinceController?.Color ?? Colors.Black;
                var ownerIdeologyColor = province.ProvinceOwner?.IdeologyColor ?? Colors.Black;
                var controllerIdeologyColor = province.ProvinceController?.IdeologyColor ?? Colors.Black;

                colorMapPolitical.SetPixel(x, y, ownerColor);
                colorMapPolitical.SetPixel(x, y + 100, controllerColor);
                colorMapIdeology.SetPixel(x, y, ownerIdeologyColor);
                colorMapIdeology.SetPixel(x, y + 100, controllerIdeologyColor);
            }

            else
            {
                if (province.Type == "sea" || province.Type == "lake")
                {
                    var ownerColor = oceanColor;
                    var controllerColor = oceanColor;
                    var ownerIdeologyColor = oceanColor;
                    var controllerIdeologyColor = oceanColor;

                    colorMapPolitical.SetPixel(x, y, ownerColor);
                    colorMapPolitical.SetPixel(x, y + 100, controllerColor);
                    colorMapIdeology.SetPixel(x, y, ownerIdeologyColor);
                    colorMapIdeology.SetPixel(x, y + 100, controllerIdeologyColor);
                }
            }
        }
    }

    private void UpdateColorMap(Color inputColor, Color outputColor, int offset)
    {
        if (provinceColorToLookup.TryGetValue(inputColor, out var lookup))
        {
            int x = (int)(lookup.R * 255);
            int y = (int)(lookup.G * 255);
            colorMapPolitical.SetPixel(x, y + offset, outputColor);
            colorMapIdeology.SetPixel(x, y + offset, outputColor);
            currentMapMode.SetPixel(x, y + offset, outputColor);
        }
    }

    private void UpdateMapShader()
    {
        colorMapTexture = ImageTexture.CreateFromImage(currentMapMode);
        mapMaterial2D.SetShaderParameter("color_map_image", colorMapTexture);
    }

    private void SetMapModePolitical()
    {
        currentMapMode = colorMapPolitical;
        UpdateMapShader();
    }

    private void SetMapModeIdeology()
    {
        currentMapMode = colorMapIdeology;
        UpdateMapShader();
    }

    public void HighlightProvince(Province selectedProvince)
    {
        DeselectProvinces();
        if (selectedProvince.Type == "land")
        {
            foreach (Province province in selectedProvince.GetParent().GetChildren())
            {
                UpdateColorMap(province.Color, Colors.White, 200);
                previouslySelectedProvinces.Add(province.Color);
            }
        }
        UpdateColorMap(selectedProvince.Color, Colors.Green, 200);
        UpdateMapShader();
        previouslySelectedProvinces.Add(selectedProvince.Color);
    }

    public void DeselectProvinces()
    {
        foreach (var color in previouslySelectedProvinces)
        {
            UpdateColorMap(color, Colors.Black, 200);
        }
        previouslySelectedProvinces.Clear();
    }

    public void OnMapModesMapModeSelected(int mode)
    {
        switch ((MapMode)mode)
        {
            case MapMode.POLITICAL:
                SetMapModePolitical();
                break;
            case MapMode.IDEOLOGY:
                SetMapModeIdeology();
                break;
        }
    }

    private void CreateCountryLabels()
    {
        var countryLabelTemplate = GD.Load<PackedScene>("res://scenes/csharp/country_label_template.tscn");
        //var countryLabelsNode = GetNode<Node>("MeshInstance3D/SubViewport2/CountryLabels");
        var countryLabelsNode = GetNode<Node>("MeshInstance3D/SubViewport2/CountryLabels");
        GD.Print($"Country Count: {Globals.TagToCountry.Count}");
        foreach (var country in Globals.TagToCountry.Values)
        {
            countryLabelsNode._Ready();
            var countryLabel = countryLabelTemplate.Instantiate();
            countryLabel.Name = country.Tag;


            countryLabelsNode.AddChild(countryLabel);
            ((CountryLabelTemplate)countryLabel).InitialData(country);
            //countryLabel.Call("InitialData", country);
            ((CountryLabelTemplate)countryLabel).UpdateData(country);
            //countryLabel.Call("UpdateData", country);
        }
    }
}
