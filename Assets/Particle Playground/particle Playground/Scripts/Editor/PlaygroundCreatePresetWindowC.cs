using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using ParticlePlayground;
using ParticlePlaygroundLanguage;

class PlaygroundCreatePresetWindowC : EditorWindow {

	public static GameObject particleSystemObject;
	public static Texture2D particleSystemIcon;
	public static string particleSystemName;
	public static bool childConnected = false;
	
	public static EditorWindow window;
	public static Vector2 scrollPosition;
	
	public int presetOrPublish = 0;
	public int selectedPreset = 0;
	public int selectedPresets = 0;

	public bool showError1 = false;

	public static PlaygroundLanguageC playgroundLanguage;
	
	public static void ShowWindow () {
		playgroundLanguage = PlaygroundSettingsC.GetLanguage();
		window = EditorWindow.GetWindow<PlaygroundCreatePresetWindowC>(true, playgroundLanguage.presetWizard);
        window.Show();
	}
	
	void OnEnable () {
		Initialize();
	}
	
	public void Initialize () {
		if (Selection.activeGameObject!=null) {
			particleSystemObject = Selection.activeGameObject;
			particleSystemName = Selection.activeGameObject.name;
		}
	}
	
	void OnGUI () {
		EditorGUILayout.BeginVertical();
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);
		EditorGUILayout.Separator();
		EditorGUILayout.LabelField(playgroundLanguage.playgroundPresetWizard, EditorStyles.largeLabel, GUILayout.Height(20));
		EditorGUILayout.Separator();
		
		GUILayout.BeginVertical("box");
		int tmpPresetOrPublish = presetOrPublish;
		presetOrPublish = GUILayout.Toolbar (presetOrPublish, new string[]{playgroundLanguage.preset,playgroundLanguage.publish}, EditorStyles.toolbarButton);
		if (presetOrPublish==0)
			EditorGUILayout.HelpBox(playgroundLanguage.presetText, MessageType.Info);
		else
			EditorGUILayout.HelpBox(playgroundLanguage.publishPresetText, MessageType.Info);
		if (tmpPresetOrPublish!=presetOrPublish && presetOrPublish==1)
			RefreshFromPresetList();
		if (presetOrPublish==1) {
			GUILayout.BeginHorizontal();
			EditorGUILayout.Space ();
			if (GUILayout.Button (playgroundLanguage.publishingGuide, EditorStyles.miniButton, GUILayout.ExpandWidth(false)))
				Application.OpenURL("http://polyfied.com/products/particle-playground/playground-resources/publishing-guide/");
			GUILayout.EndHorizontal();
			EditorGUILayout.Separator();
		}
		EditorGUILayout.Separator();
		
		GUILayout.BeginHorizontal();
		
		if (presetOrPublish==0) {
			EditorGUILayout.PrefixLabel(playgroundLanguage.particleSystem);
		
			// Particle System to become a preset
			GameObject selectedObj = particleSystemObject;
			particleSystemObject = EditorGUILayout.ObjectField(particleSystemObject, typeof(GameObject), true) as GameObject;
			if (particleSystemObject!=selectedObj) {
				
				// Check if this is a Particle Playground System
				if (particleSystemObject!=null) {
				
					// Set new name if user hasn't specified one
					if (particleSystemName=="")
						particleSystemName = particleSystemObject.name;
						
					showError1 = false;
				} else {
					showError1 = true;
				}
			}
		
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel(playgroundLanguage.icon);
			particleSystemIcon = EditorGUILayout.ObjectField(particleSystemIcon, typeof(Texture2D), false) as Texture2D;
			GUILayout.EndHorizontal();
			particleSystemName = EditorGUILayout.TextField(playgroundLanguage.nameText, particleSystemName);
			childConnected = EditorGUILayout.Toggle (playgroundLanguage.childConnected, childConnected);
		} else {
			EditorGUILayout.PrefixLabel(playgroundLanguage.presets);

			// Mask field popup of presets
			selectedPresets = EditorGUILayout.MaskField(selectedPresets, PlaygroundParticleWindowC.presetNames);
		}
		EditorGUILayout.Separator();

