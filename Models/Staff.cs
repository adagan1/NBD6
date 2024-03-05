namespace NBD6.Models
{
    public class Staff
    {
        public int StaffID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StaffPhone {  get; set; }
        public string StaffPosition { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }

        // Read-only property to get formatted phone number
        public string FormattedPhone
        {
            get
            {
                if (string.IsNullOrEmpty(StaffPhone))
                    return "";

                // Remove any non-numeric characters before formatting
                string digits = new String(StaffPhone.Where(char.IsDigit).ToArray());
                if (digits.Length == 10)
                    return $"{digits.Substring(0, 3)}-{digits.Substring(3, 3)}-{digits.Substring(6)}";
                else
                    return StaffPhone; // Return as is if not exactly 10 digits
            }
        }

        public string StaffSummary
        {
            get
            {
                return $"{StaffPosition}: {FirstName} {LastName} Phone:{FormattedPhone}";
            }
        }
    }
}
