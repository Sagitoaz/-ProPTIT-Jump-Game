using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] protected bool _destroyOnClose;
    public virtual void OpenPanel()
    {
        gameObject.SetActive(true);
    }
    public virtual void ClosePanel()
    {
        gameObject.SetActive(false);
        if (_destroyOnClose)
        {
            PanelManager.Instance.ClosePanel(name);
            Destroy(gameObject);
        }
    }
}
