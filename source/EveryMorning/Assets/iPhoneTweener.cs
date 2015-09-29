using UnityEngine;
using System.Collections;

public class iPhoneTweener : MonoBehaviour {

    public static iPhoneTweener _;

    // Use this for initialization
    void Start() {
        _ = this;
    }

    public void SlideIn( int likes, int comments ) {
        iTween.MoveTo( gameObject, iTween.Hash(
            "time", 1, "islocal", true, "position", new Vector3( 0, 0, 0 ),
            "easetype", iTween.EaseType.easeInOutCubic ) );
        StartCoroutine( StartOthers( likes, comments ) );
    }

    private IEnumerator StartOthers( int likes, int comments ) {
        yield return new WaitForSeconds( 1 );
        LikeCounter._.StartCount( likes );
        CommentPusher._.AddComments( comments );
    }

    public void SlideOut() {
        iTween.MoveTo( gameObject, iTween.Hash(
            "time", 1, "islocal", true, "position", new Vector3( 0, -550, 0 ),
            "easetype", iTween.EaseType.easeInOutCubic ) );
        iTween.MoveTo( GameObject.Find( "insta_mid" ), iTween.Hash(
            "time", 1, "islocal", true, "position", new Vector3( 0, 30, 0 ),
            "easetype", iTween.EaseType.easeInOutCubic ) );
    }
}
