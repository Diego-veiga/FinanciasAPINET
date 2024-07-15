namespace financias.src.models
{
    public class ResponseHandlerError : ResponseHandler
    {
        public string Error { get; set; }

        public ResponseHandlerError()
        {
            Success = false;
        }
    }
}