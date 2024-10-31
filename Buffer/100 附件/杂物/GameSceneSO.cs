using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tools.Scene
{
    [CreateAssetMenu(fileName = "Game Scene", menuName = "GameSceneSO", order = 0)]
    public class GameSceneSO : ScriptableObject
    {
        public AssetReference sceneReference;
    }
}