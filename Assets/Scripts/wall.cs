using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(BoxCollider2D))]
public class wall : MonoBehaviour
{
    private RectTransform rectTransform;
    private BoxCollider2D boxCollider;
    private float canvasHeight; // Canvas �߶�
    Transform child;
    List<GameObject>  children;
    // �ƶ��ٶ� (��λ����/��)
    public Vector2 moveSpeed = new Vector2(0f, -100f); 

    void Awake()
    {
        // ��ȡ RectTransform �� BoxCollider2D ���
        rectTransform = GetComponent<RectTransform>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // �� BoxCollider2D �ĳߴ��� RectTransform �ĳߴ�ͬ��
        Vector2 size = rectTransform.rect.size;
        boxCollider.size = size;

        // �� BoxCollider2D ��λ���� RectTransform ������ͬ��
        Vector2 offset = rectTransform.rect.center;
        boxCollider.offset = offset;
        canvasHeight = gameObject.GetComponent<RectTransform>().rect.height;
        child = transform.GetChild(0);
        Image originalImage = child.GetComponent<Image>();
        RectTransform originalRect = child.GetComponent<RectTransform>();
        if (originalImage == null || originalRect == null)
        {
            Debug.LogError("��һ��������ȱ�� Image �� RectTransform �����");
            return;
        }
        children = new List<GameObject>();
        // ���Ƶ�һ�������� 7 �β����ø߶Ⱥ���ɫ
        for (int i = 0; i < 7; i++)
        {
            // ����������
            GameObject newChild = Instantiate(child.gameObject, transform);
            newChild.name = $"Child_{i + 1}";

            // ���� RectTransform �ĸ߶�
            RectTransform newRect = newChild.GetComponent<RectTransform>();
            newRect.sizeDelta = new Vector2(newRect.sizeDelta.x, canvasHeight * 0.2f);

            // �����������λ�ã����µ������У�
            newRect.anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x, (i - 3)* canvasHeight * 0.2f);

            // ���� Image ����ɫ
            Image newImage = newChild.GetComponent<Image>();
            newImage.color = Random.ColorHSV(); // ���û����ɫ���飬���������ɫ
            children.Add(newChild);
        }
        
    }

    private void FixedUpdate()
    {
        float chiledPosition = 0;
        for (int i = 0; i < 7; i++)
        {
            children[i].GetComponent<RectTransform>().anchoredPosition += moveSpeed * Time.deltaTime;

            chiledPosition = children[i].GetComponent<RectTransform>().anchoredPosition.y;
            if (chiledPosition < (-3f * canvasHeight * 0.2f))
            {
                Vector2 anchoredPosition = new Vector2(children[i].GetComponent<RectTransform>().anchoredPosition.x, 3f * canvasHeight * 0.2f);
                children[i].GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
            }

        }
    }

}
