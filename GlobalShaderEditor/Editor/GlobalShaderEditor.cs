using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UGL = UnityEditor.EditorGUILayout;
namespace ShaderEditor
{
	[System.Serializable]
	public class GlobalShaderEditor : EditorWindow
	{
		public static ShaderEditorData data;

		private Vector2 scroll;

		[SerializeField]
		private DataTypes type = DataTypes.Float;

		[MenuItem("/Window/GlobalShaderEditor")]
		public static void Init()
		{
			GlobalShaderEditor window = (GlobalShaderEditor)EditorWindow.GetWindow(typeof(GlobalShaderEditor));
			window.Show();
		}

		private void OnLostFocus()
		{
		//	Save();
		}
		public void OnGUI()
		{
			if (data != null)
			{
				scroll = UGL.BeginScrollView(scroll,EditorStyles.helpBox);
				UGL.BeginHorizontal(EditorStyles.miniButton);
				type = (DataTypes)GUILayout.Toolbar((int)type, System.Enum.GetNames(typeof(DataTypes)));
				GUI.color = Color.green;
				if (GUILayout.Button("+"))
				{
					data.Add(type, string.Empty);
				}
				GUI.color = Color.white;
				UGL.EndHorizontal();
				switch (type)
				{
					case DataTypes.Color:
						ColorGUI();
						break;
					case DataTypes.Float:
						FloatGUI();
						break;
					case DataTypes.Texture:
						TextureGUI();
						break;
					case DataTypes.Vector:
						VectorGUI();
						break;
				}
				UGL.EndScrollView();
				//UGL.EndVertical();
				if (data != null)
				{
					data.SetValues();
				}
			}
		}

		private void FloatGUI()
		{
			int? toRemove = null;
			for (int i = 0; i < data.floatNames.Count; i++)
			{
				UGL.BeginHorizontal();
				data.floatNames[i] = UGL.DelayedTextField(data.floatNames[i], GUILayout.MaxWidth(Screen.width));
				GUILayout.FlexibleSpace();	
				float oldVal = i < data.floats.Count?data.floats[i]:0;
				float newVal = UGL.FloatField(oldVal);
				GUILayout.FlexibleSpace();
				GUI.color = Color.red;
				if (GUILayout.Button("-",GUILayout.Width(100)))
				{
					toRemove = i;
				}
				GUI.color = Color.white;
				if (oldVal != newVal)
				{
					if (!data.floats.Contains(oldVal))
					{
						data.floats.Add(newVal);
					}
					else
					{
						data.floats[data.floats.IndexOf(oldVal)] = newVal;
					}
				}
				UGL.EndHorizontal();
			}
			if (toRemove != null)
			{
				data.floatNames.RemoveAt((int)toRemove);
			}
		}

		private void VectorGUI()
		{
			int? toRemove = null;
			for (int i = 0; i < data.vectorNames.Count; i++)
			{
				UGL.BeginHorizontal();
				data.vectorNames[i] = UGL.DelayedTextField(data.vectorNames[i], GUILayout.MaxWidth(Screen.width));
				GUILayout.FlexibleSpace();
				Vector4 oldVal = i < data.vectors.Count?data.vectors[i]: new Vector4(0,0,0,0);
				Vector4 newVal = UGL.Vector4Field("", oldVal);
				GUILayout.FlexibleSpace();
				GUI.color = Color.red;
				if (GUILayout.Button("-", GUILayout.Width(100)))
				{
					toRemove = i;
				}
				GUI.color = Color.white;
				if (oldVal != newVal)
				{
					if (!data.vectors.Contains(oldVal))
					{
						data.vectors.Add(newVal);
					}
					else
					{
						data.vectors[data.vectors.IndexOf(oldVal)] = newVal;
					}
				}
				UGL.EndHorizontal();
			}
			if (toRemove != null)
			{
				data.vectorNames.RemoveAt((int)toRemove);
			}
		}

		private void ColorGUI()
		{
			int? toRemove = null;
			for (int i = 0; i < data.colorNames.Count; i++)
			{
				UGL.BeginHorizontal();
				data.colorNames[i] = UGL.DelayedTextField(data.colorNames[i],GUILayout.MaxWidth(Screen.width));
				GUILayout.FlexibleSpace();
				Color oldVal = i < data.colors.Count ? data.colors[i] :Color.black;
				Color newVal = UGL.ColorField("", oldVal);
				GUILayout.FlexibleSpace();
				GUI.color = Color.red;
				if (GUILayout.Button("-", GUILayout.Width(100)))
				{
					toRemove = i;
				}
				GUI.color = Color.white;
				if (oldVal != newVal)
				{
					if (!data.colors.Contains(oldVal))
					{
						data.colors.Add(newVal);
					}
					else
					{
						data.colors[data.colors.IndexOf(oldVal)] = newVal;
					}

				}
				UGL.EndHorizontal();
			}
			if (toRemove != null)
			{
				data.colorNames.RemoveAt((int)toRemove);
			}
		}

		private void TextureGUI()
		{
			data.curve = UGL.CurveField(data.curve);
			int? toRemove = null;
			for (int i = 0; i < data.textureNames.Count; i++)
			{
				UGL.BeginHorizontal();
				data.textureNames[i] = UGL.DelayedTextField(data.textureNames[i],GUILayout.MaxWidth(Screen.width));
				GUILayout.FlexibleSpace();
				Texture2D oldVal = i < data.textures.Count ? data.textures[i]: null;
				Texture2D newVal = (Texture2D)UGL.ObjectField("", oldVal, typeof(Texture2D), true,GUILayout.Width(100));
				UGL.BeginVertical();
				if (GUILayout.Button("Set As Curve",GUILayout.Width(100)))
				{
					Texture2D text = new Texture2D(100, 1);
					for (int j = 0; j < text.width; j++)
					{
						float sample = data.curve.Evaluate((float)j / text.width);
						text.SetPixel(j, 0, new Color(sample, sample, sample));
					}
					text.Apply();
					if (text != oldVal)
					{
						string path = AssetDatabase.GetAssetPath(data);
						AssetDatabase.AddObjectToAsset(text, path);
						newVal = text;
					}
				}
				GUI.color = Color.red;
				if (GUILayout.Button("-",GUILayout.Width(100)))
				{
					toRemove = i;
				}
				UGL.EndVertical();
				GUI.color = Color.white;
				if (oldVal != newVal)
				{
					if (!data.textures.Contains(oldVal))
					{
						data.textures.Add(newVal);
					}
					else
					{
						data.textures[data.textures.IndexOf(oldVal)] = newVal;
					}
				}
				UGL.EndHorizontal();
			}
			if (toRemove != null)
			{
				data.textureNames.RemoveAt((int)toRemove);
			}
		}
	}
}
