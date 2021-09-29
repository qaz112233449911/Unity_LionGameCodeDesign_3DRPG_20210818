using System.Collections;
using System.Collections.Generic;
using UnityEngine;       //引用 Unity API(倉庫-資料與功能)
using UnityEngine.Video;  //引用 影片 API

//修飾詞  類別             類別名稱  :  繼承類別
//MonoBehaviour : Unity 基底類別 ， 要掛在物件上一定要繼承
//繼承後會享有該類別的成員
//在類別以及成員上方添加三條斜線會添加摘要
//常用成員 : 欄位 Field、屬性 Property(變數)、方法 Method、事件 Event
/// <summary>
/// LiangWei 2021.09.06
/// 第三人稱控制器
/// 移動、跳躍
/// </summary>
public class ThirdPersonController : MonoBehaviour
{
    #region 欄位 Field
    //儲存遊戲資料，例如 : 移動速度、跳躍高度等等...
    //常用四大類型 : 整數 int、浮點數 float、字串 string、布林值 bool
    //欄位語法 : 修飾詞 資料類型 欄位名稱 (指定 預設值) 結尾
    //修飾詞:
    //1. 公開 public   -  允許其他類別存取 - 顯示在屬性面板 - 需要調整的資料設定為公開
    //2. 私人 private  -  禁止其他類別存取 - 隱藏在屬性面板 - 預設值
    // Unuty以屬性面板為主
    // 恢復預設值按... > Reset
    //欄位屬性 Attribute : 輔助欄位資料
    //欄位屬性語法 : [屬性名稱(屬性值)]
    //Header 標題 、 Tooltip 提示 : 滑鼠停留在欄位名稱上會顯示彈出視窗 、 Range 範圍 : 可使用在數值類型資料上, 例如 : int, float
    [Header("移動速度"), Tooltip("用來調整角色移動速度"), Range(1, 500)]
    public float speed = 10.5f;
    [Header("跳躍高度"), Tooltip("用來調整角色跳躍高度"), Range(0, 1000)]
    public float jump = 100f;

    [Header("檢查地面資料"), Tooltip("檢查角色是否在地板上")]
    public bool isGrounded;
    public Vector3 v3CheckGroundoffset;
    public float CheckGroundRadius = 0.2f;

    [Header("跳躍音效")]
    public AudioClip jumpsound;
    [Header("落地音效")]
    public AudioClip Landingsound;

    [Header("動畫參數")]
    public string animatirParWalk = "走路開關";
    public string animatirParRun = "跑步開關";
    public string animatirParHurt = "受傷開關";
    public string animatirParDead = "死亡開關";

    private AudioSource aud;
    private Rigidbody rig;
    private Animator ani;
    #region Unity 資料類型
    /** 練習Unity 資料類型
    //顏色 Color
    public Color color;
    public Color white = Color.white;       //內建顏色
    public Color yellow = Color.yellow;
    public Color color1 = new Color(0.5f, 0.5f, 0);               //自訂顏色 RGB
    public Color color2 = new Color(0, 0.5f, 0.5f, 0.5f);         //自訂顏色 RGBA

    // 座標 Vector 2 - 4
    public Vector2 v2;
    public Vector2 v2Right = Vector2.right;
    public Vector2 v2Up = Vector2.up;
    public Vector2 v2One = Vector2.one;
    public Vector2 v2Custom = new Vector2(7.5f, 100.9f);
    public Vector3 v3 = new Vector3(1, 2, 3);
    public Vector4 v4 = new Vector4(1, 2, 3, 4);

    // 按鍵 列舉資料 enum
    public KeyCode key;
    public KeyCode move = KeyCode.W;
    public KeyCode jump = KeyCode.Space;

    //遊戲資料類型：不能指定預設值
    public AudioClip sound;      //音效 MP3，ogg，wav
    public VideoClip video;      //影片 MP4
    public Sprite sprite;        //圖片 png，jpeg - 不支援 gif
    public Texture2D texture2D;  //2D 圖片 png， jpeg
    public Material material;    //材質球

    //元件 Component : 屬性面板上可折疊的
    public Transform tra;
    public Animation aniOld;
    public Animator aniNew;
    public Light lig;
    public Camera cam;

    //綠色蚯蚓
    //1. 建議不要使用此名稱 2.使用過時的API
    /**/
    #endregion

