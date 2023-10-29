shader_type canvas_item; // Tipo de canvas para 2D

uniform sampler2D dissolve_texture : source_color; // Textura de ruido de entrada
uniform float dissolve_value : hint_range(0,1);    // Valores en el tiempo
uniform vec2 center = vec2(16.0, 16.0); // Intentar pasar estos valores por codigo
uniform vec2 size = vec2(32.0, 32.0);

varying vec2 ObjectUV; // La palabra reservada Varying es para que se pase de vertex a fragment

void vertex() {
	ObjectUV.x = (VERTEX.x+center.x)/size.x;
	ObjectUV.y = (VERTEX.y+center.y)/size.y;
}

void fragment(){    // Esto se ejecuta por cada pixel
    vec4 main_texture = texture(TEXTURE, UV);           // Mi textura. UV es la posicion en el sprite.
    float noise_texture = texture(dissolve_texture, ObjectUV).x; // Ruido .x es igual que .r

	noise_texture = step(dissolve_value, noise_texture);
	
    COLOR.a = main_texture.a * noise_texture;
}