		GUI.enabled = presetOrPublish==0 || presetOrPublish==1 && selectedPresets!=0;
		if(GUILayout.Button(playgroundLanguage.create, EditorStyles.toolbarButton, GUILayout.ExpandWidth(false))){
			particleSystemName = particleSystemName.Trim();
			if (presetOrPublish==0 && particleSystemObject!=null && particleSystemName!="") {
				string tmpName = particleSystemObject.name;
				particleSystemObject.name = particleSystemName;
				if (AssetDatabase.LoadAssetAtPath("Assets/"+PlaygroundParticleSystemInspectorC.playgroundSettings.playgroundPath+PlaygroundParticleSystemInspectorC.playgroundSettings.presetPath+particleSystemName+".prefab", typeof(GameObject))) {
					if (EditorUtility.DisplayDialog(playgroundLanguage.presetWithSameNameFound, 
						particleSystemName+" "+playgroundLanguage.presetWithSameNameFoundText, 
                		playgroundLanguage.yes, 
                        playgroundLanguage.no))
							CreatePreset();
				} else CreatePreset();
				particleSystemObject.name = tmpName;
			} else
			if (presetOrPublish==1) {
				CreatePublicPreset();
			}
		}
		GUI.enabled = true;
		GUILayout.EndVertical();
		
		// Space for error messages
		if (showError1 && particleSystemObject!=null)
			EditorGUILayout.HelpBox(playgroundLanguage.gameObjectIsNotPlayground, MessageType.Error);
		
