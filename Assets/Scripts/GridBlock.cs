using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GridBlock : MonoBehaviour
{
    public int blockIndex;

    private Button button;
    private Image image;

    Color normalColor = new Color(0.85f, 0.85f, 0.85f);
    Color glowColor = new Color(1f, 0.9f, 0.4f);

    public float t = 0.5f;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        image.color = normalColor;

        button.onClick.AddListener(OnClicked);
    }

    void OnClicked()
    {
        GameManager.Instance.BlockPressed(blockIndex);
    }

    public IEnumerator Flash()
    {
        image.color = glowColor;

        yield return new WaitForSeconds(t);

        image.color = normalColor;
    }

    public void SetInteractable(bool value)
    {
        button.interactable = value;
    }
}