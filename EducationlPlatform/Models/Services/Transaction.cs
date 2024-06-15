namespace EducationlPlatform.Models.Services
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public DateTime TransactionDate { get; set; }
        public float TransactionAmount { get; set; }
        // Fees for the platform
        public float PlatformFees { get; set; }

        // Payer (Student)
        public string PayerUserId { get; set; }
        public User PayerUser { get; set; }

        // Payee (Tutor)
        public string PayeeUserId { get; set; }
        public User PayeeUser { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
