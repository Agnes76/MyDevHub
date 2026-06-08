namespace CalcTaxApp.Services
{
    public class TaxService : ITaxService
    {
        public (decimal tax, decimal cra, decimal taxableIncome) Calculate(decimal grossIncome)
        {
            // CRA: Consolidated Relief Allowance
            decimal cra = (0.01m * grossIncome) + 200000m + (0.20m * grossIncome);

            decimal taxableIncome = grossIncome - cra;

            if (taxableIncome <= 0)
                return (0, cra, 0);

            decimal tax = 0;
            decimal remaining = taxableIncome;

            // Band 1
            decimal band = Math.Min(remaining, 300000);
            tax += band * 0.07m;
            remaining -= band;

            // Band 2
            if (remaining > 0)
            {
                band = Math.Min(remaining, 300000);
                tax += band * 0.11m;
                remaining -= band;
            }

            // Band 3
            if (remaining > 0)
            {
                band = Math.Min(remaining, 500000);
                tax += band * 0.15m;
                remaining -= band;
            }

            // Band 4
            if (remaining > 0)
            {
                band = Math.Min(remaining, 500000);
                tax += band * 0.19m;
                remaining -= band;
            }

            // Band 5
            if (remaining > 0)
            {
                band = Math.Min(remaining, 1600000);
                tax += band * 0.21m;
                remaining -= band;
            }

            // Band 6
            if (remaining > 0)
            {
                tax += remaining * 0.24m;
            }

            return (tax, cra, taxableIncome);
        }
    }
}
