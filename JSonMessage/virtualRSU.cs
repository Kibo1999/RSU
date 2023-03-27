namespace RSU.JSonMessage
{
    public class virtualRSU
    {
        public int virtualStationID { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
        public RSUReferencePosition referencePosition { get; set; }
        public int range { get; set; }
        public DateTime timeStamp { get; set; }
    }
}