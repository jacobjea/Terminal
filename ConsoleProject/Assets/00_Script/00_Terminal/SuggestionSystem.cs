/********************************************************************************/
// 작성일 : 2023.02.17
// 작성자 : -
// 설  명 : 
/********************************************************************************/
// 수정일     | 종류 | 수정자 | 내용
// 2023.02.17 | ADD  | 작성자 | 신규 작성
/********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;


namespace Mammossix.Galaxity.Terminal
{
    public static class SuggestionSystem
    {
#if (UNITY_EDITOR)
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void OpenUI()
        {
            GameObject obj = new GameObject("TerminalSystem");
            obj.AddComponent<TerminalFunc>();
        }
#endif
    }
}