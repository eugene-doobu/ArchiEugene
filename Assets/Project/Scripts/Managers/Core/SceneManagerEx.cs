﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ArchiEugene.Scene;

namespace ArchiEugene.Scene
{
    public class SceneManagerEx
    {
        public BaseScene CurrentScene => Object.FindObjectOfType<BaseScene>();

        public void LoadScene(Define.Scene type)
        {
            Managers.Clear();

            SceneManager.LoadScene(GetSceneName(type));
        }

        string GetSceneName(Define.Scene type)
        {
            string name = System.Enum.GetName(typeof(Define.Scene), type);
            return name;
        }

        public void Clear()
        {
            if(CurrentScene != null)
                CurrentScene.Clear();
        }
    }
}


