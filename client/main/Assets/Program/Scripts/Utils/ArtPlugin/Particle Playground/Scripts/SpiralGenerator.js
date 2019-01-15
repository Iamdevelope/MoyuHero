// 	Copyright 2012 Unluck Software, Egil Andre Larsen 
//	http://www.chemicalbliss.com
//
//  For a smooth spiral, keep depht/frequency/speed the same as height/frequency/speed
//  Performance notes:
//		Try to keep all unused variables to 0
//		Use rotate instead of Sine Speeds if possible
//		runOnceThenDisable turn off the spiral generation, huge performance gain (rotation and material animation still works)
//	Creating a new spiral:
//		Use an existing prefab
//		Press play
//		Change the variables while in play mode
//		When the spiral looks like it is suppose to, drag it from Hierarchy to Project folder
//		Drag the prefab from Project to Hierarcy
//		Remove lineRenderer component from "Beam" child (optional but recommended)
//		Assign start and end targets
//		Delete the old spiral from Hierarcy if needed
//	Creating a multi spiral:
//		Create or use a existing spiral prefab
//		Use a premade multiSpiral prefab or attach the multiSpiral script to a empty gameObject
//		Set how many spirals the multiSpiral should spawn in the "spiralWithRotationArray" 
//		Assign the spirals to each index of the array (if using the same spiral for several, set lenght to 1 first and assign the spiral then set to desired lenght)
//		
//		
var colorStart: Color = Color.yellow;			// Start color
var colorEnd: Color = Color.red;				// End color
var targetStart: Transform;						// Line start target transform
var targetEnd: Transform;						// Line end target transform
var noise: float = 0;							// Line renderer perlin noise
var noiseScale:float = 1;						// Noise frequency
var noiseSpeed:float = 1;						// Noise flow speed (Time.time*noiseSpeed)
var LenghtOfLineRenderer : float = 100;			// Sets base vertex count (more = more cpu)
var smooth:boolean = true;						// Increases vertex count based on "lenghtResolutionMultiplier" 
private var lenghtResolutionMultiplier:float =.05;// Increases vertex count based on the lines lenght (.05 = default/max needed, this can be set lower for better performance while "smooth" is active)
var lineMaterial: Material;						// Material used for the line renderer
var offsetMaterialSpeed: float = 0;				// Material x offset over time
var offsetYMaterialSpeed: float = 0;			// Material y offset over time
var stretchMaterial:float = 10;					// Sets material x tiling
var stretchYMaterial:float = 1;					// Sets material y tiling
var widthStart : float = 0.1;					// line renderer start width
var widthEnd : float = 0.2;						// line renderer end width
var rotateSpeed:float = 0;						// Rotates the line, using this and runOnceThenDisable will imporeve performance +++
var rotationOffset:float;						// Rotates the beam at start
var sineDepth:float = -.02;						// Depht of the slopes (Mathf.Cos)
var sineDepthFrequency:float = 5.8;				// How many slopes the line has sideways
var sineHeight:float = -.05;					// Height of the slopes (Mathf.Sin)
var sineHeightFrequency:float = 5;				// How many slopes the line has up and down
var sineLenght:float = 0;						// Ripple height 
var sineLenghtFrequency:float = 0;				// Ripple height frequency 
var sineGrowMultiplier:float = 250;				// Gradually grows the spiral/wave bigger, if used with sine this can be used for a bulge effect in the middle of the spiral/wave
var growCurve:AnimationCurve = AnimationCurve(Keyframe(0, 0), Keyframe(.5, .5), Keyframe(1, 0));					// How the spiral grows over time
var sineLenghtSpeed:float = 0;					// Speeds the ripples moves, can also be used for warp effect on big ripples
var sineHeightSpeed:float = -5;					// Speed the spiral moves towards or away from target		
var sineDepthSpeed:float = -5;					// Sideways speed
var runOnceThenDisable:boolean;					// Stops generating spiral shape after completed drawing once, rotation is still enabled (use this for simple rotating spirals that do not change in size)
var start:Transform;							// Stores the Start transform that is positioned at the Start Target (good place to put particle systems and similar | no warning if missing)
var end:Transform;								// Stores the End transform that is positioned at the End Target
var vertexCount:float;							// For monitoring vertex count (this is not a editable value)
var lineRenderer:LineRenderer;					// LineRenderer attached to Beam gameObject



