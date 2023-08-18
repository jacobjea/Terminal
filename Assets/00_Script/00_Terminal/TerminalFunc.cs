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
using System.Text;

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
        RegisterFunc(nameof(TestParameterFunc));
        RegisterFunc(nameof(TestParamterIntFunc));
    }

    #region Func
    private void TestDebug()
    {
        Debug.Log("Test Call Func");
    }

    private void TestParameterFunc(string teststring, int tentstInt, float testFloat)
    {
        Debug.Log(teststring);
        Debug.Log(tentstInt);
        Debug.Log(testFloat);
    }

    private void TestParamterIntFunc(int testInt)
    {
        Debug.Log(testInt);
    }

    private void GetCurrentTime()
    {
        InsertLog(DateTime.Now.ToString(), LOG_TYPE.DEBUG);
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
            MethodInfo methodInfo = methodList.FirstOrDefault(x => x.Name == funcName);
            ParameterInfo[] parameters = methodInfo.GetParameters();
            // 파라미터가 없다면 바로 함수 호출
            if(parameters.Length == 0)
            {
                InvokeMethod(methodInfo, null);
                TerminalSystem.UI_Termianl.ClearInputField();
            }
            else
            {
                //StringBuilder parameterInfoLogText = new StringBuilder();
                //parameterInfoLogText.Append("       ");

                //foreach (var parameterInfo in methodInfo.GetParameters())
                //    parameterInfoLogText.Append($"{parameterInfo.Name} ");

                //InsertLog(parameterInfoLogText.ToString(), LOG_TYPE.INFO);


                TerminalSystem.UI_Termianl.Mode = INPUT_MODE.PARAMETER;
                TerminalSystem.UI_Termianl.SetParameterMode(methodInfo);
            }
        }
        else
        {
            InsertLog($"No such function exists : {funcName}",LOG_TYPE.ERROR);
        }
    }

    public void InvokeMethod(MethodInfo methodInfo, object[] parameter)
    {
        methodInfo?.Invoke(this, parameter);
        InsertLog($"Called {methodInfo.Name}();. . .", LOG_TYPE.DEBUG);
    }
    private void InsertLog(string log, LOG_TYPE log_type)
    {
        TerminalSystem.UI_Termianl.InsertLog(log, log_type);
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
