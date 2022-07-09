﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public static string PLAYER_TAG = "Player";
    
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }
}
