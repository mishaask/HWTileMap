# Weekly HomeWork implemented on top of "Unity week 5: Two-dimensional scene-building and path-finding"

This solution implements:
2 enemy states/behaviours
-When enemy loses sight of the player he will go to the last know location of the player, wait for a couple of seconds and then go back to his patrol.
-After chasing the player for a specified(in a serialised field) ammount of time the enemy will get tired and stop chasing the player

Using Dijkstra instead of BFS for weighted paths.

TestDijksta folder with tests for the Dijkstra algorithm in the Vscode.

New scripts were added to accomidate the new enemy behaviour and they are located in HW directory in the Scripts directory

itch.io: https://mishaaskk.itch.io/tilemapgame
