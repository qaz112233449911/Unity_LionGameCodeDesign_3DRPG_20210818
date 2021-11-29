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

        #region 方法 : 公開
        /// <summary>
        /// 開始淡入最後介面
        /// </summary>
        /// <param name="win">是否獲勝</param>
        public void StatFadeFinalUI(bool win)
        {
            StartCoroutine(FadeFinalUI(win ? titleWin : titleLose));
        }
        #endregion

        #region 方法 : 私人
        /// <summary>
        /// 淡入結束畫面
        /// </summary>
        /// <param name="title">標題</param>
        /// <returns></returns>
        private IEnumerator FadeFinalUI(string title)
        {
            textTitle.text = title;
            groupFinal.interactable = true;
            groupFinal.blocksRaycasts = true;

            for (int i = 0; i < 10; i++)
            {
                groupFinal.alpha += 0.1f;
                yield return new WaitForSeconds(0.02f);
            }
        }
        #endregion
    }
}