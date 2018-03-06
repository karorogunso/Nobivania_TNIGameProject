using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ItemController : MonoBehaviour {

    public ItemType Type;
    public static ItemType Current;

    public AnimationCurve FloatingCurve;

    const float FloatSpeed = 0.6f;
    const float MaxDistance = 0.5f;

    private Vector3 StartPosition;
    private float FloatPosition;

    private Image UI;

#if UNITY_EDITOR
    private void OnGUI()
    {
        FloatingCurve = EditorGUILayout.CurveField(FloatingCurve);
    }
#endif

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collosion2d" + collision.gameObject.tag);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger2d" + collision.gameObject.tag);
        if (collision.tag == "Player")
        {
            Current = Type;
            UI.sprite = GetComponent<SpriteRenderer>().sprite;
            UI.color = new Color(1, 1, 1, 1);
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        StartPosition = transform.position;

    }

    private void Update()
    {
        FloatPosition += Time.deltaTime * FloatSpeed;
        if (FloatPosition > 1f)
            FloatPosition -= 1f;
        float value = FloatingCurve.Evaluate(FloatPosition);
        value *= MaxDistance;
        Vector3 position = StartPosition;
        position.y += value;
        transform.position = position;
        
    }
}
