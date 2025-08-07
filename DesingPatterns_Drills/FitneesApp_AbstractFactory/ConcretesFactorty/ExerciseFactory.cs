using FitneesApp_AbstractFactory.Abstract;
using FitneesApp_AbstractFactory.ExerciseCreateArea;
using FitneesApp_AbstractFactory.ConcreteExercises;

namespace FitneesApp_AbstractFactory.ConcretesFactorty
{
    public class ExerciseFactory : IExerciseCreate // Concrete Factory
    {
        public IBenchPress CreateBenchPress()
        {
            return new BenchPressConcrete();
        }

        public IDeadlift CreateDeadlift()
        {
            return new DeadliftConcrete();
        }

        public IRow CreateRow()
        {
            return new RowConcrete();
        }

        public ISquat CreateSquat()
        {
            return new SquatConcrete();
        }
    }
}
