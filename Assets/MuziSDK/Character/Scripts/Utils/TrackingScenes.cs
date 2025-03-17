using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace MuziCharacter
{
    public static class TrackingScenes
    {
        public static readonly Stack<SceneInfo> _sceneStack = new Stack<SceneInfo>();
        private static bool addStartScene = true;
        public static void Push(string sceneName)
        {
            if (addStartScene)
            {
                _sceneStack.Push(new SceneInfo() {
                    SceneName = SceneManager.GetActiveScene().name,
                    SceneData = new Dictionary<string, object>()
                });

                addStartScene = false;
            }
            if (Peek() != null && Peek().SceneName == sceneName)
            {
                return;
            }
            _sceneStack.Push(new SceneInfo() {
                SceneName = sceneName,
                SceneData = new Dictionary<string, object>()
            });
        }

        public static SceneInfo Peek() => _sceneStack.TryPeek(out SceneInfo currentScene) ? currentScene : null;
        public static SceneInfo Pop() => _sceneStack.Pop();
        public static SceneInfo PrevScene()
        {
            var done = _sceneStack.TryPop(out SceneInfo currentScene);
            return done ? Peek() : null;
        } 
    }

    public class SceneInfo
    {
        public string SceneName;
        public Dictionary<string, object> SceneData = new Dictionary<string, object>();

        public void SetData(string key, object data)
        {
            if (SceneData.ContainsKey(key))
            {
                SceneData[key] = data;
                return;
            }
            
            SceneData.Add(key, data);
            
        }
    }
}