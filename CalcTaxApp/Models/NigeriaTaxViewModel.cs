namespace CalcTaxApp.Models
{
    public class NigeriaTaxViewModel
    {
        public decimal GrossIncome { get; set; }
        public decimal CRA { get; set; }
        public decimal TaxableIncome { get; set; }
        public decimal Tax { get; set; }
        public decimal NetIncome { get; set; }
    }
}
