using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ItemController : MonoBehaviour {

    public ItemType Type;
    public static ItemType Current;

    public AnimationCurve FloatingCurve;

    const float FloatSpeed = 0.6f;
    const float MaxDistance = 0.5f;

    private Vector3 StartPosition;
    private float FloatPosition;

    private void OnGUI()
    {
        FloatingCurve = EditorGUILayout.CurveField(FloatingCurve);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Current = Type;
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
