using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terminalScript : MonoBehaviour {

	// Use this for initialization
	// public Texture m_MainTexture, m_Normal, m_Metal;
	// Renderer m_Renderer;

	// Use this for initialization
	void Start () {
		//Fetch the Renderer from the GameObject
		// m_Renderer = GetComponent<Renderer> ();
		// m_Renderer.material.EnableKeyword ("_NORMALMAP");
        // m_Renderer.material.EnableKeyword ("_METALLICGLOSSMAP");
	}

	// Update is called once per frame
	void Update () {

	}

	public void ChangeOpenTerminal(){
		// m_Renderer.material.SetTexture("_MainTex", m_MainTexture);
		// m_Renderer.material.SetTexture("_BumpMap", m_Normal);
	}
}