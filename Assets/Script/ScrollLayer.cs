using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollLayer : MonoBehaviour {

    public float scrollSpeed;
    private Vector2 savedOffset;
    private SpriteRenderer renderer;
    // Use this for initialization
    void Start () {
        renderer = GetComponent<SpriteRenderer>();
        savedOffset = renderer.sharedMaterial.GetTextureOffset("_MainTex");
    }
	
	// Update is called once per frame
	void Update () {
        float y = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(savedOffset.x, y);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void OnDisable()
    {
        renderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}
