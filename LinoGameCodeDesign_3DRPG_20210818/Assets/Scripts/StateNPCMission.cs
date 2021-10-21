namespace LiangWei.Dialogue
{
    //列舉 : enum (enumeration)下拉式選單，可自行定義選項
    //語法 : 修飾詞 列舉 列舉名稱 { 列舉內容1， ...， 列舉內容n}
    //定義列舉 : 
    /// <summary>
    /// NPC 任務狀態列舉
    /// 接任務前、任務進行中、完成任務
    /// </summary>
    public enum StateNPCMission
    {
        BeforeMission, Missionning, AfterMission
    }
}