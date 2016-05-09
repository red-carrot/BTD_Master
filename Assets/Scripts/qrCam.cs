/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Hi, this project has been tested on Unity 3.5.2f2 in Editor on my laptop. *
 * It runs fine. If you have any problems, contact me at aurodeus@gmail.com  *
 * Thanks. Aurodeus, 2012.                                                   *
 *                                                                           *
 * PS: The modified Zxing Visual Studio project can be downloaded from       *
 *     http://aurodeus.16mb.com/misc/zxingVSproj.zip                         *
 *                                                                           *
 *     The original Zxing project is located at                              *
 *     http://code.google.com/p/zxing/                                       *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.UI;

using com.google.zxing.qrcode;

public class qrCam : MonoBehaviour {
	
	private WebCamTexture camTexture;
	private Thread qrThread;

	private Color32[] c;
	private sbyte[] d;
	private int W, H, WxH;
	private int x, y, z;

	private Rect screenRect;
	
	private bool isQuit = false;
	
	public string textoLeido="Prueba";

	
	void OnGUI () {
		GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
				GUI.Label (new Rect (0,0,300,300), textoLeido);
	}
	
	void OnEnable () {
		if(camTexture != null) {
			camTexture.Play();
			W = camTexture.width;
			H = camTexture.height;
			WxH = W * H;
		}
	}
	
	void OnDisable () {
		if(camTexture != null) {
			camTexture.Pause();
		}
	}
	
	void OnDestroy () {
		qrThread.Abort();
		camTexture.Stop();
		isQuit = true;
	}
	
	// It's better to stop the thread by itself rather than abort it.
	void OnApplicationQuit () {
		isQuit = true;
	}
	
	void Start () {
				screenRect = new Rect(0, 0, Screen.width, Screen.height);
		
		camTexture = new WebCamTexture();
		
		OnEnable();
		c = new Color32[camTexture.width * camTexture.height];
				Invoke ("hiloDecodificador", 3);
	}
	
	void Update () {

		W = camTexture.width;
		H = camTexture.height;
				WxH = W * H;
		c = camTexture.GetPixels32();

	}
	
		void hiloDecodificador()
		{
				qrThread = new Thread(DecodeQR);
				qrThread.Start();
		}
	
	void DecodeQR () {
		while(true) {
						
			if(isQuit) break;

			try {
				d = new sbyte[WxH];
				z = 0;
								print(H);
								print(W);
				print(WxH);
					for(y = H-1; y >= 0; y--) { // This is flipped vertically because the Color32 array from Unity is reversed vertically,
												// it means that the top most row of the image would be the bottom most in the array.
					for(x = 0; x < W; x++) {
						d[z++] = (sbyte)(((int)c[y * W + x].r) << 16 | ((int)c[y * W + x].g) << 8 | ((int)c[y * W + x].b));
			
					}
				}
								QRCodeReader reader = new QRCodeReader();
								textoLeido = reader.decode(d, W, H).ToString();
								print(reader.decode(d, W, H).Text);
							
			}
			catch {
								textoLeido = "Catch";				
				continue;
			}
		}
	}
}