private var runSpiral:boolean = true;
var lineRendererMat:Material;			

function Start () {
	CreateSpiral();
}

function CreateSpiral () {
	if(!transform.FindChild("Beam").gameObject.GetComponent(LineRenderer))
	lineRenderer = transform.FindChild("Beam").gameObject.AddComponent(LineRenderer);
	else 
	lineRenderer = transform.FindChild("Beam").gameObject.GetComponent(LineRenderer);
	transform.FindChild("Beam").transform.Rotate(0,0,rotationOffset);
	lineRenderer.SetVertexCount (LenghtOfLineRenderer);
	lineRenderer.useWorldSpace = false;				
	lineRenderer.material = lineMaterial;
	
	
	if(!end)
	end = transform.FindChild("End");			// Sets start and end for positioning
	if(!start)
	start = transform.FindChild("Start");
	
	if(targetEnd)
	end.position = targetEnd.position;
	if(targetStart)
	start.position = targetStart.position;
	
	lineRendererMat = lineRenderer.sharedMaterial;
}

function Update () {
	if(runSpiral){
			GenerateSpiral(Time.time);
	}
	PositionSpiral();
}

function PositionSpiral () {
	if(stretchMaterial != 0)
		lineRendererMat.mainTextureScale.x = stretchMaterial;
	if(stretchYMaterial != 0)
		lineRendererMat.mainTextureScale.y = stretchYMaterial;
	if(offsetMaterialSpeed != 0 )
		lineRendererMat.mainTextureOffset.x = Time.time*offsetMaterialSpeed;
	if(offsetYMaterialSpeed != 0 )
		lineRendererMat.mainTextureOffset.y = Time.time*offsetYMaterialSpeed;
	if(targetStart) transform.position = start.position;
	transform.LookAt(end, transform.up);
	transform.Rotate(Vector3.forward*Time.deltaTime*rotateSpeed);
}

function GenerateSpiral (time:float) {
		if(targetEnd && end.position!=targetEnd) end.position = targetEnd.position;
		if(targetStart && start.position!=targetStart) start.position = targetStart.position;
		lineRenderer.SetWidth ( widthStart, widthEnd);
		lineRenderer.SetColors (colorStart, colorEnd);
		var distance:float = Vector3.Distance(start.position,end.position);	
		if(smooth)
			vertexCount = Mathf.FloorToInt(LenghtOfLineRenderer*distance*lenghtResolutionMultiplier);
		else
			vertexCount = Mathf.FloorToInt(LenghtOfLineRenderer);
		if(vertexCount<15)
			vertexCount =15;
		lineRenderer.SetVertexCount (vertexCount);
		var c:float = 1f/vertexCount;
		var n:float = vertexCount/1000;	//fix for noiseScale
		
		for(var i :int = 0; i < vertexCount; i++){
			var pos : float = distance*c*i;			
			var grow:float = growCurve.Evaluate (i/vertexCount)*sineGrowMultiplier;
			var rr:float = 1;
			if(noise != 0)
				rr =Mathf.PerlinNoise((i*1.0)/distance*noiseScale*n,time*noiseSpeed)*noise+1;		
			if(sineHeightFrequency!=0)
				var ry:float = Mathf.Sin(sineHeightFrequency*pos+(time*sineHeightSpeed))*sineHeight*grow*rr;
			if(sineDepthFrequency!=0)
				var rx:float = Mathf.Cos(sineDepthFrequency*pos+(time*sineDepthSpeed))*sineDepth*grow*rr;
			if(sineLenghtFrequency!=0)
				var rz:float = Mathf.Sin(sineLenghtFrequency*pos+(time*sineLenghtSpeed))*sineLenght*grow*rr;
			lineRenderer.SetPosition(i, Vector3(rx, ry, rz + pos));		
		}	
		if(runOnceThenDisable)
			runSpiral = false;
}
