using UnityEngine;
using System.Collections;

public class Razor : MonoBehaviour {

    Texture2D texture;

    public float ShaveTime = 5;
    private float ShaveTimer = 0;

    public Color c;
	void Start () {
        ResetTex();
        c = new Color(0, 0, 1f, 0);
	}

    public void ResetTex() {
        texture = new Texture2D(512, 512, TextureFormat.RGBA32, false);
        texture.hideFlags = HideFlags.HideAndDontSave;

        texture.SetPixels((GameObject.Find("Quad").GetComponent<Renderer>().material.mainTexture as Texture2D).GetPixels());

        texture.Apply();

        GameObject.Find("Quad").GetComponent<Renderer>().material.mainTexture = texture;
    }

    bool Shave = false;
	
	// Update is called once per frame
	void Update () {
        ShaveTimer += Time.deltaTime;
        if (ShaveTimer >= ShaveTime) {
            var sym1 = 0.0f;
            var sym2 = 0.0f;            

            var clear = Color.clear;

            for (var x = 0; x < texture.width; x++) {
                for (var y = 0; y < texture.height; y++) {
                    if (x < texture.width / 2) {
                        if (texture.GetPixel(x, y).b == 1f) {
                            sym1 += 1;
                        }
                    } else {
                        if (texture.GetPixel(x, y).b == 1f) {
                            sym2 += 1;
                        }
                    }
                }
            }

            var diff = Mathf.Abs(sym1 - sym2);

            ShaveTimer = 0;            
        }

        if (!Shave && Input.GetMouseButtonDown(0)) {
            Shave = true;
            GetComponent<AudioSource>().Play();
            iTween.MoveTo(gameObject, iTween.Hash("z", -6.05f, "time", 0.2f, "easetype", iTween.EaseType.easeOutCubic));
            GetComponentInChildren<ParticleSystem>().Play();
        }

        if(Shave && Input.GetMouseButtonUp(0)) {
            Shave = false;
            GetComponent<AudioSource>().Stop();

            GetComponentInChildren<ParticleSystem>().Stop();

            iTween.MoveTo(gameObject, iTween.Hash("z", -6.415f, "time", 0.1f, "easetype", iTween.EaseType.easeOutCubic));
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        if (Physics.Raycast(ray, out info)) {
            var newPos = new Vector3(info.point.x + 0.2f, info.point.y - 0.5f, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 5);            
            

            if (info.collider.name != "Quad") return;
            if (!Shave) return;

            var tex = texture;

            var clear = Color.clear;
            for (var x = 0; x < 50; x++) {
                for (var y = 0; y < 20; y++) {
                    var coord = info.textureCoord;

                    coord.x *= tex.width;
                    coord.y *= tex.height;

                    coord.x += -85 + x;
                    coord.y += 75 + y;
                    if (coord.x < tex.width && y < tex.height) {
                        tex.SetPixel((int)coord.x - 12, (int)coord.y - 12, c);
                    } 
                }
            }

            tex.Apply();

            //info.collider.GetComponent<Renderer>().material.mainTexture = newTex;

        }       
	}
}
