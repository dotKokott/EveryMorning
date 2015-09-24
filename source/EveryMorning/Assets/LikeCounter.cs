using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LikeCounter : MonoBehaviour {

    public static LikeCounter _ {
        get { return instance; }
    }
    private static LikeCounter instance;

    private Text label;

    // Use this for initialization
    void Start() {
        label = GetComponent<Text>();
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        if ( Input.GetKeyDown( KeyCode.Space ) ) {
            StartCount( Random.Range( 0, 100 ) );
        }
    }

    public void StartCount( int max ) {
        iTween.ValueTo( gameObject, iTween.Hash(
            "time", 3, "from", 0, "to", max,
            "onupdate", "updatecounter",
            "easetype", iTween.EaseType.easeOutBack ) );
    }

    private void updatecounter( float value ) {
        label.text = string.Format( "{0} likes", Mathf.FloorToInt( value ) );
    }
}
