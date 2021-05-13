using System;
using System.Collections.Generic;
using DG.Tweening;
using Framework.Extensions;
using Framework.Utils.Positioning;
using UnityEngine;

namespace Sports.Playback.Camera
{
    [Serializable]
    public class AnchorTransform
    {
        public Vector3 Position;
        public Vector3 Rotation;
    }

    [RequireComponent(typeof(PositionFollower))]
    public class CameraController : MonoBehaviour
    {
        private bool _isInitialized;
        private Vector3 _defaultPosition;
        private Vector3 _defaultRotation;
        private PositionFollower _positionFollower;

        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private Transform _cameraAnchor;
        [SerializeField] private float _focusTime;
        [SerializeField] private Ease _focusEase;
        [SerializeField] private List<AnchorTransform> _focusAnchors;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (_isInitialized)
            {
                ResetState();
                return;
            }

            _positionFollower = GetComponent<PositionFollower>();
            _defaultPosition = _cameraAnchor.transform.localPosition;
            _defaultRotation = _cameraAnchor.transform.localRotation.eulerAngles;

            _isInitialized = true;
        }

        public void Activate(bool isActive)
        {
            _positionFollower.Activate(isActive);
        }

        public void SetTarget(Transform target)
        {
            _positionFollower.SetTarget(target, true);
        }

        public void ResetState()
        {
            _camera.transform.DOKill();
            _cameraAnchor.transform.DOKill();
            _positionFollower.ResetState();
        }

        public void Focus(int index, bool immediately = false, Action callback = null)
        {
            if (index < _focusAnchors.Count)
            {
                var focusTransform = _focusAnchors[index];

                if (immediately)
                {
                    _cameraAnchor.transform.localPosition = focusTransform.Position;
                    _cameraAnchor.transform.localRotation = Quaternion.Euler(focusTransform.Rotation);

                    ResetCamera(true);
                    callback?.Invoke();
                }
                else
                {
                    _cameraAnchor.transform.DOKill();
                    _cameraAnchor.transform.DOLocalMove(focusTransform.Position, _focusTime).SetUpdate(UpdateType.Fixed)
                        .SetEase(_focusEase);
                    _cameraAnchor.transform.DOLocalRotate(focusTransform.Rotation, _focusTime)
                        .SetUpdate(UpdateType.Fixed).SetEase(_focusEase);

                    this.WaitForSeconds(_focusTime, () => callback?.Invoke());
                }
            }
        }

        public void UnFocus(Action callback = null)
        {
            _cameraAnchor.transform.DOKill();
            _cameraAnchor.transform.DOLocalMove(_defaultPosition, _focusTime).SetUpdate(UpdateType.Fixed)
                .SetEase(_focusEase);
            _cameraAnchor.transform.DOLocalRotate(_defaultRotation, _focusTime).SetUpdate(UpdateType.Fixed)
                .SetEase(_focusEase);

            this.WaitForSeconds(_focusTime, () => callback?.Invoke());
        }

        public bool IsVisible(GameObject target)
        {
            var screenPoint = _camera.WorldToViewportPoint(target.transform.position);
            if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
            {
                return true;
            }

            return false;
        }

        private void ResetCamera(bool immediately = false)
        {
            if (immediately)
            {
                _camera.transform.localPosition = Vector3.zero;
                _camera.transform.localRotation = Quaternion.Euler(Vector3.zero);
            }
            else
            {
                _camera.transform.DOLocalMove(Vector3.zero, 0.25f);
                _camera.transform.DOLocalRotate(Vector3.zero, 0.25f);
            }
        }
    }
}