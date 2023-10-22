namespace cnsOOPStudent
{
    internal class Student
    {
        public event EventHandler AgeCreated;
        public delegate void EventHandlerInt(object? sender , EventArgs e, int new_age);
        public event EventHandlerInt AgeCreatedInt;

        private int age;

        public string Name { get; internal set; }
        public string Surname { get; internal set; }
        public int Age
        {
            get
            {
                return age;
            }

            internal set
            {
                if (value > 0)
                    age = value;
                AgeCreated?.Invoke(this, EventArgs.Empty);
                AgeCreatedInt?.Invoke(this, EventArgs.Empty, Age);
            }
        }

        internal string GetFullName()
        {
            return $"{Name} {Surname} ({Age})";
        }
    }
}