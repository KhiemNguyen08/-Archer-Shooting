using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Arrow arrowPbs;
    public float dragCtrl;
    public float fireForce;
    public Transform arrowPoint;
    public Transform arrowSpawnPoint;
    public Transform arrowDirection;

    public float m_minLimited;
    public float m_maxLimited;
    Vector2 m_dragPos1;
    Vector2 m_dragPos2;
    float m_dragDist;
    bool m_isDragging;
    Arrow m_arrowClone;
    private void Start()
    {
        SpawnArrow();
    }
    void Update()
    {
        if (GameManager.Ins.state != GameManager.GameSate.Playing) return;
        if (Input.GetButtonDown("Fire1"))
        {
            m_isDragging = true;
            m_dragPos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
           
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            m_isDragging = false;
            arrowPoint.localPosition = new Vector3(m_maxLimited, 0f, 0f);
            arrowDirection.localScale = new Vector3(0, 0, 0);
            if (m_dragDist > 0.1f)
            {
                Fire();
            }
        }
        if (m_isDragging)
        {
            m_dragPos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            m_dragDist = Vector2.Distance(m_dragPos1, m_dragPos2) * dragCtrl;
            if (m_dragDist < 0.05f)
                return;
            // tinh huong cua 2 vecto
           
            var dragDir = new Vector2(m_dragPos1.x - m_dragPos2.x, m_dragPos1.y - m_dragPos2.y);
            float alpha = Mathf.Atan2(dragDir.y, dragDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, alpha);

            float dragX = m_maxLimited - m_dragDist;
            dragX = Mathf.Clamp(dragX, m_minLimited, m_maxLimited);
            arrowPoint.localPosition = new Vector3(dragX, 0, 0);

            float dirPointScaleX = Mathf.Clamp(m_dragDist, 0, 0.5f) * 2;
            arrowDirection.localScale = new Vector3(dirPointScaleX, 1, 1);
        }
        
    }
    void SpawnArrow()
    {
        if (arrowPbs == null) return;
        m_arrowClone = Instantiate(arrowPbs);
        m_arrowClone.transform.SetParent(arrowSpawnPoint, false);
        m_arrowClone.transform.localScale = Vector3.one;
        m_arrowClone.transform.localPosition = Vector3.zero;
    }
    IEnumerator SpawnNextArrow(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnArrow();
    }
    public void Fire()
    {
        if (m_arrowClone == null) return;
        float curForce = Mathf.Clamp(m_dragDist, 0, 0.5f) * fireForce;
        m_arrowClone.Fire(curForce);
        StartCoroutine(SpawnNextArrow(0.2f));
    }
}
