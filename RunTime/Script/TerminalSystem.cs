/********************************************************************************/
// 작성일 : 2023.02.17
// 작성자 : - 제승욱
// 설  명 : 초기설정 및 주 기능 클레스 관리하는 메인 클레스
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


namespace JACOB.Terminal
{
    public static class TerminalSystem
    {
        private static TerminalFunc terminalFunc;
        public static TerminalFunc TerminalFunc => terminalFunc;

        private static UI_Terminal ui_Terminal;
        public static UI_Terminal UI_Termianl { get { return ui_Terminal; } set { ui_Terminal = value; } }
     

#if (UNITY_EDITOR)
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void Init()
        {
            GameObject terminalSystem = new GameObject("TerminalSystem");
            terminalFunc = terminalSystem.AddComponent<TerminalFunc>();
            terminalFunc.Init();
        }
#endif
    }
}