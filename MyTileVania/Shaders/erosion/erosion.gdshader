shader_type canvas_item; // Tipo de canvas para 2D

uniform sampler2D dissolve_texture : source_color; // Textura de ruido de entrada
uniform float dissolve_value : hint_range(0,1);    // Valores en el tiempo
uniform vec2 size = vec2(32.0, 32.0); // Tamaño del objeto
uniform vec4 dissolve_color : source_color; // Color del borde

varying vec2 ObjectUV; // La palabra reservada Varying es para que se pase de vertex a fragment.

void vertex() {
	ObjectUV = VERTEX/size; // Calcular el UV pero relativo al tamaño indicado del objeto.
}

void fragment(){    // Esto se ejecuta por cada pixel
    vec4 main_texture = texture(TEXTURE, UV); // Mi textura. UV es la posicion en el sprite.
    float noise_texture = texture(dissolve_texture, ObjectUV).x; // Ruido .x es igual que .r

	float texture_filter = step(dissolve_value, noise_texture); // Filtro a disolver
	float outline_dissolve = step(dissolve_value-0.15, noise_texture); // Igual pero un poco más tarde
	float outlier = outline_dissolve - texture_filter; // solo el borde
	
	vec4 coloredOutlier = vec4(outlier) * dissolve_color; // Borde coloreado
	COLOR *= vec4(1.0-outlier); // Eliminamos de la imagen el borde
	COLOR += coloredOutlier; // Añadimos ahora el borde coloreado
    COLOR.a = main_texture.a * outline_dissolve; // Aplicamos el alfa original junto con el dissolve*/
}
