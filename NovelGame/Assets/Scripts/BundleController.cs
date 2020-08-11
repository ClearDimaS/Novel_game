using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BundleController
{
    public void loadBundle(string bundleName, WebInfo webInfo, Image img) 
    {
        //if failed to load from local
        webInfo.UpdImage(img);
    }


}
