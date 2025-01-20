namespace Models.Response
{
    public class DatePredictionModel
    {
        public int CustId { get; set; }
        public string CustomerName { get; set; } = null!;
        public DateTime LastOrderDate { get; set; }
        public DateTime NextPredictedOrder { get; set; }
    }
}
