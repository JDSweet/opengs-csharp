using Godot;
using System;

using OGS.Map;
namespace OGS.Map;

/*
 extends CanvasLayer
@onready var province_id = $PanelContainer/GridContainer/LabelProvinceID
@onready var province_color = $PanelContainer/GridContainer/ColorPickerProvinceColor
@onready var province_type = $PanelContainer/GridContainer/LabelProvinceType
@onready var province_owner = $PanelContainer/GridContainer/LabelOwner
@onready var province_controller = $PanelContainer/GridContainer/LabelController
@onready var province_state = $PanelContainer/GridContainer/LabelState
@onready var province_position = $PanelContainer/GridContainer/LabelPosition

var is_setting_province_position: bool = false

signal save_provinces

func update_labels(province:Province):
	province_id.text  = str(province.id)
	province_color.color = province.color
	province_type.text = province.type
	province_position.text = str(province.position)
	if province_type.text == "land":
		province_owner.text = province.province_owner.country_name
		province_controller.text = province.province_controller.country_name
		province_state.text = str(province.get_parent().id)
	else:
		province_owner.text = ""
		province_controller.text = ""
		province_state.text =""


func _on_button_set_position_button_up() -> void:
	is_setting_province_position = true
	
func set_position(province: Province, coordinates):
	if is_setting_province_position:
		province.position = Vector2(coordinates.x*10, coordinates.y*10)
		is_setting_province_position = false
		update_labels(province)


func _on_button_save_button_up() -> void:
	save_provinces.emit()

 */

public partial class ProvinceSelected : CanvasLayer
{
    private Label provinceId;
    private ColorPickerButton provinceColor;
    private Label provinceType;
    private Label provinceOwner;
    private Label provinceController;
    private Label provinceState;
    private Label provincePosition;

    private bool isSettingProvincePosition = false;

    [Signal]
    public delegate void SaveProvincesEventHandler();

    public override void _Ready()
    {
        provinceId = GetNode<Label>("PanelContainer/GridContainer/LabelProvinceID");
        provinceColor = GetNode<ColorPickerButton>("PanelContainer/GridContainer/ColorPickerProvinceColor");
        provinceType = GetNode<Label>("PanelContainer/GridContainer/LabelProvinceType");
        provinceOwner = GetNode<Label>("PanelContainer/GridContainer/LabelOwner");
        provinceController = GetNode<Label>("PanelContainer/GridContainer/LabelController");
        provinceState = GetNode<Label>("PanelContainer/GridContainer/LabelState");
        provincePosition = GetNode<Label>("PanelContainer/GridContainer/LabelPosition");
    }

    public void UpdateLabels(Province province)
    {
        provinceId.Text = province.Id.ToString();
        provinceColor.Color = province.Color;
        provinceType.Text = province.Type;
        provincePosition.Text = province.Position.ToString();

        if (provinceType.Text == "land")
        {
            provinceOwner.Text = province.ProvinceOwner.CountryName;
            provinceController.Text = province.ProvinceController.CountryName;
            provinceState.Text = province.GetParent().Get("Id").ToString();
        }
        else
        {
            provinceOwner.Text = "";
            provinceController.Text = "";
            provinceState.Text = "";
        }
    }

    public void OnButtonSetPositionButtonUp()
    {
        isSettingProvincePosition = true;
    }

    public void SetPosition(Province province, Vector2 coordinates)
    {
        if (isSettingProvincePosition)
        {
            province.Position = new Vector2(coordinates.X * 10, coordinates.Y * 10);
            isSettingProvincePosition = false;
            UpdateLabels(province);
        }
    }

    public void OnButtonSaveButtonUp()
    {
        EmitSignal(SignalName.SaveProvinces);
    }
}
