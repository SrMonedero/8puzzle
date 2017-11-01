using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControllerBehaviour : MonoBehaviour {
	private PuzzleState puzzleState;
	private readonly int numOfPieces = 8;
	public GameObject piece;
	public Vector2[] positions;
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
	private int[] initialPositions = new int[] {
		0, 1, 2, 3, 4, 5, 6, 7, 8
	};

	void Start() {
		puzzleState = PuzzleState.UNSOLVED;
		freePosition = 8;
		Shuffle();
		while (!IsSolvable()) {
			Shuffle();
		}
		for (int i = 0; i < numOfPieces; i++) {
			int[] color = Levels.level1[i];
			Vector2 position = positions[initialPositions[i]];
			GameObject newPiece = Instantiate<GameObject>(piece, new Vector3(position.x, position.y, 0), Quaternion.identity);
			newPiece.GetComponent<SpriteRenderer>().material.color = new Color(color[0]/255f, color[1]/255f, color[2]/255f);
			PieceBehaviour pieceBehaviour = newPiece.GetComponent<PieceBehaviour>();
			pieceBehaviour.position = initialPositions[i];
			pieceBehaviour.goodPosition = i;
			pieceBehaviour.puzzleController = this;
		}
		freePosition = initialPositions[numOfPieces];
	}

    public void Move(GameObject piece) {
		if (PuzzleState.SOLVED.Equals(puzzleState)) {
			return;
		}
		int position = piece.GetComponent<PieceBehaviour>().position;
		int[] possibleNextPositions = nextPositions[position];
		if (Array.Exists(possibleNextPositions, element => element == freePosition)) {
			piece.transform.position = positions[freePosition];
			piece.GetComponent<PieceBehaviour>().position = freePosition;
			freePosition = position;
			if (IsSolved()) {
				puzzleState = PuzzleState.SOLVED;
			}
		}
	}

    private bool IsSolved() {
		bool result = true;
		GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");
		foreach (GameObject piece in pieces) {
			PieceBehaviour pieceBehaviour = piece.GetComponent<PieceBehaviour>();
			if (pieceBehaviour.position != pieceBehaviour.goodPosition) {
				result = false;
				break;
			}
		}
        return result;
    }

    private void Shuffle()
    {
		for (int i = 0; i < initialPositions.Length; i++) {
			int randomPosition = UnityEngine.Random.Range(0, initialPositions.Length);
			int aux = initialPositions[i];
			initialPositions[i] = initialPositions[randomPosition];
			initialPositions[randomPosition] = aux;
		}
    }

	private bool IsSolvable() {
		bool result = false;
		int inversions = 0;
		for (int i = 0; i < initialPositions.Length; i++) {
			for (int j = i+1; j < initialPositions.Length; j++) {
				if (initialPositions[i] != 8 && initialPositions[j] != 8 && initialPositions[i] > initialPositions[j]) {
					inversions++;
				}
			}
		}
		if (inversions%2 == 0) {
			result = true;
		}
		return result;
	}
}
