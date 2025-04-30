namespace MC.ApplicationServices.Messaging
{
    public class IntegerServiceRequestBase : ServiceRequestBase
    {
        public int Id { get; set; }

        public IntegerServiceRequestBase(int id)
        {
            Id = id;
        }
    }
}
