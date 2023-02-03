using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssetsRef : MonoBehaviour
{
    private static GameAssetsRef _Instance; //internal reference to the script

    public static GameAssetsRef Instance
    {
        get { 
            if(_Instance == null) { _Instance = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssetsRef>(); }
            return _Instance; }
    }

    //Add as many public references to any Game Asset here and simply call the instance of the GameAssetsRef to reference them

    public Transform pfCannonBall;
}
