using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

    public IEnumerator DoFadeInOut(float time, float delayInBetween = 0) {
        yield return StartCoroutine(DoFade(0, 1, time));
        yield return new WaitForSeconds(delayInBetween);
        yield return StartCoroutine(DoFade(1, 0, time));
        yield break;
    }

    public IEnumerator DoFadeIn(float time) {
        yield return StartCoroutine(DoFade(0, 1, time));
        yield break;
    }

    public IEnumerator DoFadeOut(float time) {
        yield return StartCoroutine(DoFade(1, 0, time));
        yield break;
    }

    public IEnumerator DoFade(float from, float to, float time) {
        var plane = GetFadePlane(from);
        var renderer = plane.GetComponent<SpriteRenderer>();
        var color = renderer.color;

        if (from < to) {
            while (color.a < to) {
                color.a += Time.deltaTime * (1f / time);
                renderer.color = color;
                yield return new WaitForEndOfFrame();
            }
        } else {
            while (color.a > to) {
                color.a -= Time.deltaTime * (1f / time);
                renderer.color = color;
                yield return new WaitForEndOfFrame();
            }
        }

        renderer.color = color = new Color(1, 1, 1, to);
        yield break;
    }

    private static GameObject GetFadePlane(float alpha = 0) {
        var gObj = GameObject.Find("_bpCameraFade");

        if (gObj == null) {
            gObj = new GameObject("_bpCameraFade");

            var tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            tex.hideFlags = HideFlags.HideAndDontSave;
            tex.SetPixel(0, 0, Color.white);
            tex.Apply();

            var ren = gObj.AddComponent<SpriteRenderer>();
            ren.sprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
            ren.color = new Color(1, 1, 1, alpha);
            ren.sortingOrder = 1000;

            gObj.AddComponent<Fader>();
        } else {
            var ren = gObj.GetComponent<SpriteRenderer>();
            ren.color = new Color(1, 1, 1, alpha);
        }

        var size = new Vector2(100, 100);
        gObj.transform.localScale = new Vector3(size.x * 10, size.y * 10, 1);
        gObj.transform.position = Camera.main.transform.position + new Vector3(0, 0, 1);

        return gObj;
    }

    public static void Destroy() {
        Destroy(GameObject.Find("_bpCameraFade"));
    }

    public static void FadeInOut(float time, float delayInBetween = 0) {
        var plane = GetFadePlane(0).GetComponent<Fader>();
        plane.StartCoroutine(plane.DoFadeInOut(time, delayInBetween));

        //GetFadePlane( 0 ).GetComponent<Fader>().DoFadeInOut( time, delayInBetween );

        //var e = Fade( 0, 1, time );
        //while ( e.MoveNext() ) yield return e.Current;
        //yield return new WaitForSeconds( delayInBetween );
        //e = Fade( 1, 0, time );
        //while ( e.MoveNext() ) yield return e.Current;
    }

    public static void FadeIn(float time) {
        Fade(0, 1, time);
        //var e = Fade( 0, 1, time );
        //while ( e.MoveNext() ) yield return e.Current;
        //yield break;
    }

    public static void FadeOut(float time) {
        Fade(1, 0, time);
        //var e = Fade( 1, 0, time );
        //while ( e.MoveNext() ) yield return e.Current;
        //yield break;
    }

    private static void Fade(float from, float to, float time) {
        var plane = GetFadePlane(from).GetComponent<Fader>();
        plane.StartCoroutine(plane.DoFade(from, to, time));

        //plane.GetComponent<Fader>().DoFade( from, to, time );
        //GetFadePlane( from ).GetComponent<Fader>().DoFade( from, to, time );

        //var plane = GetFadePlane( from );
        //var renderer = plane.GetComponent<SpriteRenderer>();
        //var color = renderer.color;

        //if ( from < to ) {
        //    while ( color.a < to ) {
        //        color.a += Time.deltaTime * ( 1f / time );
        //        renderer.color = color;
        //        yield return new WaitForEndOfFrame();
        //    }
        //} else {
        //    while ( color.a > to ) {
        //        color.a -= Time.deltaTime * ( 1f / time );
        //        renderer.color = color;
        //        yield return new WaitForEndOfFrame();
        //    }
        //}

        //renderer.color = color = new Color( 0, 0, 0, to );
        //yield break;
    }
}
