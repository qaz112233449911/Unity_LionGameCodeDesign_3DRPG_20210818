using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiangWei.Dialogue
{
    /// <summary>
    /// 對話系統的資料
    /// NPC 要對話的三個階段內容
    /// 接任務前、任務進行中、完成任務
    /// </summary>
    // ScriptableObject 繼承此類別會變成腳本化物件
    // 可將此腳本資料當成物件保存在專案 Project內
    // CreatAssetMenu 類別屬性 : 為此類別建立專案內選單
    // menuName 選單名稱 : 可用 / 分層
    // fileName 檔案名稱
    [CreateAssetMenu(menuName ="Wei/對話資料", fileName = "NPC 對話資料")]
    public class DataDialogue : ScriptableObject
    {
        //陣列:保存相同資料類型的結構
        //TextArea 字串用屬性，可設定行數
        [Header("任務前對話內容"), TextArea(2, 7)]
        public string[] beforeMission;
        [Header("任務進行中對話內容"), TextArea(2, 7)]
        public string[] Missionning;
        [Header("任務完成對話內容"), TextArea(2, 7)]
        public string[] afterMission;
        [Header("任務需求數量"), Range(0, 100)]
        public int countNeed;
        //使用列舉 : 
        //語法 : 修飾詞 列舉名稱 自定義欄位名稱;
        public StateNPCMission stateNPCMission = StateNPCMission.BeforeMission;
    }
}