    #endregion

    #region 屬性 Property
    /**屬性練習
    // 儲存資料，與欄位相同
    // 差異在於 : 可以設定存取權限 Get Set
    // 屬性語法 : 修飾詞 資料類型 屬性名稱 { 取; 存; }
    public int readAndWrite { get; set; }
    // 唯獨屬性 : 只能取得 get
    public int read { get; }
    // 唯獨屬性 : 透過 get 設定預設值，關鍵字 return 為傳回值
    public int readValue 
    {
        get
        {
            return 77;
        }
    }
    // 唯寫屬性 : 禁止，必須要有 get
    // public int write { set; }
    // value 指的是指定的值
    private int _hp;
    public int hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    */
    public KeyCode keyjump { get; }
    #endregion

    #region 方法 Method
    //定義與實作較複雜程式的區塊、功能
    //方法語法 : 修飾詞 傳回資料類型 方法名稱 (參數1， ...參數N) { 程式區塊 }
    //常用傳回類型 : 無傳回 void - 此方法沒有傳回資料
    //格式化 : Ctrl + K D
    //自訂方法 :
    //自訂方法需要被呼叫才會執行方法內的程式
    //名稱顏色為淡黃色 - 沒有被呼叫
    //名稱顏色為亮黃色 - 有被呼叫
    /*練習方法
    private void Test()
    {
        print("我是自訂方法~");
        print("練習練習~");
    }
    
    private int ReturnJump() 
    {
        return 999;
    }
    
    //參數語法 : 資料類型 參數名稱
    //有預設值的參數可以不輸入引數，選填式參數
    private void Skill(int damage,string effect = "灰塵特效",string sound = "嘎嘎嘎")
    {
        print("參數版本 - 傷害值 : " + damage);
        print("參數版本 - 技能特效 : "+ effect);
        print("參數版本 - 音效 : " + sound);
    }
    /*錯誤 : 選填式參數沒有在()右邊
    private void ErrorSkill(string effect = "灰塵特效",int damage)
    {

    }
    
    //對照組 : 不使用參數
    //降低維護與擴充性
    private void Skill100()
    {
        print("傷害值 : " + 100);
        print("技能特效");
    }
    private void Skill150()
    {
        print("傷害值 : " + 150);
        print("技能特效");
    }
    private void Skill200()
    {
        print("傷害值 : " + 200);
        print("技能特效");
    }
    
    //BMI = 體重 / 身高 * 身高 (公尺)
    //非必要但很重要
    /// <summary>
    /// 計算 BMI 方法
    /// </summary>
    /// <param name="weight">體重，單位為公斤</param>
    /// <param name="height">身高，單位為公尺</param>
    /// <param name="name">名稱，測量者名稱</param>
    /// <returns></returns>
    private float BMI(float weight, float height, string name = "測試")
    {
        print(name + " 的 BMI ");

        return weight / (height * height);
    }
    */
    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="speedMove">移動速度</param>
    private void Move(float speedMove)
    {
        //請取消 Animator 屬性 Apply Root Motion : 勾選時使用動畫位移資訊
        //剛體.加速度 = 三維向量 - 加速度用來控制剛體三個軸向的運動速度
        //前方*輸入值*移動速度
        //使用前後左右軸向運動並且保持原本的地心引力
        rig.velocity =
            Vector3.forward * MoveInput("Vertical") * speedMove +
            Vector3.right * MoveInput("Horizontal") * speedMove +
            Vector3.up * rig.velocity.y;
    }

    /// <summary>
    /// 移動按鍵輸入
    /// </summary>
    /// <param name="axisName">要取得的軸向名稱</param>
    /// <returns>移動按鍵值</returns>
    private float MoveInput(string axisName)
    {
        return Input.GetAxis(axisName);
    }

