namespace Bank
{
    public class Client
    {
        private readonly string firstName;
        private readonly string lastName;
        public readonly string passportId;
        public readonly string address;

        public Client(string firstName, string lastName, string passportId, string address)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.passportId = passportId;
            this.address = address;
        }

        public override string ToString()
        {
            return $"Client {firstName} {lastName}\n\tAddress: {address}\n\tPassport: {passportId}\n";
        }
    }

    class ClientBuilder
    {
        private string firstName;
        private string lastName;
        private string passportId;
        private string address;

        public void AddFullName(string fName, string lName)
        {
            this.firstName = fName;
            this.lastName = lName;
        }

        public void AddPassport(string id)
        {
            this.passportId = id;
        }

        public void AddAddress(string address)
        {
            this.address = address;
        }

        public Client BuildClient()
        {
            return new Client(this.firstName, this.lastName, this.passportId, this.address);
        }
    }
}
