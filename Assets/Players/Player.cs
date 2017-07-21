﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerData
{
	public float x, y, rotation;
	public int score, health;

	// Construct from json
	public PlayerData(SimpleJSON.JSONNode json)
	{
		this.x = json["x"].AsFloat;
		this.y = json["y"].AsFloat;
		this.rotation = json["rotation"].AsFloat;
		this.score = json["score"].AsInt;
		this.health = json["health"].AsInt;
	}
}

public class Player : MonoBehaviour
{
	public static bool Create(int id, PlayerData playerData)
	{
		// It might have already been created.
		if (GameObject.Find(PlayerId(id)) != null)
			return true;

		// Generate sphere.
		GameObject player = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		if (player == null)
			return false;

		// DEBUG
		if (id == 1) 
		{
			GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
			plane.GetComponent<Renderer>().material.color = Color.green;
			plane.transform.localPosition = new Vector3(0.0f, -0.45f, 0.0f);
			plane.transform.localScale = new Vector3(0.6f, 1.0f, 0.6f);
			plane.transform.parent = player.transform;
		}

		player.transform.position = new Vector3(playerData.x, 0.5f, playerData.y);
		player.name = PlayerId(id);
		player.AddComponent<PlayerController>();

		// Assign random colour.
		player.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();

		// Add score text.
		GameObject text = new GameObject();
		Vector3 textPosition = player.transform.position;
		textPosition.y += 1.5f;
		textPosition.z += 0.25f;
		text.transform.position = textPosition;
		text.transform.rotation = Quaternion.Euler(0.0f, 45.0f, 0.0f);
		text.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

		text.AddComponent<TextMesh>();
		text.AddComponent<MeshRenderer>();
		text.GetComponent<TextMesh>().text = Convert.ToString(playerData.score);
		text.GetComponent<TextMesh>().fontSize = 80;

		text.transform.parent = player.transform;

		return true;
	}

	public static bool Delete(int id)
	{
		GameObject playerToDestroy = GameObject.Find(PlayerId(id));
		if (playerToDestroy == null)
			return false;

		Destroy(playerToDestroy);

		return true;
	}

	public static bool Update(int id, PlayerData playerData)
	{
		GameObject playerToUpdate = GameObject.Find(PlayerId(id));
		if (playerToUpdate == null)
			return false;

		PlayerController controller = playerToUpdate.GetComponent<PlayerController>();
		if (controller == null)
			return false;

		// The controller will change the position, score and health.
		controller.SetNextState(playerData);



		return true;	
	}

	private static string PlayerId(int id)
	{
		return "player" + Convert.ToString(id);
	}
}
