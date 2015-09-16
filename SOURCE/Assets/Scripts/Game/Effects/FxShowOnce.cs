using UnityEngine;
using System.Collections;

public class FxShowOnce : MonoBehaviour
{
    public float MoveYValue = 2;
    const float MoveTime = 0.6f;
    void OnEnable()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color col = sr.color;
        col.a = 1;
        sr.color = col;
        Vector3 vPos = gameObject.transform.position;
        Vector3 vMoveTo = new Vector3(vPos.x, vPos.y + MoveYValue, vPos.z);
        LeanTween.move(gameObject, vMoveTo, MoveTime);
        LeanTween.alpha(gameObject, 0, MoveTime).setOnComplete(OnComplete);

    }


    void OnComplete()
    {
        TrashMan.despawn(gameObject);
    }
}