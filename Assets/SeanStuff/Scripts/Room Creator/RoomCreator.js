// Editor Script that creates walls of a room
	
	class RoomCreator extends EditorWindow {
		
		var x : int = 3;
		var z : int = 3;
		var width : float = 2.5;
		var num : int = 1;
		var adj : float = 0.05;
		
		var ori;
		
		@MenuItem("Room Creator/Create from Selection")
		
		static function Init() {
			var window = GetWindow(RoomCreator);
			window.Show();
		}

		//create one object from prefab
		function createObj(ori,par,pos,rot)
		{
			var one;
			
			//check if prefab exist
			if (PrefabUtility.GetPrefabType(ori).ToString()!="None")
			{
				one =  PrefabUtility.InstantiatePrefab(ori);
			}
			else
			{
				one =  Instantiate(ori,Vector3.zero,Quaternion.identity);
			}
			
			one.transform.position = pos;
			one.transform.rotation = rot;
			one.name = ori.name;
			one.transform.parent = par.transform;		
		}

		//create floor
		function createfloor(ori:GameObject)
		{
			//create empty object to hold them
			var floor_group = new GameObject("FLOORS");
			var roof_group =  new GameObject("ROOFS");
			
			for(var i = 0; i < x; i++)
			{
				for (var j = 0; j < z; j++)
				{
					createObj(ori,floor_group,Vector3(j*width,0,i*width),Quaternion.identity);
					//create roof together
					createObj(ori,roof_group,Vector3(j*width,3,i*width),Quaternion.AngleAxis(180,Vector3.left));
				}
			}
				
		}

		//create corner
		function  createcorner(ori:GameObject)
		{
			//create empty object to hold them
			var corner_group = new GameObject("WALL_CORNERS");
			
			for (var i = 0;i < 4; i++)
			{
				//set position
				var pos : Vector3 ;
				switch (i)
				{
					case 0: pos = Vector3(0,0,0);break;
					case 1: pos = Vector3(0,0,(x-1)*width);break;
					case 2: pos = Vector3((z-1)*width,0,(x-1)*width);break;
					case 3: pos = Vector3((z-1)*width,0,0);break;
				}
				
				createObj(ori,corner_group,pos,Quaternion.AngleAxis(i*90, Vector3.up));
			}
			
		}

		//create wall
		function createwall(ori:GameObject)
		{
				//create empty object to hold them
				var wall_group = new GameObject("WALLS");
				
				//wall 1
				for (var i=1; i < x-1; i++)
				{
					createObj(ori,wall_group,Vector3(0,0,i*width),Quaternion.identity);
				}
				
				//wall 2
				for (i=1; i < x-1; i++)
				{
					createObj(ori,wall_group,Vector3((z-1)*width,0,i*width),Quaternion.AngleAxis(180, Vector3.up));
				}
				
				//wall 3
				for (i=1; i < z-1; i++)
				{
					createObj(ori,wall_group,Vector3(i*width,0,0),Quaternion.AngleAxis(-90, Vector3.up));

				}
				
				//wall 4
				for (i=1; i < z-1; i++)
				{
					createObj(ori,wall_group,Vector3(i*width,0,(x-1)*width),Quaternion.AngleAxis(90, Vector3.up));
				}
				

		}

		//create window
		function createwindow(ori:GameObject)
		{
				//create empty object to hold them
				var window_group = new GameObject("WINDOWS");
				
				var spaceX = width*x/(num+1);
				var spaceZ = width*z/(num+1);
				
				var i;
				var win;
				
				//window 1
				for (i=0;i<num;i++)
				{
					createObj(ori,window_group,Vector3(-width/2+adj,0,spaceX*(i+1)-width/2),Quaternion.identity);
				}
				
				//window 2
				for (i=0;i<num;i++)
				{
					createObj(ori,window_group,Vector3((z-1)*width+width/2-adj,0,spaceX*(i+1)-width/2),Quaternion.AngleAxis(180, Vector3.up));
				}
				
				//window 3
				for (i=0;i<num;i++)
				{
					createObj(ori,window_group,Vector3(spaceZ*(i+1)-width/2,0,-width/2+adj),Quaternion.AngleAxis(-90, Vector3.up));
				}
				
				//window 4
				for (i=0;i<num;i++)
				{
					createObj(ori,window_group,Vector3(spaceZ*(i+1)-width/2,0,width*(x-1)+width/2-adj),Quaternion.AngleAxis(90, Vector3.up));
				}
		
		}
		//--------------------------------------------------------------------------
		function OnGUI()
		{
			
			//input field
			x = EditorGUI.IntField(Rect(10,35,position.width-20,15),"Horizon tiles:", x);
			z = EditorGUI.IntField(Rect(10,55,position.width-20,15),"Vertical tiles:", z);
			width = EditorGUI.FloatField(Rect(10,75,position.width-20,15),"Width per tile:", width);
			
			//hint
			if (Selection.activeGameObject!=null)
			{
				EditorGUI.LabelField (Rect(10,10,position.width-20,15), GUIContent ("Selected object: "+Selection.activeGameObject.name));
				EditorGUI.LabelField (Rect(10,280,position.width-20,15), GUIContent ("Selected object: "+Selection.activeGameObject.name));
			}
			else
			{
				EditorGUI.LabelField (Rect(10,10,position.width-20,15), GUIContent ("【Please select an object/prefab first!】"));
				EditorGUI.LabelField (Rect(10,280,position.width-20,15), GUIContent ("【Please select an object/prefab first!】"));
			}
			this.Repaint();
			
			//buttons
			if(GUI.Button(Rect(10,110,position.width-20, 20), "Create Floor") && Selection.activeGameObject!=null)
			{
				createfloor(Selection.activeGameObject);
			}

			if(GUI.Button(Rect(10,140,position.width-20, 20), "Create Corner") && Selection.activeGameObject!=null)
			{
				createcorner(Selection.activeGameObject);
			}

			if(GUI.Button(Rect(10,170,position.width-20, 20), "Create Wall") && Selection.activeGameObject!=null)
			{
				createwall(Selection.activeGameObject);
			}
			
			num = EditorGUI.IntField(Rect(10,200,position.width-20,15),"Window per side:", num);
			adj = EditorGUI.FloatField(Rect(10,220,position.width-20,15),"Adjust space:", adj);
			
			if(GUI.Button(Rect(10,250,position.width-20, 20), "Create Window") && Selection.activeGameObject!=null)
			{
				createwindow(Selection.activeGameObject);
			}

		}
		
	}