using UnityEngine;

namespace TicTacToe3D
{
    [RequireComponent(typeof(MeshRenderer))]
    public class CellView : MonoBehaviour
    {
        [HideInInspector] public int x, y, z;
        private GameManager gm;

        public void Init(GameManager manager)
        {
            gm = manager;
        }

        public void Configure(int x, int y, int z)
        {
            this.x = x; this.y = y; this.z = z;
        }

        ///зміна матеріала куба (X, O або пусто)
        public void SetMaterial(Material mat)
        {
            GetComponent<Renderer>().material = mat;
        }

        private void OnMouseDown()
        {
            gm.OnCellClicked(x, y, z, this);
        }
    }
}
