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

    public Animator PlayerAnimator;

#if UNITY_EDITOR
    private void OnGUI()
    {
        FloatingCurve = EditorGUILayout.CurveField(FloatingCurve);
    }
#endif

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            Current = Type;
            UI.sprite = GetComponent<SpriteRenderer>().sprite;
            UI.color = new Color(1, 1, 1, 1);
            Destroy(this.gameObject);
            PlayerAnimator.SetBool("Cannon", Type == ItemType.AirCannon);
            
        }
    }

    private void Start()
    {
        StartPosition = transform.position;
        UI = GameObject.Find("ItemUI").GetComponent<Image>();
        PlayerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
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