    /// <summary>
    /// 檢查地板
    /// </summary>
    /// <returns>是否碰到地板</returns>
    private bool CheckGround()
    {
        //物理.覆蓋球體(中心點，半徑，圖層)
        Collider[] hits = Physics.OverlapSphere(transform.position
            + transform.right * v3CheckGroundoffset.x
            + transform.up * v3CheckGroundoffset.y
            + transform.forward * v3CheckGroundoffset.z
            , CheckGroundRadius, 1 << 3);

        //print("球體碰到的第一個物件 : " + hits[0].name);

        //傳回 碰撞陣列數量 > 0 - 只要碰到指定圖層物件就代表在地面上
        return hits.Length > 0;
    }
    private void Jump()
    {
        print("是否在地面上 : " + CheckGround());
    }
    private void updateAnimation()
    {

    }
    #endregion

    public GameObject playerObject;
    #region 事件 Event
    // 特定時間點會執行的方法，程式的入口 Start 等於 Console Main
    // 開始事件 : 遊戲開始時執行一次 - 處理初始化、取得資料等等
    private void Start()
    {
        /**事件練習
        print(BMI(56, 1.73f, "LiangWei"));
        print(BMI(66, 1.85f, "測試"));

        Skill100();
        Skill150();
        Skill200();
        //呼叫有參數方法時，必須輸入對應的引數
        Skill(300);
        Skill(999, "爆炸特效");
        //需求 : 傷害值 500，特效用預設值，音效換成 咻咻咻
        //有多個選填式參數時可使用指名參數語法 : 參數名稱 : 值
        Skill(500, sound: "咻咻咻");
        #region 輸出 方法
        /**
        print("哈囉，沃德");

        Debug.Log("一般訊息");
        Debug.LogWarning("警告訊息");
        Debug.LogError("錯誤訊息");
        */
        #endregion

        /**屬性練習
        print("欄位資料 - 移動速度 :" + speed);
        print("屬性資料 - 讀寫屬性 :" + readAndWrite);
        speed = 20.5f;
        readAndWrite = 90;
        print("修改後的資料");
        print("欄位資料 - 移動速度 :" + speed);
        print("屬性資料 - 讀寫屬性 :" + readAndWrite);
        //唯獨屬性
        //read = 7;   //唯獨屬性不能設定 set
        print("唯獨屬性 :" + read);
        print("唯獨屬性，有設預設值 :" + readValue);

        //屬性存取練習
        print("HP :" + hp);
        hp = 100;
        print("HP :" + hp);
        

        //呼叫自訂方法語法 : 方法名稱();
        Test();
        Test();
        //呼叫有傳回值的方法
        //1. 區域變數指定傳回值 - 區域變數僅能在此結構 (大括號) 內存取
        int j = ReturnJump();
        print("跳躍值 : " + j);
        //2. 將傳回方當成值使用
        print("跳躍值，當值使用 : " + (ReturnJump() + 1));
        */

        //取得元件的方式
        //1. 物件欄位名稱.取得元件(類型(元件類型))當作 元件類型
        aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
        //2. 此腳本遊戲物件.取得元件<泛型>();
        rig = gameObject.GetComponent<Rigidbody>();
        //3. 取得元件<泛型>();
        //類別可以使用繼承類別(父類別)的成員，公開或保護 欄位、屬性與方法
        ani = GetComponent<Animator>();
    }

    // 更新事件 : 一秒約執行60次，60FPS - Frame Per Second
    // 處理持續性運動，移動物件，監聽玩家輸入按鍵
    private void Update()
    #region
    {
        CheckGround();
        Jump();
    }
    
    //固定更新事件: 固定0.02秒執行一次 - 50FPS
    //處理物理行為，例如:Rigidbody API
    private void FixedUpdate()
    {
        Move(speed);
    }

    //繪製圖示事件
    //在Unity Editor 內繪製圖示輔助開發，發布後會自動隱藏
    private void OnDrawGizmos()
    {
        // 1 . 指定顏色
        // 2 . 繪製圖形
        Gizmos.color = new Color(1, 0, 0.2f, 0.3f);

        //transform 與此腳本在同階層的 Transform元件
        Gizmos.DrawSphere(
            transform.position
            + transform.right * v3CheckGroundoffset.x
            + transform.up * v3CheckGroundoffset.y
            + transform.forward * v3CheckGroundoffset.z
            , CheckGroundRadius);
    }
    #endregion
}
