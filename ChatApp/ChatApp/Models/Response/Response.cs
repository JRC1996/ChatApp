namespace ChatApp.Models.Response
{
    public class Response
    {
        public Response()
        {

            this.Success = 0;
        }

        public int Success { get; set; }

        public string Message { get; set; }

        public Object Data { get; set; }
    }
}
