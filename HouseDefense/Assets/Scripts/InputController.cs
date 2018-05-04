using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour {
    public Texture2D CursorTexture;

	// Use this for initialization
	void Start () {

        //Cursor.SetCursor(CursorTexture, new Vector2(35, 35), CursorMode.ForceSoftware);
    }
	
	// Update is called once per frame
	void Update () {

        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
        //}
        //else
        //{
        //    Cursor.SetCursor(CursorTexture, new Vector2(35, 35), CursorMode.ForceSoftware);
        //}
    }
}
