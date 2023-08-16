/********************************************************************************/
// 작성일 : 2023.03.13
// 작성자 : -
// 설  명 : 
/********************************************************************************/
// 수정일     | 종류 | 수정자 | 내용
// 2023.03.13 | ADD  | 작성자 | 신규 작성
/********************************************************************************/
using Terminal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerminalFunc : MonoBehaviour
{
    public List<MethodInfo> methodList = new List<MethodInfo>();

    public void Init()
    {
        DontDestroyOnLoad(gameObject);

        // 터미널 생성 UI 생성
        TerminalSystem.UI_Termianl = Instantiate(Resources.Load<GameObject>("Terminal/UI_Terminal"), transform).GetComponent<UI_Terminal>();
        TerminalSystem.UI_Termianl.gameObject.SetActive(false);

        // 함수이름으로 등록
        RegisterFunc(nameof(TestDebug));
    }

    #region Func
    private void TestDebug()
    {
        Debug.Log("Test Call Func");
    }

    private void GetCurrentTime()
    {
        InsertLog(DateTime.Now.ToString());
    }
    #endregion


    #region Util
    public void RegisterFunc(string methodName)
    {
        methodList.Add(typeof(TerminalFunc).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public));
    }

    public void CallFunctionByName(string funcName)
    {
        if (funcName == "") return;

        if (methodList.Any(x => x.Name == funcName))
        {
            InsertLog($"Called {funcName}();. . .");
            MethodInfo method = typeof(TerminalFunc).GetMethod(funcName, BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(this, null);
        }
        else
        {
            TerminalSystem.UI_Termianl.InsertError($"No such function exists : {funcName}");
        }
    }

    private void InsertLog(string log)
    {
        TerminalSystem.UI_Termianl.InsertLog(log);
    }

    private void InsertError(string error)
    {
        TerminalSystem.UI_Termianl.InsertError(error);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKey(KeyCode.Return))
        {
            TerminalSystem.UI_Termianl.ActiveToggle();
        }
        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Return))
        {
            TerminalSystem.UI_Termianl.ActiveToggle();
        }
        else if (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKey(KeyCode.KeypadEnter))
        {
            TerminalSystem.UI_Termianl.ActiveToggle();
        }
        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            TerminalSystem.UI_Termianl.ActiveToggle();
        }
    }
    #endregion
}
