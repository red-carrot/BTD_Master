using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GeoMap : MonoBehaviour 
{
	private string  urlMap= "";

	public RawImage ImageMap;
	public Text latitudText;
	public Text longitudText;
	public int veces = 0;

	public int zoom = 20;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine ("GetMap");
		//ImageMap = GameObject.Find ("ImagenMapa");
	}

	public void ActualizarMapa(string escena)
	{
		Application.LoadLevel(escena);
	}

	public void ZoomInButton()
	{
		zoom++;
		StartCoroutine ("GetMap");
	}

	public void ZoomOutButton()
	{
		if ( zoom >=0) zoom--;
		StartCoroutine ("GetMap");
	}

	public IEnumerator GetMap()
	{
		veces = veces+1;

		Input.location.Start ();
		float latitud = Input.location.lastData.latitude;
		yield return latitud;
		float longitud = Input.location.lastData.longitude;

		urlMap = "http://maps.google.com/maps/api/staticmap?center=" + latitud + "," + longitud + "&markers=color:red%7Clabel:S%" + latitud + "," + longitud + "&zoom=" + zoom + "&size=512x512" + "&maptype=hybrid&markers=color:red|label:|" + latitud + "," + longitud + "&type=hybrid&sensor=true?a.jpeg";

		WWW www = new WWW (urlMap);
		yield return www;

		ImageMap.texture = www.texture;
		latitudText.text = "Latitud " + latitud;
		longitudText.text = "Longitud " + longitud;
	}
	
	// Update is called once per frame
	void Update () {


		//print ("Siiii");
	
	}
}
