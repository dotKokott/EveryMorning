using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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
        "chi_doubleu",
        "MountainDopp",
        "aparaitre",
        "RYStorm",
        "norkibit",
        "onlyfortheart"
    };
    private string[] messages = {
        "Whoah, what have you done dude!?",
        "Swell",
        "Yesterday it was shit, but today it's amazing",
        "OMG",
        "What...",
        "Are you kidding?",
        "I like what you've done with the mustache!",
        "Look at this cutie",
        "What god gives...",
        "Nice!",
        "Oh this looks cool man",
        "Barely recognize you",
        "So much better",
        "I don't like it",
        "Is this fake?",
        "Are you forreal?",
        "This is cool!",
        "It suits you man",
        "www.frapscracked.com",
        "Aren't you the guy from Fru?",
        // From David :D
        "<color=#0000FF>#Selfie-Beach</color>",
        "I mustache you a question: Why?",
        "Your new persona scares me",
        "Mind the gap",
        "The Games Industry is grooming!",
        "Breda Beard Baron",
        "So fresh and so clean, clean",
        "Mattia Traverso in: \"Stubble Trouble\"",
        "It's just a phase. You'll get over it. We'll all get over it...",
        "This beard tells me you are neither woman, nor a child.",
        "Looks quite jizzable",
        "Ron Swanson approves",
        "It compliments your cheekbones",
        "Looks like a less masculine truckdriver"
    };

    // Use this for initialization
    void Start() {
        rtransform = GetComponent<RectTransform>();
        instance = this;
        prefab = Resources.Load<GameObject>( "insta_comment" );
    }

    public void AddComments( int max ) {
        StartCoroutine( addComments( Mathf.Min(max, messages.Length)) );
    }

    private IEnumerator addComments( int max ) {
        rtransform.localPosition = new Vector3( 0, 30 );
        var oldComment = GameObject.Find( "insta_comment(Clone)" );
        while ( oldComment != null ) {
            DestroyImmediate( oldComment, false );
            oldComment = GameObject.Find( "insta_comment(Clone)" );
        }

        var au = new List<string>( authors );
        var me = new List<string>( messages );

        for ( int i = 0; i < max; i++ ) {
            var c = Instantiate( prefab );
            c.transform.SetParent( transform );

            if (au.Count == 0) {
                au = new List<string>(authors);
            }

            int index = Random.Range( 0, au.Count );
            c.transform.Find( "comment_author" ).GetComponent<Text>().text = au[index];// authors[Random.Range( 0, authors.Length )];
            au.RemoveAt( index );

            index = Random.Range( 0, me.Count );
            c.transform.Find( "comment_message" ).GetComponent<Text>().text = me[index];// messages[Random.Range( 0, messages.Length )];
            me.RemoveAt( index );

            var pos = new Vector2( 0, -135 - ( 30 * i ) );
            c.GetComponent<RectTransform>().localPosition = pos;
            c.GetComponent<RectTransform>().localScale = new Vector3( 1, 1, 1 );

            var delay = Random.Range( 0.25f, 1 );
            if ( i >= 3 ) {
                var val = rtransform.localPosition.y;
                iTween.ValueTo( gameObject, iTween.Hash( "time", 0.25f, "easetype", iTween.EaseType.easeInOutCubic,
                    "onupdate", "movecomments", "from", val, "to", val + 30 ) );
            }
            yield return new WaitForSeconds( delay );
        }

        yield break;
    }

    private void movecomments( float value ) {
        var v = rtransform.localPosition;
        v.y = value;
        rtransform.localPosition = v;
    }
}
