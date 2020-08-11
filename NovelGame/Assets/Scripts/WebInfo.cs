using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
//using Assets;

public class WebInfo : MonoBehaviour
{
    public void UpdImage(Image _speakerImg)
    {
        StartCoroutine(GetRequest("https://picsum.photos/200", _speakerImg));
    }

    IEnumerator GetRequest(string uri, Image _speakerImg)
    {
        Image speakerImg = _speakerImg;
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Texture2D myTexture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
                speakerImg.sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.height, myTexture.width), Vector2.zero);
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
}
