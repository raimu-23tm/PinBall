using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{

    //ボールが見える可能性のあるz軸の最大値
    private float visiblePosZ = -6.7f;

    //テキストオブジェクト
    private GameObject gameoverText;
    private GameObject pointText;

    // 現在の得点
    public static int currentPoint;

    // ゲームオーバー状態フラグ
    private bool gameOverFlg;

    private Rigidbody _rigidbody;


    // Use this for initialization
    void Start()
    {
        //テキストオブジェクト取得
        this.gameoverText = GameObject.Find("GameOverText");
        this.pointText = GameObject.Find("PointText");

        //初期設定
        currentPoint = 0;
        gameOverFlg = false;
        _rigidbody = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //ボールが画面外に出た場合
        if (this.transform.position.z < this.visiblePosZ)
        {
            //GameoverTextにゲームオーバを表示
            this.gameoverText.GetComponent<Text>().text = "Game Over";
            gameOverFlg = true;
            _rigidbody.velocity = Vector3.zero;

        }

        if (gameOverFlg == true)
        {
            //スペースキーを押すとリトライ
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RetryGame();
            }

            //（スマホの場合）タッチするとリトライ
            foreach (Touch t in Input.touches)
            {
                var id = t.fingerId;

                switch (t.phase)
                {
                    case TouchPhase.Began:

                        RetryGame();
                        break;
                }
            }

        }

        //超速防止
        if (_rigidbody.velocity.magnitude > 20)
        {
            Vector3 currentVelo = _rigidbody.velocity;
            currentVelo = currentVelo * (20 / _rigidbody.velocity.magnitude);
            _rigidbody.velocity = currentVelo;
        }

    }

    // Update is called once per frame
    void RetryGame()
    {
        this.gameoverText.GetComponent<Text>().text = "";
        this.pointText.GetComponent<Text>().text = "Point: 0";
        gameOverFlg = false;
        _rigidbody.velocity = Vector3.zero;
        currentPoint = 0;
        this.transform.position = new Vector3(3f, 2.8f, 4f);
    }

}
