using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PositionEmailPreviewsScript : MonoBehaviour {

    public GameObject[] emailPreviews;
    private float distanceToTop = 0;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < emailPreviews.Length; i++) {
            emailPreviews[i].transform.localPosition = new Vector3(
                emailPreviews[i].transform.localPosition.x,
                emailPreviews[i].transform.localPosition.y - distanceToTop,
                emailPreviews[i].transform.localPosition.z
            );
            distanceToTop += emailPreviews[i].GetComponent<RectTransform>().rect.height;
        }
	}
}