		GUILayout.EndScrollView();
		GUILayout.EndVertical();
	}
	
	public void CreatePreset () {

		if (childConnected) {

			// Try to child all connected objects to the particle system
			PlaygroundParticlesC[] ppScript = particleSystemObject.GetComponentsInChildren<PlaygroundParticlesC>();

			int i=0;
			for (int x = 0; x<ppScript.Length; x++) {
				for (; i<ppScript[x].manipulators.Count; i++)
					if (ppScript[x].manipulators[i].transform.available)
						ppScript[x].manipulators[i].transform.transform.parent = particleSystemObject.transform;
				for (i = 0; i<ppScript[x].paint.paintPositions.Count; i++)
					if (ppScript[x].paint.paintPositions[i].parent)
						ppScript[x].paint.paintPositions[i].parent.parent = particleSystemObject.transform;
				for (i = 0; i<ppScript[x].states.Count; i++)
					if (ppScript[x].states[i].stateTransform)
						ppScript[x].states[i].stateTransform.parent = particleSystemObject.transform;
				if (ppScript[x].sourceTransform)
					ppScript[x].sourceTransform.parent = particleSystemObject.transform;
				if (ppScript[x].worldObject.transform)
					ppScript[x].worldObject.transform.parent = particleSystemObject.transform;
				if (ppScript[x].skinnedWorldObject.transform)
					ppScript[x].skinnedWorldObject.transform.parent = particleSystemObject.transform;
			}
		}

		// Save it as prefab in presetPath and import
		GameObject particleSystemPrefab = PrefabUtility.CreatePrefab("Assets/"+PlaygroundParticleSystemInspectorC.playgroundSettings.playgroundPath+PlaygroundParticleSystemInspectorC.playgroundSettings.presetPath+particleSystemObject.name+".prefab", particleSystemObject, ReplacePrefabOptions.ReplaceNameBased);
		AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(particleSystemIcon as UnityEngine.Object), "Assets/"+PlaygroundParticleSystemInspectorC.playgroundSettings.playgroundPath+PlaygroundParticleSystemInspectorC.playgroundSettings.iconPath+particleSystemPrefab.name+".png");
		AssetDatabase.ImportAsset("Assets/"+PlaygroundParticleSystemInspectorC.playgroundSettings.playgroundPath+PlaygroundParticleSystemInspectorC.playgroundSettings.iconPath+particleSystemPrefab.name+".png");

		// Refresh the project
		AssetDatabase.Refresh();

		// Close window
		window.Close();
	}

	int matched = 0;
	public void CreatePublicPreset () {
			
		// List all dependencies
		List<string> presets = new List<string>();
		List<string> presetIcons = new List<string>();
		List<string> presetDependencies = new List<string>();
		for (int i = 0; i<PlaygroundParticleWindowC.presetObjects.Count; i++) {

			// Match the mask to i
			if ((selectedPresets & 1<<i) != 0) {

				// Add the preset object (prefab)
				presets.Add (AssetDatabase.GetAssetPath(PlaygroundParticleWindowC.presetObjects[i].presetObject));

				// Add the icon if existing
				if (PlaygroundParticleWindowC.presetObjects[i].presetImage!=null)
					presetIcons.Add (AssetDatabase.GetAssetPath(PlaygroundParticleWindowC.presetObjects[i].presetImage));

				matched = i;
			}
		}
		if (presets.Count==0)
			return;
		string[] tmpPresetDependencies = AssetDatabase.GetDependencies(presets.ToArray());

		// Check that the operation won't disturb any of the unnecessary files from the framework
		for (int i = 0; i<tmpPresetDependencies.Length; i++) {

			if (!ContainsDisallowedAsset(tmpPresetDependencies[i])) {

				// Add to dependencies list
				presetDependencies.Add(tmpPresetDependencies[i]);
			}
		}

		// Add the icons
		for (int i = 0; i<presetIcons.Count; i++)
			presetDependencies.Add (presetIcons[i]);
		
		// Check that necessary files are in
		if (!presetDependencies.Contains("PlaygroundC.cs"))
			presetDependencies.Add("Assets/"+PlaygroundParticleSystemInspectorC.playgroundSettings.playgroundPath+PlaygroundParticleSystemInspectorC.playgroundSettings.scriptPath+"PlaygroundC.cs");
		if (!presetDependencies.Contains("PlaygroundParticlesC.cs"))
			presetDependencies.Add("Assets/"+PlaygroundParticleSystemInspectorC.playgroundSettings.playgroundPath+PlaygroundParticleSystemInspectorC.playgroundSettings.scriptPath+"PlaygroundParticlesC.cs");
		if (!presetDependencies.Contains("PlaygroundSpline.cs"))
			presetDependencies.Add("Assets/"+PlaygroundParticleSystemInspectorC.playgroundSettings.playgroundPath+PlaygroundParticleSystemInspectorC.playgroundSettings.scriptPath+"Playground Splines/"+"PlaygroundSpline.cs");
		if (!presetDependencies.Contains("Playground Manager"))
			presetDependencies.Add("Assets/"+PlaygroundParticleSystemInspectorC.playgroundSettings.playgroundPath+"Resources/Playground Manager.prefab");
		
		// Create a package
		string assetPackagePath = EditorUtility.SaveFilePanel ("Save Preset", "", presets.Count>1?"Playground Preset Bundle ("+presets.Count+" presets).unitypackage":"Playground Preset - "+PlaygroundParticleWindowC.presetObjects[matched].presetObject.name+".unitypackage", "unitypackage");
		if (assetPackagePath.Length!=0)
			AssetDatabase.ExportPackage(presetDependencies.ToArray(), assetPackagePath, ExportPackageOptions.Interactive);

		// Refresh the project
		AssetDatabase.Refresh();
		
		// Close window
		if (assetPackagePath.Length!=0)
			window.Close();
	}

	public bool ContainsDisallowedAsset (string asset) {
		return 
				asset.Contains("PlaygroundBrushPresetInspectorC.cs") || 
				asset.Contains("PlaygroundCreateBrushWindowC.cs") || 
				asset.Contains("PlaygroundCreatePresetWindowC.cs") || 
				asset.Contains("PlaygroundInspectorC.cs") || 
				asset.Contains("PlaygroundParticleSystemInspectorC.cs") ||
				asset.Contains("PlaygroundParticleWindowC.cs") ||
				asset.Contains("PlaygroundBrushPresetC.cs") ||
				asset.Contains("PlaygroundSplineInspector.cs");
	}
	
	public void RefreshFromPresetList () {
		if (PlaygroundParticleWindowC.presetNames.Length==0) return;
		particleSystemIcon = AssetDatabase.LoadAssetAtPath(PlaygroundParticleSystemInspectorC.playgroundSettings.playgroundPath+PlaygroundParticleSystemInspectorC.playgroundSettings.iconPath+PlaygroundParticleWindowC.presetNames[selectedPreset]+".png", typeof(Texture2D)) as Texture2D;
		particleSystemName = PlaygroundParticleWindowC.presetNames[selectedPreset];
	}
}