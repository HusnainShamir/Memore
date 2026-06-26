using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public int blockIndex;
    public GameManager gameManager;

    private Image rend;
    private Color defaultColor;

    private void Awake()
    {
        rend = GetComponent<Image>();
        defaultColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        gameManager.BlockClicked(blockIndex);
    }

    public void LightOn()
    {
        rend.material.color = Color.white;
    }

    public void LightOff()
    {
        rend.material.color = defaultColor;
    }
}