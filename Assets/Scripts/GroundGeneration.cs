using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Grid))]
public class GroundGeneration : MonoBehaviour
{
    public float spawnChance = 0.5f;
    public bool randomSize = false;
    public GameObject[] prefabs;
    List<GameObject> queue = new List<GameObject>();

    private Camera camera;
    private List<string> touchedSpots = new();

    private Grid grid;
    public int gridSize;

    void Start()
    {
        camera = Camera.main;

        grid = GetComponent<Grid>();
        gridSize = (int) grid.cellSize.x;
    }

    void Update() {
        // camera.pixelWidth/2, 
        Vector3 worldPos = camera.ScreenToWorldPoint(new Vector3(-camera.pixelWidth / 2, -camera.pixelHeight / 2, camera.nearClipPlane));
        Vector3 worldPos2 = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2, camera.nearClipPlane));

        Vector3Int gridPos = grid.WorldToCell(GetBottomLeftAtY0());
        Vector3Int gridPos2 = grid.WorldToCell(GetTopRightAtY0());

        for (int x = 0; x < (gridPos2.x - gridPos.x) * 2 + 2; x++) {
            for (int z = 1; z < (gridPos2.z - gridPos.z) * 2 + 2; z++) {
                string str = "" + (gridPos.x + x) + "," + (gridPos.z + z);
                if (touchedSpots.Contains(str)) continue;
                touchedSpots.Add(str);
                if (gridPos.x + x == 0 && gridPos.z + z == 0) continue;

                if (Random.Range(0, 1) > spawnChance) {
                    // continue;
                }

                Vector3Int newPos = new(gridPos.x + x, 0, gridPos.z + z);
                Vector3 backToWorldPos = grid.CellToWorld(newPos);
                backToWorldPos.x += Random.Range(-gridSize / 2f, gridSize / 2f);
                backToWorldPos.z += Random.Range(-gridSize / 2f, gridSize / 2f);

                // float randomAngle = UnityEngine.Random.Range(0f, 360f);
                // Quaternion randomRotation = Quaternion.Euler(0, 0, randomAngle);
                Quaternion randomRotation = Quaternion.Euler(0, 0, 0);

                GameObject go = Instantiate(getRandomObject(), backToWorldPos, randomRotation);
                go.transform.Rotate(0, Random.Range(0f, 360f), 0);

                if (randomSize) {
                    float scale = Random.Range(3f, 5f);
                    go.transform.localScale = new Vector3(scale, scale, scale);
                }

            }
        }
    }

    Vector3 GetBottomLeftAtY0() {
        // Bottom-left corner of the screen
        Vector3 screenPoint = new Vector3(0, 0, 0);

        Ray ray = camera.ScreenPointToRay(screenPoint);

        if (ray.direction.y == 0)
            return ray.origin; // parallel, fallback

        float t = -ray.origin.y / ray.direction.y;
        return ray.origin + ray.direction * t;
    }
    
    Vector3 GetTopRightAtY0() {
        // Top-right corner of the screen
        Vector3 screenPoint = new Vector3(camera.pixelWidth, camera.pixelHeight, 0);

        Ray ray = camera.ScreenPointToRay(screenPoint);

        if (ray.direction.y == 0)
            return ray.origin;

        float t = -ray.origin.y / ray.direction.y;
        return ray.origin + ray.direction * t;
    }

    GameObject getRandomObject()
    {
        if (queue.Count == 0)
        {
            for (int i = 0; i < prefabs.Length; i++)
            {
                for (int ii = 0; ii < 3; ii++)
                {
                    queue.Add(prefabs[i]);
                }
            }
            Shuffle(queue);
        }
        GameObject ret = queue[0];
        queue.RemoveAt(0);
        return ret;
    }

    private static System.Random rng = new System.Random();

    public static void Shuffle(IList<GameObject> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            GameObject value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}