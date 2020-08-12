using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class ApiHandler : MonoBehaviour
{
    /// <summary>
    /// DownlaodsImageFromUri
    /// </summary>
    /// <param name="eventName"></param> The event that will be called after the load is complete
    public void GetImgFromUri(string eventName, string uri)
    {
        StartCoroutine(GetRequest(eventName, uri));
    }

    IEnumerator GetRequest(string eventName, string uri)
    {
        Texture2D myTexture = null;
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
                myTexture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
            EventManager.TriggerEvent(eventName, myTexture);
        }
    }
}