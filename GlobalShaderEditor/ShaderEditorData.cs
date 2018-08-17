using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShaderEditor
{
	[System.Serializable, CreateAssetMenuAttribute(fileName = "ShaderEditorSO", menuName = "Create Shader Editor SO", order = 0)]
	public class ShaderEditorData : ScriptableObject
	{
		public List<string> vectorNames = new List<string>(), floatNames = new List<string>(), textureNames = new List<string>(), colorNames = new List<string>();
		public List<Texture2D> textures = new List<Texture2D>();
		public List<float> floats = new List<float>();
		public List<Color> colors = new List<Color>();
		public List<Vector4> vectors = new List<Vector4>();
		public AnimationCurve curve = new AnimationCurve();
		public void SetValues()
		{
			for (int i = 0; i < Mathf.Min(vectorNames.Count,vectors.Count); i++)
			{
				Shader.SetGlobalVector(vectorNames[i], vectors[i]);
			}
			for (int i = 0; i < Mathf.Min(floatNames.Count,floats.Count); i++)
			{
				Shader.SetGlobalFloat(floatNames[i], floats[i]);
			}
			for (int i = 0; i < Mathf.Min(colorNames.Count,colors.Count); i++)
			{
				Shader.SetGlobalColor(colorNames[i], colors[i]);
			}
			for (int i = 0; i < Mathf.Min(textureNames.Count,textures.Count); i++)
			{
				Shader.SetGlobalTexture(textureNames[i], textures[i]);
			}
		}

		private void OnEnable()
		{
			SetValues();
		}

		private void Awake()
		{
			SetValues();
		}

		public void Add( DataTypes type, string val)
		{
			switch (type)
			{
				case DataTypes.Color:
					colorNames.Add(val);
					break;
				case DataTypes.Float:
					floatNames.Add(val);
					break;
				case DataTypes.Texture:
					textureNames.Add(val);
					break;
				case DataTypes.Vector:
					vectorNames.Add(val);
					break;
			}
		}
	}
	public enum DataTypes { Float, Texture, Vector, Color };

}
