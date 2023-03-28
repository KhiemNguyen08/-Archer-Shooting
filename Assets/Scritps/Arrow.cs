using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    bool m_isFiring = false;
    Rigidbody2D m_rb;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector2 vec = m_rb.velocity;
        float alpha = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, alpha);
    }
    public void Fire(float force)
    {
        if (!m_rb) return;
        
            m_rb.isKinematic = false;
        transform.parent = null;
        m_isFiring = true;
        m_rb.AddRelativeForce(new Vector2(force, 0), ForceMode2D.Force);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        TagetController tg = col.transform.root.GetComponent<TagetController>();
        if (col.gameObject.CompareTag(Const.APPLE_TAG))
        {
            var c2D = col.GetComponent<Collider2D>();
            //if (c2D)
            //    c2D.enabled = false;
            col.transform.SetParent(transform);
            GameManager.Ins.Score++;
            GameManager.Ins.SpawnTaget();
            GameGUIManager.Ins.UPdateApple(GameManager.Ins.Score);
            AudioController.Ins.PlaySound(AudioController.Ins.appleHit);

        }
        else if (col.gameObject.CompareTag(Const.HEAD_TAG))
        {
            GameManager.Ins.state = GameManager.GameSate.Gameover;
            GameManager.Ins.Gameover();
            AudioController.Ins.PlaySound(AudioController.Ins.bodyHit);
        }
        if (tg)
        {
            tg.Fall();
        }

    }
}
