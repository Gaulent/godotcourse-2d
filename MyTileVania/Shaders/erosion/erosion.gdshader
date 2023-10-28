shader_type canvas_item; // Tipo de canvas para 2D

uniform sampler2D dissolve_texture : source_color; // Textura de ruido de entrada
uniform float dissolve_value : hint_range(0,1);    // Valores en el tiempo
uniform int sheet_columns = 1;
uniform int sheet_rows = 1;


vec2 getNewUV(sampler2D currentTexture, vec2 current_uv){
	vec2 sprite_sheet_size = vec2(textureSize(currentTexture,0));
	vec2 frame_size;
	frame_size.x = sprite_sheet_size.x/float(sheet_columns);
	frame_size.y = sprite_sheet_size.y/float(sheet_rows);
	vec2 current_point = sprite_sheet_size * current_uv;
	float row = floor(current_point.y / frame_size.y);
	float column = floor(current_point.x / frame_size.x);
	vec2 max_point = (frame_size * vec2(column, row)) + frame_size;
	vec2 new_uv = 1.0 - (max_point - current_point) / frame_size;
	
	return new_uv;
}


void fragment(){    // Esto se ejecuta por cada pixel
    vec4 main_texture = texture(TEXTURE, UV);           // Mi textura. UV es la posicion en el sprite.

	vec2 new_uv=getNewUV(TEXTURE,UV);

    float noise_texture = texture(dissolve_texture, new_uv).x; // Ruido .x es igual que .r

	if(noise_texture<dissolve_value)
		noise_texture = 0.0;
	else
		noise_texture = 1.0;
	
    COLOR.a = main_texture.a * noise_texture;
}
