public class Rectangle
{
    public double Width { get; set; }
    public double Height { get; set; }

    // Implement a constructor that takes two parameters: width and height and sets the Width and Height properties.

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    // Implement a method called CalculateArea that returns the area of the rectangle (Width * Height).

    public double CalculateArea()
    {
        return Width * Height;
    }
}