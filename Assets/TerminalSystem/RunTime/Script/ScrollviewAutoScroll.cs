/********************************************************************************/
// 작성일 : 2023.06.14
// 작성자 : 
// 설  명 : 
/********************************************************************************/
// 수정일     | 종류 | 수정자 | 내용
// 2023.06.14 | ADD  | 작성자 | 신규 작성
/********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JACOB.Terminal
{
    public class ScrollviewAutoScroll : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _viewportRectTransform;
        [SerializeField]
        private RectTransform _content;
        [SerializeField] private float _transitionDuration = 0.2f;

        private TransitionHelper _transitionHelper = new TransitionHelper();


        private void Update()
        {
            if (_transitionHelper.InProgress == true)
            {
                _transitionHelper.Update();
                _content.transform.localPosition = _transitionHelper.PosCurrent;
            }
        }

        public void HandleOnSelectChange(GameObject obj)
        {
            float viewportTopBorderY = GetBorderTopYLocal(_viewportRectTransform.gameObject);
            float viewportBottomBorderY = GetBorderBottomYLocal(_viewportRectTransform.gameObject);

            // top
            float targetTopBorderY = GetBorderTopYRelative(obj);
            float targetTopYWithViewportOffset = targetTopBorderY + viewportTopBorderY;

            // bottom
            float targetBottomBorderY = GetBorderBottomYRelative(obj);
            float targetBottomYWithViewportOffset = targetBottomBorderY - viewportBottomBorderY;

            //top difference
            float topDiff = targetTopYWithViewportOffset - viewportTopBorderY;
            if (topDiff > 0)
            {
                MoveContentObjectByAmount((topDiff * 100f) + GetVerticalLayoutGroup().padding.top);
            }

            //bottom difference
            float bottomDiff = targetBottomYWithViewportOffset - viewportBottomBorderY;
            if (bottomDiff < 0f)
            {
                MoveContentObjectByAmount((bottomDiff * 100f) - GetVerticalLayoutGroup().padding.bottom);
            }
        }

        private float GetBorderTopYLocal(GameObject obj)
        {
            Vector3 pos = obj.transform.localPosition / 100;
            return pos.y;
        }

        private float GetBorderBottomYLocal(GameObject obj)
        {
            Vector2 rectSize = obj.GetComponent<RectTransform>().rect.size * 0.01f;
            Vector3 pos = obj.transform.localPosition / 100f;

            pos.y -= rectSize.y;
            return pos.y;
        }

        private float GetBorderTopYRelative(GameObject obj)
        {
            float contentY = _content.transform.localPosition.y / 100;
            float targetBorderUpYLocal = GetBorderTopYLocal(obj);
            float targetborderUpYRelative = targetBorderUpYLocal + contentY;

            return targetborderUpYRelative;
        }

        private float GetBorderBottomYRelative(GameObject obj)
        {
            float contentY = _content.transform.localPosition.y / 100;
            float targetBordferBottomYLocal = GetBorderBottomYLocal(obj);
            float targetborderBottomYRelative = targetBordferBottomYLocal + contentY;
            return targetborderBottomYRelative;
        }

        private void MoveContentObjectByAmount(float amount)
        {
            Vector2 posScrollFrom = _content.transform.localPosition;
            Vector2 posScrollTo = posScrollFrom;
            posScrollTo.y -= amount;

            _transitionHelper.TransitionPositionFromTo(posScrollFrom, posScrollTo, _transitionDuration);
        }

        private VerticalLayoutGroup GetVerticalLayoutGroup()
        {
            return _content.GetComponent<VerticalLayoutGroup>();
        }

        public void ScrollToTop()
        {
            float targetTopBorderY = GetBorderTopYLocal(_content.gameObject);
            float targetTopYWithViewportOffset = targetTopBorderY + GetBorderTopYLocal(_viewportRectTransform.gameObject);

            float topDiff = targetTopYWithViewportOffset - GetBorderTopYLocal(_viewportRectTransform.gameObject);
            if (topDiff > 0)
            {
                MoveContentObjectByAmount((topDiff * 100f) + GetVerticalLayoutGroup().padding.top);
            }
        }

        private class TransitionHelper
        {
            private float _duration = 0f;
            private float _timeElapsed = 0;
            private float _progress = 0;

            private bool _inProgress = false;

            private Vector2 _posCurrent;
            private Vector2 _posFrom;
            private Vector2 _posTo;

            public bool InProgress => _inProgress;

            public Vector2 PosCurrent => _posCurrent;

            public void Update()
            {
                Tick();

                CalculatePosition();
            }

            public void TransitionPositionFromTo(Vector2 posfrom, Vector2 posTo, float duration)
            {
                Clear();

                _posFrom = posfrom;
                _posTo = posTo;
                _duration = duration;

                _inProgress = true;
            }

            private void Clear()
            {
                _duration = 0;
                _timeElapsed = 0;
                _progress = 0;

                _inProgress = false;
            }

            private void CalculatePosition()
            {
                _posCurrent.x = Mathf.Lerp(_posFrom.x, _posTo.x, _progress);
                _posCurrent.y = Mathf.Lerp(_posFrom.y, _posTo.y, _progress);
            }

            private void Tick()
            {
                if (_inProgress == false) return;

                _timeElapsed += Time.deltaTime;
                _progress = _timeElapsed / _duration;
                if (_progress > 1f)
                {
                    _progress = 1;
                }

                if (_progress >= 1f)
                {
                    TransitionComplete();
                }
            }

            private void TransitionComplete()
            {
                _inProgress = false;
            }
        }
    }
}


