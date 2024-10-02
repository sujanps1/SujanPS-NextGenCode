namespace HandlerReq
{
    public class identity
    {
        public string type { get; set; }
        public string number { get; set; }
        public string state { get; set; }
        public string country { get; set; }
    }

    public class PatronRequestCaps
    {
        public string transactionSource { get; set; }

        public string patronId { get; set; }

        public string playerCardNumber { get; set; }

        public identity identity { get; set; }


        public Boolean isPrivacy { get; set; }

        public Boolean isMarketing { get; set; }

        public Boolean eCheckDetails { get; set; }

        public string dob { get; set; }

        public Boolean additionalPatronDetails { get; set; }

        public Boolean fetchFlexChargeTC { get; set; }
    }
}
