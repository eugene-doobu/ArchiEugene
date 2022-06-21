using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void AppInitialize()
    {
        Managers.Init();
    }
}
