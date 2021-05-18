// ReSharper disable InconsistentNaming

namespace TaskWebApi
{
    public class ErrorClass
    {
        public string Description { get; set; }
        public ErrorList ErrorCode { get; set; }
    }

    public enum ErrorList
    {
        OK = -1,
        ERROR_DUPLICATE = 0,
        ERROR_INVALID_INPUT = 1,
        ERROR_NON_EXISTENT = 2,
        INCORRECT_FORMAT_FILE = 3
    }
}