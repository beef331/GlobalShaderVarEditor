using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShadowEditorSetter : MonoBehaviour {

	[SerializeField]
	private ShaderEditor.ShaderEditorData editorData;

	public static void SetData(ShaderEditor.ShaderEditorData data)
	{
		data.SetValues();
	}

	private void Update()
	{
		SetData(editorData);
	}

}
