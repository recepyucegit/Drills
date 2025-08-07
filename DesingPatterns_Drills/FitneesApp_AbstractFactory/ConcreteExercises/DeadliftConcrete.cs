using FitneesApp_AbstractFactory.Abstract;

namespace FitneesApp_AbstractFactory.ConcreteExercises
{
    public class DeadliftConcrete : IDeadlift
    {
        public void PerformDeadlift()
        {
            Console.WriteLine("3x5 RPE 8 Deadlift yapılıyor...");
        }

        public void RestDeadlift()
        {
            Console.WriteLine("3-5 dakika set arası dinlenildi...");
        }
    }
}
