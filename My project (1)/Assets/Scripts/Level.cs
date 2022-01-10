using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{

    [SerializeField] private RectTransform rect;
    [SerializeField] private Image image;
    [SerializeField] private bool newLevelFired;
    [SerializeField] Text levelText;


    public System.Action OnStartNewLevel;
    public System.Action<Level> OnFinishLevel;
    
    public Vector3 AnchoredPosition { get { return rect.anchoredPosition3D; } set { rect.anchoredPosition3D = value; } }
    public Vector2 Size { get { return rect.sizeDelta; } set { rect.sizeDelta = value; } }
    public Color BackColor { get { return image.color; } set { image.color = value; } }


    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (GameController.Instanse.State == GameController.GameState.PLAY)
        {
            AnchoredPosition += Vector3.down * Time.deltaTime * 400;
        }
    }

    private void LateUpdate()
    {
        if (!newLevelFired && AnchoredPosition.y < 500)
        {
            OnStartNewLevel?.Invoke();
            newLevelFired = true;
        }
        if (AnchoredPosition.y < -Size.y - 100)
        {
            OnFinishLevel?.Invoke(this);
        }
    }

    public void SetUp(Vector3 pos, Color color, int level)
    {
        newLevelFired = false;
        AnchoredPosition = pos;
        BackColor = color;
        levelText.text.ToString();
    }

    

}
