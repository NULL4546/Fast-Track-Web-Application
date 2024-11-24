namespace Fast_Track_Web_Application.Models
{
    public enum DeliveryStage
    {
        OrderReceived,
        DispatchedFromWarehouse,
        ArrivedAtLocalCourier,
        DispatchedForFinalDelivery,
        Delivered
    }

    public class Shipment
    {
        public int Id { get; set; }
        public string TrackingNumber { get; set; }
        public DeliveryStage CurrentStage { get; set; }
        public string Notes { get; set; }
        public string StartDestination { get; set; }
        public string FinalDestination { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }

}
