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

    public void StartCount( int amount ) {
        iTween.ValueTo( gameObject, iTween.Hash(
            "time", 3, "from", 0, "to", amount,
            "onupdate", "updatecounter",
            "easetype", iTween.EaseType.easeOutBack ) );
    }

    private void updatecounter( float value ) {
        label.text = string.Format( "{0} likes", Mathf.FloorToInt( value ) );
    }
}
