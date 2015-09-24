using UnityEngine;
using System.Collections;

public class Flash : MonoBehaviour {

    public Color color;

	// Use this for initialization
	void Start () {
        iTween.ColorTo(gameObject, iTween.Hash("color", color, "time", 0.5f, "looptype", iTween.LoopType.pingPong));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
