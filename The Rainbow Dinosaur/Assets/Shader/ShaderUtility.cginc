

float rand(float x ,float y){
	//return 1.0;
	//return random(float2(x,y));
	return frac(sin(dot(float2(x,y) ,float2(82.9898,78.233))) * 943758.5453);
}
float rand(float x){
	return rand(x,0);
	}
float rand(float2 uv){
	return rand(uv.x,uv.y);
	}
float smooth (float t) {
		return t * t * t * (t * (t * 6.0 - 15.0) + 10.0);
	}