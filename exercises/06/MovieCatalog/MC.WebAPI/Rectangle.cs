namespace MC.WebAPI
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Rectangle
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public double Perimeter { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public double Aria { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public Rectangle(double a, double b) 
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            Perimeter = a * 2 + b * 2;
            Aria = a * b;
        }
    }
}
