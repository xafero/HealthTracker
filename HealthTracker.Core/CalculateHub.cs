using HealthTracker.API;

namespace HealthTracker.Core
{
    public class CalculateHub : IHealthHub
    {
        public event DataHandler OnHealthEvent;

        private readonly BodyMassIndex bmi = new BodyMassIndex();
        private readonly IBasalMetaCalc bmr = new HarrisBenedictBMR();
        private readonly BodyFatEstimate bfe = new BodyFatEstimate();

        private IPerson Person { get; }

        public CalculateHub(IPerson person)
        {
            Person = person;
        }

        public void OnRawHealthEvent(IHealthHub hub, IDataEvent data)
        {
            if (data.Unit == Unit.kg)
            {
                var weight = data.Data;
                var height = Person.Height;
                var bmiVal = bmi.CalcBMI(weight, height);
                OnHealthEvent?.Invoke(this, new SimpleData { Data = bmiVal, Unit = Unit.bmi, Status = data.Status, Time = data.Time });
                var category = bmi.RateBMI(bmiVal);
                OnHealthEvent?.Invoke(this, new SimpleData { Data = (int)category, Unit = Unit.wcat, Status = data.Status, Time = data.Time });
                var sex = Person.Sex;
                var age = Person.Age;
                var bmrVal = bmr.CalcBMR(sex, weight, height, age);
                OnHealthEvent?.Invoke(this, new SimpleData { Data = bmrVal, Unit = Unit.bmr, Status = data.Status, Time = data.Time });
                var bfeVal = bfe.EstimateFat(weight, height, age, sex);
                OnHealthEvent?.Invoke(this, new SimpleData { Data = bfeVal, Unit = Unit.bfe, Status = data.Status, Time = data.Time });
            }
        }

        public void Dispose()
        {
        }
    }
}