using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitArea : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerExit2D(Collider2D col)
    {
        Destroy(col.gameObject);
        if(col.tag == Const.APPLE_TAG )
        {
            GameManager.Ins.SpawnTaget();
        }
    }
}
