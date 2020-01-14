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
        foreach (Touch t in Input.touches)
        {
            var id = t.fingerId;

            switch (t.phase)
            {
                case TouchPhase.Began:

                    if (t.position.x < (Screen.width / 2) && tag == "LeftFripperTag")
                    {
                        SetAngle(this.flickAngle);
                        leftFlipperFlg = true;
                    }
                    if (t.position.x >= (Screen.width / 2) && tag == "RightFripperTag")
                    {
                        SetAngle(this.flickAngle);
                        rightFlipperFlg = true;
                    }

                    break;
                   
                case TouchPhase.Ended:

                    if (t.position.x < (Screen.width / 2))
                    {

                        if (leftFlipperFlg == true && tag == "LeftFripperTag")
                        {
                            SetAngle(this.defaultAngle);
                            leftFlipperFlg = false;
                        }
                        else if (leftFlipperFlg == false && tag == "RightFripperTag")
                        {
                            SetAngle(this.defaultAngle);
                            rightFlipperFlg = false;
                        }

                    }
                    else if (t.position.x >= (Screen.width / 2))
                    {
                        if (rightFlipperFlg == true && tag == "RightFripperTag")
                        {
                            SetAngle(this.defaultAngle);
                            rightFlipperFlg = false;
                        }
                        else if (rightFlipperFlg == false && tag == "LeftFripperTag")
                        {
                            SetAngle(this.defaultAngle);
                            leftFlipperFlg = false;
                        }

                    }

                    break;

            }
        }

    }

    //フリッパーの傾きを設定
    public void SetAngle(float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }

}
