using System;
using UnityEngine;
using NUnit.Framework;
using MapFeatures.Pickups;
using MapFeatures.Obstacles;
using MapFeatures.ScoreLocations;
using Players;

namespace AIMMOUnityTest
{
    [TestFixture]
    internal class DTODeserialisationTests
    {
        [Test]
        public void TestPlayerDTODeserialisation()
        {
            string playerJSON = @" {
                ""players"": [
                    {
                        ""id"": 1,
                        ""score"": 10,
                        ""health"": 5,
                        ""location"": {
                            ""x"": 2,
                            ""y"": 2
                        },
                        ""orientation"": ""East""
                    },
                    {
                        ""id"": 1,
                        ""score"": 11,
                        ""health"": 8,
                        ""location"": {
                            ""x"": 2,
                            ""y"": 1
                        },
                        ""orientation"": ""South""
                    }
                ]
            }";
            PlayersDTO playersDTO = JsonUtility.FromJson<PlayersDTO>(playerJSON);
            Assert.AreEqual(2, playersDTO.players.Length);
            PlayerDTO player = playersDTO.players[0];
            Assert.IsNotNull(player);
            Assert.AreEqual(1, player.id);
            Assert.AreEqual(10, player.score);
            Assert.AreEqual(5, player.health);
            Assert.AreEqual(new Location(2, 2), player.location);
            Assert.AreEqual(Orientation.East, player.orientationType);
        }

        [Test]
        public void TestPickupDTODeserialisation()
        {
            string pickupJSON = @" {
                ""pickups"": [
                    {
                        ""type"": ""Invulnerability"",
                        ""location"": {
                            ""x"": 1,
                            ""y"": 3
                        } 
                    },
                    {
                        ""type"": ""health"",
                        ""location"": {
                            ""x"": -2,
                            ""y"": 9
                        } 
                    }
                ]
            }";

            PickupsDTO pickupsDTO = JsonUtility.FromJson<PickupsDTO>(pickupJSON);
            Assert.AreEqual(2, pickupsDTO.pickups.Length);
            PickupDTO pickupDTO = pickupsDTO.pickups[0];
            Assert.AreEqual(new Location(1, 3), pickupDTO.location);
            Assert.AreEqual(PickupType.Invulnerability, pickupDTO.PickupType);
        }

        [Test]
        public void TestScoreDTODeserialisation()
        {
            string scoreLocationJSON = @" {
                ""scoreLocations"": [
                    {
                        ""location"": {
                            ""x"": -2,
                            ""y"": -9
                        } 
                    },
                    {
                        ""location"": {
                            ""x"": -2,
                            ""y"": 1
                        } 
                    },
                    {
                        ""location"": {
                            ""x"": -2,
                            ""y"": 4
                        } 
                    }
                ]
            } ";

            ScoreLocationsDTO scoreLocationsDTO = JsonUtility.FromJson<ScoreLocationsDTO>(scoreLocationJSON);
            Assert.AreEqual(3, scoreLocationsDTO.scoreLocations.Length);
            ScoreLocationDTO scoreLocationDTO = scoreLocationsDTO.scoreLocations[0];
            Assert.AreEqual(new Location(-2, -9), scoreLocationDTO.location);
        }

        [Test]
        public void TestObstacleDTODeserialisation()
        {
            string obstacleJSON = @" {
                ""obstacles"": [
                    {
                        ""location"": {
                            ""x"": 0,
                            ""y"": 1
                        },
                        ""width"": 2,
                        ""height"": 1,
                        ""type"": ""van"",
                        ""orientation"": ""east""
                    },
                    {
                        ""location"": {
                            ""x"": 9,
                            ""y"": 1
                        },
                        ""width"": 20,
                        ""height"": 2,
                        ""type"": ""wall"",
                        ""orientation"": ""west""
                    }
                ]
            } ";

            ObstaclesDTO obstaclesDTO = JsonUtility.FromJson<ObstaclesDTO>(obstacleJSON);
            Assert.AreEqual(2, obstaclesDTO.obstacles.Length);

            ObstacleDTO obstacleDTO = obstaclesDTO.obstacles[0];
            Assert.AreEqual(new Location(0, 1), obstacleDTO.location);
            Assert.AreEqual(2, obstacleDTO.width);
            Assert.AreEqual(1, obstacleDTO.height);
            Assert.AreEqual(ObstacleType.Van, obstacleDTO.ObstacleType);
            Assert.AreEqual(Orientation.East, obstacleDTO.OrientationType);
        }

        [Test]
        public void TestGameUpdateDTODeserialisation()
        {
            string gameUpdateJSON = @" {
                ""era"": ""less_flat"",
                ""southWestCorner"": {
                    ""x"": -2,
                    ""y"": -2
                },
                ""northEastCorner"": {
                    ""x"": 2,
                    ""y"": 2
                },
                ""players"": [
                    {
                        ""id"": 1,
                        ""score"": 0,
                        ""health"": 5,
                        ""location"": {
                            ""x"": 2,
                            ""y"": 2
                        },
                        ""orientation"": ""east""
                    }
                ],
                ""pickups"": [
                    {
                        ""type"": ""boost"",
                        ""location"": {
                            ""x"": 1,
                            ""y"": 3
                        }
                    }
                ],
                ""scoreLocations"": [
                    {
                        ""location"": {
                            ""x"": 1,
                            ""y"": 3
                        }
                    }
                ],
                ""obstacles"": [
                    {
                        ""location"": {
                            ""x"": 0,
                            ""y"": 1
                        },
                        ""width"": 2,
                        ""height"": 1,
                        ""type"": ""van"",
                        ""orientation"": ""east""
                    }
                ]

            }";

            GameStateDTO gameState = JsonUtility.FromJson<GameStateDTO>(gameUpdateJSON);
            Assert.AreEqual(Era.Future, gameState.EraType);
            Assert.AreEqual(new Location(-2, -2), gameState.southWestCorner);
            Assert.AreEqual(new Location(2, 2), gameState.northEastCorner);
            Assert.AreEqual(1, gameState.players.Length);
            Assert.AreEqual(1, gameState.pickups.Length);
            Assert.AreEqual(1, gameState.scoreLocations.Length);
            Assert.AreEqual(1, gameState.obstacles.Length);
        }
    }
}
