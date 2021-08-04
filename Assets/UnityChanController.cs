using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;
    //Unityちゃんを移動させるコンポーネントを入れる（追加）
    private Rigidbody myRigidbody;
    //前方向の速度（追加）
    private float velocityZ = 16f;

    //上方向の速度（追加）
    private float velocityY = 10f;

    private float velocityX = 10f;
    //左右の移動できる範囲（追加）
    private float movableRange = 3.4f;

    //動きを減速させる係数（追加）
    private float coefficient = 0.99f;
    //ゲーム終了の判定（追加）
    private bool isEnd = false;
    private GameObject stateText;
    private GameObject scoreText;
    //得点（追加）
    private int score = 0;
    //左ボタン押下の判定（追加）
    private bool isLButtonDown = false;
    //右ボタン押下の判定（追加）
    private bool isRButtonDown = false;
    //ジャンプボタン押下の判定（追加）
    private bool isJButtonDown = false;

    // Use this for initialization
    void Start()
    {
        //アニメータコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", 1);

        //Rigidbodyコンポーネントを取得（追加）
        this.myRigidbody = GetComponent<Rigidbody>();
        this.stateText = GameObject.Find("GameResultText");
        //シーン中のscoreTextオブジェクトを取得（追加）
        this.scoreText = GameObject.Find("ScoreText");

    }

    // Update is called once per frame
    void Update()
    {
        float inputVelocityX = 0;
        float inputVelocityY = 0;
        //ゲーム終了ならUnityちゃんの動きを減衰する（追加）
        if (this.isEnd)
        {
            this.velocityZ *= this.coefficient;
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }
        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる（追加）

        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる（追加）
        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            //左方向への速度を代入
            inputVelocityX = -this.velocityX;
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            //右方向への速度を代入
            inputVelocityX = this.velocityX;
        }

        //ジャンプしていない時にスペースまたはボタンが押されたらジャンプする（追加）
        if ((Input.GetKeyDown(KeyCode.Space) || this.isJButtonDown) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生
            this.myAnimator.SetBool("Jump", true);
            //上方向への速度を代入
            inputVelocityY = this.velocityY;
        }
        else
        {
            //現在のY軸の速度を代入
            inputVelocityY = this.myRigidbody.velocity.y;
        }

       

        //Jumpステートの場合はJumpにfalseをセットする（追加）
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }
       
        //Unityちゃんに速度を与える（変更）
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, velocityZ);
    }
    void OnTriggerEnter(Collider other)
    {

        //障害物に衝突した場合（追加）
        if (other.gameObject.tag == "GarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "GAMEOVER";
        }

        //ゴール地点に到達した場合（追加）
        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }
        //コインに衝突した場合（追加）
        if (other.gameObject.tag == "CoinTag")
        {
            //接触したコインのオブジェクトを破棄（追加）
            Destroy(other.gameObject);
            GetComponent<ParticleSystem>().Play();
            // スコアを加算(追加)
            this.score += 10;

            //ScoreText獲得した点数を表示(追加)
            this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";

        }
    }

    public void GetMyJumpButtonDown()
    {
        this.isJButtonDown = true;
    }

    //ジャンプボタンを離した場合の処理（追加）
    public void GetMyJumpButtonUp()
    {
        this.isJButtonDown = false;
    }

    //左ボタンを押し続けた場合の処理（追加）
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }
    //左ボタンを離した場合の処理（追加）
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    //右ボタンを押し続けた場合の処理（追加）
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    //右ボタンを離した場合の処理（追加）
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }
}


