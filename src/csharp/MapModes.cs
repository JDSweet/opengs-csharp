using Godot;
using System;

/*
 extends CanvasLayer
signal map_mode_selected(mode)
enum MapMode {POLITICAL, IDEOLOGY}

func _on_button_political_button_up() -> void:
	map_mode_selected.emit(MapMode.POLITICAL)

func _on_button_ideology_button_up() -> void:
	map_mode_selected.emit(MapMode.IDEOLOGY)

 */

public partial class MapModes : CanvasLayer
{
    public enum MapMode { POLITICAL, IDEOLOGY }

    [Signal]
    public delegate void MapModeSelectedEventHandler(MapMode mode);

    public void OnButtonPoliticalButtonUp()
    {
        EmitSignal(SignalName.MapModeSelected, Variant.From(MapMode.POLITICAL));
    }

    public void OnButtonIdeologyButtonUp()
    {
        EmitSignal(SignalName.MapModeSelected, Variant.From(MapMode.IDEOLOGY));
    }
}
