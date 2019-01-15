
/****************************************
	Animated Text Generator Editor v1.0						
	Copyright 2013 Unluck Software	
 	www.chemicalbliss.com																																				
*****************************************/
@CustomEditor(SpiralGenerator)
@CanEditMultipleObjects
public class SpiralGeneratorEditor extends Editor {
 	var myProperty:SerializedProperty;
 	var counter:float;
 	//var toggleAuto:boolean;
		
    override function OnInspectorGUI () {
    
     	EditorGUIUtility.LookLikeInspector();
     	
     EditorGUILayout.Space();
     if(GUILayout.Button("Preview Spiral")) {
     	target.CreateSpiral();
     	target.GenerateSpiral(Random.value);
     	target.PositionSpiral();
     }
//     toggleAuto = EditorGUILayout.Toggle("Auto Preview", toggleAuto);
     	EditorGUILayout.Space();
     	
     
     EditorGUILayout.LabelField("Start to End Colors", EditorStyles.boldLabel);
     target.colorStart = EditorGUILayout.ColorField(target.colorStart);
     target.colorEnd = EditorGUILayout.ColorField(target.colorEnd);
     EditorGUILayout.Space();
     EditorGUILayout.LabelField("Start to End Positions", EditorStyles.boldLabel); 
	target.targetStart = EditorGUILayout.ObjectField("Start Transform", target.targetStart, Transform, true);
	target.targetEnd = EditorGUILayout.ObjectField("End Transform", target.targetEnd, Transform, true);
	EditorGUILayout.Space();
	EditorGUILayout.LabelField("Spiral Curve", EditorStyles.boldLabel);
	target.growCurve = EditorGUILayout.CurveField(target.growCurve);
	
	EditorGUILayout.Space();
		EditorGUILayout.LabelField("Noise", EditorStyles.boldLabel);
	target.noise =  EditorGUILayout.FloatField("Noise Amount", target.noise);
	if(target.noise != 0){
		target.noiseScale = EditorGUILayout.FloatField("Noise Scale", target.noiseScale);
		target.noiseSpeed = EditorGUILayout.FloatField("Noise Speed", target.noiseSpeed);
	}
	
	EditorGUILayout.Space();
	EditorGUILayout.LabelField("Line Renderer", EditorStyles.boldLabel);
	target.widthStart = EditorGUILayout.FloatField("Line Start Width", target.widthStart);
    target.widthEnd = EditorGUILayout.FloatField("Line End Width", target.widthEnd); 
	target.LenghtOfLineRenderer = EditorGUILayout.FloatField("Vertex Resolution", target.LenghtOfLineRenderer);
	target.smooth = EditorGUILayout.Toggle("Smooth Resolution", target.smooth);
   
    EditorGUILayout.Space();
	EditorGUILayout.LabelField("Material", EditorStyles.boldLabel);
    target.lineMaterial = EditorGUILayout.ObjectField("Material", target.lineMaterial, Material, false); 
    target.stretchMaterial = EditorGUILayout.FloatField("Material X Tiling", target.stretchMaterial);
    target.stretchYMaterial = EditorGUILayout.FloatField("Material Y Tiling", target.stretchYMaterial);
    target.offsetMaterialSpeed = EditorGUILayout.FloatField("Material X Speed", target.offsetMaterialSpeed);
    target.offsetYMaterialSpeed = EditorGUILayout.FloatField("Material Y Speed", target.offsetYMaterialSpeed); 
    
    EditorGUILayout.Space();
	EditorGUILayout.LabelField("Rotation", EditorStyles.boldLabel);
    target.rotateSpeed = EditorGUILayout.FloatField("Rotation Speed", target.rotateSpeed);
    target.rotationOffset = EditorGUILayout.FloatField("Rotation Offset", target.rotationOffset);  
    
    EditorGUILayout.Space();
    EditorGUILayout.LabelField("Spiral", EditorStyles.boldLabel);
    
    target.sineDepth = EditorGUILayout.FloatField("Width", target.sineDepth);
    target.sineDepthFrequency = EditorGUILayout.FloatField("Width Frequency", target.sineDepthFrequency); 
    target.sineHeight = EditorGUILayout.FloatField("Height", target.sineHeight);
    target.sineHeightFrequency = EditorGUILayout.FloatField("Height Frequency", target.sineHeightFrequency); 
    target.sineLenght = EditorGUILayout.FloatField("Inner Wave", target.sineLenght);
    target.sineLenghtFrequency = EditorGUILayout.FloatField("Wave Frequency", target.sineLenghtFrequency); 
    target.sineGrowMultiplier = EditorGUILayout.FloatField("Grow Multiplier", target.sineGrowMultiplier);
    target.sineLenghtSpeed = EditorGUILayout.FloatField("Wave Speed", target.sineLenghtSpeed); 
    target.sineHeightSpeed = EditorGUILayout.FloatField("Height Speed", target.sineHeightSpeed);
    target.sineDepthSpeed = EditorGUILayout.FloatField("Depth Speed", target.sineDepthSpeed); 
    EditorGUILayout.Space();
    target.runOnceThenDisable = EditorGUILayout.Toggle("Stop On Start", target.runOnceThenDisable);
    EditorGUILayout.LabelField("Disable calculation when the game starts (spiral will still rotate)", EditorStyles.miniLabel); 
	
     
	EditorGUILayout.Space();EditorGUILayout.Space();	
	if (GUI.changed) {
            EditorUtility.SetDirty (target);
         if(!Application.isPlaying){
			target.GenerateSpiral(counter);
   			counter+=.1;
   		 	target.PositionSpiral();
   		 }
   	}
	//DrawDefaultInspector();
    }	
}