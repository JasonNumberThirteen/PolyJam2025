using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CursorImageUI : MonoBehaviour
{
	private RectTransform rectTransform;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	private void OnEnable()
	{
		Cursor.visible = false;
	}

	private void OnDisable()
	{
		Cursor.visible = true;
	}
	
	private void Update()
	{
		var x = Input.mousePosition.x - rectTransform.sizeDelta.x*0.5f;
		var y = Input.mousePosition.y + rectTransform.sizeDelta.y*0.5f;
		
		rectTransform.position = new Vector3(x, y, 0f);
	}
}