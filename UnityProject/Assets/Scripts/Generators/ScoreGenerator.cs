﻿using UnityEngine;
using MapFeatures.ScoreLocations;

public class ScoreGenerator
{
    public static GameObject GenerateScore(GameObject scorePrefab, Transform parent)
    {
        GameObject score = Object.Instantiate(
            scorePrefab,
            Vector3.zero,
            Quaternion.identity) as GameObject;
        score.transform.SetParent(parent, false);

        return score;
    }

    public static GameObject GenerateScore(ScoreLocationDTO scoreDTO, Transform parent)
    {
        // TODO: pass type from back-end. Hardcoded to bluedisk for now.
        string resource = "Prefabs/Scores/score_" + "bluedisk";
        GameObject scorePrefab = Resources.Load<GameObject>(resource);

        GameObject score = Object.Instantiate(
            scorePrefab,
            new Vector3(scoreDTO.location.x, 0, scoreDTO.location.y),
            Quaternion.identity) as GameObject;
        score.transform.SetParent(parent, false);

        // TODO: make name dynamic to type passed too.
        score.name = "score_" + "bluedisk" + "_" + scoreDTO.location.x + "_" + scoreDTO.location.y;

        return score;
    }
}
