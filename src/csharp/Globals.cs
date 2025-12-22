using Godot;

using System;
using System.Collections.Generic;

using OGS.Map;
namespace OGS.Map;

/*
 extends Node
	@onready var tag_to_country : Dictionary
 */

public partial class Globals : Node
{
    public static Dictionary<string, Country> TagToCountry { get; } = new();

    public override void _Ready()
    {

    }

    public override void _Process(double delta)
    {
    }
}
