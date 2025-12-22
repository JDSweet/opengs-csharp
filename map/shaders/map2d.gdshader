shader_type canvas_item;

uniform sampler2D lookup_image: source_color, filter_nearest;
uniform sampler2D color_map_image: source_color, filter_nearest;

void fragment() {
	// Variables
	float normalized_offset = 100.0 / float(textureSize(color_map_image,0).y);
	float checkers_width = 10.0;
	float texture_width = float(textureSize(lookup_image,0).x);
	float texture_height = float(textureSize(lookup_image,0).y);
	int checkers_pattern = int(mod(floor(UV.x * texture_width / checkers_width) + floor(UV.y * texture_height / checkers_width), 2.0));

	// Textures

	vec3 lookup_texture = texture(lookup_image, UV).rgb;
	vec4 map_mode_primary = texture(color_map_image, vec2(lookup_texture.r,lookup_texture.g));
	vec4 map_mode_secondary = texture(color_map_image, vec2(lookup_texture.r,lookup_texture.g + normalized_offset));
	vec4 selected_color = texture(color_map_image, vec2(lookup_texture.r,lookup_texture.g + 2.0*normalized_offset));
	
	// Set checkers
	vec4 final_color = mix(map_mode_primary,map_mode_secondary,float(checkers_pattern));

	// Show selected provinces
	final_color = mix(final_color, selected_color, step(0.5, float(selected_color.rgb != vec3(0.0))));
	
	COLOR = final_color;
}
