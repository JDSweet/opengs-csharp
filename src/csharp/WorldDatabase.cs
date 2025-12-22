using Godot;
using System;

using OGS.Map;
using System.Collections.Generic;

public partial class WorldDatabase : Node3D
{
    [Export]
    public ProvinceImporter provinceImporter;

    [Export]
	public StateImporter stateImporter;

	[Export]
    public CountryImporter countryImporter;

	[Export]
	public Main main;

	[Export]
    public Map map;

	private List<Province> landProvinces;
    private Dictionary<string, List<Province>> provincesByOwnerTag;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        this.provinceImporter = (ProvinceImporter)FindChild("Provinces", true);
        this.countryImporter = (CountryImporter)FindChild("Countries", true);
        this.stateImporter = (StateImporter)FindChild("States", true);
        this.main = (Main)FindChild("Main", true);
        this.map = (Map)FindChild("Map", true);

        landProvinces = new List<Province>();
        provincesByOwnerTag = new Dictionary<string, List<Province>>();

        foreach (var colorAndProv in provinceImporter.colorToProvince)
        {
            var prov = colorAndProv.Value;
            if (colorAndProv.Value.Type == "land")
            {
                landProvinces.Add(prov);
                var owner = prov.ProvinceOwner;
                var ownerTag = owner.Tag;
                if (ownerTag != null && !provincesByOwnerTag.ContainsKey(ownerTag))
                    provincesByOwnerTag[ownerTag] = new List<Province>();
                if (ownerTag != null)
                    provincesByOwnerTag[ownerTag].Add(prov);
            }
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
