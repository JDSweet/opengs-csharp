using Godot;

using System;
using System.Collections.Generic;

using OGS.Map;
using System.Linq;
namespace OGS.Map;

/*
 extends Node2D

var intercept: float
var slope: float
var angle: float

func initial_data(country: Country) -> void:
	name = country.tag
	$Label.text = country.country_name
	$Label.modulate.a = 0.9
	country.map_label = self

func update_data(country: Country) -> void:
	var owned_cites = country.owned_provinces.filter(func(p): return p.position != Vector2(0,0))
	
	if owned_cites.is_empty() or country.tag == "NNN":
		$Label.hide()
		return
	else:
		$Label.show()
	
	calculate_linear_regression(owned_cites)
	var city_min_x: float = min_x(owned_cites)
	var city_max_x: float = max_x(owned_cites)
	var point_start = Vector2(city_min_x, intercept + (slope * city_min_x))
	var point_end = Vector2(city_max_x, intercept + (slope * city_max_x))
	$Line2D.points = [point_start, point_end]
	
	angle = $Line2D.points[0].angle_to_point($Line2D.points[1])
	angle *= 180/3.14
	if angle > 90:
		angle -= 180
	if angle < -90:
		angle += 180
		
	var center: Vector2 = ($Line2D.points[0]+$Line2D.points[1])/2
	$Label.position = center
	
	$Label.size.y = 1
	var distance = $Line2D.points[0].distance_to($Line2D.points[1])
	var ratio = $Label.size.x / $Label.size.y
	
	var font_size = max(10, ((distance/1.25) / (2 + ratio/1.15)))
	$Label.add_theme_font_size_override("font_size", font_size)
	$Label.size = Vector2(1,1)
	$Label.pivot_offset.x = $Label.get_minimum_size().x / 2.0
	$Label.pivot_offset.y = $Label.get_minimum_size().y / 2.0
	$Label.position.x -= $Label.get_minimum_size().x / 2.0
	$Label.position.y -= $Label.get_minimum_size().y / 2.0
	
	$Label.rotation_degrees = angle
	
func calculate_linear_regression(points: Array):
	var n = points.size()
	var sum_x = 0.0
	var sum_y = 0.0
	var sum_xy = 0.0
	var sum_x_squared = 0.0
	
	for point in points:
		var x = point.position.x
		var y = point.position.y
		sum_x += x
		sum_y += y
		sum_xy += x * y
		sum_x_squared += x * x
		
	slope = (n * sum_xy - sum_x * sum_y) / (n * sum_x_squared - sum_x * sum_x)
	intercept = (sum_y - slope * sum_x) / n

func min_x(cities: Array) -> float:
	var x = 0.0
	for city:Province in cities:
		if city.position.x > x:
			x = city.position.x
	return x

func max_x(cities: Array) -> float:
	var x = 100000.0
	for city:Province in cities:
		if city.position.x < x:
			x = city.position.x
	return x

 */

public partial class CountryLabelTemplate : Node2D
{
    private float intercept;
    private float slope;
    private float angle;

    [Export]
    public Label label;

    [Export]
    public Line2D line2D;

    public override void _Ready()
    {
        label = GetNode<Label>("Label");
        line2D = GetNode<Line2D>("Line2D");

        if(label == null)
            GD.PrintErr("CountryLabelTemplate._Ready: label is null");
        //label = GetChild<Label>(1);
        //line2D = GetChild<Line2D>(0);
    }

    public void InitialData(Country country)
    {
        if(country == null)
            GD.PrintErr("CountryLabelTemplate.InitialData: country is null");
        if(label == null)
        {
            GD.PrintErr("CountryLabelTemplate.InitialData: label is null");
            return;
        }
        GD.Print("Initializing label for country: " + country.CountryName);
        GD.Print("Country tag: " + country.Tag);
        Name = country.Tag;
        label.Text = country.CountryName;
        label.Modulate = new Color(label.Modulate.R, label.Modulate.G, label.Modulate.B, 0.9f);
        country.MapLabel = this;
    }

    public void UpdateData(Country country)
    {
        var ownedCities = country.OwnedProvinces
            .Where(p => p.Position != Vector2.Zero)
            .ToList();

        if (ownedCities.Count == 0 || country.Tag == "NNN")
        {
            label.Hide();
            return;
        }
        else
        {
            label.Show();
        }

        CalculateLinearRegression(ownedCities);

        float cityMinX = MinX(ownedCities);
        float cityMaxX = MaxX(ownedCities);

        Vector2 pointStart = new Vector2(cityMinX, intercept + (slope * cityMinX));
        Vector2 pointEnd = new Vector2(cityMaxX, intercept + (slope * cityMaxX));
        line2D.Points = new Vector2[] { pointStart, pointEnd };

        angle = pointStart.AngleToPoint(pointEnd);
        angle *= 180 / 3.14f;
        if (angle > 90)
            angle -= 180;
        if (angle < -90)
            angle += 180;

        Vector2 center = (pointStart + pointEnd) / 2;
        label.Position = center;

        label.Size = new Vector2(label.Size.X, 1);
        float distance = pointStart.DistanceTo(pointEnd);
        float ratio = label.Size.X / label.Size.Y;

        float fontSize = MathF.Max(10, ((distance / 1.25f) / (2 + ratio / 1.15f)));
        label.AddThemeFontSizeOverride("font_size", (int)fontSize);
        label.Size = new Vector2(1, 1);

        Vector2 minSize = label.GetMinimumSize();
        label.PivotOffset = new Vector2(minSize.X / 2.0f, minSize.Y / 2.0f);
        label.Position -= new Vector2(minSize.X / 2.0f, minSize.Y / 2.0f);

        label.RotationDegrees = angle;
    }

    private void CalculateLinearRegression(List<Province> points)
    {
        int n = points.Count;
        float sumX = 0.0f;
        float sumY = 0.0f;
        float sumXY = 0.0f;
        float sumXSquared = 0.0f;

        foreach (var point in points)
        {
            float x = point.Position.X;
            float y = point.Position.Y;
            sumX += x;
            sumY += y;
            sumXY += x * y;
            sumXSquared += x * x;
        }

        slope = (n * sumXY - sumX * sumY) / (n * sumXSquared - sumX * sumX);
        intercept = (sumY - slope * sumX) / n;
    }

    private float MinX(List<Province> cities)
    {
        float x = float.MaxValue;
        foreach (var city in cities)
        {
            if (city.Position.X < x)
                x = city.Position.X;
        }
        return x;
    }

    private float MaxX(List<Province> cities)
    {
        float x = float.MinValue;
        foreach (var city in cities)
        {
            if (city.Position.X > x)
                x = city.Position.X;
        }
        return x;
    }
}
