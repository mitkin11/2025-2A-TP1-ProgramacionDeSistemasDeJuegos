using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Excercise1
{
    [Serializable]
    public class SceneRef : ISerializationCallbackReceiver
    {
#if UNITY_EDITOR
        [SerializeField] private SceneAsset scene;
#endif
        [field: SerializeField] public int Index { get; private set; }
        [field: SerializeField] public string Name { get; private set; }

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (!scene)
                return;
            var index = SceneUtility.GetBuildIndexByScenePath(AssetDatabase.GetAssetPath(scene));
            if (index == Index)
                return;
            Index = index;
            Name = scene.name;
#endif
        }

        public void OnAfterDeserialize() { }
    }
}