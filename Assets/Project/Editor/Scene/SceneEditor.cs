using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;

namespace ArchiEugene
{
    public class SceneEditor : MonoBehaviour
    {
        [InitializeOnLoad]
        static class EditorSceneManagerSceneSaved {
            static EditorSceneManagerSceneSaved () {
                UnityEditor.SceneManagement.EditorSceneManager.sceneSaving += OnSceneSaving;
            }

            static void OnSceneSaving (UnityEngine.SceneManagement.Scene scene, string path) {
                Debug.Log($"Saving scene '{scene.name}' to {path}");
                
                CleaningEventSystem();
            }

            static void CleaningEventSystem()
            {
                var eventTriggers = FindObjectsOfType<EventSystem>();
                foreach (var eventTrigger in eventTriggers)
                {
                    Debug.LogWarning($"Removed EventSystem GameObject: {eventTrigger.gameObject.name}");
                    DestroyImmediate(eventTrigger.gameObject);
                }
            }
        }
    }
}
