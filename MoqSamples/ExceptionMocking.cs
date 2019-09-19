using System.Diagnostics;

namespace MoqSamples
{
    public class ExceptionMocking
    {
        private readonly IEngine engine;

        public ExceptionMocking(IEngine engine)
        {
            this.engine = engine;
        }

        public void StartEngine()
        {
            const int maxAttempts = 3;
            var currentAttempt = 1;
            while (currentAttempt <= maxAttempts)
            {
                try
                {
                    this.engine.StartUp();
                    break;
                }
                catch (EngineStartException ex)
                {
                    Debug.WriteLine($"Attempt {currentAttempt} failed with error: {ex.Message}");

                    if (currentAttempt == maxAttempts)
                    {
                        throw;
                    }
                }
                finally
                {
                    currentAttempt++;
                }
            }

            Debug.WriteLine($"Engine started @ attempt {currentAttempt - 1}");
        }
    }
}