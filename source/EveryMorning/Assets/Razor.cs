using UnityEngine;
using System.Collections;

public class Razor : MonoBehaviour {

    Texture2D texture;
	// Use this for initialization
	void Start () {
        ResetTex();
	}

    public void ResetTex() {
        texture = new Texture2D(512, 512, TextureFormat.RGBA32, false);
        texture.hideFlags = HideFlags.HideAndDontSave;

        texture.SetPixels((GameObject.Find("Quad").GetComponent<Renderer>().material.mainTexture as Texture2D).GetPixels());

        texture.Apply();

        GameObject.Find("Quad").GetComponent<Renderer>().material.mainTexture = texture;
    }

	
	// Update is called once per frame
	void Update () {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        if (Physics.Raycast(ray, out info)) {
            transform.position = new Vector3(info.point.x + 0.2f, info.point.y - 0.5f, transform.position.z);
            
            if (info.collider.name != "Quad") return;

            var tex = texture;

            var clear = Color.clear;
            for (var x = 0; x < 50; x++) {
                for (var y = 0; y < 20; y++) {
                    var coord = info.textureCoord;
                    coord.x *= tex.width;
                    coord.y *= tex.height;

                    coord.x += -105 + x;
                    coord.y += 75 + y;
                    if (coord.x < tex.width && y < tex.height) {
                        tex.SetPixel((int)coord.x - 12, (int)coord.y - 12, clear);
                    } 
                }
            }

            tex.Apply();

            //info.collider.GetComponent<Renderer>().material.mainTexture = newTex;

        }       
	}
}
