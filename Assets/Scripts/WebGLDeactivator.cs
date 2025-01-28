using UnityEngine;

public class WebGLDeactivator : MonoBehaviour
{
#if UNITY_WEBGL
	private void Awake()
	{
		gameObject.SetActive(false);
	}
#endif
}