using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private Rigidbody2D rb;

    float canvasWidth = 309.985f * 2;  // Canvas �Ŀ��
    float canvasHeight = 1320f; // Canvas �ĸ߶�
    public float jumpHeight = 150f;
    float timeToReach = 0.25f;  // ��Ծ����ʱ��
    public float gravity = 0.0f;    // �������ٶ�
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
        //UpdateGravityDirection(); // ��ʼ����������
        gravity = 8.0f * jumpHeight / Mathf.Pow(timeToReach,2.0f);
        Debug.Log("gravity,,"+ gravity);
        initialVelocity = new Vector2(1, 0);
        //���ó�ʼλ�á��ٶȺ��������ٶ�
        transform.position = new Vector2(-309.985f, -400f);
        rb.velocity = new Vector2(0.0f, 0.0f);
        Physics2D.gravity = new Vector2(0f, 0f);
        position = new Vector2(transform.position.x, transform.position.y);
    }
    private void Update()
    {

        // ���� G ���л���������
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("jump????");
            initialVelocity = CalculateInitialVelocity(canvasWidth, jumpHeight);

            if (transform.position.x < 0f)
            {
                //������
                rb.velocity = new Vector2(initialVelocity.x, initialVelocity.y);
            }
            else
            {
                //������
                rb.velocity = new Vector2(-initialVelocity.x, initialVelocity.y);
            }

            Physics2D.gravity = new Vector2(0, -gravity);
            Debug.Log("ˮƽʱ�䣺" + (canvasWidth / initialVelocity.x) +
                "   ��ֱʱ��:" + Mathf.Sqrt(2 * jumpHeight / gravity) * 2 +
                ((canvasWidth / initialVelocity.x) == Mathf.Sqrt(2 * jumpHeight / gravity) * 2));
        }
        /*Debug.LogError("ˮƽʱ�䣺" + canvasWidth / initialVelocity.x +"   ��ֱʱ��:" + jumpHeight / initialVelocity.y);*/

        Velocity = rb.velocity;
    }
    void FixedUpdate()
    {
        // ���� G ���л���������
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("jump????");
            /*            initialVelocity = CalculateInitialVelocity(canvasWidth, jumpHeight) ;

                        if (transform.position.x < 0f)
                        {
                            //������
                            rb.velocity = new Vector2(initialVelocity.x, initialVelocity.y);
                        }
                        else
                        {
                            //������
                            rb.velocity = new Vector2(-initialVelocity.x, initialVelocity.y);
                        }

                        Physics2D.gravity = new Vector2(0, -gravity);
                        Debug.Log("ˮƽʱ�䣺" + (canvasWidth / initialVelocity.x) +
                            "   ��ֱʱ��:" + Mathf.Sqrt(2* jumpHeight/gravity)*2 + 
                            ((canvasWidth / initialVelocity.x) == Mathf.Sqrt(2 * jumpHeight / gravity) * 2));*/
        }
        /*Debug.LogError("ˮƽʱ�䣺" + canvasWidth / initialVelocity.x +"   ��ֱʱ��:" + jumpHeight / initialVelocity.y);*/

        Velocity = rb.velocity;

    }

    Vector2 CalculateInitialVelocity(float width, float height)
    {
        // ����ˮƽ������ٶ�
        float vx = width / timeToReach;

        // ���㴹ֱ������ٶ�
        //float vy = gravity * timeToReach;
        float vy = 0.5f * gravity * timeToReach * test;

        // ���س��ٶ�����
        return new Vector2(vx * Mathf.Sign(initialVelocity.x), vy);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"��ײ����: {collision.gameObject.name}");
        rb.velocity = new Vector2(0.0f, 0.0f);
        Physics2D.gravity = new Vector2(0f, 0f);
        Debug.Log(position - new Vector2(transform.position.x, transform.position.y));
        position = new Vector2(transform.position.x, transform.position.y);
    }

}
