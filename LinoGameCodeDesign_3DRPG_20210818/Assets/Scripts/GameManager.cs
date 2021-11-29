using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LiangWei
{
    /// <summary>
    /// 遊戲管理器
    /// 結束處理
    /// 1. 任務完成
    /// 2. 玩家死亡
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region 欄位
        [Header("群組物件")]
        public CanvasGroup groupFinal;
        [Header("結束畫面標題")]
        public Text textTitle;

        private string titleWin = "You Win";
        private string titleLose = "You Failed..";
        #endregion
    }
}