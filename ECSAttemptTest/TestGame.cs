using ECSAttempt;

namespace ECSAttemptTest {
    public class TestGame {
        public static GameEngine engine;

        public TestGame() {
            engine = new GameEngine();
        }

        public void Start() {
            // first setup game engine parameters needed before warming up ,,,

            engine.SetContentRoot("Content");

            // ... then push starting state like engine/game splash ...

            TestState testState = new TestState("TestState"); 
            GameStateManager.PushGameState(testState);

            // ... warm up the engine ...
            
            engine.WarmUp();

            // ... then setup game engine parameters that are needed after warm up
            // but before complete start and also game parameters e.g when you need
            // GraphicsDevice for setting resolution but which is null in Core 
            // constructor but only set on intialization by MonoGame ...

            engine.SetResolution(1366, 768, false);
            //engine.SetResolution(1920, 1080, true);

            // ... then start engine

            engine.Start();

            // nothing after this can be called because it's in a loop unless it exits (handed 
            // off to states from here on out)
        }
    }
}