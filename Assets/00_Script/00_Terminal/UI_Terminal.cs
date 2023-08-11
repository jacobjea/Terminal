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
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Mammossix.Galaxity.Terminal
{
    public class UI_Terminal : MonoBehaviour
    {
        TerminalFunc terminalFunc;

        [SerializeField] TMP_InputField inputField;
        public TMP_InputField InputField => inputField;

        [SerializeField] TextMeshProUGUI logText;
        [SerializeField] ScrollRect scrollView;

        [SerializeField]
        SuggestionScroll suggestionScroll;


        [Header("Prefeb")]
        [SerializeField]
        GameObject suggestionItem;

        string debugLogColor = "#76F416";
        string errorLogColor = "D02222";

        private void Awake()
        {
            terminalFunc = GetComponentInParent<TerminalFunc>();

        }
        void Start()
        {
            suggestionScroll.CreateItem(TerminalFunc.Instance.methodList);
            LayoutRebuilder.ForceRebuildLayoutImmediate(scrollView.content);

            inputField.onValueChanged.AddListener(Changed);
        }
     
        public void Changed(string value)
        {
            suggestionScroll.SortSuggestion(value);
        }

        public void Open()
        {
            gameObject.SetActive(!gameObject.activeSelf);


            inputField.ActivateInputField();
            inputField.text = "";

            suggestionScroll.gameObject.SetActive(false);
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

        public void InsertLog(string logMessage)
        {
            logText.text += $"\n<color={debugLogColor}>{logMessage}</color>";
            ResetScroll();
        }

        public void InsertError(string logMessage)
        {
            logText.text += $"\n<color={errorLogColor}>{logMessage}</color>";
            ResetScroll();
        }

        public void ClearInputField()
        {
            inputField.text = "";
            inputField.ActivateInputField();
        }

        private void Update()
        {
            if (inputField.isFocused && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                suggestionScroll.gameObject.SetActive(true);
                inputField.DeactivateInputField();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                inputField.ActivateInputField();
                suggestionScroll.gameObject.SetActive(false);
            }
        }
    }
}

