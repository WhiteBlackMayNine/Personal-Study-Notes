using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.Parallax
{

    public class ParallaxController : MonoBehaviour
    {
        [Serializable]
        public class ParallaxSprite
        {
            public string layerName;
            public GameObject parallaxPrefab;
            public float textureUnitSizeX;
            public float textureUnitSizeY;
            public bool lockX;
            public bool lockY;
            public Sprite sprite;
        }
        
        public Vector2 deltaMovement;
        
        [SerializeField] private List<ParallaxSprite> parallaxLayers = new List<ParallaxSprite>();
        
        private Transform _playerTransform;
        private Vector3 _lastCameraPosition;
        
        
        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
            _lastCameraPosition = _playerTransform.position;
            
            foreach (var e in parallaxLayers)
            {
                e.sprite = e.parallaxPrefab.GetComponent<SpriteRenderer>().sprite;
                e.textureUnitSizeX = e.sprite.texture.width / e.sprite.pixelsPerUnit;
                e.textureUnitSizeY = e.sprite.texture.height / e.sprite.pixelsPerUnit;
            }
        }

        private void Update()
        {
            ParallaxPositionMoveX();
            ParallaxPositionMoveY();
            CameraMove();
        }
        

        private void ParallaxPositionMoveX()
        {
            var i = 0.8f;
            foreach (var e in parallaxLayers)
            {
                e.parallaxPrefab.transform.position += new Vector3(deltaMovement.x * i, deltaMovement.y * i * 0.6f, 0);
                i -= 0.2f;
                
                if (Mathf.Abs(_playerTransform.position.x - e.parallaxPrefab.transform.position.x ) >=
                    e.textureUnitSizeX)
                {
                    if(e.lockX) return;
                    var offset = _playerTransform.position.x - e.parallaxPrefab.transform.position.x;
                    e.parallaxPrefab.transform.position = new Vector3(_playerTransform.position.x + offset,
                        e.parallaxPrefab.transform.position.y,0);
                }
            }
        }

        private void ParallaxPositionMoveY()
        {
            foreach (var e in parallaxLayers)
            {
                if (Mathf.Abs(_playerTransform.position.y - e.parallaxPrefab.transform.position.y ) >=
                    e.textureUnitSizeY)
                {
                    if(e.lockY) return;
                    var offset = _playerTransform.position.y - e.parallaxPrefab.transform.position.y;
                    e.parallaxPrefab.transform.position = new Vector3(e.parallaxPrefab.transform.position.x,
                        _playerTransform.position.y + offset,0);
                }
            }
        }

        private void CameraMove()
        {
            deltaMovement = _playerTransform.position -  _lastCameraPosition;
            _lastCameraPosition =  _playerTransform.position;
        }

    }
}


