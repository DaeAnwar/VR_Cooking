using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PopUpTool : MonoBehaviour
{

    [SerializeField]  TMP_Text NotficationText;
    [SerializeField] public GameObject NotificationPanel;
    [SerializeField]    RectTransform  NotificationPanelTransform;
    
    [SerializeField] Image image;
    public void SetText(string Text)
    {
        NotficationText.text = Text;


    }
    public void SetPosition(Vector3 notificationPanelTransform, Quaternion notificationPanelRotation)
    {
        NotificationPanelTransform.position = notificationPanelTransform;
        NotificationPanelTransform.rotation = notificationPanelRotation; 
    }
    public void SetColor(Color color)
    {
        image.color = color;
    }
}



