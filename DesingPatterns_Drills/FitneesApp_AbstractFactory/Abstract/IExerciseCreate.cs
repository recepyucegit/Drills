namespace FitneesApp_AbstractFactory.Abstract
{
    public interface IExerciseCreate
    {
        public IDeadlift CreateDeadlift();
        public IBenchPress CreateBenchPress();
        public ISquat CreateSquat();
        public IRow CreateRow();
    }
}
