using UnityEngine;
using UnityEngine.UI;

public class ImageUpd : MonoBehaviour
{
    [SerializeField]
    Image ImageToUpd;

    [SerializeField]
    ApiHandler WebApiHandler;

    string url = "https://picsum.photos/200"; // can be modified later, for now keep it this way
    public void LoadImage()
    {
        EventManager.StartListening<Texture2D>("OnImageLoad", OnImageLoad);

        WebApiHandler.GetImgFromUri("OnImageLoad", url);
    }

    private void OnImageLoad(Texture2D myTexture)
    {
        if (myTexture != null) // no errors
        {
            ImageToUpd.sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.height, myTexture.width), Vector2.zero);
        }
        else
        {
            Debug.Log("Error Getting Texture");
        }

        EventManager.StopListening<Texture2D>("OnImageLoad", OnImageLoad);
    }
}