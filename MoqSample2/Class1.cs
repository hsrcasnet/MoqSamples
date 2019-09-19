public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public decimal RawPrice { get; set; }

    public decimal GetPriceWithTax(ITaxCalculator calculator)
    {
        return calculator.GetTax(this.RawPrice) + this.RawPrice;
    }
}

public interface ITaxCalculator
{
    decimal GetTax(decimal rawPrice);
}