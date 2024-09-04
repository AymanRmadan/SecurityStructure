namespace ServicesLayer.Features.Accounts.Requests
{
    public record AddRegistrationRequest
    {

        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

}
