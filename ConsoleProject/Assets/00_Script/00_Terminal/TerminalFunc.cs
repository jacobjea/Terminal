/********************************************************************************/
// 작성일 : 2023.03.13
// 작성자 : -
// 설  명 : 
/********************************************************************************/
// 수정일     | 종류 | 수정자 | 내용
// 2023.03.13 | ADD  | 작성자 | 신규 작성
/********************************************************************************/
using Mammossix.Galaxity.Terminal;
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
    private static TerminalFunc instance;
    public static TerminalFunc Instance => instance;

    public List<MethodInfo> methodList = new List<MethodInfo>();
    public UI_Terminal ui_terminal;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }

    //    ui_terminal = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Popup/UI_Terminal"), transform).GetComponent<UI_Terminal>();
    //    ui_terminal.gameObject.SetActive(false);

    //    methodList.Add(GetMethodInfo(nameof(GoLobby)));
    //    methodList.Add(GetMethodInfo(nameof(GoKhaosan)));
    //    methodList.Add(GetMethodInfo(nameof(GoSocialRoom)));
    //    methodList.Add(GetMethodInfo(nameof(GoMyRoom)));
    //    methodList.Add(GetMethodInfo(nameof(SkipTimeline)));
    //    methodList.Add(GetMethodInfo(nameof(GetNextTutorial)));
    //    methodList.Add(GetMethodInfo(nameof(SendTestSocialInvite)));
    //    methodList.Add(GetMethodInfo(nameof(ConnectChat)));
    //    methodList.Add(GetMethodInfo(nameof(GetCurrentRoomProperies)));
    //    methodList.Add(GetMethodInfo(nameof(ResetGold)));
    //    methodList.Add(GetMethodInfo(nameof(GetGold)));
    //    methodList.Add(GetMethodInfo(nameof(DisconnectChat)));
    //    methodList.Add(GetMethodInfo(nameof(LeavRoom)));
    //    methodList.Add(GetMethodInfo(nameof(JoinPhotonLobby)));
    //    methodList.Add(GetMethodInfo(nameof(GetOptionValue)));
    //    methodList.Add(GetMethodInfo(nameof(CreateFpsChecker)));
    //    methodList.Add(GetMethodInfo(nameof(ResetDiamond)));
    //    methodList.Add(GetMethodInfo(nameof(GetDiamond)));
    //    methodList.Add(GetMethodInfo(nameof(GiveTestMails)));
    //    methodList.Add(GetMethodInfo(nameof(ActiveSleepMode)));
    //    methodList.Add(GetMethodInfo(nameof(LogOut)));
    //}


    //#region Func
    //private void GoLobby()
    //{
    //    if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
    //    Managers.Assets.LoadSceneAsset(Define.E_Scene.Lobby);
    //}

    //private void GoKhaosan()
    //{
    //    if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
    //    Managers.Assets.LoadSceneAsset(Define.E_Scene.GameScene_Khaosan);
    //}

    //private void GoSocialRoom()
    //{
    //    if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
    //    Managers.Assets.LoadSceneAsset(Define.E_Scene.SocialRoom);
    //}

    //private void ConnectChat()
    //{
    //    Managers.Chat.Connect();
    //}

    //private void DisconnectChat()
    //{
    //    Managers.Chat.ChatClient.Disconnect();
    //}

    //private void GoMyRoom()
    //{
    //    if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
    //    Managers.Assets.LoadSceneAsset(Define.E_Scene.MyRoom);
    //}

    //private void SkipTimeline()
    //{
    //    // 20프레임 뒤로 스킵
    //    var timelineController = Managers.Timeline.GetCurrentTimelineController();
    //    timelineController.Director.time += 100;
    //}

    //private void GetNextTutorial()
    //{
    //    InsertLog("NextTutorial : " + Common.User.NextTutorial);
    //}

    //private void SendTestSocialInvite()
    //{
    //    Managers.Chat.OnSendPrivateMessage(new PrivateChatData().RequestInviteSocialRoom(Common.User.userId));
    //}

    //private void GetCurrentRoomProperies()
    //{
    //    if (!PhotonNetwork.InRoom)
    //    {
    //        InsertError("you are not in room");
    //        return;
    //    }
    //    else
    //    {
    //        InsertLog($"\nRoom_Id : " + PhotonNetwork.CurrentRoom.Name);
    //        InsertLog($"MaxPlayerCount : " + PhotonNetwork.CurrentRoom.MaxPlayers);
    //        foreach (var key in PhotonNetwork.CurrentRoom.CustomProperties.Keys)
    //        {
    //            InsertLog($"[{key}] : {PhotonNetwork.CurrentRoom.CustomProperties[key]}");
    //        }
    //        InsertLog("\n");
    //    }
    //}

    

    //private void ResetGold()
    //{
    //    Common.User.Gold = 0;
    //    Managers.DB.UpdateAmount();
    //}

    //private void GetGold()
    //{
    //    Common.User.Gold += 10000;
    //    Managers.DB.UpdateAmount();
    //}

    //private void ResetDiamond()
    //{
    //    Common.User.Diamond = 0;
    //    Managers.DB.UpdateAmount();
    //}

    //private void GetDiamond()
    //{
    //    Common.User.Diamond += 100;
    //    Managers.DB.UpdateAmount();
    //}

    //private void LeavRoom()
    //{
    //    PhotonNetwork.LeaveRoom();
    //}

    //private void JoinPhotonLobby()
    //{
    //    PhotonNetwork.JoinLobby();
    //}

    //private void CreateFpsChecker()
    //{
    //    GameObject gameobject = new GameObject("FPS_Checker");
    //    gameobject.AddComponent<FPSChecker>();
    //}

    //private void GetOptionValue()
    //{
    //    if (Managers.Option.myOptionSO == null) Debug.Log("optionso 없음");

    //    Type type = Managers.Option.myOptionSO.GetType();
    //    FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
    //    PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

    //    string log = $"DebugLog for {type.Name}:\n";

    //    foreach (FieldInfo field in fields)
    //    {
    //        object value = field.GetValue(Managers.Option.myOptionSO);
    //        log += $"{field.Name}: {value}\n";
    //    }

    //    foreach (PropertyInfo property in properties)
    //    {
    //        object value = property.GetValue(Managers.Option.myOptionSO);
    //        log += $"{property.Name}: {value}\n";
    //    }

    //    InsertLog(log);
    //}

    //private void GiveTestMails()
    //{
    //    string targetUserID = Common.User.Id;

    //    Managers.DB.GiveTestMails(targetUserID, (string resultData) =>
    //    {
    //        if (resultData.Equals("OK"))
    //        {
    //            Managers.Chat.OnSendPrivateMessage(new PrivateChatData().NoticeSendMail(targetUserID));    // 우편 전송 알림
    //            if (targetUserID == Common.User.Id) { Managers.Messenger.NoticeReceiveMail(); }    // 자신에게 보내는 경우 강제 알림 (임시)
                
    //            Managers.CUI.ToastMessage(1, "테스트 우편 지급 완료");
    //        }
    //        else
    //        {
    //            Managers.CUI.ToastMessage(1, "테스트 우편 지급 실패");
    //        }
    //    });
    //}
    
    //private void ActiveSleepMode()
    //{
    //    Managers.SleepMode.ActiveSleepMode();
    //}

    //private void LogOut()
    //{
    //    AsyncLogOut();
    //}

    //private async void AsyncLogOut()
    //{
    //    PhotonNetwork.LeaveRoom();
    //    Common.User.auth.SignOut();

    //    Managers.Scene.nextSceneType = Define.E_Scene.Login;
    //    Managers.Dialogue.ProtectDialogueCanvas();

    //    // * 사운드 페이드 아웃
    //    Managers.Sound.Clear();

    //    // Scene 변경 중 모든 카메라를 Off 변경하여 핑크 쉐이더(메모리에서 Off)로 표시되는 문제 수정
    //    Camera[] _cameras = GameObject.FindObjectsOfType<Camera>();
    //    for (int i = 0; i < _cameras.Length; i++)
    //    {
    //        _cameras[i].targetTexture = null;
    //        _cameras[i].enabled = false;
    //    }

    //    await Managers.Scene.LoadLoadingScene();

    //    // Managers.CUI.fadeImage.color = new Color(0, 0, 0, 0);

    //    Managers.Scene.progressPercent = 0f;

    //    // UI 정보 초기화
    //    Managers.Clear();
    //    Managers.UI.InitStateUI();
    //    await Resources.UnloadUnusedAssets();

    //    // 예전 에셋 정보 삭제
    //    Managers.Assets.ReleaseAsset();


    //    // 가비지 커넥터 실행
    //    GC.Collect();

    //    var scene = SceneManager.LoadSceneAsync(Define.E_Scene.Login.ToString());
    //    scene.allowSceneActivation = false;

    //    while (scene.progress < 0.9f)
    //    {
    //        Managers.Scene.progressPercent = scene.progress;
    //    }
    //    await Task.Delay(1000); // 강제 딜레이
    //    scene.allowSceneActivation = true;
    //    Managers.CUI.FadeOut(1f,Color.white);
    //}
    //#endregion


















    #region Util

    private void InsertLog(string log)
    {
        ui_terminal.InsertLog(log);
    }

    private void InsertError(string error)
    {
        ui_terminal.InsertError(error);
    }



    private MethodInfo GetMethodInfo(string methodName)
    {
        return typeof(TerminalFunc).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
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
            ui_terminal.InsertError($"No such function exists : {funcName}");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && Input.GetKey(KeyCode.Return))
        {
            ui_terminal.Open();
        }
        else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Return))
        {
            ui_terminal.Open();
        }
    }
    #endregion
}
