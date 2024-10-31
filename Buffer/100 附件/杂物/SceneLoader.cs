using System.Collections;
using Tools.Manager.GameEvent;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Tools.Scene
{
    public class SceneLoader : MonoBehaviour
    {
        public Transform playerTrans;
        public Vector3 firstPosition;
    
        [Header("第一次加载的场景")]
        public GameSceneSO firstLoadScene;

        public float fadeDuration;
        //public TestGameSceneSO menuScene;
        private GameSceneSO _currentLoadedScene;//当前加载的场景
        private GameSceneSO _sceneToLoad;
        private Vector3 _positionToGo;
        private bool _fadeScreen;
        private bool _isLoading;
        
        private void Start()
        {
            NewGame();
        }
        private void Awake()
        {
            _currentLoadedScene = firstLoadScene;
            _currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
            playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        private void OnEnable()
        {
            GameEventManager.MainInstance.AddEventListener<GameSceneSO,Vector3,
                bool>("场景加载",OnLoadRequestEvent);
        }
        private void OnDisable()
        {
            GameEventManager.MainInstance.RemoveEventListener<GameSceneSO,Vector3,
                bool>("场景加载",OnLoadRequestEvent);
        }
        
        private void NewGame()
        {
            _sceneToLoad = firstLoadScene;
            OnLoadRequestEvent(_sceneToLoad,firstPosition,true);
        }
        
        /// <summary>
        /// 场景加载事件请求
        /// </summary>
        /// <param name="locationToLoad"></param>
        /// <param name="posToGo"></param>
        /// <param name="fadeScreen"></param>
        private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadeScreen)
        {
            if (_isLoading) return;
        
            _isLoading = true;
            _sceneToLoad = locationToLoad;
            _positionToGo = posToGo;
            this._fadeScreen = fadeScreen;

            if (_currentLoadedScene != null)
            {
                StartCoroutine(UnLoadPreviousScene());
            }else
            {
                LoadNewScene();
            }
        }
        
        /// <summary>
        /// 卸载场景
        /// </summary>
        /// <returns></returns>
        private IEnumerator UnLoadPreviousScene()
        {
            if (_fadeScreen)
            {
                GameEventManager.MainInstance.CallEvent("逐渐变黑",fadeDuration);
            }
        
            yield return new WaitForSeconds(fadeDuration);
            yield return _currentLoadedScene.sceneReference.UnLoadScene();//Unity 提供的卸载方法
            playerTrans.gameObject.SetActive(false);
            LoadNewScene();
        }
        
        private void LoadNewScene()
        {
            var loadingOption = _sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
            loadingOption.Completed += OnLoadCompleted;
        }
        
        private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
        {
            _currentLoadedScene = _sceneToLoad;
            playerTrans.position = _positionToGo; 
            playerTrans.gameObject.SetActive(true);
            if (_fadeScreen)
            {
                GameEventManager.MainInstance.CallEvent("逐渐变透明",fadeDuration);
            }
    
            _isLoading = false;
        }
    }
}