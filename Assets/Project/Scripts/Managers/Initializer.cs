using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchiEugene
{
    public static class Initializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void AppInitialize()
        {
            Managers.Init();
        }
    }
}
