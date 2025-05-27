using UnityEngine;

namespace TicTacToe3D
{
    public class GridSpawner : MonoBehaviour
    {
        [Tooltip("Ссылка на GameManager")]
        public GameManager gm;

        [Tooltip("Материал пустой клетки")]
        public Material emptyMaterial;

        [Tooltip("Расстояние между кубами")]
        public float spacing = 1.2f;

        private void Awake()
        {
            var cells = new CellView[27];
            int idx = 0;

            for (int z = 0; z < 3; z++)
                for (int y = 0; y < 3; y++)
                    for (int x = 0; x < 3; x++)
                    {
                        // робим Cube
                        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        go.name = $"Cell_{x}{y}{z}";
                        go.transform.parent = transform;
                        go.transform.localPosition = new Vector3(x * spacing, y * spacing, -z * spacing);

                        // пустий матеріал
                        var mr = go.GetComponent<MeshRenderer>();
                        mr.material = emptyMaterial;

                        // логика клітини
                        var cv = go.AddComponent<CellView>();
                        cv.Configure(x, y, z);
                        cv.Init(gm);

                        cells[idx++] = cv;
                    }

            gm.SetCells(cells);
        }
    }
}
