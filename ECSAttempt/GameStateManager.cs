using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECSAttempt.Logger;
using ECSAttempt.ECS;

/// <summary>
/// NOTE:
/// The GameStateManager is built to behave like a stack for when it's
/// necessary for resource management but states individually can be paused and resumed
/// although that functionality in states is not yet implemented.
/// </summary>
namespace ECSAttempt
{
    public class GameStateManager
    {
        public static List<GameState> statesPool = new List<GameState>();

        private static Stack<GameState> states = new Stack<GameState>();
        private static bool updateUpdateCoreInstance = true;
        private static bool updateDrawCoreInstance = true;

        public static void ChangeState(GameState gameState) {
            Utils.Log("Changing game state to '" + gameState.Name + "'");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            updateUpdateCoreInstance = true;
            updateDrawCoreInstance = true;
            
            UnloadGameState();
            PushGameState(gameState);
            InitializeGameState();
            LoadGameState();

            sw.Stop();

            Utils.Log("Game state changed to '" + gameState.Name + "'");
            Utils.Log(string.Format("\tTook {0}ms", sw.Elapsed.TotalMilliseconds));
        }

        public static void InitializeGameState() {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            if (!Empty()) {
                Utils.Log("Initializing game state '" + PeekGameState().Name + "'");
                PeekGameState().RefreshCoreInstance();
                PeekGameState().Initialize();
                Utils.Log("Initialized");
            }
            else
                Utils.Log("A game state needs to be pushed before initializing!", LogType.Error);

            sw.Stop();
            Utils.Log(string.Format("\tTook {0}ms", sw.Elapsed.TotalMilliseconds));
        }

        public static void LoadGameState() {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            if (!Empty()) {
                Utils.Log("Loading content");
                PeekGameState().RefreshCoreInstance();
                PeekGameState().Load();
                Utils.Log("Loaded");
            }
            else
                Utils.Log("A game state needs to be pushed before loading!", LogType.Error);

            sw.Stop();
            Utils.Log(string.Format("\tTook {0}ms", sw.Elapsed.TotalMilliseconds));
        }

        public static void UpdateGameState() {
            if (!Empty()) {
                // we use this so we don't have to update core instance each loop, only once per
                // game state change. it's only necessary for perf reasons if UpdateCoreInstance
                // had to have heavy processing but if its just assigning references it has
                // practically no impact. Testable with Stopwatch.
                if (updateUpdateCoreInstance) {
                    PeekGameState().RefreshCoreInstance();
                    updateUpdateCoreInstance = false;
                }
                PeekGameState().Update();
            }
            else
                Utils.Log("A game state needs to exist to update!", LogType.Error);
        }

        public static void DrawGameState() {
            if (!Empty()) {
                if (updateDrawCoreInstance) {
                    PeekGameState().RefreshCoreInstance();
                    updateDrawCoreInstance = false;
                }
                PeekGameState().Draw();
            }
            else
                Utils.Log("A game state needs to exist before drawing!", LogType.Error);
        }

        public static void UnloadGameState() {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            if (!Empty()) {
                Utils.Log("Unloading '" + states.Peek().Name + "' content");
                PeekGameState().RefreshCoreInstance();
                PeekGameState().Unload();
                PopGameState().UnloadContent();
                EntityManager.entities.Clear();
                Utils.Log("Unloaded");
            }
            else
                Utils.Log("A game state needs to exist before unloading!", LogType.Error);

            sw.Stop();
            Utils.Log(string.Format("\tTook {0}ms", sw.Elapsed.TotalMilliseconds));
        }

        public static int GetStatesCount() {
            return states.Count;
        }

        public static bool Empty() {
            if (GetStatesCount() < 1)
                return true;
            else
                return false;
        }

        public static GameState PeekGameState() {
            return states.Peek();
        }

        public static GameState PopGameState() {
            return states.Pop();
        }

        public static void PushGameState(GameState gameState) {
            states.Push(gameState);
        }
    }
}
