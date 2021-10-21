using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LiangWei.Dialogue
{
    /// <summary>
    /// 對話系統
    /// 顯示對話框﹑對話內容打字效果
    /// </summary>
    public class DialogueSystem : MonoBehaviour
    {
        [Header("對話系統需要的介面物件")]
        public CanvasGroup groupDialogue;
        public Text textName;
        public Text textContent;
        public GameObject goTriangle;
        [Header("對話間隔"), Range(0, 10)]
        public float dialogueInterval = 0.3f;

        /// <summary>
        /// 開始對話
        /// </summary>
        public void Dialogue(DataDialogue data)
        {
            StartCoroutine(SwitchDialogueGroup());        //啟動協同程序
            StartCoroutine(ShowDialogueContent(data));
        }

        private IEnumerator SwitchDialogueGroup()
        {
            for (int i = 0; i < 10; i++)                  //迴圈指定執行次數
            {
                groupDialogue.alpha += 0.1f;              //群組元件 透明度 遞增
                yield return new WaitForSeconds(0.01f);   //等待時間
            }
        }

        private IEnumerator ShowDialogueContent(DataDialogue data)
        {
            textContent.text = "";
            textName.text = "";

            for (int i = 0; i < data.beforeMission[0].Length; i++)
            {
                textContent.text += data.beforeMission[0][i];
                yield return new WaitForSeconds(dialogueInterval);
            }
        }
    }
}