using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedEntry : MonoBehaviour {

	[SpaceAttribute(10)]
	[HeaderAttribute("Booleans")]
	public bool animateOnStart = false;
	public bool isOffset = false;

	[SpaceAttribute(10)]
	[HeaderAttribute("Timing")]
	public float delay = 0;
	public float effectTime = 1;

	[SpaceAttribute(10)]
	[HeaderAttribute("Scale")]
	public Vector3 startScale;
	public AnimationCurve scaleCurve;

	[SpaceAttribute(10)]
	[HeaderAttribute("Position")]
	public Vector3 startPos;
	public AnimationCurve posCurve;

	Vector3 endScale;
	Vector3 endPos;

	// For use with pause menus and variable time scales
	void Awake() {
		if (!animateOnStart) {
			SetupVariables();
		}
	}

	void Start () {
		if (animateOnStart) {
			SetupVariables();
			StartCoroutine(Animation());
		}
	}

	void SetupVariables() {
		endScale = transform.localScale;
		endPos = transform.localPosition;
		// Failsafe for if you don't have objects centered or are scaling canvas
		if (isOffset) {
			startPos += endPos;
		}
	}

	IEnumerator Animation() {
		transform.localScale = startScale;
		transform.localPosition = startPos;
		// Waits for chosen delay time before executing
		yield return new WaitForSecondsRealtime(delay);
		float time = 0;
		float percentageComp = 0;
		// Ensures we know time regardless of time scale - useful for pause menus
		float lastTime = Time.realtimeSinceStartup;
		do {
			time += Time.realtimeSinceStartup - lastTime;
			lastTime = Time.realtimeSinceStartup;
			percentageComp = Mathf.Clamp01( time / effectTime );
			// Unclamped lerp so that we can go slightly past 1 for "overshooting" in animation
			Vector3 tempScale = Vector3.LerpUnclamped(startScale, endScale, scaleCurve.Evaluate(percentageComp));
			Vector3 tempPos = Vector3.LerpUnclamped(startPos, endPos, posCurve.Evaluate(percentageComp));
			transform.localScale = tempScale;
			transform.localPosition = tempPos;
			yield return null;
		} while (percentageComp < 1);
		// Failsafe to ensure they snap to correct end position in case of framerate drops etc.
		transform.localScale = endScale;
		transform.localPosition = endPos;
		yield return null;
	}
}
