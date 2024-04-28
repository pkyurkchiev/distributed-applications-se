namespace MC.WebAPI
{
    public class Rectangle
    {
        public double Perimeter { get; set; }
        public double Aria { get; set; }

        public Rectangle(double a, double b) 
        {
            Perimeter = a * 2 + b * 2;
            Aria = a * b;
        }
    }
}
