/********************************************************************************/
// 작성일 : 2023.06.13
// 작성자 : 
// 설  명 : 
/********************************************************************************/
// 수정일     | 종류 | 수정자 | 내용
// 2023.06.13 | ADD  | 작성자 | 신규 작성
/********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;


namespace Mammossix.Galaxity.Terminal
{
    public class SuggestionScroll : MonoBehaviour
    {
        [SerializeField]
        ScrollRect scrollview;

        [Header("Prefeb")]
        [SerializeField]
        GameObject suggestionItem;

        List<SuggestionItem> suggestionItems = new List<SuggestionItem>();

        ScrollviewAutoScroll scrollviewAutoScroll;

        [SerializeField]
        private SuggestionItemEvent eventItemClicked;
        [SerializeField]
        private SuggestionItemEvent eventItemOnSelect;
        [SerializeField]
        private SuggestionItemEvent eventItemOnSubmit;

        private void Awake()
        {
            scrollviewAutoScroll = GetComponent<ScrollviewAutoScroll>();
        }

        private void HandleEventItemOnSubmit(SuggestionItem item)
        {
            TerminalFunc.Instance.CallFunctionByName(item.methodInfo.Name);
            TerminalFunc.Instance.ui_terminal.ClearInputField();

            gameObject.SetActive(false);

            eventItemOnSubmit?.Invoke(item);
        }

        private void HandleEventItemOnClick(SuggestionItem item)
        {
            eventItemClicked?.Invoke(item);
        }

        private void HandleEventItemOnSelect(SuggestionItem item)
        {
            scrollviewAutoScroll.HandleOnSelectChange(item.gameObject);
            eventItemOnSelect?.Invoke(item);
        }

        void Start()
        {
            //for (int i = 0; i < TerminalFunc.Instance.methodList.Count; i++)
            //{
            //    var item = Instantiate(suggestionItem, scrollview.content.transform).GetComponent<SuggestionItem>().Init(TerminalFunc.Instance.methodList[i]);
            //    item.OnSelectEvent.AddListener((suggestionItem) => { HandleEventItemOnSelect(item); });
            //    item.OnClickEvent.AddListener((suggestionItem) => { HandleEventItemOnClick(item); });
            //    item.OnSubmitEvent.AddListener((suggestionItem) => { HandleEventItemOnSubmit(item); });
            //    item.name = item.name + i;
            //    suggestionItems.Add(item);
            //}
            //InitNavigation();
        }

        public void CreateItem(List<MethodInfo> methodInfo)
        {
            for (int i = 0; i < methodInfo.Count; i++)
            {
                var item = Instantiate(suggestionItem, scrollview.content.transform).GetComponent<SuggestionItem>().Init(TerminalFunc.Instance.methodList[i]);
                item.OnSelectEvent.AddListener((suggestionItem) => { HandleEventItemOnSelect(item); });
                item.OnClickEvent.AddListener((suggestionItem) => { HandleEventItemOnClick(item); });
                item.OnSubmitEvent.AddListener((suggestionItem) => { HandleEventItemOnSubmit(item); });
                item.name = item.name + i;
                suggestionItems.Add(item);
            }
            InitNavigation();
        }

        public void SelectChild(int index)
        {
            var item = scrollview.content.GetChild(index).GetComponent<SuggestionItem>();
            item.ObtainSelectionFocus();
        }

        IEnumerator DelaySelectChild(int index)
        {
            yield return new WaitForSeconds(0.1f);
            SelectChild(index);
        }
       
        private void InitNavigation()
        {
            Button button;
            Navigation navigation;

            for (int i = 0; i < scrollview.content.childCount; i++)
            {
                button = scrollview.content.GetChild(i).GetComponent<Button>();

                navigation = button.navigation;
                navigation.selectOnUp = GetNaviGationUp(i, scrollview.content.childCount);
                navigation.selectOnDown = GetNaviGationDown(i, scrollview.content.childCount);

                button.navigation = navigation;
            }

            var field = TerminalFunc.Instance.ui_terminal.InputField;

            navigation = TerminalFunc.Instance.ui_terminal.InputField.navigation;
            navigation.selectOnDown = scrollview.content.GetChild(0).GetComponent<Selectable>();
            TerminalFunc.Instance.ui_terminal.InputField.navigation = navigation;
        }

        private Selectable GetNaviGationUp(int indexCurrent, int totalCount)
        {
            SuggestionItem item;
            if(indexCurrent == 0)
            {
                item = scrollview.content.GetChild(totalCount -1).GetComponent<SuggestionItem>();
            }
            else
            {
                item = scrollview.content.GetChild(indexCurrent - 1).GetComponent<SuggestionItem>();
            }

            return item.GetComponent<Selectable>();
        }

        private Selectable GetNaviGationDown(int indexCurrent, int totalCount)
        {
            SuggestionItem item;
            if (indexCurrent == totalCount -1)
            {
                item = scrollview.content.GetChild(0).GetComponent<SuggestionItem>();
            }
            else
            {
                item = scrollview.content.GetChild(indexCurrent + 1).GetComponent<SuggestionItem>();
            }

            return item.GetComponent<Selectable>();
        }

        

        private int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            for (int i = 0; i <= n; i++)
            {
                d[i, 0] = i;
            }

            for (int j = 0; j <= m; j++)
            {
                d[0, j] = j;
            }

            for (int j = 1; j <= m; j++)
            {
                for (int i = 1; i <= n; i++)
                {
                    if (s[i - 1] == t[j - 1])
                    {
                        d[i, j] = d[i - 1, j - 1];
                    }
                    else
                    {
                        d[i, j] = Math.Min(d[i - 1, j], Math.Min(d[i, j - 1], d[i - 1, j - 1])) + 1;
                    }
                }
            }

            return d[n, m];
        }
        

        public void SortSuggestion(string value)
        {
            scrollview.gameObject.SetActive(value.Length != 0);

            //var matchingWords = terminalFunc.wordList.Where(w => w.StartsWith(value, StringComparison.OrdinalIgnoreCase));
            //var matchingWords = terminalFunc.wordList.ToList();
            Dictionary<SuggestionItem, int> methodToDistance = new Dictionary<SuggestionItem, int>();

            foreach (var item in suggestionItems)
            {
                value = value.ToLower();
                string wordLower = item.methodInfo.Name.ToLower();

                // 단어의 거리 계산
                int distance = LevenshteinDistance(value, wordLower);
                methodToDistance.Add(item, distance);
            }
            //var orderByDic = methodToDistance.OrderByDescending(x => x.Key.methodInfo.Name.StartsWith(value, StringComparison.OrdinalIgnoreCase) ? 0 : 1).OrderByDescending(x => x.Value);
            var orderByDic = methodToDistance.OrderBy(x => x.Key.methodInfo.Name.StartsWith(value, StringComparison.OrdinalIgnoreCase) ? 0 : 1).ThenBy(x => x.Value);

            int index = 0;
            foreach (var dic in orderByDic)
            {
                dic.Key.transform.SetSiblingIndex(index);
                index++;
            }

            InitNavigation();
            ScrollToTop();
        }

        public void ScrollToTop()
        {
            scrollviewAutoScroll.ScrollToTop();
        }

        private void OnEnable()
        {
            DelaySelectChild(0);
        }

        private void OnDisable()
        {

        }
    }
}
