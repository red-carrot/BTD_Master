using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CodigoCamara : MonoBehaviour 
{
	public RawImage ImagenCam;
	private WebCamTexture CamTexture;
	
	// Use this for initialization
	void Start () 
	{
		CamTexture = new WebCamTexture ();
		ImagenCam.texture = CamTexture;
		CamTexture.Play();
	}
}