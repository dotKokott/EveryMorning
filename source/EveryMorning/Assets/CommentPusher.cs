using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CommentPusher : MonoBehaviour {

    public static CommentPusher _ {
        get { return instance; }
    }
    private static CommentPusher instance;

    private GameObject prefab;
    private RectTransform rtransform;

    private string[] authors = {
        "dotKokott",
        "Thundernerd",
        "Chikwan",
        "MountainDopp",
        "Laurenla"
    };
    private string[] messages = {
        "Whoah, what have you done dude!?",
        "Swell",
        "Yesterday it was shit, but today it's amazing",
        "OMG",
        "What...",
        "Are you kidding?",
        "I like what you've done with the mustache!",
        "Look at this cutie"
    };

    // Use this for initialization
    void Start() {
        rtransform = GetComponent<RectTransform>();
        instance = this;
        prefab = Resources.Load<GameObject>( "insta_comment" );
    }

    // Update is called once per frame
    void Update() {
        if ( Input.GetKeyDown( KeyCode.Space ) ) {
            AddComments( Random.Range( 5, 10 ) );
            //for ( int i = 0; i < 5; i++ ) {
            //    var c = Instantiate( prefab );
            //    c.transform.SetParent( transform );
            //    c.transform.Find( "comment_author" ).GetComponent<Text>().text = authors[Random.Range( 0, authors.Length )];
            //    c.transform.Find( "comment_message" ).GetComponent<Text>().text = messages[Random.Range( 0, messages.Length )];
            //    var pos = new Vector2( 0, -140 - ( 30 * i ) );
            //    c.GetComponent<RectTransform>().localPosition = pos;
            //    c.GetComponent<RectTransform>().localScale = new Vector3( 1, 1, 1 );
            //}
        }
    }

    public void AddComments( int max ) {
        StartCoroutine( addComments( max ) );
    }

    private IEnumerator addComments( int max ) {
        for ( int i = 0; i < max; i++ ) {
            var c = Instantiate( prefab );
            c.transform.SetParent( transform );
            c.transform.Find( "comment_author" ).GetComponent<Text>().text = authors[Random.Range( 0, authors.Length )];
            c.transform.Find( "comment_message" ).GetComponent<Text>().text = messages[Random.Range( 0, messages.Length )];
            var pos = new Vector2( 0, -135 - ( 30 * i ) );
            c.GetComponent<RectTransform>().localPosition = pos;
            c.GetComponent<RectTransform>().localScale = new Vector3( 1, 1, 1 );

            var delay = Random.Range( 0.1f, 1 );
            if ( i >= 3 ) {
                var val = rtransform.localPosition.y;
                Debug.Log( "-------------" );
                iTween.ValueTo( gameObject, iTween.Hash( "time", 0.25f, "easetype", iTween.EaseType.easeInOutCubic,
                    "onupdate", "movecomments", "from", val, "to", val + 30 ) );
                //iTween.MoveBy( gameObject, new Vector3( 0, 30, 0 ), 0.25f );
            }
            yield return new WaitForSeconds( delay );
        }

        yield break;
    }

    private void movecomments( float value ) {
        Debug.Log( value );
        var v = rtransform.localPosition;
        v.y = value;
        rtransform.localPosition = v;
    }
}
