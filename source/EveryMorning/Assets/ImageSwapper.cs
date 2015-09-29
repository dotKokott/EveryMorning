using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageSwapper : MonoBehaviour {

    public static ImageSwapper _;
    private Image image;

    public Texture2D texxxx;

    // Use this for initialization
    void Start() {
        _ = this;
        image = GetComponent<Image>();
    }

    public void SwapImage( Texture2D newImage ) {
        image.sprite = Sprite.Create( newImage, new Rect( 0, 0, newImage.width, newImage.height ), new Vector2( 0.5f, 0.5f ) );
    }
}
