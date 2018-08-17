using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ShaderEditor;
[CustomEditor(typeof(ShaderEditorData))]
public class ShaderEditorDataEditor : Editor {
	private void OnEnable()
	{
		EditorUtility.SetDirty(target);
		GlobalShaderEditor.data = (ShaderEditorData)target;
		GlobalShaderEditor.data.SetValues();
	}

	public override void OnInspectorGUI()
	{

	}
}
