using UnityEngine;
using System.Collections;

public class FripperController : MonoBehaviour {

    //HingeJointコンポーネントを入れる
    private HingeJoint myHingeJoint;

    //初期の傾き
    private float defaultAngle = 20;
    //弾いた時の傾き
    private float flickAngle = -20;

    //フリッパーフラグ
    private bool rightFlipperFlg = false;
    private bool leftFlipperFlg = false;

    /// <summary>フリッパーを操作している fingerId</summary>
    int fingerId = -1;


    // Use this for initialization
    void Start()
    {
        //HingeJointコンポーネント取得
        this.myHingeJoint = GetComponent<HingeJoint>();

        //フリッパーの傾きを設定
        SetAngle(this.defaultAngle);
    }

    // Update is called once per frame
    void Update()
    {

        //左矢印キーを押した時左フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.LeftArrow) && tag == "LeftFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        //右矢印キーを押した時右フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.RightArrow) && tag == "RightFripperTag")
        {
            SetAngle(this.flickAngle);
        }

        //矢印キー離された時フリッパーを元に戻す
        if (Input.GetKeyUp(KeyCode.LeftArrow) && tag == "LeftFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && tag == "RightFripperTag")
        {
            SetAngle(this.defaultAngle);
        }

        //タッチ操作に対応
        #region touchControl
        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x > Screen.width / 2 && tag == "RightFripperTag")
                {
                    fingerId = t.fingerId;
                    SetAngle(this.flickAngle);
                }
                else if (t.position.x <= Screen.width / 2 && tag == "LeftFripperTag")
                {
                    fingerId = t.fingerId;
                    SetAngle(this.flickAngle);
                }
            }
            else if (t.phase == TouchPhase.Ended)
            {
                if (t.fingerId == fingerId && tag == "RightFripperTag")
                {
                    fingerId = -1;
                    SetAngle(this.defaultAngle);
                }
                else if (t.fingerId == fingerId && tag == "LeftFripperTag")
                {
                    fingerId = -1;
                    SetAngle(this.defaultAngle);
                }
            }
        }
        #endregion

    }

    //フリッパーの傾きを設定
    public void SetAngle(float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }

}
