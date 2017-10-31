using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBehaviour : MonoBehaviour {
	public int position;
	public PuzzleControllerBehaviour puzzleController;

	void OnMouseDown() {
		puzzleController.Move(gameObject);
	}
}
