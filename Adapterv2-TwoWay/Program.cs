using System;

namespace Adapterv2_TwoWay
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Experiment 1: test the aircraft engine");
            IAircraft aircraft = new Aircraft();
            aircraft.TakeOff();
            if (aircraft.Airborne) Console.WriteLine(
                "The aircraft engine is fine, flying at "
                + aircraft.Height + "meters");
            // Classic usage of an adapter
            Console.WriteLine("\nExperiment 2: Use the engine in the Seabird");
            IAircraft seabird = new Seabird();
            seabird.TakeOff(); // And automatically increases speed
            Console.WriteLine("The Seabird took off");
            // Two-way adapter: using seacraft instructions on an IAircraft object
            // (where they are not in the IAircraft interface)
            Console.WriteLine("\nExperiment 3: Increase the speed of the Seabird:");
            ((ISeacraft) seabird).IncreaseRevs();
            ((ISeacraft) seabird).IncreaseRevs();
            if (seabird.Airborne)
                Console.WriteLine("Seabird flying at height " + seabird.Height +
                                  " meters and speed " + ((ISeacraft) seabird).Speed + " knots");
            Console.WriteLine("Experiments successful; the Seabird flies!");
        }
    }

    public interface IAircraft
    {
        bool Airborne { get; }
        void TakeOff();
        int Height { get; }
    }

    //target
    public sealed class Aircraft : IAircraft
    {
        private int height;
        private bool airborne;

        public Aircraft()
        {
            height = 0;
            airborne = false;
        }

        public bool Airborne => airborne;

        public int Height => height;

        public void TakeOff()
        {
            Console.WriteLine("Aircraft engine takeoff");
            airborne = true;
            height = 200; // Meters
        }

    }

    //interface
    public interface ISeacraft
    {
        int Speed { get; }
        void IncreaseRevs();
    }

    public class Seacraft : ISeacraft
    {
        int speed = 0;
        public int Speed => speed;

        public virtual void IncreaseRevs()
        {
            speed += 10;
            Console.WriteLine("Seacraft engine increases revs to " + speed + " knots");
        }
    }

    //Adapter;
    public class Seabird : Seacraft, IAircraft
    {
        public bool Airborne => Height > 50;

        public int Height { get; set; } = 0;

        public void TakeOff()
        {
            while (!Airborne)
            {
                IncreaseRevs();
            }
        }

        public override void IncreaseRevs()
        {
            base.IncreaseRevs();
            if (Speed > 40)
                Height += 100;
        }
    }
}
