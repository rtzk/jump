using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private Rigidbody2D rb;

    float canvasWidth = 309.985f * 2;  // Canvas 的宽度
    float canvasHeight = 1320f; // Canvas 的高度
    public float jumpHeight = 150f;
    float timeToReach = 0.25f;  // 跳跃所需时间
    public float gravity = 0.0f;    // 重力加速度
    Vector2 initialVelocity;
    public Vector2 Velocity;
    Vector2 position;
    public float test = 1;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("PlayerController: Rigidbody2D component is missing!");
        }
    }

    void Start()
    {
        //UpdateGravityDirection(); // 初始化重力方向
        gravity = 8.0f * jumpHeight / Mathf.Pow(timeToReach,2.0f);
        Debug.Log("gravity,,"+ gravity);
        initialVelocity = new Vector2(1, 0);
        //设置初始位置、速度和重力加速度
        transform.position = new Vector2(-309.985f, -400f);
        rb.velocity = new Vector2(0.0f, 0.0f);
        Physics2D.gravity = new Vector2(0f, 0f);
        position = new Vector2(transform.position.x, transform.position.y);
    }
    private void Update()
    {

        // 按下 G 键切换重力方向
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("jump????");
            initialVelocity = CalculateInitialVelocity(canvasWidth, jumpHeight);

            if (transform.position.x < 0f)
            {
                //向右跳
                rb.velocity = new Vector2(initialVelocity.x, initialVelocity.y);
            }
            else
            {
                //向左跳
                rb.velocity = new Vector2(-initialVelocity.x, initialVelocity.y);
            }

            Physics2D.gravity = new Vector2(0, -gravity);
            Debug.Log("水平时间：" + (canvasWidth / initialVelocity.x) +
                "   垂直时间:" + Mathf.Sqrt(2 * jumpHeight / gravity) * 2 +
                ((canvasWidth / initialVelocity.x) == Mathf.Sqrt(2 * jumpHeight / gravity) * 2));
        }
        /*Debug.LogError("水平时间：" + canvasWidth / initialVelocity.x +"   垂直时间:" + jumpHeight / initialVelocity.y);*/

        Velocity = rb.velocity;
    }
    void FixedUpdate()
    {
        // 按下 G 键切换重力方向
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("jump????");
            /*            initialVelocity = CalculateInitialVelocity(canvasWidth, jumpHeight) ;

                        if (transform.position.x < 0f)
                        {
                            //向右跳
                            rb.velocity = new Vector2(initialVelocity.x, initialVelocity.y);
                        }
                        else
                        {
                            //向左跳
                            rb.velocity = new Vector2(-initialVelocity.x, initialVelocity.y);
                        }

                        Physics2D.gravity = new Vector2(0, -gravity);
                        Debug.Log("水平时间：" + (canvasWidth / initialVelocity.x) +
                            "   垂直时间:" + Mathf.Sqrt(2* jumpHeight/gravity)*2 + 
                            ((canvasWidth / initialVelocity.x) == Mathf.Sqrt(2 * jumpHeight / gravity) * 2));*/
        }
        /*Debug.LogError("水平时间：" + canvasWidth / initialVelocity.x +"   垂直时间:" + jumpHeight / initialVelocity.y);*/

        Velocity = rb.velocity;

    }

    Vector2 CalculateInitialVelocity(float width, float height)
    {
        // 计算水平方向初速度
        float vx = width / timeToReach;

        // 计算垂直方向初速度
        //float vy = gravity * timeToReach;
        float vy = 0.5f * gravity * timeToReach * test;

        // 返回初速度向量
        return new Vector2(vx * Mathf.Sign(initialVelocity.x), vy);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"碰撞对象: {collision.gameObject.name}");
        rb.velocity = new Vector2(0.0f, 0.0f);
        Physics2D.gravity = new Vector2(0f, 0f);
        Debug.Log(position - new Vector2(transform.position.x, transform.position.y));
        position = new Vector2(transform.position.x, transform.position.y);
    }

}
