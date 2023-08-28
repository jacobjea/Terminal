/********************************************************************************/
// 작성일 : 2023.06.07
// 작성자 : 
// 설  명 : 
/********************************************************************************/
// 수정일     | 종류 | 수정자 | 내용
// 2023.06.07 | ADD  | 작성자 | 신규 작성
/********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace JACOB.Terminal
{
    public class UI_Terminal : MonoBehaviour
    {
        [SerializeField] TMP_InputField searchFuncInputField;
        [SerializeField] TMP_InputField parameterInputField;

        [SerializeField] GameObject parameterArea;
        [SerializeField] TMP_Text parameterNameText;
        public TMP_InputField InputField => searchFuncInputField;

        [SerializeField] TMP_Text logText;
        [SerializeField] ScrollRect scrollView;

        [SerializeField]
        SuggestionScroll suggestionScroll;

        const string debugLogColor = "#76F416";
        const string errorLogColor = "#D02222";
        const string infoLogColor = "#9CDCFE";

        INPUT_MODE mode = INPUT_MODE.SUGGESTION;
        public INPUT_MODE Mode 
        {
            get => mode;
            set
            {
                mode = value;
                searchFuncInputField.text = "";
                parameterInputField.text = "";
                searchFuncInputField.gameObject.SetActive(mode == INPUT_MODE.SUGGESTION);
                parameterArea.SetActive(mode == INPUT_MODE.PARAMETER);
                if (mode == INPUT_MODE.SUGGESTION) searchFuncInputField.ActivateInputField();
                if (mode == INPUT_MODE.PARAMETER) parameterInputField.ActivateInputField();
            }
        }

        MethodInfo currentMethodInfo;
        ParameterInfo currentParameterInfo;

        Queue<ParameterInfo> parameterInfoQueue = new Queue<ParameterInfo>();
        List<object> parameterValueList = new List<object>();

        void Start()
        {
            suggestionScroll.CreateItem(TerminalSystem.TerminalFunc.methodList);
            LayoutRebuilder.ForceRebuildLayoutImmediate(scrollView.content);

            searchFuncInputField.onValueChanged.AddListener(OnSearchFuncInputFieldEvent);
            parameterInputField.onSubmit.AddListener(OnParameterInputFieldEvent);
        }

        private void OnSearchFuncInputFieldEvent(string value)
        {
            suggestionScroll.SortSuggestion(value);
        }


        private void OnParameterInputFieldEvent(string value)
        {
            if (value == "") return;
            try
            {
                object conversionValue = Convert.ChangeType(value, currentParameterInfo.ParameterType);
                parameterValueList.Add(conversionValue);
                InsertLog($"{currentParameterInfo.Name} : {value}",LOG_TYPE.INFO);
            }
            catch 
            {
                InsertLog($"Type conversion is not possible. Check the type.({value})", LOG_TYPE.ERROR);
                parameterInputField.text = "";
                parameterInputField.ActivateInputField();
                return;
            }

            if (parameterValueList.Count == currentMethodInfo.GetParameters().Length)
            {
                TerminalSystem.TerminalFunc.InvokeMethod(currentMethodInfo, parameterValueList.ToArray());
                Mode = INPUT_MODE.SUGGESTION;
            }
            else
            {
                SetNextParameter();
            }
        }
        
        public void SetParameterMode(MethodInfo targetMethodInfo)
        {
            currentMethodInfo = targetMethodInfo;
            parameterInfoQueue.Clear();
            parameterValueList.Clear();

            StringBuilder parameterInfoLogText = new StringBuilder();
            parameterInfoLogText.Append($"\nParameter : ");

            foreach (var parameterInfo in targetMethodInfo.GetParameters())
            {
                parameterInfoQueue.Enqueue(parameterInfo);
                parameterInfoLogText.Append($"{parameterInfo.Name}, ");
            }
            parameterInfoLogText.Length = parameterInfoLogText.Length - 2;
            InsertLog(parameterInfoLogText.ToString(), LOG_TYPE.INFO);
            SetNextParameter();
        }

        public void SetNextParameter()
        {
            currentParameterInfo = parameterInfoQueue.Dequeue();
            parameterNameText.text = currentParameterInfo.Name;
            parameterInputField.text = "";
            parameterInputField.ActivateInputField();
        }

        public void ActiveToggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);

            if(gameObject.activeSelf == true)
            {
                Mode = INPUT_MODE.SUGGESTION;
                //searchFuncInputField.ActivateInputField();
                //searchFuncInputField.text = "";
                suggestionScroll.gameObject.SetActive(false);
            }

        }

        private void ResetScroll()
        {
            StartCoroutine(ResetScroll_C());
        }

        IEnumerator ResetScroll_C()
        {
            yield return new WaitForSeconds(0.1f);
            LayoutRebuilder.ForceRebuildLayoutImmediate(scrollView.content);
            scrollView.verticalScrollbar.value = 0;
        }

        public void InsertLog(string logMessage, LOG_TYPE logType)
        {
            string logColor = logType switch 
            {
                LOG_TYPE.DEBUG => debugLogColor, 
                LOG_TYPE.ERROR => errorLogColor,
                LOG_TYPE.INFO => infoLogColor,
                _ => debugLogColor 
            };
            logText.text += $"\n<color={logColor}>{logMessage}</color>";
            ResetScroll();
        }

        public void ClearInputField()
        {
            searchFuncInputField.text = "";
            searchFuncInputField.ActivateInputField();
        }

        private void Update()
        {
            if (searchFuncInputField.isFocused && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                suggestionScroll.gameObject.SetActive(true);
                searchFuncInputField.DeactivateInputField();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(mode == INPUT_MODE.SUGGESTION)
                {
                    searchFuncInputField.ActivateInputField();
                    suggestionScroll.gameObject.SetActive(false);
                }
                else if(mode == INPUT_MODE.PARAMETER)
                {
                    Mode = INPUT_MODE.SUGGESTION;
                }
                
            }
        }
    }
}

