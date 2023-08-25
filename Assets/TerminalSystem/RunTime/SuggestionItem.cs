/********************************************************************************/
// 작성일 : 2023.03.24
// 작성자 : 제승욱
// 설  명 : 
/********************************************************************************/
// 수정일     | 종류 | 수정자 | 내용
// 2023.03.24 | ADD  | 제승욱 | 신규 작성
/********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace Terminal
{
    public class SuggestionItem : MonoBehaviour, ISelectHandler, IPointerClickHandler, ISubmitHandler
    {
        public MethodInfo myMethodInfo;

        [SerializeField]
        TextMeshProUGUI methodNameText;

        [SerializeField]
        private SuggestionItemEvent onSelectEvent;

        [SerializeField]
        private SuggestionItemEvent onSubmitEvent;

        [SerializeField]
        private SuggestionItemEvent onClickEvent;

        public SuggestionItemEvent OnSelectEvent
        {
            get => onSelectEvent;
            set => onSelectEvent = value;
        }

        public SuggestionItemEvent OnSubmitEvent
        {
            get => onSubmitEvent;
            set => onSubmitEvent = value;
        }

        public SuggestionItemEvent OnClickEvent
        {
            get => onClickEvent;
            set => onClickEvent = value;
        }



        public SuggestionItem Init(MethodInfo methodInfo)
        {
            myMethodInfo = methodInfo;

            // 매개변수가 있는 경우 *표시
            if (myMethodInfo.GetParameters().Length == 0)
                methodNameText.text = methodInfo.Name;
            else
                methodNameText.text = methodInfo.Name + "*";

            gameObject.SetActive(true);
            return this;
        }


        public SuggestionItem Select()
        {
            return this;
        }

        public SuggestionItem Deselec()
        {
            return this;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClickEvent.Invoke(this);
        }
        public void OnSelect(BaseEventData eventData)
        {
            onSelectEvent?.Invoke(this);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            onSubmitEvent?.Invoke(this);
        }

        public void ObtainSelectionFocus()
        {
            EventSystem.current.SetSelectedGameObject(this.gameObject);
            onSelectEvent.Invoke(this);
        }
    }
    [System.Serializable]
    public class SuggestionItemEvent : UnityEvent<SuggestionItem> { }
}

