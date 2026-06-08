namespace CalcTaxApp.Services
{
    public interface ITaxService
    {
        (decimal tax, decimal cra, decimal taxableIncome) Calculate(decimal grossIncome);
    }
}
