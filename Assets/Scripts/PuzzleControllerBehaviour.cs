using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControllerBehaviour : MonoBehaviour {
	private readonly int numOfPieces = 8;
	public GameObject piece;
	public Vector2[] positions;

	void Start() {
		for (int i = 0; i < numOfPieces; i++) {
			Vector2 position = positions[i];
			Instantiate(piece, new Vector3(position.x, position.y, 0), Quaternion.identity);
		}
	}
}
