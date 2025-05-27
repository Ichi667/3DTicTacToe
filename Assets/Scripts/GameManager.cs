using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace TicTacToe3D
{
    public class GameManager : MonoBehaviour
    {
        [Header("3D Logic")]
        public GameObject spherePrefab;
        public CellView[] cells;
        public Material matX;
        public Material matEmpty;

        [Header("End-Game UI")]
        public GameObject endPanel;
        public TMP_Text resultText;
        public TMP_Text durationText;
        public Button playAgainButton;
        public Button mainMenuButton;

        private Board3D board;
        private AiEngine ai;
        private StatsManager stats;
        private int currentPlayer = 1;
        private float startTime;
        private bool isGameOver = false;   // флаг завершення гри

        void Awake()
        {
            board = new Board3D();
            stats = new StatsManager();

            // ІІ тільки в SinglePlayer
            if (GameSettings.Mode == GameMode.SinglePlayer)
                ai = new AiEngine(2);
            else
                ai = null;

            if (spherePrefab == null)
                spherePrefab = Resources.Load<GameObject>("Prefabs/O_Sphere");

            if (cells != null)
                foreach (var c in cells)
                    c.Init(this);

            // робим UI
            if (endPanel != null) endPanel.SetActive(false);
            if (playAgainButton != null) playAgainButton.onClick.AddListener(Restart);
            if (mainMenuButton != null) mainMenuButton.onClick.AddListener(BackToMainMenu);

            NewGame();
            Redraw();
        }

        public void SetCells(CellView[] generated)
        {
            cells = generated;
            foreach (var c in cells)
                c.Init(this);
            NewGame();
            Redraw();
        }

        public void OnCellClicked(int x, int y, int z, CellView view)
        {
            // кліки після завершення гри
            if (isGameOver) return;

            // блок if клітинка зайнята
            if (board[x, y, z] != 0) return;

            // SinglePlayer ходить лише X
            if (GameSettings.Mode == GameMode.SinglePlayer && currentPlayer != 1)
                return;

            // записуємо хід
            board[x, y, z] = currentPlayer;
            Redraw();

            // гра завершилася?
            int winner = LineChecker.CheckWinner(board);
            if (winner != 0 || board.IsFull())
            {
                EndGame(winner);
                return;
            }

            // двоє гравців - міняємо чергу
            if (GameSettings.Mode == GameMode.TwoPlayers)
            {
                currentPlayer = -currentPlayer;
                return;
            }

            // SinglePlayer хід ІІ
            currentPlayer = -1;
            NextTurn();
        }

        private void NextTurn()
        {
            // хід ІІ SinglePlayer
            if (GameSettings.Mode == GameMode.SinglePlayer && ai != null && currentPlayer == -1)
            {
                var mv = ai.NextMove(board, -1);
                board[mv.x, mv.y, mv.z] = -1;
                currentPlayer = 1;
                Redraw();
            }

            // перевіряємо кінець гри
            int winner = LineChecker.CheckWinner(board);
            if (winner != 0 || board.IsFull())
            {
                EndGame(winner);
            }
        }

        private void EndGame(int winner)
        {
            stats.RegisterResult(winner);
            isGameOver = true;  // вимикаємо кліки

            if (endPanel != null)
            {
                endPanel.SetActive(true);

                // хто переміг
                if (resultText != null)
                    resultText.text = winner == 0
                        ? "Нічия"
                        : $"Переміг: {(winner == 1 ? "X" : "O")}";

                // скільки тривала партія
                float duration = Time.time - startTime;
                if (durationText != null)
                    durationText.text = $"Час: {duration:F2} с";
            }
        }

        private void Restart()
        {
            board.Clear();
            currentPlayer = 1;
            NewGame();
            Redraw();
        }

        private void BackToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void NewGame()
        {
            startTime = Time.time;
            isGameOver = false;
            if (endPanel != null) endPanel.SetActive(false);
        }

        private void Redraw()
        {
            if (cells == null) return;

            foreach (var c in cells)
            {
                int v = board[c.x, c.y, c.z];
                if (v == -1)
                {
                    // мінус куб і плюс сфера
                    c.GetComponent<MeshRenderer>().enabled = false;
                    if (c.transform.Find("O_Sphere(Clone)") == null)
                    {
                        var s = Instantiate(spherePrefab, c.transform);
                        s.name = "O_Sphere";
                        s.transform.localPosition = Vector3.zero;
                    }
                }
                else
                {
                    // мінус сфера, плюс куб
                    var old = c.transform.Find("O_Sphere");
                    if (old != null) Destroy(old.gameObject);
                    var mr = c.GetComponent<MeshRenderer>();
                    mr.enabled = true;
                    mr.material = (v == 1 ? matX : matEmpty);
                }
            }
        }
    }
}
