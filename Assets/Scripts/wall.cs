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
    private float canvasHeight; // Canvas 高度
    Transform child;
    List<GameObject>  children;
    // 移动速度 (单位：米/秒)
    public Vector2 moveSpeed = new Vector2(0f, -100f); 

    void Awake()
    {
        // 获取 RectTransform 和 BoxCollider2D 组件
        rectTransform = GetComponent<RectTransform>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 将 BoxCollider2D 的尺寸与 RectTransform 的尺寸同步
        Vector2 size = rectTransform.rect.size;
        boxCollider.size = size;

        // 将 BoxCollider2D 的位置与 RectTransform 的中心同步
        Vector2 offset = rectTransform.rect.center;
        boxCollider.offset = offset;
        canvasHeight = gameObject.GetComponent<RectTransform>().rect.height;
        child = transform.GetChild(0);
        Image originalImage = child.GetComponent<Image>();
        RectTransform originalRect = child.GetComponent<RectTransform>();
        if (originalImage == null || originalRect == null)
        {
            Debug.LogError("第一个子物体缺少 Image 或 RectTransform 组件！");
            return;
        }
        children = new List<GameObject>();
        // 复制第一个子物体 7 次并设置高度和颜色
        for (int i = 0; i < 7; i++)
        {
            // 复制子物体
            GameObject newChild = Instantiate(child.gameObject, transform);
            newChild.name = $"Child_{i + 1}";

            // 设置 RectTransform 的高度
            RectTransform newRect = newChild.GetComponent<RectTransform>();
            newRect.sizeDelta = new Vector2(newRect.sizeDelta.x, canvasHeight * 0.2f);

            // 设置子物体的位置（从下到上排列）
            newRect.anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x, (i - 3)* canvasHeight * 0.2f);

            // 设置 Image 的颜色
            Image newImage = newChild.GetComponent<Image>();
            newImage.color = Random.ColorHSV(); // 如果没有颜色数组，随机设置颜色
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
