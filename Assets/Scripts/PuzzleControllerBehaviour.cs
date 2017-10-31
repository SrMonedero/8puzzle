using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControllerBehaviour : MonoBehaviour {
	private readonly int numOfPieces = 8;
	public GameObject piece;
	public Vector2[] positions;
	private bool[] occupiedState;
	private int freePosition;
	private int[][] nextPositions = new int[][] {
		new int[2] {1, 3},
		new int[3] {0, 2, 4},
		new int[2] {1, 5},
		new int[3] {0, 4, 6},
		new int[4] {1, 3, 5, 7},
		new int[3] {2, 4, 8},
		new int[2] {3, 7},
		new int[3] {4, 6, 8},
		new int[2] {5, 7}
	};

	void Start() {
		occupiedState = new bool[9];
		freePosition = 8;
		for (int i = 0; i < numOfPieces; i++) {
			Vector2 position = positions[i];
			GameObject newPiece = Instantiate<GameObject>(piece, new Vector3(position.x, position.y, 0), Quaternion.identity);
			PieceBehaviour pieceBehaviour = newPiece.GetComponent<PieceBehaviour>();
			pieceBehaviour.position = i;
			pieceBehaviour.puzzleController = this;
			occupiedState[i] = true;
		}
	}

	public void Move(GameObject piece) {
		int position = piece.GetComponent<PieceBehaviour>().position;
		int[] possibleNextPositions = nextPositions[position];
		if (Array.Exists(possibleNextPositions, element => element == freePosition)) {
			piece.transform.position = positions[freePosition];
			piece.GetComponent<PieceBehaviour>().position = freePosition;
			freePosition = position;
		}
	}
}
