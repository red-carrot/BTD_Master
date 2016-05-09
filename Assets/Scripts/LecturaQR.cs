using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LecturaQR : MonoBehaviour 
{
	qrCam camaraQR;
	GeoMap GeoMap;
	public Text Debug;

	void Start ()
	{
		camaraQR = GameObject.FindWithTag("MainCamera").GetComponent<qrCam>();
		GeoMap = GameObject.FindWithTag("geomap").GetComponent<GeoMap>();
	}

	void Update ()
	{
		Debug.text="-_-";

		if (camaraQR.textoLeido == "btd-donostia")
		{
			GeoMap.ActualizarMapa("index");
			Debug.text="Alex es tonto :D";
		}
	}
}
